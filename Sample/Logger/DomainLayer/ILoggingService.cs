using System.Collections.Generic;

namespace Sample.Logger.DomainLayer
{
    // 로깅 서비스 인터페이스
    public interface ILoggingService
    {
        void Add(LogItem item);
        IEnumerable<LogItem> Get();
    }
}
