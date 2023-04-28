using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserService
{
    // 유저 리포지토리 구현체
    public class UserRepository : IUserRepository
    {
        // 디비 테이블이라 가정
        List<User> Users = new List<User>();

        public void Add(User user)
        {
            Users.Add(user);
        }

        public void Delete(User user)
        {
            Users.Remove(user);
        }

        public User Get(string username)
        {
            return Users.FirstOrDefault(o => o.Name == username);
        }
    }
}
