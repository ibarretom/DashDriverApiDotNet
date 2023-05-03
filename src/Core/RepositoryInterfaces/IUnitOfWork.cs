namespace Core.RepositoryInterfaces;

public interface IUnitOfWork
{
    Task Commit();
}
