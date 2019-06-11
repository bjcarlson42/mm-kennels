using System;
using System.Collections.Generic;
using System.Text;

namespace MM_Kennels
{
    class Cage
    {
        public int cageWeightMin { get; }
        public int cageWeightMax { get; }
        public int dayScheduled { get; }
        public bool isOccupied { get; }

        public Cage(int min, int max, int dscheduled, bool occupied)
        {
            cageWeightMin = min;
            cageWeightMax = max;
            dayScheduled = dscheduled;
            isOccupied = occupied;
        }

        public override string ToString()
        {
            return $"Min: {cageWeightMin}\nMax: {cageWeightMax}\nDay Scheduled: {dayScheduled}, \nOccupied: {isOccupied}";
        }
    }
}
