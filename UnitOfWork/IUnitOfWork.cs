using CodeSnippets.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync();
    }
}
