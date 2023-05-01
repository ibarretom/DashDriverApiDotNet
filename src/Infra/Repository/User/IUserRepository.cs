using Shared.ValueObject.DTO;

namespace Infra.Repository.User;

public interface IUserRepository
{
    Task<Core.Domain.Entities.User> Create(UserDTO user);
}
