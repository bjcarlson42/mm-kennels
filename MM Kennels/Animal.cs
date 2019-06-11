using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Animal
    {
        public string name { get; }
        public int weight { get; }
        public bool inCage { get; }
        public int startDate { get; }
        public int lengthOfStay { get; }

        public Animal(string n, int w, bool c, int s, int l)
        {
            name = n;
            weight = w;
            inCage = c;
            startDate = s;
            lengthOfStay = l;
        }

        public override string ToString()
        {
            return $"Name: {name}\nWeight: {weight}\nIn a cage: {inCage}";
        }
    }
}
