using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public interface IGenericUnitOfWork : IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : IEntity;
        void SetRepository<TEntity>(IRepository<TEntity> repository) where TEntity : IEntity;
    }
}