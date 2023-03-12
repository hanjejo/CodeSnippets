using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Ioc
{
    public interface IContainer
    {
        void Register<TFrom, TTo>() where TTo : TFrom;
        void RegisterInstance<T>(T instance);
        void RegisterSingleton<TFrom, TTo>() where TTo : TFrom, new();
        T Resolve<T>();
    }
}
