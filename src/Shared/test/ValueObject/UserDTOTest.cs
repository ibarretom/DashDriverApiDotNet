using Bogus;
using Shared.ValueObject.DTO;

namespace Shared.test.ValueObject;

public static class UserDTOTest
{
    public static UserDTO Build()
    {
        return new Faker<UserDTO>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password());
    }

}
