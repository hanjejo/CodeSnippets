using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnipets.Ioc
{
    public interface IContainer
    {
        void Register<TFrom, TTo>() where TTo : TFrom;
        void RegisterInstance<T>(T instance);
        void RegisterSingleton<TFrom, TTo>() where TTo : TFrom, new();
        T Resolve<T>();
    }

    public class Container : IContainer
    {
        private readonly Dictionary<Type, Func<object>> _registeredTypes = new Dictionary<Type, Func<object>>();

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            _registeredTypes[typeof(TFrom)] = () => Activator.CreateInstance(typeof(TTo));
        }

        public void RegisterInstance<T>(T instance)
        {
            _registeredTypes[typeof(T)] = () => instance;
        }

        public void RegisterSingleton<TFrom, TTo>() where TTo : TFrom, new()
        {
            var instance = (TTo)Activator.CreateInstance(typeof(TTo));
            _registeredTypes[typeof(TFrom)] = () => instance;
        }

        public T Resolve<T>()
        {
            Func<object> creator;
            if (_registeredTypes.TryGetValue(typeof(T), out creator))
            {
                return (T)creator();
            }

            throw new InvalidOperationException($"Type {typeof(T)} is not registered in the container.");
        }
    }

}
