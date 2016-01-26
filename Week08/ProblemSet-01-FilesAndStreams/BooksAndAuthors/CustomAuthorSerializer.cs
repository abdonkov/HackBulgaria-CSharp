using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndAuthors
{
    public class CustomAuthorSerializer : IAuthorSerializer
    {
        public void SerializeAuthor(Author author, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(author.Name);
                sw.WriteLine(author.Email);
                foreach (var book in author.Books)
                {
                    sw.WriteLine(string.Join("$", book.Title, book.PublishDate));
                }                
            }
        }

        public Author DeserializeAuthor(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length == 0) return new Author(string.Empty, string.Empty, new List<Book>());
            if (lines.Length == 1) return new Author(lines[0], string.Empty, new List<Book>());
            if (lines.Length == 2) return new Author(lines[0], lines[1], new List<Book>());

            List<Book> authorBooks = new List<Book>();
            for (int i = 2; i < lines.Length; i++)
            {
                var bookAttr = lines[i].Split('$');
                if (bookAttr.Length == 2) authorBooks.Add(new Book(bookAttr[0], Convert.ToDateTime(bookAttr[1])));
            }

            return new Author(lines[0], lines[1], authorBooks);
        }
    }
}
