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
        public Cage(int min, int max, int id)
        {
            CageWeightMin = min;
            CageWeightMax = max;
            ID = id;
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
