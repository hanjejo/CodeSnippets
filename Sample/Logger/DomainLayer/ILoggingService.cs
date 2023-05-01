using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.LoggingService
{
    // 로깅 서비스 인터페이스
    public interface ILoggingService
    {
        void Add(LogItem item);
        IEnumerable<LogItem> Get();
    }
}
