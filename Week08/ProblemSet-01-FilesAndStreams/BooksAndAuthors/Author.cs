using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndAuthors
{
    [Serializable]
    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; }

        public Author()
        {

        }

        public Author(string name, string email, List<Book> books)
        {
            Name = name;
            Email = email;
            Books = books;
        }
    }
}
