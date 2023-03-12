using CodeSnippets.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.Mapper.Test
{

    // 테스트 클래스
    [TestClass]
    public class PropertyMapperTest
    {
        [TestMethod]
        public void TestPropertyMap()
        {
            // Arrange
            var map = new PropertyMap<Person, PersonDto>(p => p.Name, dto => dto.FullName);

            // Act
            var sourceProperty = map.SourceProperty;
            var destinationProperty = map.DestinationProperty;

            // Assert
            Assert.AreEqual("Name", ((MemberExpression)sourceProperty.Body).Member.Name);
            Assert.AreEqual("FullName", ((MemberExpression)destinationProperty.Body).Member.Name);
        }

        [TestMethod]
        public void TestMapper()
        {
            // Arrange
            var mapper = new Mapper<Person, PersonDto>();
            mapper.AddMap(new PropertyMap<Person, PersonDto>(p => p.Name, dto => dto.FullName));

            var person = new Person
            {
                Name = "John Smith",
                Age = 30,
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            // Act
            var personDto = mapper.Map(person);

            // Assert
            Assert.AreEqual(person.Name, personDto.FullName);
        }

        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
        }

        private class PersonDto
        {
            public string FullName { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
        }
    }
}
