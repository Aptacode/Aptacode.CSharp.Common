using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface IRepository<in TKey, TEntity> where TEntity : IEntity<TKey>
    {
        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(TKey id);

        Task Delete(TKey id);

        IQueryable<TEntity> AsQueryable();
    }
}