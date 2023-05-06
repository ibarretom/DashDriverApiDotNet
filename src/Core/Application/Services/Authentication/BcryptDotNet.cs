using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoreTest")]
namespace Core.Application.Services.Authentication;

internal class BcryptDotNet : IPasswordHashService
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
