using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MM_Kennels
{
    public class Animal
    {
        [Key]
        public string Name { get; set; }
        public int Weight { get; set; }
        public int StartDate { get; set; }
        public int LengthOfStay { get; set; }
        
        public Cage Cage { get; set; }

        public override string ToString()
        {
            return $"{Name} {Weight} {StartDate} {StartDate + LengthOfStay}";
        }
    }
}
