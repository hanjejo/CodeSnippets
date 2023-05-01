using CodeSnippets.EventAggregator;
using Sample.Infrastructure;
using Sample.UserService;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이벤트 어그리게이터 생성
            var ea = new EventAggregator();

            // GUI, TCP, Console << 전부 이벤트 기반
            // 아래에서는 이벤트 드리븐 형식으로 처리를 구현함
            var sample = new UserManagementCommandHandler(ea);

            // 화면을 통해 유저 생성요청
            sample.CreateUserEvent();

            // 화면을 통해 유저를 선택했다고 가정

            // 화면을 통해 유저 삭제요청
            sample.DeleteUserEvent();
        }
    }

    public class UserManagementCommandHandler
    {
        private readonly IEventAggregator _ea;
        private readonly IUserManagementService _userManagementService;
        public UserManagementCommandHandler(IEventAggregator ea)
        {
            _ea = ea;
            _userManagementService = new UserManagementService(_ea, new UserRepository(new SampleDbContext()));
        }

        public void CreateUserEvent()
        {
            // UI를 통해서 User를 전달받았다고 가정
            // 유저 생성 
            _userManagementService.Create(new User() {
                Name = "Test",
                Age = 20
            });
        }

        public void DeleteUserEvent()
        {
            // UI를 통해 User를 전달받았다고 가정
            var user = _userManagementService.Get("Test");

            // 유저 삭제
            _userManagementService.Delete(user);
        }
    }
}
