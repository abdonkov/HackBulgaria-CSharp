using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndAuthors
{
    public interface IAuthorSerializer
    {
        void SerializeAuthor(Author author, string filename);
        Author DeserializeAuthor(string filename);
    }
}
