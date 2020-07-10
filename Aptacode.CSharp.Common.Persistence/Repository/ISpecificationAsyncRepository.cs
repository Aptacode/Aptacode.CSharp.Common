using System.Collections.Generic;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Patterns.Specification;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface ISpecificationAsyncRepository<in TKey, TEntity> : 
        IGenericAsyncRepository<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        Task<IReadOnlyCollection<TEntity>> GetAsync(Specification<TEntity> specification);
    }
}