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
        public bool IsOccupied { get; set; }
        public Cage(int min, int max, int id, bool io)
        {
            CageWeightMin = min;
            CageWeightMax = max;
            ID = id;
            IsOccupied = io;
        }

        public override string ToString()
        {
            return $"{ID} {CageWeightMin} {CageWeightMax}";
        }
    }
}
