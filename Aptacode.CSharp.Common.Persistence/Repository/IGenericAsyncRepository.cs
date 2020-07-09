using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface IGenericAsyncRepository<in TKey, TEntity> : IGenericRepository<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(TKey id);

        Task DeleteAsync(TKey id);
    }
}