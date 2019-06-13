using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Animal
    {
        public string Name { get; }
        public int Weight { get; } 
        public int Ssd { get; } //ssd = scheduled start date
        public int LengthOfStay { get; }
        //public Cage Cage { get; set; }

        public Animal(string n, int w, int s, int l)
        {
            Name = n;
            Weight = w;
            Ssd = s;
            LengthOfStay = l;
        }

        public override string ToString()
        {
            return $"{Name} {Weight} {Ssd} {Ssd + LengthOfStay}";
        }
    }
}
