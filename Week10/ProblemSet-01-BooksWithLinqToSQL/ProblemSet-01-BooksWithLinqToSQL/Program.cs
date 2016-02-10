using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_BooksWithLinqToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            BooksDataContext db = new BooksDataContext();
            PrintCommands();

            string input = string.Empty;
            while (input != "exit")
            {
                Console.Write("Command: ");
                input = Console.ReadLine().Trim();
                if (input == string.Empty) continue;
                string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch(words[0].ToLower())
                {
                    #region Add Commands
                    case "add":
                        {
                            if (words.Length != 2) goto default;
                            switch (words[1].ToLower())
                            {
                                case "author":
                                    CreateAuthor(db);
                                    break;
                                case "book":
                                    CreateBook(db);
                                    break;
                                case "genre":
                                    CreateGenre(db);
                                    break;
                                case "user":
                                    CreateUser(db);
                                    break;
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    #endregion
                    #region Book Commands
                    case "book":
                        {
                            if (words.Length != 4) goto default;
                            string type = words[2].ToLower();
                            switch (words[1].ToLower())
                            {
                                case "set":
                                    if (type == "authors") SetAuthors(db, words[3]);
                                    else if (type == "genres") SetGenres(db, words[3]);
                                    else goto default;
                                    break;
                                case "info":
                                    if (type == "isbn") PrintBookByIsbn(db, words[3]);
                                    else if (type == "title") PrintBookByTitle(db, words[3]);
                                    else goto default;
                                    break;
                                default:
                                    Console.WriteLine("Invalid command! Type help for list of available commands!");
                                    break;
                            }
                            break;
                        }
                    #endregion
                    #region Books Commands
                    case "books":
                        {
                            if (words.Length != 4 && words.Length != 5) goto default;
                            if (words[1].ToLower() == "sorted")
                            {
                                string sortBy = words[3].ToLower();
                                switch (sortBy)
                                {
                                    case "title":
                                        PrintSortedBooksByTitle(db);
                                        break;
                                    case "author":
                                        PrintSortedBooksByAuthor(db);
                                        break;
                                    case "genre":
                                        PrintSortedBooksByGenre(db);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid command! Type help for list of available commands!");
                                        break;
                                }
                            }
                            else if (words[2].ToLower() == "author")
                                PrintBooksFromAuthor(db, words[3], words[4]);
                            else
                                Console.WriteLine("Invalid command! Type help for list of available commands!");
                            break;
                        }
                    #endregion
                    case "genres":
                        {
                            if (words.Length != 5) goto default;
                            if (words[2].ToLower() == "author")
                            {
                                PrintGenresFromAuthor(db, words[3], words[4]);
                            }
                            else Console.WriteLine("Invalid command! Type help for list of available commands!");
                            break;
                        }
                    case "give":
                        {
                            if (words.Length != 6) goto default;
                            if (words[1].ToLower() == "book" && words[4].ToLower() == "user")
                            {
                                GiveBookToUser(db, words[2], words[5]);
                            }
                            else Console.WriteLine("Invalid command! Type help for list of available commands!");
                            break;
                        }
                    case "take":
                        {
                            if (words.Length != 6) goto default;
                            if (words[1].ToLower() == "book" && words[4].ToLower() == "user")
                            {
                                TakeBookFromUser(db, words[2], words[5]);
                            }
                            else Console.WriteLine("Invalid command! Type help for list of available commands!");
                            break;
                        }
                    case "list":
                        {
                            if (words.Length != 2) goto default;
                            if (words[1].ToLower() == "books") ListBooks(db);
                            else if (words[1].ToLower() == "users") ListUsers(db);
                            else Console.WriteLine("Invalid command! Type help for list of available commands!");
                            break;
                        }
                    case "help":
                        {
                            PrintCommands();
                            break;
                        }
                    case "exit":
                        {
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid command! Type help for list of available commands!");
                        break;
                }
            }
        }

        public static void PrintCommands()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("add author - begin operation for adding an author");
            Console.WriteLine("add book - begin operation for adding a book");
            Console.WriteLine("add genre - begin operation for adding a genre");
            Console.WriteLine("add user - begin operation for adding an user");
            Console.WriteLine("book set authors <bookID> - choose book and set it's authors");
            Console.WriteLine("book set genres <bookID> - choose book and set it's genres");
            Console.WriteLine("book info isbn <ISBN> - get book info by ISBN");
            Console.WriteLine("book info title <title> - get book info by title or part of the title");
            Console.WriteLine("books sorted by title - get books sorted by title");
            Console.WriteLine("books sorted by author - get books sorted by author");
            Console.WriteLine("books sorted by genre - get books sorted by genre");
            Console.WriteLine("books from author <authorFirstName> <authorLastName>- get books from author");
            Console.WriteLine("genres from author <authorFirstName> <authorLastName> - get genres from author");
            Console.WriteLine("give book <bookID> to user <userID> - give book to user");
            Console.WriteLine("take book <bookID> from user <userID> - take book from user");
            Console.WriteLine("list books - lists books with Id and Title");
            Console.WriteLine("list users - lists users with Id and Name");
            Console.WriteLine("help - returns list of commands");
            Console.WriteLine("exit - exits the program");
            Console.WriteLine();
        }

        public static void CreateAuthor(BooksDataContext db)
        {
            Console.WriteLine("Creating author!");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine().Trim();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine().Trim();
            Console.Write("Year born(dd-MM-yyyy format): ");
            var yearBorn = Console.ReadLine().Trim();
            Console.Write("Year died(dd-MM-yyyy format) if alive leave blank:");
            var yearDied = Console.ReadLine().Trim();

            Author auth = null;
            try
            {
                auth = new Author
                {
                    FirstName = firstName,
                    LastName = lastName,
                    YearBorn = DateTime.ParseExact(yearBorn, "dd-MM-yyyy", null),                 
                };
                if (yearDied != string.Empty) auth.YearDied = DateTime.ParseExact(yearDied, "dd-MM-yyyy", null);

                db.Authors.InsertOnSubmit(auth);
                db.SubmitChanges();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Given data is invalid! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot insert author in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Author created successfully!");
        }

        public static void CreateBook(BooksDataContext db)
        {
            Console.WriteLine("Creating book!");
            Console.Write("Title: ");
            var title = Console.ReadLine().Trim();
            Console.Write("Description: ");
            var description = Console.ReadLine().Trim();
            Console.Write("Date published(dd-MM-yyyy format): ");
            var datePublished = Console.ReadLine().Trim();
            Console.Write("Publisher: ");
            var publisher = Console.ReadLine().Trim();
            Console.Write("Pages(number): ");
            var pages = Console.ReadLine().Trim();
            Console.Write("ISBN: ");
            var isbn = Console.ReadLine().Trim();
            Console.Write("Quantity(number): ");
            var quantity = Console.ReadLine().Trim();

            Book book = null;
            try
            {
                book = new Book
                {
                    Title = title,
                    Description = description,
                    DatePublished = DateTime.ParseExact(datePublished, "dd-MM-yyyy", null),
                    Publisher = publisher,
                    Pages = int.Parse(pages),
                    ISBN = isbn,
                    Quantity = int.Parse(quantity)
                };

                db.Books.InsertOnSubmit(book);
                db.SubmitChanges();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Given data is invalid! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot insert book in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Book created successfully!");
        }

        public static void CreateGenre(BooksDataContext db)
        {
            Console.WriteLine("Creating genre!");
            Console.Write("Name: ");
            var name = Console.ReadLine().Trim();

            Genre genre = null;
            try
            {
                genre = new Genre
                {
                    Name = name
                };

                db.Genres.InsertOnSubmit(genre);
                db.SubmitChanges();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Given data is invalid! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot insert genre in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Genre created successfully!");
        }

        public static void CreateUser(BooksDataContext db)
        {
            Console.WriteLine("Creating user!");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine().Trim();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine().Trim();
            Console.Write("Pseudonim: ");
            var pseudonim = Console.ReadLine().Trim();
            Console.Write("Email:");
            var email = Console.ReadLine().Trim();
            Console.Write("Phone:");
            var phone = Console.ReadLine().Trim();

            User user = null;
            try
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Pseudonim = pseudonim,
                    Email = email,
                    Phone = phone                    
                };

                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Given data is invalid! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot insert user in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("User created successfully!");
        }

        public static void SetAuthors(BooksDataContext db, string bookArg)
        {
            int bookId;
            if (!int.TryParse(bookArg, out bookId))
            {
                Console.WriteLine("Invalid book ID! Operation aborted!");
                return;
            }

            var bookForUpdate = (from book in db.Books
                                where book.BookID == bookId
                                select book).SingleOrDefault();

            if (bookForUpdate == null)
            {
                Console.WriteLine("No such book! Operation aborted!");
                return;
            }

            Console.WriteLine("Found book: ");
            Console.WriteLine($"BookID: {bookForUpdate.BookID}");
            Console.WriteLine($"Title: {bookForUpdate.Title}");
            Console.WriteLine($"Description: {bookForUpdate.Description}");
            Console.WriteLine($"Publisher: {bookForUpdate.Publisher}");
            Console.WriteLine($"Pages: {bookForUpdate.Pages}");
            Console.WriteLine($"ISBN: {bookForUpdate.ISBN}");
            Console.WriteLine();

            HashSet<int> authorIds = new HashSet<int>();
            foreach (var author in db.Authors)
            {
                authorIds.Add(author.AuthorID);
                Console.WriteLine($"{author.AuthorID} - {author.FirstName} {author.LastName}");                
            }
            Console.WriteLine("Please write IDs of authors you want to add, separated by comma or space");
            int[] idsToAdd;
            try
            {
                idsToAdd = Console.ReadLine().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid ids format! Operation aborted!");
                Console.WriteLine(ex.Message);
                return;
            }

            bool allExist = true;
            foreach (var id in idsToAdd)
            {
                if (!authorIds.Contains(id))
                {
                    allExist = false;
                    break;
                }
            }

            if (!allExist)
            {
                Console.WriteLine("Ids provided does not exist! Operation aborted!");
                return;
            }

            foreach (var authID in idsToAdd)
            {
                var newAuth = new BookAuthor();
                newAuth.AuthorID = authID;
                bookForUpdate.BookAuthors.Add(newAuth);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot update book authors! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Book authors updated successfully!");
        }

        public static void SetGenres(BooksDataContext db, string bookArg)
        {
            int bookId;
            if (!int.TryParse(bookArg, out bookId))
            {
                Console.WriteLine("Invalid book ID! Operation aborted!");
                return;
            }

            var bookForUpdate = (from book in db.Books
                                 where book.BookID == bookId
                                 select book).SingleOrDefault();

            if (bookForUpdate == null)
            {
                Console.WriteLine("No such book! Operation aborted!");
                return;
            }

            Console.WriteLine("Found book: ");
            Console.WriteLine($"BookID: {bookForUpdate.BookID}");
            Console.WriteLine($"Title: {bookForUpdate.Title}");
            Console.WriteLine($"Description: {bookForUpdate.Description}");
            Console.WriteLine($"Publisher: {bookForUpdate.Publisher}");
            Console.WriteLine($"Pages: {bookForUpdate.Pages}");
            Console.WriteLine($"ISBN: {bookForUpdate.ISBN}");
            Console.WriteLine();

            HashSet<int> genreIds = new HashSet<int>();
            foreach (var genre in db.Genres)
            {
                Console.WriteLine($"{genre.GenreID} - {genre.Name}");
                genreIds.Add(genre.GenreID);
            }
            Console.WriteLine("Please write IDs of genres you want to add, separated by comma or space");
            int[] idsToAdd;
            try
            {
                idsToAdd = Console.ReadLine().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ids format! Operation aborted!");
                return;
            }

            bool allExist = true;
            foreach (var id in idsToAdd)
            {
                if (!genreIds.Contains(id))
                {
                    allExist = false;
                    break;
                }
            }
            if (!allExist)
            {
                Console.WriteLine("Invalid ids format! Operation aborted!");
                return;
            }

            foreach (var genreId in idsToAdd)
            {
                var newGenre = new BookGenre();
                newGenre.GenreID = genreId;
                bookForUpdate.BookGenres.Add(newGenre);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot update book genres! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Book genres updated successfully!");
        }

        public static void PrintBookByIsbn(BooksDataContext db, string isbn)
        {
            var searchedBook = (from book in db.Books
                               where book.ISBN == isbn
                               select book).SingleOrDefault();

            if (searchedBook == null)
            {
                Console.WriteLine("Nothing found!");
                return;
            }

            Console.WriteLine("Book found:");
            Console.WriteLine($"BookID: {searchedBook.BookID}");
            Console.WriteLine($"Title: {searchedBook.Title}");
            Console.WriteLine($"Description: {searchedBook.Description}");
            Console.WriteLine($"Publisher: {searchedBook.Publisher}");
            Console.WriteLine($"Pages: {searchedBook.Pages}");
            Console.WriteLine($"ISBN: {searchedBook.ISBN}");
            Console.WriteLine($"Quantity: {searchedBook.Quantity}");
            Console.WriteLine("Book authors: ");

            foreach (var item in searchedBook.BookAuthors)
            {
                Console.WriteLine($"{ item.Author.FirstName} {item.Author.LastName}");
            }

            Console.WriteLine("Book genres: ");

            foreach (var item in searchedBook.BookGenres)
            {
                Console.WriteLine(item.Genre.Name);
            }
        }

        public static void PrintBookByTitle(BooksDataContext db, string title)
        {
            var searchedBook = (from book in db.Books
                                where book.Title.Contains(title)
                                select book)?.First();

            if (searchedBook == null)
            {
                Console.WriteLine("Nothing found!");
                return;
            }

            Console.WriteLine("Book found:");
            Console.WriteLine($"BookID: {searchedBook.BookID}");
            Console.WriteLine($"Title: {searchedBook.Title}");
            Console.WriteLine($"Description: {searchedBook.Description}");
            Console.WriteLine($"Publisher: {searchedBook.Publisher}");
            Console.WriteLine($"Pages: {searchedBook.Pages}");
            Console.WriteLine($"ISBN: {searchedBook.ISBN}");
            Console.WriteLine($"Quantity: {searchedBook.Quantity}");
            Console.WriteLine("Book authors: ");

            foreach (var item in searchedBook.BookAuthors)
            {
                Console.WriteLine($"{ item.Author.FirstName} {item.Author.LastName}");
            }

            Console.WriteLine("Book genres: ");

            foreach (var item in searchedBook.BookGenres)
            {
                Console.WriteLine(item.Genre.Name);
            }
        }

        public static void PrintSortedBooksByTitle(BooksDataContext db)
        {
            var sorted = from book in db.Books
                         orderby book.Title
                         select book;

            foreach (var book in sorted)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"BookId: {book.BookID}");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Description: {book.Description}");
                Console.WriteLine($"Publisher: {book.Publisher}");
                Console.WriteLine($"Pages: {book.Pages}");
                Console.WriteLine($"ISBN: {book.ISBN}");
                Console.WriteLine($"Quantity: {book.Quantity}");
                Console.WriteLine("Book authors: ");

                foreach (var item in book.BookAuthors)
                {
                    Console.WriteLine($"{ item.Author.FirstName} {item.Author.LastName}");
                }

                Console.WriteLine("Book genres: ");

                foreach (var item in book.BookGenres)
                {
                    Console.WriteLine(item.Genre.Name);
                }
            }
            Console.WriteLine();
        }

        public static void PrintSortedBooksByAuthor(BooksDataContext db)
        {
            var sorted = from book in db.Books
                         let bookAuthorNames = (from bookAuth in book.BookAuthors
                                                orderby bookAuth.Author.FirstName,
                                                        bookAuth.Author.LastName
                                                select new
                                                {
                                                    FirstName = bookAuth.Author.FirstName,
                                                    LastName = bookAuth.Author.LastName
                                                })
                         orderby bookAuthorNames.First().FirstName,
                                 bookAuthorNames.First().LastName,
                                 bookAuthorNames.Count()
                         select new
                         {
                             Book = book,
                             AuthorNames = bookAuthorNames
                         };

            foreach (var book in sorted)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"BookId: {book.Book.BookID}");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Title: {book.Book.Title}");
                Console.WriteLine($"Description: {book.Book.Description}");
                Console.WriteLine($"Publisher: {book.Book.Publisher}");
                Console.WriteLine($"Pages: {book.Book.Pages}");
                Console.WriteLine($"ISBN: {book.Book.ISBN}");
                Console.WriteLine($"Quantity: {book.Book.Quantity}");
                Console.WriteLine("Book authors: ");

                foreach (var item in book.AuthorNames)
                {
                    Console.WriteLine($"{ item.FirstName} {item.LastName}");
                }

                Console.WriteLine("Book genres: ");

                foreach (var item in book.Book.BookGenres)
                {
                    Console.WriteLine(item.Genre.Name);
                }
            }
            Console.WriteLine();
        }

        public static void PrintSortedBooksByGenre(BooksDataContext db)
        {
            var sorted = from book in db.Books
                         let bookGenres = (from bookGenre in book.BookGenres
                                           orderby bookGenre.Genre.Name
                                           select bookGenre)
                         orderby bookGenres.First().Genre.Name,
                                 bookGenres.Count()
                         select new
                         {
                             Book = book,
                             BookGenres = bookGenres
                         };

            foreach (var book in sorted)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"BookId: {book.Book.BookID}");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Title: {book.Book.Title}");
                Console.WriteLine($"Description: {book.Book.Description}");
                Console.WriteLine($"Publisher: {book.Book.Publisher}");
                Console.WriteLine($"Pages: {book.Book.Pages}");
                Console.WriteLine($"ISBN: {book.Book.ISBN}");
                Console.WriteLine($"Quantity: {book.Book.Quantity}");
                Console.WriteLine("Book authors: ");

                foreach (var item in book.Book.BookAuthors)
                {
                    Console.WriteLine($"{ item.Author.FirstName} {item.Author.LastName}");
                }

                Console.WriteLine("Book genres: ");

                foreach (var item in book.BookGenres)
                {
                    Console.WriteLine(item.Genre.Name);
                }
            }
            Console.WriteLine();
        }

        public static void PrintBooksFromAuthor(BooksDataContext db, string authorFirstName, string authorLastName)
        {
            var booksToPrint = (from author in db.Authors
                               where author.FirstName == authorFirstName
                                     && author.LastName == authorLastName
                               let books =  (from bookAuthor in author.BookAuthors
                                            select bookAuthor.Book)
                               select books).SingleOrDefault();

            if (booksToPrint == null)
            {
                Console.WriteLine("No books from this author!");
                return;
            }

            foreach (var searchedBook in booksToPrint)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"BookID: {searchedBook.BookID}");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Title: {searchedBook.Title}");
                Console.WriteLine($"Description: {searchedBook.Description}");
                Console.WriteLine($"Publisher: {searchedBook.Publisher}");
                Console.WriteLine($"Pages: {searchedBook.Pages}");
                Console.WriteLine($"ISBN: {searchedBook.ISBN}");
                Console.WriteLine($"Quantity: {searchedBook.Quantity}");
                Console.WriteLine("Book authors: ");

                foreach (var item in searchedBook.BookAuthors)
                {
                    Console.WriteLine($"{ item.Author.FirstName} {item.Author.LastName}");
                }

                Console.WriteLine("Book genres: ");

                foreach (var item in searchedBook.BookGenres)
                {
                    Console.WriteLine(item.Genre.Name);
                }
            }
            Console.WriteLine();
        }

        public static void PrintGenresFromAuthor(BooksDataContext db, string authorFirstName, string authorLastName)
        {
            var genresToPrint = (from author in db.Authors
                                 where author.FirstName == authorFirstName
                                       && author.LastName == authorLastName
                                 let genres = (from bookAuthor in author.BookAuthors
                                              join bookGenre in db.BookGenres
                                              on bookAuthor.BookID equals bookGenre.BookID
                                              select bookGenre.Genre)
                                              .Distinct()
                                              .OrderBy(x => x.Name)
                                 select genres).SingleOrDefault();

            if (genresToPrint == null)
            {
                Console.WriteLine("No genres from this author!");
                return;
            }

            foreach (var genre in genresToPrint)
            {
                Console.WriteLine(genre.Name);
            }
            Console.WriteLine();
        }

        public static void GiveBookToUser(BooksDataContext db, string bookArg, string authorArg)
        {
            int userId;
            if (!int.TryParse(authorArg, out userId))
            {
                Console.WriteLine("Invalid user ID! Operation aborted!");
                return;
            }

            int bookId;
            if (!int.TryParse(bookArg, out bookId))
            {
                Console.WriteLine("Invalid book ID! Operation aborted!");
                return;
            }

            var userToLendTo = (from user in db.Users
                               where user.UserID == userId
                               select user).SingleOrDefault();

            if (userToLendTo == null)
            {
                Console.WriteLine("No such user! Operation aborted!");
                return;
            }

            var bookToLend = (from book in db.Books
                              where book.BookID == bookId
                              select book).SingleOrDefault();

            if (bookToLend == null)
            {
                Console.WriteLine("No such book! Operation aborted!");
                return;
            }

            var userLoanings = from loanedBook in db.LoanedBooks
                               where loanedBook.UserID == userId
                               select loanedBook.Quantity;

            int loanedBooksByUser = userLoanings.Any() ? userLoanings.Sum() : 0;

            if (loanedBooksByUser == 5)
            {
                Console.WriteLine("User can't lend more than 5 books! Operation aborted!");
                return;
            }

            var bookQuantities = (from book in db.LoanedBooks
                                  where book.BookID == bookId
                                  select book.Quantity);

            int givenCopies = bookQuantities.Any() ? bookQuantities.Sum() : 0;

            var availableCopies = bookToLend.Quantity - givenCopies;

            if (availableCopies == 0)
            {
                Console.WriteLine("No more available copies of the book! Operation aborted!");
                return;
            }

            var bookLoan = (from loanedBook in db.LoanedBooks
                            where loanedBook.BookID == bookId && loanedBook.UserID == userId
                            select loanedBook).SingleOrDefault();

            if (bookLoan == null)
            {
                var newBookLoan = new LoanedBook();
                newBookLoan.BookID = bookId;
                newBookLoan.UserID = userId;
                newBookLoan.Quantity = 1;
                newBookLoan.LoanDate = DateTime.Now;
                newBookLoan.ExpirationDate = DateTime.Now.AddMonths(1);
                db.LoanedBooks.InsertOnSubmit(newBookLoan);
            }
            else
            {
                bookLoan.Quantity++;
                bookLoan.LoanDate = DateTime.Now;
                bookLoan.ExpirationDate = DateTime.Now.AddMonths(1);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot update info in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Book loaned successfully!");
        }

        public static void TakeBookFromUser(BooksDataContext db, string bookArg, string authorArg)
        {
            int userId;
            if (!int.TryParse(authorArg, out userId))
            {
                Console.WriteLine("Invalid user ID! Operation aborted!");
                return;
            }

            int bookId;
            if (!int.TryParse(bookArg, out bookId))
            {
                Console.WriteLine("Invalid book ID! Operation aborted!");
                return;
            }

            var userReturningBook = (from user in db.Users
                                     where user.UserID == userId
                                     select user).SingleOrDefault();

            if (userReturningBook == null)
            {
                Console.WriteLine("No such user! Operation aborted!");
                return;
            }

            var returnedBook = (from book in db.Books
                                where book.BookID == bookId
                                select book).SingleOrDefault();

            if (returnedBook == null)
            {
                Console.WriteLine("No such book! Operation aborted!");
                return;
            }

            var bookLoan = (from loanedBook in db.LoanedBooks
                            where loanedBook.BookID == bookId && loanedBook.UserID == userId
                            select loanedBook).SingleOrDefault();

            if (bookLoan == null)
            {
                Console.WriteLine("User has not loaned this book! Operation aborted!");
                return;
            }

            var expirationDate = bookLoan.ExpirationDate;

            try
            {
                if (bookLoan.Quantity == 1)
                {
                    db.LoanedBooks.DeleteOnSubmit(bookLoan);
                }
                else bookLoan.Quantity--;

                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                Console.WriteLine("Cannot update info in database! Operation exited with message:");
                Console.WriteLine(ex.Message);
                return;
            }

            if (expirationDate < DateTime.Now)
            {
                Console.WriteLine($"Book returned successfully with delay of {(int)(DateTime.Now - expirationDate).TotalDays} days!");
            }
            else
            {
                Console.WriteLine($"Book returned successfully on time {(int)(expirationDate - DateTime.Now).TotalDays} days earlier!");
            }
        }

        public static void ListBooks(BooksDataContext db)
        {
            Console.WriteLine("BookId - Title");
            Console.WriteLine("--------------");
            foreach (var book in db.Books)
            {
                Console.WriteLine($"{book.BookID} - {book.Title}");
            }
            Console.WriteLine();
        }

        public static void ListUsers(BooksDataContext db)
        {
            Console.WriteLine("UserId - FirstName LastName");
            Console.WriteLine("---------------------------");
            foreach (var user in db.Users)
            {
                Console.WriteLine($"{user.UserID} - {user.FirstName} {user.LastName}");
            }
            Console.WriteLine();
        }
    }
}
