using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class KennelDatabase : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Cage> Cages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MMKennels;Integrated Security=True");
        }
    }
}