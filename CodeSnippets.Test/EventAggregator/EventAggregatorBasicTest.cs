using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.EventAggregator.Test
{
    [TestClass]
    public class EventAggregatorTests
    {
        private EventAggregator _ea;
        private Logger _logger;

        [TestMethod]
        public void EventAggregatorTest()
        {
            // 1. 이벤트 어그리게이터 생성
            // 이벤트 어그리게이터 생성 -> 어플리케이션에 하나만 있어야함
            _ea = new EventAggregator();
            _logger = new Logger();

            // 2. 대상 메서드를 구독
            // 이벤트를 파라미터로 받는 메서드를 구독
            _ea.Subscribe<UserCreated>(AddLog);
            _ea.Subscribe<UserDeleted>(DeleteLog);

            // 3. 유저 생성 이벤트 발생
            // 아래의 이벤트를 구독하는 메서드를 실행하고 UserCreated(payload)를 구독 메서드에 전달함
            _ea.Publish<UserCreated>(new UserCreated()
            {
                // 유저 정보 페이로드로 탑재
            });
            
            // 5. 유저 삭제 이벤트 발생
            // 아래의 이벤트를 구독하는 메서드를 실행하고 UserDeleted(payload)를 구독 메서드에 전달함
            _ea.Publish<UserDeleted>(new UserDeleted()
            {
                // 유저 정보 페이로드로 탑재
            });

            // 이벤트에대해 구독을 등록할 시, 어디서나 타입만으로 해당 메서드를 호출할 수 있음
        }

        public void AddLog(UserCreated created)
        {
            // 4. 3의 실행에대해 구독 메서드 실행됨
            _logger.Add(new LogItem()
            {
                // 페이로드에 들어있는 로그정보
            });
        }

        public void DeleteLog(UserDeleted created)
        {
            // 6. 4의 실행에대해 구독 메서드 실행됨
            _logger.Add(new LogItem()
            {
                // 페이로드에 들어있는 로그정보
            });
        }
    }

    public class UserCreated : Event  // 유저 생성 이벤트 정의
    {
        public string Name { get; set; }
    }
    
    public class UserDeleted : Event  // 유저 삭제 이벤트 정의
    {
        public string Name { get; set; }
    }

    public class Logger // 로그 서비스
    {
        public void Add(LogItem log)
        {
            // 로그 추가 로직
        }
    }

    public class LogItem
    {
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
