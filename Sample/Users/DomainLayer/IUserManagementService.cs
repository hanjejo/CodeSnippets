using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Users.DomainLayer
{
    // 유저 관리 서비스 인터페이스
    public interface IUserManagementService
    {
        void Create(User user);
        void Delete(User user);

        User Get(string username);
    }
}
