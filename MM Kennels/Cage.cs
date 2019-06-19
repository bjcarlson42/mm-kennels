using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Cage
    {
        public int CageWeightMin { get; set; }
        public int CageWeightMax { get; set; } 
        public int ID { get; set; }

        public override string ToString()
        {
            return $"{ID} {CageWeightMin} {CageWeightMax}";
        }
    }
}
