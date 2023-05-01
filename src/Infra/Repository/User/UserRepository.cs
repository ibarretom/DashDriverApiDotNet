using Infra.Database;
using Shared.ValueObject.DTO;

namespace Infra.Repository.User;

internal class UserRepository : IUserRepository
{
    private DashDriverContext _context;
    public UserRepository(DashDriverContext context)
    {
        _context = context;
    }
    public async Task<Core.Domain.Entities.User> Create(UserDTO user)
    {

        var created_user = await _context.Users.AddAsync(new Core.Domain.Entities.User
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        });
        
        return created_user.Entity;
    }
}
