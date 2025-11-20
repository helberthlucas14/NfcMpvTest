using DomainEntity = NfcMpvTest.Domain.Entity;

namespace NfcMpvTest.Domain.Services
{
    public interface IBaseServiceEntity<TEntity> : IBaseService where TEntity : DomainEntity.Entity
    {
        Task<TEntity> RegisterAsync(TEntity entity);
        Task Update(TEntity entity);
        IQueryable<TEntity> GetAllQuery { get; }
        IQueryable<TEntity> GetAllQueryAsNoTracking { get; }
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
