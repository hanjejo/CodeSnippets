using CodeSnippets.Test.Repository;
using CodeSnippets.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Test.UnitOfWork
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = new CodeSnippets.UnitOfWork.UnitOfWork(new TestDbContext());
        }

        [TestMethod]
        public async Task SaveAsync_SavesChangesToDatabase()
        {
            // Arrange
            var customer = new Customer { FirstName = "John", LastName = "Doe" };
            var repo = _unitOfWork.GetRepository<Customer>();
            repo.Add(customer);
            await _unitOfWork.SaveChangesAsync();

            // Act
            var savedCustomer = await repo.GetByIdAsync(customer.Id);

            // Assert
            Assert.IsNotNull(savedCustomer);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
        }

        [TestMethod]
        public void BeginTransaction_StartsNewTransaction()
        {
            // Act
            _unitOfWork.BeginTransaction();

            // Assert
            Assert.IsNotNull(_unitOfWork.Context.Database.CurrentTransaction);
        }

        [TestMethod]
        public async Task Commit_SavesChangesToDatabase()
        {
            // Arrange
            var customer = new Customer { FirstName = "John", LastName = "Doe" };
            var repo = _unitOfWork.GetRepository<Customer>();
            var beforeCount = repo.Count();
            _unitOfWork.BeginTransaction();

            // Act
            repo.Add(customer);
            _unitOfWork.CommitTransaction();
            await _unitOfWork.SaveChangesAsync();
            var afterCount_context = repo.Count();

            // Assert
            Assert.AreEqual(beforeCount + 1, afterCount_context);
            Assert.IsNull(_unitOfWork.Context.Database.CurrentTransaction);
        }

        [TestMethod]
        public async Task Rollback_DiscardsChangesToDatabase()
        {
            // Arrange
            var customer = new Customer { FirstName = "John", LastName = "Doe" };
            var repo = _unitOfWork.GetRepository<Customer>();
            var beforeCount = repo.Count();
            _unitOfWork.BeginTransaction();

            // Act
            repo.Add(customer);
            _unitOfWork.RollbackTransaction();
            // await _unitOfWork.SaveChangesAsync();
            var afterCount= repo.Count();

            // Assert
            Assert.AreEqual(beforeCount, afterCount);
            Assert.IsNull(_unitOfWork.Context.Database.CurrentTransaction);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _unitOfWork.Context.Dispose();
        }
    }
}
