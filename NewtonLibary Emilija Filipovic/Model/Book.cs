using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibary_Emilija_Filipovic.Model
{
    public class Book
    {

       // public Nullable<string> BookId{ get; set; }

        public int BookId { get; set; }
        public string Title { get; set; }
        public int? Year {  get; set; }
        public int YearPublished { get; set; }
        public int Rating { get; set; }

        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnTime { get; set; }
        public Borrower? BorrowedBy { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();
        

        public DateTime? ReturnDate { get; private set; } // ska vara returnde date
        public Guid Isbn { get; set; } = Guid.NewGuid();

        public int Grade { get; set; } = new Random().Next(1, 5);

    }
}
      
    


