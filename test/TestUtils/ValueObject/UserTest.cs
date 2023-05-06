using Bogus;
using Core.Domain.Entities;

namespace TestUtils.ValueObject;

public static class UserTest
{
    public static User Build()
    {
        return new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.CreatedAt, f => DateTime.Now.Add(f.Date.Timespan()));
    }
}
