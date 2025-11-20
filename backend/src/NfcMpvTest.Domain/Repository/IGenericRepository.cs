using DomainEntity = NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.Domain.Repository
{
    public interface IGenericRepository<TEntity> : IRepository
        where TEntity : DomainEntity.Entity
    {
        public Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);
        public Task InsertAsync(TEntity aggregate, CancellationToken cancellationToken);
        public Task UpdateAsync(TEntity aggregate, CancellationToken cancellationToken);
        public Task DeleteAsync(TEntity aggregate, CancellationToken cancellationToken);
    }
}
