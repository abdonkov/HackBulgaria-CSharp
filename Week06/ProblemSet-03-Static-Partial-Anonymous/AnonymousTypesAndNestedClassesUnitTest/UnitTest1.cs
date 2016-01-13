using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnonymousTypesAndNestedClassesLibrary;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnonymousTypesAndNestedClassesUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SortItemsWithoutSameTitles()
        {
            List<Book> books = new List<Book>()
            {
                new Book("abc", 1),
                new Book("aaa", 22),
                new Book("ccc", 4)
            };
            List<Magazine> magazines = new List<Magazine>()
            {
                new Magazine("abb", 1),
                new Magazine("cbb", 1),
                new Magazine("cca", 1)
            };
            List<string> sortedNames = new List<string>()
            {
                "aaa",
                "abb",
                "abc",
                "cbb",
                "cca",
                "ccc"
            };
            bool allItemsAreTheSame = true;
            int counter = 0;
            foreach (var name in MagazineAndBookSorter.Sort(books, magazines))
            {
                if (!name.Equals(sortedNames[counter++])) allItemsAreTheSame = false;
            }
            Assert.IsTrue(allItemsAreTheSame);
        }

        [TestMethod]
        public void SortItemsWithSameTitles()
        {
            List<Book> books = new List<Book>()
            {
                new Book("abc", 1),
                new Book("aaa", 22),
                new Book("ccc", 4)
            };
            List<Magazine> magazines = new List<Magazine>()
            {
                new Magazine("abb", 1),
                new Magazine("cbb", 1),
                new Magazine("aaa", 1)
            };
            List<string> sortedNames = new List<string>()
            {
                "aaa",
                "aaa",
                "abb",
                "abc",
                "cbb",
                "ccc"
            };
            bool allItemsAreTheSame = true;
            int counter = 0;
            foreach (var name in MagazineAndBookSorter.Sort(books, magazines))
            {
                if (!name.Equals(sortedNames[counter++])) allItemsAreTheSame = false;
            }
            Assert.IsTrue(allItemsAreTheSame);
        }
    }
}
