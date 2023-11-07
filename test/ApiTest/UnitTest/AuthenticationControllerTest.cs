using Api.Controllers;
using Core.Application.UseCases.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;
using System.Net;
using TestUtils.ValueObject;

namespace ApiTest.UnitTest;

public class AuthenticationControllerTest
{
    [Fact]
    public async Task ShouldSignUpAUser()
    {
        UserDTO user = UserDTOTest.Build();

        UserResponse userResponse = new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        };

        var useCase = new Mock<ISignUpUseCase>();
        useCase.Setup(useCase => useCase.Execute(It.IsAny<UserDTO>()).Result).Returns(userResponse);

        var controller = new AuthenticationController();

        var result = Assert.IsType<CreatedResult>(await controller.SignUp(useCase.Object, user));
        var objReturn = Assert.IsType<UserResponse>(result.Value);

        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(userResponse.Id, objReturn.Id);
        Assert.True(objReturn.Name.Equals(userResponse.Name));
        Assert.True(objReturn.Email.Equals(userResponse.Email));
        Assert.True(objReturn.CreatedAt.Equals(userResponse.CreatedAt));
        Assert.True(string.IsNullOrEmpty(objReturn.PhotoUrl));
    }
}
