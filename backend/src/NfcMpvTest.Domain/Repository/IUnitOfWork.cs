namespace NfcMpvTest.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public Task CommitAsync(CancellationToken cancellationToken);
        public Task RollbackAsync(CancellationToken cancellationToken);
    }
}
