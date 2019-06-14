using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MM_Kennels
{
    class Program
    {
        static List<Animal> animals = new List<Animal>();
        static List<Cage> cages = new List<Cage>();
        static List<Days> days = new List<Days>();

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: mmkennels cagefile");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File not found: {args[0]}");
                return;
            }

            string request;

            using (var reader = new StreamReader(args[0]))
            {
                string line;
                int i = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(' ');

                    if ((values.Length == 2) &&
                           int.TryParse(values[0], out var minWeight) &&
                           int.TryParse(values[1], out var maxWeight))
                        cages.Add(new Cage(minWeight, maxWeight, i++));
                }
            }

            for (int day = 1; day <= 365; day++)
            {
                foreach (Cage cage in cages)
                {
                    days.Add(new Days(day, cage, true));
                }
            }

            Console.WriteLine("Enter commands, one per line (press Ctrl+Z to exit):");
            Console.WriteLine();

            while ((request = Console.ReadLine()) != null)
            {
                var values = request.Split(' ');

                switch (values[0])
                {
                    case "animal":
                        if (values.Length != 2)
                            goto default;

                        IsScheduled(values[1]);
                        break;

                    case "day":
                        if ((values.Length != 2) ||
                               !int.TryParse(values[1], out var day))
                            goto default;

                        Info(day);
                        break;

                    case "schedule":
                        if ((values.Length != 5) ||
                               !int.TryParse(values[2], out var weight) ||
                               !int.TryParse(values[3], out var startDay) ||
                               !int.TryParse(values[4], out var numDays))
                            goto default;

                        Schedule(values[1], weight, startDay, numDays);
                        break;

                    default:
                        Console.WriteLine($"Invalid request: {request}");
                        break;
                }
                Console.WriteLine();
            }
        }

        public static void Schedule(string petName, int petWeight, int startDay, int lengthOfStay)
        {
            var canBeScheduled = false;
            var c = cages[0];
            var emptyDays = (from Days day in days
                              where day.IsEmpty
                              where petWeight > day.SpecificCage.CageWeightMin && petWeight < day.SpecificCage.CageWeightMax
                              where day.Day >= startDay && day.Day < startDay + lengthOfStay
                              select day);

            foreach (Cage cage in cages)
            {
                int counter = 0;
                foreach (Days day in emptyDays)
                {
                    if(cage == day.SpecificCage)
                    {
                        counter++;
                    }                       
                }
                if(counter == lengthOfStay)
                {
                    canBeScheduled = true;
                    c = cage;
                    break;
                }
            }

            if (canBeScheduled)
            {
                animals.Add(new Animal(petName, petWeight, startDay, lengthOfStay));
                for (int i = startDay; i < lengthOfStay + startDay; i++)
                {
                    foreach (Days day in emptyDays)
                    {
                        if (day.Day == i && c == day.SpecificCage)
                        {
                            day.IsEmpty = false;   
                        }
                    }
                }
                Console.WriteLine($"{petName} is scheduled for cage {c}");
            }
            else
            {
                Console.WriteLine($"{petName} can not be scheduled for that day.");
            }
        }


        public static void Info(int dayNumber)
        {
            Console.WriteLine("Animals scheduled on that day:");
            foreach (Animal a in animals)
            {
                if(a.Ssd <= dayNumber && a.Ssd + a.LengthOfStay > dayNumber)
                {
                    Console.WriteLine(a);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Cages empty on that day:");
            var emptyCages = (from Days d in days where d.Day == dayNumber && d.IsEmpty select d);
            foreach (var c in emptyCages)
            {
                 Console.WriteLine($"{c.SpecificCage.ToString()}");
            }
        }

        public static void IsScheduled(string name)
        {
            var scheduled = false;
            var animalInfo = "";
            foreach (Animal a in animals)
            {
                if (name == a.Name)
                {
                    scheduled = true;
                    animalInfo = a.ToString();
                }
            }
            if(scheduled)
            {
                Console.WriteLine(animalInfo);
            }
            else
            {
                Console.WriteLine($"{name} not scheduled");
            }
        }
    }
}

