using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Repository.Test
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }


    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public static class TestData
    {
        public static List<Customer> Customers = new List<Customer>
    {
        new Customer { Id = 1, Name = "John", Age = 25 },
        new Customer { Id = 2, Name = "Jane", Age = 35 },
        new Customer { Id = 3, Name = "Bob", Age = 45 },
        new Customer { Id = 4, Name = "Alice", Age = 55 },
        new Customer { Id = 5, Name = "Dave", Age = 30 }
    };
    }

    public class TestDbContext : DbContext
    {
        public TestDbContext() : base("TestDbContext")
        {
            Database.SetInitializer(new TestDbInitializer());
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
    }

    public class TestDbInitializer : DropCreateDatabaseAlways<TestDbContext>
    {
        protected override void Seed(TestDbContext context)
        {
            context.Customers.AddRange(TestData.Customers);
            context.SaveChanges();
        }
    }

    [TestClass]
    public class RepositoryTest
    {
        private TestDbContext _context;
        private IRepository<Customer> _customerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new TestDbContext();

            _customerRepository = new Repository<Customer>(_context);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsEntityIfExists()
        {
            // Arrange
            var customerId = TestData.Customers[0].Id;

            // Act
            var result = await _customerRepository.GetByIdAsync(customerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestData.Customers[0].Name, result.Name);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNullIfEntityDoesNotExist()
        {
            // Arrange
            var customerId = -1;

            // Act
            var result = await _customerRepository.GetByIdAsync(customerId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            // Act
            var result = await _customerRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(TestData.Customers.Count, result.Count());
        }

        [TestMethod]
        public async Task GetAsync_WithFilter_ReturnsMatchingEntities()
        {
            // Arrange
            var searchQuery = "J";

            // Act
            var result = await _customerRepository.GetAsync(c => c.Name.Contains(searchQuery));

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(c => c.Name.Contains(searchQuery)));
        }

        [TestMethod]
        public async Task GetAsync_WithFilterAndOrderBy_ReturnsMatchingEntitiesInCorrectOrder()
        {
            // Arrange
            var searchQuery = "J";

            // Act
            var result = await _customerRepository.GetAsync(c => c.Name.Contains(searchQuery),
                                                             q => q.OrderBy(c => c.Name));

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(c => c.Name.Contains(searchQuery)));
            Assert.IsTrue(result.ElementAt(0).Name.CompareTo(result.ElementAt(1).Name) < 0);
        }

        [TestMethod]
        public async Task GetAsync_WithFilterAndOrderByAndIncludes_ReturnsMatchingEntitiesInCorrectOrder()
        {
            // Arrange
            var searchQuery = "J";

            // Act
            var result = await _customerRepository.GetAsync(c => c.Name.Contains(searchQuery),
                                                             q => q.OrderBy(c => c.Name),
                                                             c => c.Orders);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(c => c.Name.Contains(searchQuery)));
            Assert.IsTrue(result.ElementAt(0).Name.CompareTo(result.ElementAt(1).Name) < 0);
            Assert.IsNotNull(result.ElementAt(0).Orders);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesExistingEntity()
        {
            // Arrange
            var existingCustomer = await _customerRepository.GetByIdAsync(1);
            existingCustomer.Name = "Updated Customer Name";

            // Act
            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();
            var updatedCustomer = await _customerRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(existingCustomer.Name, updatedCustomer.Name);
        }

        [TestMethod]
        public async Task RemoveAsync_RemovesEntity()
        {
            // Arrange
            var existingCustomer = await _customerRepository.GetByIdAsync(1);

            // Act
            _customerRepository.Remove(existingCustomer);
            _customerRepository.Save();
            var deletedCustomer = await _customerRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNull(deletedCustomer);
        }

        [TestMethod]
        public async Task RemoveRangeAsync_RemovesMultipleEntities()
        {
            // Arrange
            var customersToDelete = await _customerRepository.GetAsync(c => c.Id > 1);

            // Act
            _customerRepository.RemoveRange(customersToDelete);
            _customerRepository.Save();
            var remainingCustomers = await _customerRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(TestData.Customers.Count() - customersToDelete.Count(), remainingCustomers.Count());
        }

        [TestMethod]
        public async Task SaveAsync_SavesChanges()
        {
            // Arrange
            var existingCustomer = await _customerRepository.GetByIdAsync(1);
            existingCustomer.Name = "Updated Customer Name";

            // Act
            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();
            var updatedCustomer = await _customerRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(existingCustomer.Name, updatedCustomer.Name);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
