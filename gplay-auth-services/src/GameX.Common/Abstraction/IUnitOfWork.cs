namespace gamex.Common;

public interface IUnitOfWork
{
    Task<Result<string>> SaveChangesAsync(CancellationToken cancellationToken = default);
}