using BCrypt.Net;
using Core.Application.Services.Authentication;

namespace CoreTest.Application.Services.Authentication;

public class BcryptDotNetTest
{
    [Fact]
    private void Should_Encrypt_Password_Correctly()
    {
        string password = "password";
        BcryptDotNet hashService = new ();

        string hashedPassword = hashService.Hash(password);

        Assert.True(BCrypt.Net.BCrypt.Verify(password, hashedPassword));
    }
}
