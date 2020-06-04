using System;
using System.Threading.Tasks;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void RejectChanges();
    }
}