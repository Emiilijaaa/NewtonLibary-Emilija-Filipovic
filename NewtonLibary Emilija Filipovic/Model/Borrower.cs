using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtonLibary_Emilija_Filipovic.Model
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string LibaryCardNumber { get; set; }
        public int PIN { get; set; }


        public Borrower() 
        {
        }
    }
}
