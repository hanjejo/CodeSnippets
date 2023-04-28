using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.LoggingService
{
    // 로그 엔티티
    public class LogItem
    {
        public DateTime Time { set; get; }
        public string Message { set; get; }
        public string Type { set; get; }
    }
}
