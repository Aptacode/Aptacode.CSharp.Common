using System.Collections.Generic;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.Specification
{
    public interface ISpecificationRepositoryAsync<in TKey, TEntity> : 
        ISpecificationRepository<TKey, TEntity>, 
        IGenericAsyncRepository<TKey, TEntity>
        where TEntity : IEntity<TKey>
    {
        Task<IReadOnlyCollection<TEntity>> GetAsync(Specification<TEntity> specification);
    }
}