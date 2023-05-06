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
    public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHashService passwordHash)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHashService = passwordHash;
    }

    public async Task<UserResponse> SignUp(UserDTO user)
    {
        User user_created = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = _passwordHashService.Hash(user.Password)
        };

        if (await _userRepository.FindByEmail(user.Email) != null)
        {
            throw new UserAlreadyRegisteredException(ErrorMessagesResource.USER_ALREADY_REGISTERED);
        }

        user_created = await _userRepository.Create(user_created);
        await _unitOfWork.Commit();

        return new UserResponse
        {
            Id = user_created.Id,
            Name = user_created.Name,
            Email = user_created.Email,
            PhotoUrl = user_created.PhotoURL,
            CreatedAt = user_created.CreatedAt
        };
    }
}
