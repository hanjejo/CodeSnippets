using CodeSnippets.EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserService
{
    // 유저 삭제 이벤트
    public class UserDeleted : Event
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime Time { set; get; }
    }
}
