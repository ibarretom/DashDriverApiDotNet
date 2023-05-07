using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;

namespace Core.Application.UseCases.Authentication;

public interface ISignUpUseCase
{
    Task<UserResponse> Execute(UserDTO user);
}
