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
    [TestClass]
    public class PropertyMapperTests
    {
        [TestMethod]
        public void Map_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var person = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
            var mapper = new PropertyMapper<Person, PersonDto>();
            mapper.AddPropertyMap(p => p.FirstName, d => d.First);
            mapper.AddPropertyMap(p => p.LastName, d => d.Last);

            // Act
            var personDto = mapper.Map(person);

            // Assert
            Assert.AreEqual("John", personDto.First);
            Assert.AreEqual("Doe", personDto.Last);
            Assert.AreEqual(30, personDto.Age);
        }

        [TestMethod]
        public void Map_ShouldMapPropertiesUsingDefaultMappings()
        {
            // Arrange
            var person = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
            var mapper = new PropertyMapper<Person, PersonDto>();

            // Act
            var personDto = mapper.Map(person);

            // Assert
            Assert.AreNotEqual("John", personDto.First);
            Assert.AreNotEqual("Doe", personDto.Last);
            Assert.AreEqual(30, personDto.Age);
        }

        [TestMethod]
        public void Map_ShouldIgnorePropertiesThatDoNotHaveMappings()
        {
            // Arrange
            var person = new Person { FirstName = "John", LastName = "Doe", Age = 30 };
            var mapper = new PropertyMapper<Person, PersonDto>();
            mapper.AddPropertyMap(p => p.FirstName, d => d.First);

            // Act
            var personDto = mapper.Map(person);

            // Assert
            Assert.AreEqual("John", personDto.First);
            Assert.IsNull(personDto.Last);
            Assert.AreEqual(30, personDto.Age);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PersonDto
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
    }

}
