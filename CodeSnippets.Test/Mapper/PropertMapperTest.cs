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
        public void Map_ShouldMapProperties_WhenPropertyNamesAreTheSame()
        {
            // Arrange
            var mapper = new PropertyMapper<Source, Target>();
            var source = new Source { Id = 1, Name = "John" };

            // Act
            var target = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Id, target.Id);
            Assert.AreNotEqual(source.Name, target.FullName);
        }

        [TestMethod]
        public void Map_ShouldMapProperties_WhenPropertyNamesAreDifferent()
        {
            // Arrange
            var mapper = new PropertyMapper<Source, Target>();
            mapper.CreateMap("Name", "FullName");
            var source = new Source { Id = 1, Name = "John" };

            // Act
            var target = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Id, target.Id);
            Assert.AreEqual(source.Name, target.FullName);
        }
    }

    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
