using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibary_Emilija_Filipovic.Model
{
    public class BookLoan
    {
        public int BookLoanId { get; set; }
        public int BorrowerId { get; set; }  
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }                
        public Borrower Borrower { get; set; }
        
        

        public ICollection<Book> Books { get; set; } = new List<Book>();
        public BookLoan()
        {

        }

        internal void Remove(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
