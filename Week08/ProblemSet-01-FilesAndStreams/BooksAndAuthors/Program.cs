using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndAuthors
{
    class Program
    {
        static void Main(string[] args)
        {
            var author1 = new Author("authorName", "authorEmail",
                new List<Book>()
                {
                    new Book("Book-title1", DateTime.Now),
                    new Book("Book-title2", DateTime.Now.AddDays(-465)),
                    new Book("Book-title3", DateTime.Now.AddMonths(-45))
                });

            XmlAuthorSerializer xas = new XmlAuthorSerializer();
            xas.SerializeAuthor(author1, "author1.xml");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("----Build-in XML Serializer output----");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(File.ReadAllText("author1.xml"));

            Author author2 = xas.DeserializeAuthor("author1.xml");

            CustomAuthorSerializer bas = new CustomAuthorSerializer();
            bas.SerializeAuthor(author2, "author2.txt");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("-----Custom author serialization-----");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(File.ReadAllText("author2.txt"));
            Console.WriteLine("--------------------------------------");
            var author3 = bas.DeserializeAuthor("author2.txt");

            Console.WriteLine("---Used author---");
            Console.WriteLine($"Name: {author3.Name}");
            Console.WriteLine($"Email: {author3.Email}");
            Console.WriteLine("Books: ");
            Console.WriteLine(string.Join(Environment.NewLine, author3.Books));
            Console.WriteLine("--------------------------------------");
            Console.ReadKey();
        }
    }
}
