using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.EventAggregator
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;
        void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;
    }
}
