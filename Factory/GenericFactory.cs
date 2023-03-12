using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Factory
{
    // 팩토리 클래스
    public class GenericFactory<T> where T : new()
    {
        private readonly IDictionary<Type, Func<T>> _productCreators;

        public GenericFactory()
        {
            _productCreators = new Dictionary<Type, Func<T>>();
        }

        public void AddProduct(Func<T> productCreator)
        {
            _productCreators[typeof(T)] = productCreator;
        }

        public T Create()
        {
            if (!_productCreators.TryGetValue(typeof(T), out var creator))
            {
                throw new ArgumentException("Invalid product type.");
            }

            return creator();
        }
    }
}
