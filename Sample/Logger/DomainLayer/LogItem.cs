using System;

namespace Sample.Logger.DomainLayer
{
    // 로그 엔티티
    public class LogItem
    {
        public DateTime Time { set; get; }
        public string Message { set; get; }
        public string Type { set; get; }
    }
}
