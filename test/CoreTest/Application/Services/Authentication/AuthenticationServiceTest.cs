using Core.Application.Services.Authentication;
using Core.Domain.Entities;
using Core.RepositoryInterfaces;
using Moq;
using Shared;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Exceptions;
using Shared.ValueObject.Response;
using TestUtils.ValueObject;

namespace Core.Test.Application.Services.Authentication;

public class AuthenticationServiceTest
{
    [Fact]
    private async Task Success()
    {
        //Setup
        DateTime timeInTestBeginning = DateTime.Now;
        UserDTO userDTO = UserDTOTest.Build();
        User user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            Password = userDTO.Password
        };

        var repository = new Mock<IUserRepository>();

        repository.Setup(ur => ur.Create(It.IsAny<User>()).Result).Returns(new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            PhotoURL = user.PhotoURL,
            CreatedAt = DateTime.Now
        });

        var unitOfWork = new Mock<IUnitOfWork>();

        var passwordHashService = new Mock<IPasswordHashService>();

        passwordHashService.Setup(h => h.Hash(It.IsAny<string>())).Returns(userDTO.Password);

        IAuthenticationService authService = new AuthenticationService(repository.Object, unitOfWork.Object, passwordHashService.Object);

        //Execution
        UserResponse user_created = await authService.SignUp(userDTO);

        //Assertion
        Assert.Equal(user_created.Name, user.Name);
        Assert.Equal(user_created.Email, user.Email);
        Assert.NotEqual(user_created.Id, Guid.Empty);
        Assert.True(string.IsNullOrEmpty(user_created.PhotoUrl));
        Assert.True(DateTime.Compare(user_created.CreatedAt, timeInTestBeginning) > 0);
    }

    [Fact]
    public async Task ShouldNotBeAbleToCreateAUserThatIsAlreadyRegistered()
    {
        //Setup
        UserDTO userDTO = UserDTOTest.Build();

        User user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            Password = userDTO.Password
        };

        var repository = new Mock<IUserRepository>();
        repository.Setup(ur => ur.Create(It.IsAny<User>()).Result).Returns(new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            PhotoURL = user.PhotoURL,
            CreatedAt = DateTime.Now
        });
        repository.Setup(ur => ur.FindByEmail(userDTO.Email)).ReturnsAsync(user);

        var unitOfWork = new Mock<IUnitOfWork>();

        var passwordHashService = new Mock<IPasswordHashService>();
        passwordHashService.Setup(h => h.Hash(It.IsAny<string>())).Returns(userDTO.Password);

        IAuthenticationService authService = new AuthenticationService(repository.Object, unitOfWork.Object, passwordHashService.Object);

        //Execution
        Func<Task> action = async () => await authService.SignUp(userDTO);

        //Assertion
        var exception = await Assert.ThrowsAsync<UserAlreadyRegisteredException>(action);

        Assert.Equal(ErrorMessagesResource.USER_ALREADY_REGISTERED, exception.Message);
    }
}
