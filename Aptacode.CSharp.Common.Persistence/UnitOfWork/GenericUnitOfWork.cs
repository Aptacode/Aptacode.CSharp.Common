using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public abstract class GenericUnitOfWork : IUnitOfWork
    {
        #region Repositories
        public abstract TRepo Get<TRepo>();

        #endregion

        #region Unit Of Work

        public abstract void Dispose();
        public abstract Task Commit();
        public abstract void RejectChanges();

        #endregion

    }
}