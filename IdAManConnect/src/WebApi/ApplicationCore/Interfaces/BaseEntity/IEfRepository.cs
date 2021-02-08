using ApplicationCore.Interfaces.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.BaseEntity
{
    public interface IEfRepository<TEntity, TId> where TEntity : class, IModel<TId>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecificationQuery<TEntity> spec);

        Task<TEntity> GetAsync(ISpecificationQuery<TEntity> spec);

        Task<TEntity> GetAsync(ISpecificationQuery<TEntity> spec, TId id);

        Task<TEntity> GetAsync(TId id);

        Task<int> CountAllAsync();

        Task<int> CountAsync(ISpecificationQuery<TEntity> spec);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
