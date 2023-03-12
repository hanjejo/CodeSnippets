using CodeSnipets.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodeSnippets.Ioc.Test
{
    [TestClass]
    public class ContainerTest
    {
        private Container _container;

        [TestInitialize]
        public void Initialize()
        {
            _container = new Container();
        }

        [TestMethod]
        public void Register_Should_Register_Type()
        {
            // Given
            _container.Register<IService, Service>();

            // When
            var service = _container.Resolve<IService>();

            // Then
            Assert.IsInstanceOfType(service, typeof(Service));
        }

        [TestMethod]
        public void Resolve_Should_Throw_Exception_If_Type_Not_Registered()
        {
            // Given

            // When
            Action action = () => _container.Resolve<IService>();

            // Then
            Assert.ThrowsException<InvalidOperationException>(action);
        }

        [TestMethod]
        public void Resolve_Should_Resolve_Type_With_Dependencies()
        {
            // Given
            _container.Register<ILogger, ConsoleLogger>();
            _container.Register<ICalculator, Calculator>();

            // When
            var calculator = _container.Resolve<ICalculator>();
            var result = calculator.Add(2, 3);

            // Then
            Assert.AreEqual(5, result);
        }
    }

    public interface IService
    {
    }

    public class Service : IService
    {
    }

    public interface ILogger
    {

    }

    public class ConsoleLogger : ILogger
    {

    }

    public interface ICalculator
    {
        int Add(int a, int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
