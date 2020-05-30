using Aptacode.CSharp.Utilities.Persistence.Repository;

namespace Aptacode.CSharp.Utilities.Persistence.UnitOfWork
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : IEntity;
        void SetRepository<TEntity>(IRepository<TEntity> repository) where TEntity : IEntity;
    }
}