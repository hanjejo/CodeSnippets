using Sample.Logger.DomainLayer;
using System.Collections.Generic;

namespace Sample.Logger.ApplicationLayer
{
    // 로깅 서비스 구현체
    public class LoggingService : ILoggingService
    {
        private readonly List<LogItem> Logs = new List<LogItem>();
        public void Add(LogItem item)
        {
            Logs.Add(item);
        }

        public IEnumerable<LogItem> Get()
        {
            return Logs;
        }
    }
}
