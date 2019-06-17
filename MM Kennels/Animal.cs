using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Animal
    {
        public string Name { get; }
        public int Weight { get; } 
        public int StartDate { get; }
        public int LengthOfStay { get; }
        
        public Cage Cage { get; set; }

        public Animal(string n, int w, int s, int l, Cage c)
        {
            Name = n;
            Weight = w;
            StartDate = s;
            LengthOfStay = l;
            Cage = c;
        }

        public override string ToString()
        {
            return $"{Name} {Weight} {StartDate} {StartDate + LengthOfStay}";
        }
    }
}
