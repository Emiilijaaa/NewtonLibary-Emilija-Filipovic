﻿using NewtonLibary_Emilija_Filipovic.Model;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NewtonLibary_Emilija_Filipovic.Data
{
    public class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }

       
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=NewtonLibrary;Trusted_Connection=True; Trust Server Certificate =Yes;User Id=Library;Password=Ajilime17;");
        }

    }
   
}

