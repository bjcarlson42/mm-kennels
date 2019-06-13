using System.Text;

namespace MM_Kennels
{
    class Days
    {
        public int Day { get; set; }
        public Cage SpecificCage { get; }
        public bool IsEmpty = true;

        public Days(int d, Cage c, bool ie)
        {
            Day = d;
            SpecificCage = c;
            IsEmpty = ie;
        }
    }
}
