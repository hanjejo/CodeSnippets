using CodeSnippets.EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserService
{
    // 유저 관리 서비스 구현체
    public class UserManagementService : IUserManagementService
    {
        private readonly IEventAggregator _ea;
        private readonly IUserRepository _userRepository;

        public UserManagementService(IEventAggregator ea,IUserRepository userRepository) {
            _ea = ea;
            _userRepository = userRepository;
        }
        
        public void Create(User user)
        {
            // 유저 추가
            _userRepository.Add(user);

            // 유저 생성 알람 발생
            _ea.Publish<UserCreated>(new UserCreated() {
                UserName = user.Name,
                Age = user.Age,
                Time = DateTime.Now
            });
        }

        public void Delete(User user)
        {
            // 유저 삭제
            _userRepository.Delete(user);

            // 유저 삭제 알람 발생
            _ea.Publish<UserDeleted>(new UserDeleted()
            {
                UserName = user.Name,
                Time = DateTime.Now
            });
        }

        public User Get(string username)
        {
            return _userRepository.Get(username);
        }
    }
}
