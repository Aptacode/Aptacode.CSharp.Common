using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface IGenericRepository<in TKey, TEntity> where TEntity : IEntity<TKey>
    {
        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task<IReadOnlyCollection<TEntity>> GetAll();

        Task<TEntity> Get(TKey id);

        Task Delete(TKey id);
    }
}