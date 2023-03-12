using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Mapper
{
    public class PropertyMap<TSource, TDestination>
    {
        public PropertyMap(Expression<Func<TSource, object>> sourceProperty, Expression<Func<TDestination, object>> destinationProperty)
        {
            SourceProperty = sourceProperty;
            DestinationProperty = destinationProperty;
        }

        public Expression<Func<TSource, object>> SourceProperty { get; set; }
        public Expression<Func<TDestination, object>> DestinationProperty { get; set; }
    }
}
