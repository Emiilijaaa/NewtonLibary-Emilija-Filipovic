using Microsoft.EntityFrameworkCore;
using NewtonLibary_Emilija_Filipovic.Model;
using System.ComponentModel;
using Helpers;

namespace NewtonLibary_Emilija_Filipovic.Data
{

     public static class StringExtensions
{
    public static string[] SplitCamelCase(this string input)
    {
        return System.Text.RegularExpressions.Regex.Split(input, @"(?<!^)(?=[A-Z])");
    }
}


   public class DataAccess
  {

        public enum BookTitles
        {
            [Description("Solsidan")] Felix, Herngren, [Description("Sune på bilsemester")] Hannes, Holm, [Description("Sune i fjällen")] Gustav, Åkerblom,
            [Description("Game Of Thrones")] George, Martin, [Description("Emily in Paris")] Darren, Star, [Description("Batman Bin Suparman")] FMARVEL, Halo,
            [Description("Titanic")] James, Cameron, [Description("Hungergames 1")] Garry, Ros, [Description("Alfons Åberg")] Gunnilla, Bergström, [Description("A star is born")] Emilija, Filipovic, [Description("The Hobbit")] Peter, Jacksson,
            [Description("Babblarna")] Agnes, Åkerlind, [Description("Gossip Girl")] Stephanie, Savage, [Description("Gåsmamman")] Camilla, Ahlgren, [Description("Johan Falk")] Jacob, Eklund, [Description("Mr Bean")] Rowan, Atickson
        }

        //public enum AuthorNames
        //{
        //    JohnDoe, JaneSmith, AstridLindgren, EmilijaFilipovic, ZlatanIbrahimovic, HannesHolm, GunillaBergström, PärLagerqvist, SelmaLagerlöf
        //    // Lägg till fler författarnamn här
        //}

        public csSeedGenerator rnd = new csSeedGenerator();

        public void CreateFiller()
        {


            using (var context = new Context())
            {
                for (int i = 0; i < 10; i++)
                {
                    csSeedGenerator rnd = new csSeedGenerator();
                    Borrower person = new Borrower();
                    person.LibaryCardNumber = "123";

                    person.FirstName = rnd.FirstName;
                    person.LastName = rnd.LastName;

                    Book book = new Book();
                    book.Year = rnd.Next(1900, 2023);
                    book.Rating = rnd.Next(1, 10);



                    book.Title = GetEnumDescription(rnd.FromEnum<BookTitles>());

                    
                    
                    /*
                    autor.FirstName = rnd.FirstName;
                    autor.LastName = rnd.LastName;

                    context.Borrowers.Add(person);
                    context.Books.Add(book);
                    context.Authors.Add(autor);
                    */


                }

                context.SaveChanges();
            }
        }

       
        public void BorrowBook(int bookId, int borrowerId)
        {
            using (Context context = new())
            {
                Book book = context.Books.Find(bookId);
                Borrower borrower = context.Borrowers.Find(borrowerId);

                if (book != null && borrower != null && book.BorrowedBy == null)
                
                {
                    //Set the book as loned
                    book.BorrowedBy = borrower;
                    book.BorrowDate = DateTime.Now;
                    //book.ReturnTime = DateTime.Now;

                    
                    context.SaveChanges();

                }
            }
        }
        
        public void ReturnBook(int bookId)
        {
            using (var context = new Context())
            {
                Book book = context.Books.Include(b => b.BorrowedBy).FirstOrDefault(b => b.BookId == bookId);

                if (book != null && book.BorrowedBy != null)
                {
                    // Explicit loading av relaterad data (BorrowedBy)
                    context.Entry(book).Reference(b => b.BorrowedBy).Load();

                    // Returnera boken
                    book.BorrowedBy = null;
                    book.BorrowDate = null;
                    book.ReturnTime = DateTime.Now;

                    context.SaveChanges();
                }
            }
        }
        //public void ConnectAuthorAndBook(int authorId, int bookId)
        //{
        //    using (var context = new Context())
        //    {
        //        // Hämta författaren och boken från databasen
        //        Author author = context.Authors.Find(authorId);
        //        Book book = context.Books.Find(bookId);
        //        context.SaveChanges();

