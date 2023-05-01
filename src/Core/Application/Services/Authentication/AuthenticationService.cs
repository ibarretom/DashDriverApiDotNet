using Core.Domain.Entities;
using Core.RepositoryInterfaces;
using Infra.Repository;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;

namespace Core.Application.Services.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResponse> SignUp(UserDTO user)
    {
        User user_created = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };

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
