using CodeSnippets.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private DbContextTransaction _transaction;

        public DbContext Context => _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out object repository))
            {
                return (IRepository<TEntity>)repository;
            }
            IRepository<TEntity> newRepository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), newRepository);
            return newRepository;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
