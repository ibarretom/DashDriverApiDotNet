using Infra.Database;

namespace Infra.Repository;

internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DashDriverContext _context;
    private bool _disposed;

    public UnitOfWork(DashDriverContext context)
    {
        _context = context;
    }
    
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        Dispose(true);
    }


    public void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }


}
