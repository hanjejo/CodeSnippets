using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Mapper
{
    public class PropertyMapper<TSource, TDestination>
    {
        private readonly Dictionary<string, string> _propertyMap = new Dictionary<string, string>();

        public PropertyMapper<TSource, TDestination> CreateMap(string sourcePropertyName, string targetPropertyName)
        {
            _propertyMap.Add(sourcePropertyName, targetPropertyName);
            return this;
        }

        public TDestination Map(TSource source)
        {
            var target = Activator.CreateInstance<TDestination>();

            var sourceProperties = typeof(TSource).GetProperties();
            var targetProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (_propertyMap.TryGetValue(sourceProperty.Name, out var targetPropertyName))
                {
                    var targetProperty = targetProperties.FirstOrDefault(p => p.Name == targetPropertyName);
                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                }
                else
                {
                    var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                }
            }

            return target;
        }
    }
}
