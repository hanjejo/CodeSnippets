using CodeSnippets.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnipets
{
    public class Source
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class Destination
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }

    public static class Program
    {
        public static void Main()
        {
            var propertyMapper = new PropertyMapper<Source, Destination>();
            propertyMapper.AddPropertyMap(src => src.FirstName, dest => dest.FullName);
                
            var source = new Source { FirstName = "John", LastName = "Doe", Age = 25 };
            var destination = propertyMapper.Map(source);

            Console.WriteLine($"Source: {source.FirstName}, {source.LastName}, {source.Age}");
            Console.WriteLine($"Destination: {destination.FullName}, {destination.Age}");
        }
    }
}
