using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Cage
    {
        public int CageWeightMin { get; }
        public int CageWeightMax { get; }
        public int ID { get; set; }
        public int Ssd { get; set; } //ssd = scheduled start date

        public Cage(int min, int max, int dscheduled)
        {
            CageWeightMin = min;
            CageWeightMax = max;
            Ssd = dscheduled;
        }

        public override string ToString()
        {
            return $"{CageWeightMin}-{CageWeightMax}";
        }
    }
}
