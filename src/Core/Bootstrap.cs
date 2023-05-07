using Core.Application.Services.Authentication;
using Core.Application.UseCases.Authentication;
using Core.Domain.Services.Mappers;
using Microsoft.Extensions.DependencyInjection;
namespace Core;

public static class Bootstrap
{
    public static void AddCoreApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordHashService, BcryptDotNet>();
    }
}
