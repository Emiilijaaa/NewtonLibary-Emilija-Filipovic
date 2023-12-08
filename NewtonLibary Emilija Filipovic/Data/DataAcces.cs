using Microsoft.EntityFrameworkCore;
using NewtonLibary_Emilija_Filipovic.Model;
using helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibary_Emilija_Filipovic.Data
{
    public class DataAcces
    {
        public enum BookTitles
        {
            [Description("Metro 2033")] Metro, [Description("Lord of the rings")] Lotr, [Description("Judge Dredd")] Dredd,
            [Description("Game Of Thrones")] GOT, [Description("Silent Hill")] SH, [Description("Batman Bin Suparman")] FMARVEL, Halo,
            [Description("The Picture of Dorian Gray")] DG, [Description("Never Let Me Go")] Never, [Description("The Road")] VÄG, [Description("March: Book One (Oversized Edition)")] Stor, [Description("The Hobbit")] Liten,
            [Description("Pride and Prejudice")] TroddeDettaVaEnFilm, [Description("A Tale of Two Cities")] SimCity, [Description("Crime and Punishment")] HörtDennaVaBra, [Description("We Should All Be Feminists")] IVissaFall, [Description("Persepolis")] NuräckerD,
        }

        internal class DataAccess
        {
            internal csSeedGenerator rnd = new csSeedGenerator();


            public void CreateFiller()
            {
                using (var context = new Context())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Borrower person = new Borrower();

                        person.FirstName = rnd.FirstName;
                        person.LastName = rnd.LastName;

                        Book book = new Book();
                        book.Year = rnd.Next(1900, 2023);
                        book.Title = GetEnumDescription(rnd.FromEnum<BookTitles>());

                        BookLoan loanCard = new BookLoan();

                        Author autor = new Author();

                        autor.FirstName = rnd.FirstName;
                        autor.LastName = rnd.LastName;

                        context.Borrowers.Add(person);
                        context.Books.Add(book);
                        context.Authors.Add(autor);
                        context.BookLoans.Add(loanCard);


                    }

                    context.SaveChanges();
                }
            }


            public void MarkBookAsNotLoaned(int bookId)
            {
                using (var context = new Context())
                {
                    var book = context.Books.Include(b => b.BookId).FirstOrDefault(b => b.BookId == bookId);

                    if (book != null)
                    {
                        // Update LoanCardId to null, marking the book as not loaned
                        book.BookId = null;


                        // If the book was associated with a LoanCard, remove it from the LoanCard's collection
                        if (book.BookId != null)
                        {
                            book.LoanCard.Remove(book);
                        }

                        // Save changes to the database
                        context.SaveChanges();
                    }
                }
            }


            public void AddPersonToDatabase(string firstName, string lastName)
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

            public void AddBookToDatabase(string title, params int[] autorIds)
            {
                using (var context = new Context())
                {
                    var autors = context.Authors.Where(a => autorIds.Contains(a.AuthorId)).ToList();

                    var book = new Book
                    {
                        Title = title,
                        Authors = autors,
                        Year = new Random().Next(1900, 2023)

                    };

                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }



            public void AddLoanCardToPerson(int id)
            {
                using (var context = new Context())
                {
                    // Step 1: Retrieve the Person
                    var person = context.Borrowers.Find(id);

                    if (person == null)
                    {
                        // Handle the case where the person with the specified ID doesn't exist
                        // You can throw an exception, log a message, or take appropriate action
                        return;
                    }

                    // Step 2: Create a new LoanCard
                    var loanCard = new BookLoan();

                    // Step 3: Link the LoanCard to the Person
                    person.BookLoan = loanCard;


                    // Step 4: Save changes to the database
                    context.SaveChanges();
                }
            }

            public void AddBookIdToPersonLoanCard(int personId, int bookId)
            {
                using (var context = new Context())
                {
                    // Step 1: Retrieve the Person with LoanCard
                    var person = context.Borrowers.Include(p => p.BookLoans).SingleOrDefault(p => p.BorrowerId == personId);

                    if (person == null)
                    {
                        // Handle the case where the person with the specified ID doesn't exist
                        // You can throw an exception, log a message, or take appropriate action
                        return;
                    }

                    // Step 2: Check if the person has a LoanCard
                    if (person.BookLoans == null)
                    {
                        // Handle the case where the person doesn't have a LoanCard
                        // You can create a new LoanCard, associate it with the person, and proceed
                        return;
                    }

                    // Step 3: Link the existing book to the LoanCard using the book ID

                    var book = context.Books.Find(bookId);

                    if (book != null)
                    {
                        // Assuming LoanCardId is the foreign key in the Book entity
                        book.BookId = person.BorrowerId;
                        context.SaveChanges(); // Save changes to the book
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
                    var allLoanC = context.BookLoans.ToList();
                    context.RemoveRange(allLoanC);
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
}

         