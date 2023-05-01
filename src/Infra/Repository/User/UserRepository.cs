using Core.RepositoryInterfaces;
using Infra.Database;

namespace Infra.Repository.User;

internal class UserRepository : IUserRepository
{
    private DashDriverContext _context;
    public UserRepository(DashDriverContext context)
    {
        _context = context;
    }
    public async Task<Core.Domain.Entities.User> Create(Core.Domain.Entities.User user)
    {

        var created_user = await _context.Users.AddAsync(user);
        
        return created_user.Entity;
    }
}
