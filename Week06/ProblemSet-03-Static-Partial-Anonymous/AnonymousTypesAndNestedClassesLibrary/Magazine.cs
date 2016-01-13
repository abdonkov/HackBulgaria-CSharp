using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousTypesAndNestedClassesLibrary
{
    public class Magazine
    {
        public string Title { get; }
        public int ISBN { get; }

        public Magazine(string title, int isbn)
        {
            Title = title;
            ISBN = isbn;
        }
    }
}
