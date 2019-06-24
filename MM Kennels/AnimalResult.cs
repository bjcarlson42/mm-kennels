using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    public class AnimalResult
    {
        public AnimalResult(string name, int weight, int startDay, int endDay)
        {
            Name = name;
            Weight = weight;
            StartDay = startDay;
            EndDay = endDay;
        }

        public string Name { get; }
        public int Weight { get; }
        public int StartDay { get; }
        public int EndDay { get; }
    }
}
