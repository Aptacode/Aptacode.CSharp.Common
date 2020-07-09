using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IGenericRepository<TKey, TEntity> Get<TKey, TEntity>() where TEntity : IEntity<TKey>;

        TRepo Get<TRepo, TKey, TEntity>() where TEntity : IEntity<TKey>
            where TRepo : class, IGenericRepository<TKey, TEntity>;

        void Set<TKey, TEntity>(IGenericRepository<TKey, TEntity> repository) where TEntity : IEntity<TKey>;
    }
}