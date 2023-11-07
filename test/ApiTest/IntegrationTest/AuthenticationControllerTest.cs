using Api;
using Shared;
using Shared.ValueObject.DTO;
using System.Net;
using System.Text.Json;
using TestUtils.ValueObject;

namespace ApiTest.IntegrationTest;

public class AuthenticationControllerTest : ControllerBase
{
    public AuthenticationControllerTest(WebFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldSignUpAUser()
    {
        DateTime timeInBeginningOfTest = DateTime.Now;
        UserDTO userRequest = UserDTOTest.Build();

        var response = await PostRequest("Authentication/signup", userRequest);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        await using var body = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(body);

        Assert.True(Guid.TryParse(responseData.RootElement.GetProperty("id").GetString(), out _));
        Assert.Equal(userRequest.Name, responseData.RootElement.GetProperty("name").GetString());
        Assert.Equal(userRequest.Email, responseData.RootElement.GetProperty("email").GetString());
        Assert.True(string.IsNullOrEmpty(responseData.RootElement.GetProperty("photoUrl").GetString()));
        Assert.True(DateTime.Compare(responseData.RootElement.GetProperty("createdAt").GetDateTime(), timeInBeginningOfTest) > 0);
    }

    [Fact]
    public async Task ShouldNotSignUpAUserWithNameBlank()
    {
        UserDTO userRequest = UserDTOTest.Build();
        userRequest.Name = string.Empty;

        var response = await PostRequest("Authentication/signup", userRequest);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var body = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(body);

        Assert.Equal(ErrorMessagesResource.NAME_BLANK, responseData.RootElement.GetProperty("messages").EnumerateArray().First().GetString());
    }

    [Fact]
    public async Task ShouldNotSignUpAUserWithEmailBlank()
    {
        UserDTO userRequest = UserDTOTest.Build();
        userRequest.Email = string.Empty;

        var response = await PostRequest("Authentication/signup", userRequest);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var body = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(body);

        Assert.Equal(ErrorMessagesResource.EMAIL_BLANK, responseData.RootElement.GetProperty("messages").EnumerateArray().First().GetString());
    }

    [Fact]
    public async Task ShouldNotSignUpAUserWithInvalidEmail()
    {
        UserDTO userRequest = UserDTOTest.Build();
        userRequest.Email = "invalidEmail";

        var response = await PostRequest("Authentication/signup", userRequest);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var body = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(body);

        Assert.Equal(ErrorMessagesResource.EMAIL_INVALID, responseData.RootElement.GetProperty("messages").EnumerateArray().First().GetString());
    }

    [Fact]
    public async Task ShouldNotSignUpAUserWithPasswordBlank()
    {
        UserDTO userRequest = UserDTOTest.Build();
        userRequest.Password = string.Empty;
        var response = await PostRequest("Authentication/signup", userRequest);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var body = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(body);

        Assert.Equal(ErrorMessagesResource.PASSWORD_BLANK, responseData.RootElement.GetProperty("messages").EnumerateArray().First().GetString());
    }

    [Fact]
    public async Task ShouldNotSignUpAUserThatIsAlreadyRegistered()
    {
        UserDTO userRequest = UserDTOTest.Build();

        var response = await PostRequest("Authentication/signup", userRequest);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        response = await PostRequest("Authentication/signup", userRequest);
        
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        
        await using var body = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(body);
        
        Assert.Equal(ErrorMessagesResource.USER_ALREADY_REGISTERED, responseData.RootElement.GetProperty("messages").EnumerateArray().First().GetString());
    }

}
