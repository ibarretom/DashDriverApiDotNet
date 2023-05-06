using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Services.Mappers;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;
using TestUtils.ValueObject;
using Xunit;

namespace CoreTest.Domain.Services.Mapper;

[CollectionDefinition("AutoMapperFixture")]
public class AutoMapperTest : ICollectionFixture<AutoMapperFixture> { }


[Collection("AutoMapperFixture")]
public class TestFact{
    private readonly AutoMapperFixture _fixture;

    public TestFact(AutoMapperFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        });
        configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void TestUserDTOToUser()
    {
        UserDTO userDTO = UserDTOTest.Build();

        User user = _fixture.Mapper.Map<User>(userDTO);

        Assert.True(user.Id.Equals(Guid.Empty));
        Assert.True(user.Name.Equals(userDTO.Name));
        Assert.True(user.Email.Equals(userDTO.Email));
        Assert.True(user.Password.Equals(userDTO.Password));
        Assert.True(user.CreatedAt.Equals(DateTime.MinValue));
    }

    [Fact]
    public void TestUserToUserResponse()
    {
        User user = UserTest.Build();
        
        UserResponse userResponse = _fixture.Mapper.Map<UserResponse>(user);

        Assert.True(userResponse.Id.Equals(user.Id));
        Assert.True(userResponse.Name.Equals(user.Name));
        Assert.True(userResponse.Email.Equals(user.Email));
        Assert.True(userResponse.PhotoUrl == user.PhotoURL);
        Assert.True(userResponse.CreatedAt.Equals(user.CreatedAt));
    }
}
