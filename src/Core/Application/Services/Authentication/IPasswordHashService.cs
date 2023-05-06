
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Core.Application.Services.Authentication;

internal interface IPasswordHashService
{
    string Hash(string password);
}
