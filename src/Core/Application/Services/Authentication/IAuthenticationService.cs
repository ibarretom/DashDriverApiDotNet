using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;

namespace Core.Application.Services.Authentication;

internal interface IAuthenticationService
{
    Task<UserResponse> SignUp(UserDTO user);
}
