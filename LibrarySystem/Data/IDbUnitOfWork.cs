namespace LibrarySystem.Data;

public interface IDbUnitOfWork : IDisposable, IAsyncDisposable
{
    Task SaveChangesAsync(CancellationToken token = default);
}

public class DbUnitOfWork(LibraryContext context) : IDbUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await context.SaveChangesAsync(token);
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}