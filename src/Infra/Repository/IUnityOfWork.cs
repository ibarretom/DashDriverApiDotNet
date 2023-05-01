namespace Infra.Repository;

public interface IUnityOfWork
{
    Task Commit();
}
