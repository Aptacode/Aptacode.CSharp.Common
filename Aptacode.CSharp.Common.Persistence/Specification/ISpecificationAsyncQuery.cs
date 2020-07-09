using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.Specification
{
    public interface ISpecificationAsyncQuery<in TKey, TEntity> : ISpecificationQuery<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        Task<IReadOnlyCollection<TEntity>> GetAsync(Specification<TEntity> specification);
    }
}