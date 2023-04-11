using CodeSnippets.EventAggregator;
using CodeSnippets.EventAggregator.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Test.EventAggregator
{
    [TestClass]
    public class EventAggregatorTest
    {
        [TestMethod]
        public void Test()
        {

        }
    }

    public class UserService  // 유저 관리 서비스
    {
        public void Create(User user)
        {
            // 유저 생성 로직
        }

        public void Delete(User user)
        {
            // 유저 삭제 로직
        }
    }

    public class User
    {
        public string Name { get; set; }
    }


    public class LoggerCommandHandler : IEventHandler<UserCreated>, IEventHandler<UserDeleted>
    {
        Logger logger = new Logger();

        public void Handle(UserCreated payload)
        {
            logger.Add(new LogItem()
            {
                Time = DateTime.Now,
                Type = "Warning",
                Message = payload.Name
            });
        }

        public void Handle(UserDeleted payload)
        {
            logger.Add(new LogItem()
            {
                Time = DateTime.Now,
                Type = "Warning",
                Message = payload.Name
            });
        }
    }
}
