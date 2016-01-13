using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousTypesAndNestedClassesLibrary
{
    public static class MagazineAndBookSorter
    {
        public static List<string> Sort(List<Book> books, List<Magazine> magazines)
        {
            List<Edition> allItems = new List<Edition>();
            foreach (var book in books)
            {
                allItems.Add(new Edition(book));
            }
            foreach (var magazine in magazines)
            {
                allItems.Add(new Edition(magazine));
            }
            return allItems
                .OrderBy(x => x.EditionName)
                .ThenBy(x => x.Order)
                .Select(x => x.EditionName)
                .ToList();
        }

        public class Edition
        {
            public string EditionName { get; }
            public int Order { get; }

            public Edition(Book book)
            {
                EditionName = book.Name;
                Order = book.Id;
            }

            public Edition(Magazine magazine)
            {
                EditionName = magazine.Title;
                Order = magazine.ISBN;
            }
        }
    }
}