        //        // Kontrollera att både författaren och boken finns
        //        if (author != null && book != null)
        //        {
        //            // Skapa kopplingen mellan författaren och boken
        //            author.Books.Add(book);
        //            book.Authors.Add(author);

        //            // Spara ändringarna i databasen
        //            context.SaveChanges();
        //        }
        //    }
        //}


        public void MarkBookAsNotLoaned(int bookId)
        {
            using (var context = new Context())
            {
                var book = context.Books.Include(b => b.BorrowedBy).FirstOrDefault(b => b.BookId == bookId);

                if (book != null && book.BorrowedBy != null)
                {
                    // Update LoanCardId to null, marking the book as not loaned
                    //book.BookId = null;

                    
                    // If the book was associated with a LoanCard, remove it from the LoanCard's collection
                    book.BorrowedBy = null;


                    // Save changes to the database
                    context.SaveChanges();
                }
            }
        }

      

        public void AddBarrowerToDatabase(string firstName, string lastName)  
        {
            using (var context = new Context())
            {
                var person = new Borrower
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Borrowers.Add(person);
                context.SaveChanges();
            }
        }
        public void RemoveBorrower(int borrowerId)
        {
            using (var context = new Context())
            {
                var borrowerToRemove = context.Borrowers.Find(borrowerId);

                if (borrowerToRemove != null)
                {
                    context.Borrowers.Remove(borrowerToRemove);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No Borrower found with ID: {borrowerId}");
                }
            }
        }
        //public void AddAuthorToDatabase(AuthorNames authorName)
        //{
        //    using (var context = new Context())
        //    {
        //        string[] fullName = authorName.ToString().SplitCamelCase(); // Använd en anpassad metod för att splitta camel case

        //        var author = new Model.Author
        //        {
        //            FirstName = fullName[0],
        //            LastName = fullName.Length > 1 ? fullName[1] : string.Empty
        //        };

        //        context.Authors.Add(author);
        //        context.SaveChanges();

        //    }
        //}
        //private static string[] SplitCamelCase(this string input)
        //{
        //    return System.Text.RegularExpressions.Regex.Split(input, @"(?<!^)(?=[A-Z])");
        //}
    



//public void AddBookToDatabase(string title, params int[] autorIds)
//{
//    using (var context = new Context())
//    {
//        var autors = context.Authors.Where(a => autorIds.Contains(a.AuthorId)).ToList();

//        var book = new Book
//        {
//            Title = title,
//            Authors = autors,
//            Rating =new Random().Next(1 - 10),
//            Year = new Random().Next(1900, 2023)

//        };

//        context.Books.Add(book);
//        context.SaveChanges();
//    }
//}
public void AddBookToDatabase(string title, params int[] authorIds)
        {
            using (var context = new Context())
            {
                var authors = context.Authors.Where(a => authorIds.Contains(a.AuthorId)).ToList();

                var book = new Book
                {
                    Title = title,
                    Authors = authors,
                    Rating = new Random().Next(1, 11),  // Uppdaterat för att undvika negativa nummer
                    Year = new Random().Next(1900, 2023)
                };

                context.Books.Add(book);
               context.SaveChanges();
            }
        }

        public void RemoveBook(int bookId)
        {
            using (var context = new Context())
            {
                var bookToRemove = context.Books.Find(bookId);

                if (bookToRemove != null)
                {
                    context.Books.Remove(bookToRemove);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No Book found with ID: {bookId}");
                }
            }
        }
        public void AddAuthorToDatabase(string firstName, string lastName)
        {
            using (var context = new Context())
            {
                var author = new Model.Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Authors.Add(author);
                context.SaveChanges();
            }
        }
        public void RemoveAuthor(int authorId)
        {
            using (var context = new Context())
            {
                var authorToRemove = context.Authors.Find(authorId);

                if (authorToRemove != null)
                {
                    context.Authors.Remove(authorToRemove);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No Author found with ID: {authorId}");
                }
            }
        }



        public void Clear()
        {
            using (var context = new Context())
            {
                var allPersons = context.Borrowers.ToList();
                context.Borrowers.RemoveRange(allPersons);
                var allBooks = context.Books.ToList();
                context.Books.RemoveRange(allBooks);
                var allAutors = context.Authors.ToList();
                context.Authors.RemoveRange(allAutors);
                context.SaveChanges();
            }
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return value.ToString();
        }
    }
}


