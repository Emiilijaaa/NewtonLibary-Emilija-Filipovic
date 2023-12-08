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
        public bool IsBorrowed { get; set; }
        public string ISBN { get; set; }
        public int YearPublished { get; set; }
        public int Rating { get; set; }


        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();

        public bool Loaned
        {
            get => LoanCardId.HasValue;
            set
            {
                if (value && !_loanDate.HasValue)
                {
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(14);
                }
                else if (!value)
                {
                    _loanDate = null;
                    ReturnDate = null;
                }
            }
        }

        private DateTime? _loanDate;

        public DateTime? LoanDate
        {
            get => _loanDate;
            set
            {
                _loanDate = value;

                if (Loaned && _loanDate == null)
                {
                    // Book is still loaned, update _loanDate and ReturnDate
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(14);
                }
                else if (!Loaned)
                {
                    // Book is not loaned, reset _loanDate and ReturnDate
                    _loanDate = null;
                    ReturnDate = null;
                }
            }
        }

        public DateTime? ReturnDate { get; private set; } // ska vara returnde date
        public Guid Isbn { get; set; } = Guid.NewGuid();

        public int Grade { get; set; } = new Random().Next(1, 5);


        private int? _loanCardId;
        public int? LoanCardId
        {
            get => _loanCardId;
            set
            {
                _loanCardId = value;

                if (value == null)
                {
                    _loanDate = null;
                    ReturnDate = null;
                }
                else if (Loaned)
                {
                    // Update LoanDate and ReturnDate when LoanCardId changes and the book is loaned
                    _loanDate = DateTime.Now;
                    ReturnDate = _loanDate?.AddDays(14);
                }
            }
        }
        public BookLoan? LoanCard { get; set; }

        public ICollection<Author>? Autors { get; set; }
    }
}
      
    


