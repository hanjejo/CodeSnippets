using Sample.Shared.InfrastructureLayer;
using Sample.Users.DomainLayer;
using System.Linq;

namespace Sample.Users.InfrastructureLayer
{
    // 유저 리포지토리 구현체
    public class UserRepository : IUserRepository
    {
        private readonly SampleDbContext _dbContext;
        public UserRepository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User Get(string username)
        {
            return _dbContext.Users.FirstOrDefault(o => o.Name == username);
        }
    }
}
