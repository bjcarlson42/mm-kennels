using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Year
    {
        public List<Year> Days { get; set; }
        public bool Occupied { get; set; }

        public Year(Year d, bool o)
        {
            Days = d;
            Occupied = o;
        }
    }
}
