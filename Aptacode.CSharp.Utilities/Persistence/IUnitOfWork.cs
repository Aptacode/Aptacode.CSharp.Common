using System;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Utilities.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void RejectChanges();
    }
}
