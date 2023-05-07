using Core.Application.Services.Authentication;
using Core.Application.UseCases.Authentication;
using Moq;
using Shared;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Exceptions;
using Shared.ValueObject.Response;
using TestUtils.ValueObject;

namespace CoreTest.Application.UseCases;

public class SignUpUseCaseTest
{
    [Fact]
    public async Task ShouldBeAbleToSignUpAUser()
    {
        DateTime timeInThestBegin = DateTime.Now;

        UserDTO userDTO = UserDTOTest.Build();

        var service = new Mock<IAuthenticationService>();

        service.Setup(s => s.SignUp(It.IsAny<UserDTO>())).ReturnsAsync(new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = userDTO.Name,
            Email = userDTO.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        });

        SignUpUseCase useCase = new SignUpUseCase(service.Object);

        UserResponse user = await useCase.Execute(userDTO);

        Assert.NotNull(user);
        Assert.NotEqual(user.Id, Guid.Empty);
        Assert.True(user.Name.Equals(user.Name));
        Assert.True(user.Email.Equals(userDTO.Email));
        Assert.True(string.IsNullOrEmpty(user.PhotoUrl));
        Assert.True(DateTime.Compare(user.CreatedAt, timeInThestBegin) > 0);
    }

    [Fact]
    public async Task ShouldNotBeAbleToSignUpAUserWithBlankName()
    {
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Name = string.Empty;

        var service = new Mock<IAuthenticationService>();
        service.Setup(s => s.SignUp(It.IsAny<UserDTO>())).ReturnsAsync(new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = userDTO.Name,
            Email = userDTO.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        });

        SignUpUseCase useCase = new SignUpUseCase(service.Object);

        var exception = await Assert.ThrowsAsync<ValidationErrorException>(() => useCase.Execute(userDTO));

        Assert.True(exception.ErrorMessages.Count() == 1);
        Assert.Equal(ErrorMessagesResource.NAME_BLANK, exception.ErrorMessages.First());
    }

    [Fact]
    public async Task ShouldNotBeAbleToSignUpAUserWithBlankEmail()
    {
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Email = string.Empty;

        var service = new Mock<IAuthenticationService>();
        service.Setup(s => s.SignUp(It.IsAny<UserDTO>())).ReturnsAsync(new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = userDTO.Name,
            Email = userDTO.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        });
        
        SignUpUseCase useCase = new SignUpUseCase(service.Object);
        
        var exception = await Assert.ThrowsAsync<ValidationErrorException>(() => useCase.Execute(userDTO));
        
        Assert.True(exception.ErrorMessages.Count() == 1);
        Assert.Equal(ErrorMessagesResource.EMAIL_BLANK, exception.ErrorMessages.First());
    }

    [Fact]
    public async Task ShouldNotBeAbleToSignUpAUserWithInvalidEmail()
    {
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Email = "invalid_email";
        
        var service = new Mock<IAuthenticationService>();
        service.Setup(s => s.SignUp(It.IsAny<UserDTO>())).ReturnsAsync(new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = userDTO.Name,
            Email = userDTO.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        });
        
        SignUpUseCase useCase = new SignUpUseCase(service.Object);
        
        var exception = await Assert.ThrowsAsync<ValidationErrorException>(() => useCase.Execute(userDTO));
        
        Assert.True(exception.ErrorMessages.Count() == 1);
        Assert.Equal(ErrorMessagesResource.EMAIL_INVALID, exception.ErrorMessages.First());
    }

    [Fact]
    public async Task ShouldNotBeAbleToSignUpAUserWithBlankPassword()
    {
        UserDTO userDTO = UserDTOTest.Build();
        userDTO.Password = string.Empty;
        
        var service = new Mock<IAuthenticationService>();
        service.Setup(s => s.SignUp(It.IsAny<UserDTO>())).ReturnsAsync(new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = userDTO.Name,
            Email = userDTO.Email,
            PhotoUrl = null,
            CreatedAt = DateTime.Now
        });
        
        SignUpUseCase useCase = new SignUpUseCase(service.Object);
        
        var exception = await Assert.ThrowsAsync<ValidationErrorException>(() => useCase.Execute(userDTO));
        
        Assert.True(exception.ErrorMessages.Count() == 1);
        Assert.Equal(ErrorMessagesResource.PASSWORD_BLANK, exception.ErrorMessages.First());
    }
}
