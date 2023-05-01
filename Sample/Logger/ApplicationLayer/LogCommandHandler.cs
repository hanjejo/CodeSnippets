using CodeSnippets.EventAggregator;
using Sample.Logger.DomainLayer;
using Sample.Users.DomainLayer;

namespace Sample.LogService.ApplicationLayer
{
    // Logging 서비스 커맨드 핸들러
    // LogService에 접근하는 시나리오를 처리합니다.
    public class LogCommandHandler : IEventHandler<UserCreated>, IEventHandler<UserDeleted>
    {
        private readonly IEventAggregator _ea;
        private readonly ILoggingService _logService; 
        public LogCommandHandler(IEventAggregator ea, ILoggingService logService)
        {
            _ea = ea;
            _logService = logService;

            _ea.Subscribe<UserCreated>(Handle);
            _ea.Subscribe<UserDeleted>(Handle);
        }

        public void Handle(UserDeleted payload)
        {
            _logService.Add(new LogItem()
            {
                Time = payload.Time,
                Message = $"{payload.UserName}이 추가되었습니다."
            });
        }

        public void Handle(UserCreated payload)
        {
            _logService.Add(new LogItem()
            {
                Time = payload.Time,
                Message = $"{payload.UserName}이 삭제되었습니다."
            });
        }
    }
}
