using AutoMapper;
using Core.Domain.Entities;
using Core.RepositoryInterfaces;
using Shared;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Exceptions;
using Shared.ValueObject.Response;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoreTest")]
namespace Core.Application.Services.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;
    public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHashService passwordHash, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHashService = passwordHash;
        _mapper = mapper;
    }

    public async Task<UserResponse> SignUp(UserDTO user)
    {
        User user_created = _mapper.Map<User>(user);

        if (await _userRepository.FindByEmail(user_created.Email) != null)
        {
            throw new UserAlreadyRegisteredException(ErrorMessagesResource.USER_ALREADY_REGISTERED);
        }

        user_created = await _userRepository.Create(user_created);
        await _unitOfWork.Commit();

        return _mapper.Map<UserResponse>(user_created);
    }
}
