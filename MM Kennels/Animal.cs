using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Animal
    {
        public string Name { get; }
        public int Weight { get; }
        public bool InCage { get; }
        public int Ssd { get; } //ssd = scheduled start date
        public int LengthOfStay { get; }
        public Cage Cage { get; }
        public Year Day { get; set; }

        public Animal(string n, int w, bool c, int s, int l, Cage cage, Year day)
        {
            Name = n;
            Weight = w;
            InCage = c;
            Ssd = s;
            LengthOfStay = l;
            Cage = cage;
            Day = day;
        }

        public override string ToString()
        {
            return $"{Name} {Weight} {Ssd} {Ssd + LengthOfStay}";
        }
    }
}
