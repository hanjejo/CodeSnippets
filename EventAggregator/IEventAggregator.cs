using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.EventAggregator
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : Event;
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : Event;
    }
}
