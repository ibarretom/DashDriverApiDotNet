using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestUtils")]
namespace Core.Application.Services.Authentication;

internal interface IAuthenticationService
{
    Task<UserResponse> SignUp(UserDTO user);
}
