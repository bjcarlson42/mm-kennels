using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class CageResult
    {
        public CageResult(int id, int minWeight, int maxWeight, AnimalResult animal)
        {
            Id = id;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            Animal = animal;
        }

        public int Id { get; }
        public int MinWeight { get; }
        public int MaxWeight { get; }
        public AnimalResult Animal { get; }
    }
}
