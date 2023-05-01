namespace Infra.Repository;

public interface IUnitOfWork
{
    Task Commit();
}
