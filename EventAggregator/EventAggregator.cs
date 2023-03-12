using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<object>> _handlers = new Dictionary<Type, List<object>>();

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    ((IEventHandler<TEvent>)handler).Handle(@event);
                }
            }
        }

        public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _handlers[typeof(TEvent)] = new List<object> { handler };
            }
        }

        public void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                handlers.Remove(handler);
            }
        }
    }
}
