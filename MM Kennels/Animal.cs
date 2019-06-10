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

        public Animal(string n, int w, bool c)
        {
            name = n;
            weight = w;
            inCage = c;
        }
    }
}
