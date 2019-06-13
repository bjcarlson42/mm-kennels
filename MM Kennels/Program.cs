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
                foreach(Animal a in animals)
                {
                    Console.WriteLine(a.ToString()); //debugging purposes
                }
                Console.WriteLine();

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
                foreach (Days day in emptyDays)
                {
                        if(day.SpecificCage.Equals(lengthOfStay))
                        {
                            Console.WriteLine("Assigned to cage" + c);
                            break;
                        }
                }
            }
            for (int i = startDay; i < startDay + lengthOfStay; i++)
            {
                days[i].IsEmpty = false;
            }

            if (canBeScheduled)
            {
                animals.Add(new Animal(petName, petWeight, startDay, lengthOfStay));
                for (int i = startDay + 1; i < lengthOfStay + startDay; i++)
                {
                    animals.Add(new Animal(petName, petWeight, startDay, lengthOfStay));
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
            var scheduled = false;
            var animalName = "";
            foreach (Animal a in animals.Distinct())
            {
                foreach (var c in days)
                {
                    for(int i = a.Ssd; i < a.Ssd + a.LengthOfStay; i++)
                    {
                        if (dayNumber == i)
                        {
                            scheduled = true;
                            animalName = a.ToString();
                            c.IsEmpty = false;
                        }
                    }
                }
            }

            if (scheduled)
            {
                Console.WriteLine(animalName);
            }

            Console.WriteLine();

            Console.WriteLine("Cages empty on that day:");
            var emptyCages = (from Days d in days where d.IsEmpty == true select d);
            foreach (var c in emptyCages)
            {
                 Console.WriteLine($"{c.SpecificCage.ToString()}");
            }
        }

        public static void IsScheduled(string name)
        {
            var scheduled = false;
            var animalName = "";
            foreach (Animal a in animals.Distinct())
            {
                if (name == a.Name)
                {
                    scheduled = true;
                    animalName = a.ToString();
                }

            }
            if(scheduled)
            {
                Console.WriteLine(animalName);
            }
            else
            {
                Console.WriteLine($"{name} not scheduled");
            }
        }
    }
}

