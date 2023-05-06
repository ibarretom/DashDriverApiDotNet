using Core.Application.Services.Authentication;
using Core.Domain.Services.Validation;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;

namespace Core.Application.UseCases;

internal class SignUpUseCase
{
    private readonly IAuthenticationService _authService;
    public SignUpUseCase(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<UserResponse> Execute(UserDTO user)
    {
        new UserDTOValidation().ValidateInput(user);

        UserResponse userResponse = await _authService.SignUp(user);

        return userResponse;
    }
}
