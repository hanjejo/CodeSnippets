using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Mapper
{
    // 맵퍼 클래스
    public class Mapper<TSource, TDestination>
    {
        private readonly List<PropertyMap<TSource, TDestination>> _propertyMaps = new List<PropertyMap<TSource, TDestination>>();

        public void AddMap(PropertyMap<TSource, TDestination> propertyMap)
        {
            _propertyMaps.Add(propertyMap);
        }

        public TDestination Map(TSource source)
        {
            var destination = Activator.CreateInstance<TDestination>();

            foreach (var propertyMap in _propertyMaps)
            {
                var sourceValue = propertyMap.SourceProperty.Compile()(source);
                var destinationProperty = (System.Reflection.PropertyInfo)((MemberExpression)propertyMap.DestinationProperty.Body).Member;

                if (destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceValue);
                }
            }

            return destination;
        }
    }
}
