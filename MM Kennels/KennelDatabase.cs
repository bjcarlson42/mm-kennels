using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    public class KennelDatabase : DbContext
    {
        public KennelDatabase(DbContextOptions<KennelDatabase> options)
            : base(options)
        {
            
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Cage> Cages { get; set; }
    }
}