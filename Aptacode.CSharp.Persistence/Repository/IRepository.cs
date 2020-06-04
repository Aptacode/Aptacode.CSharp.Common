using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<int> Create(TEntity entity);

        Task Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Get(int id);

        Task Delete(int id);

        IQueryable<TEntity> AsQueryable();
    }
}