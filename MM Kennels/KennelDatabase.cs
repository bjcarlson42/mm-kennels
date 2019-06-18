using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class KennelDatabase
    {
        public ICollection<Animal> Animals { get; } = new List<Animal>();
        public ICollection<Cage> Cages { get; } = new List<Cage>();
    }
}
