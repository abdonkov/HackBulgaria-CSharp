using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndAuthors
{
    [Serializable]
    public class Book
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }

        public Book()
        {

        }

        public Book(string title, DateTime publishDate)
        {
            Title = title;
            PublishDate = publishDate;
        }

        public override string ToString()
        {
            return $"Book[Title: {Title}, PublishDate: {PublishDate}]";
        }
    }
}
