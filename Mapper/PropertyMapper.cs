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
    where TDestination : new()
    {
        private readonly Dictionary<string, string> _propertyMap;
        private readonly Func<MemberExpression, string> _getPropertyName;

        public PropertyMapper()
        {
            _propertyMap = new Dictionary<string, string>();
            _getPropertyName = GetPropertyName;
        }

        public void AddPropertyMap<TProperty>(
            Expression<Func<TSource, TProperty>> sourceProperty,
            Expression<Func<TDestination, TProperty>> destinationProperty)
        {
            var sourcePropertyName = _getPropertyName(sourceProperty.Body as MemberExpression);
            var destinationPropertyName = _getPropertyName(destinationProperty.Body as MemberExpression);
            _propertyMap[sourcePropertyName] = destinationPropertyName;
        }

        private string GetPropertyName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentException("The property expression must be a member access expression.");
            }

            return memberExpression.Member.Name;
        }

        public TDestination Map(TSource source)
        {
            TDestination destination = new TDestination();
            Type destinationType = typeof(TDestination);

            foreach (var sourceProperty in typeof(TSource).GetProperties())
            {
                string destinationPropertyName;
                if (!_propertyMap.TryGetValue(sourceProperty.Name, out destinationPropertyName))
                {
                    destinationPropertyName = sourceProperty.Name;
                }

                var destinationProperty = destinationType.GetProperty(destinationPropertyName);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
            }

            return destination;
        }
    }
}
