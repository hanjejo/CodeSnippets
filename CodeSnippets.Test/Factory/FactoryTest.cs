using CodeSnippets.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Factory.Test
{
    [TestClass]
    public class GenericFactoryTest
    {
        [TestMethod]
        public void Create_Should_Return_Correct_ProductA_Instance()
        {
            // Given
            var factory = new GenericFactory<ProductA>();
            factory.AddProduct(() => new ProductA());

            // When
            var result = factory.Create();

            // Then
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProductA));
        }

        [TestMethod]
        public void Create_Should_Return_Correct_ProductB_Instance()
        {
            // Given
            var factory = new GenericFactory<ProductB>();
            factory.AddProduct(() => new ProductB());

            // When
            var result = factory.Create();

            // Then
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProductB));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_With_Invalid_Product_Should_Throw_Exception()
        {
            // Given
            var factory = new GenericFactory<ProductA>();

            // When
            factory.Create();

            // Then - Throw ArgumentException
        }
    }

    public class ProductA
    {
        public string GetName()
        {
            return "ProductA";
        }
    }

    public class ProductB
    {
        public string GetName()
        {
            return "ProductB";
        }
    }
}
