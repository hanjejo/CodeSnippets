﻿using CodeSnippets.EventAggregator;
using System;

namespace Sample.Users.DomainLayer
{
    // 유저 생성 이벤트
    public class UserCreated : Event
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime Time { set; get; }
    }
}
