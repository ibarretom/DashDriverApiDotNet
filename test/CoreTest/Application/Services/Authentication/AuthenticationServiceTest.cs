using AutoMapper;
using Core.Application.Services.Authentication;
using Core.Domain.Entities;
using Core.Domain.Services.Mappers;
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
        
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        }).CreateMapper();

        UserDTO userDTO = UserDTOTest.Build();
        User user = mapper.Map<User>(userDTO);

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

        IAuthenticationService authService = new AuthenticationService(repository.Object, unitOfWork.Object, passwordHashService.Object, mapper);

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
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        }).CreateMapper();

        UserDTO userDTO = UserDTOTest.Build();

        User user = mapper.Map<User>(userDTO);

        var repository = new Mock<IUserRepository>();
        repository.Setup(ur => ur.FindByEmail(userDTO.Email)).ReturnsAsync(user);

        var unitOfWork = new Mock<IUnitOfWork>();

        var passwordHashService = new Mock<IPasswordHashService>();

        IAuthenticationService authService = new AuthenticationService(repository.Object, unitOfWork.Object, passwordHashService.Object, mapper);

        //Execution
        Func<Task> action = async () => await authService.SignUp(userDTO);

        //Assertion
        var exception = await Assert.ThrowsAsync<UserAlreadyRegisteredException>(action);

        Assert.Equal(ErrorMessagesResource.USER_ALREADY_REGISTERED, exception.Message);
    }
}
