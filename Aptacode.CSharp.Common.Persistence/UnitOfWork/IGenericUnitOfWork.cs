using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : IEntity<TKey>;
        void SetRepository<TKey, TEntity>(IRepository<TKey, TEntity> repository) where TEntity : IEntity<TKey>;
    }
}