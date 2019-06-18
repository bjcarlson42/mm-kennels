using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Cage
    {
        public int CageWeightMin { get; }
        public int CageWeightMax { get; } 
        public int ID { get; }
        public Cage(int id, int min, int max)
        {
            ID = id;
            CageWeightMin = min;
            CageWeightMax = max;        
        }

        public override string ToString()
        {
            return $"{ID} {CageWeightMin} {CageWeightMax}";
        }
    }
}
