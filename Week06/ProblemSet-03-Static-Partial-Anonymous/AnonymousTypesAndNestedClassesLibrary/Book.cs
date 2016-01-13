using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousTypesAndNestedClassesLibrary
{
    public class Book
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Book(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
