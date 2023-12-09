using NewtonLibary_Emilija_Filipovic.Data;

namespace NewtonLibary_Emilija_Filipovic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Libary");
            DataAccess dataAcces = new DataAccess();



            //dataAcces.CreateFiller();  
           //dataAcces.BorrowBook(5, 6); // Låna en bok
            dataAcces.ReturnBook(5);       // Lämna tilklbaka en bok

            //dataAcces.MarkBookAsNotLoaned(1); // Markera bok som lånad

            //dataAcces.AddBookToDatabase("Solsidan", 2); // Lägg till bok i databasen
           // dataAcces.RemoveBook(1);  // Ta bort en bok från databasen (ej klar)

           // dataAcces.AddAuthorToDatabase(1) // 
            //dataAcces.RemoveAuthorFromDatabase // 

            // dataAccess.RemoveBorrower(1); // Ta bort låntagare med ID 1

            //dataAccess.RemoveBook(2);      // Ta bort bok med ID 2
            //dataAccess.RemoveAuthor(3);    // Ta bort författare med ID 3
           


        }
    }
}