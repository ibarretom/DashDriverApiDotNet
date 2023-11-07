using Infra.Repository;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.ValueObject.DTO;
using Core.RepositoryInterfaces;
using Core.Application.UseCases.Authentication;
using Shared.ValueObject.Response;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController: ControllerBase
{

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> SignUp([FromServices] ISignUpUseCase useCase, [FromBody] UserDTO user)
    {
        UserResponse created_user = await useCase.Execute(user);
        return Created(string.Empty, created_user);
    }
}
