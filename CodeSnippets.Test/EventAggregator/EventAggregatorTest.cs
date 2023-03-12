using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.EventAggregator.Test
{
    [TestClass]
    public class EventAggregatorTests
    {
        private EventAggregator _aggregator;
        private FakeHandlerA _handlerA;
        private FakeHandlerB _handlerB;

        [TestInitialize]
        public void Initialize()
        {
            _aggregator = new EventAggregator();
            _handlerA = new FakeHandlerA();
            _handlerB = new FakeHandlerB();
        }

        [TestMethod]
        public void Publish_ShouldCallEventHandler()
        {
            // Given
            var payload = new FakeEvent();

            _aggregator.Subscribe(_handlerA);
            _aggregator.Subscribe(_handlerB);

            // When
            _aggregator.Publish(payload);

            // Then
            Assert.AreEqual(1, _handlerA.HandledEvents.Count);
            Assert.AreEqual(payload, _handlerA.HandledEvents[0]);

            Assert.AreEqual(1, _handlerB.HandledEvents.Count);
            Assert.AreEqual(payload, _handlerB.HandledEvents[0]);
        }

        [TestMethod]
        public void Subscribe_ShouldAddEventHandler()
        {
            // Given
            _aggregator.Subscribe(_handlerA);

            // When
            _aggregator.Publish(new FakeEvent());

            // Then
            Assert.AreEqual(1, _handlerA.HandledEvents.Count);
        }

        [TestMethod]
        public void Unsubscribe_ShouldRemoveEventHandler()
        {
            // Given
            _aggregator.Subscribe(_handlerA);
            _aggregator.Subscribe(_handlerB);

            // When
            _aggregator.Unsubscribe(_handlerA);
            _aggregator.Publish(new FakeEvent());

            // Then
            Assert.AreEqual(0, _handlerA.HandledEvents.Count);

            Assert.AreEqual(1, _handlerB.HandledEvents.Count);
        }
    }

    public class FakeEvent : IEvent { }

    public class FakeHandlerA : IEventHandler<FakeEvent>
    {
        public List<FakeEvent> HandledEvents { get; } = new List<FakeEvent>();

        public void Handle(FakeEvent payload)
        {
            HandledEvents.Add(payload);
        }
    }

    public class FakeHandlerB : IEventHandler<FakeEvent>
    {
        public List<FakeEvent> HandledEvents { get; } = new List<FakeEvent>();

        public void Handle(FakeEvent payload)
        {
            HandledEvents.Add(payload);
        }
    }
}
