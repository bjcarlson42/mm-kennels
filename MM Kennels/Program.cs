using System;
using System.Collections.Generic;
using System.IO;

namespace MM_Kennels
{
    class Program
    {
        static List<Animal> animals = new List<Animal>();
        static List<Cage> cages = new List<Cage>();
        static List<Year> days = new List<Year>();

        static void Main(string[] args)
        {
            for(int day = 1; day <= 365; day++)
            {
                days.Add(new Year(day, false));
            }

            //Console.WriteLine(days[0].day);
            //Console.WriteLine(days[364].day);

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

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(' ');

                    if ((values.Length == 2) &&
                           int.TryParse(values[0], out var minWeight) &&
                           int.TryParse(values[1], out var maxWeight))
                        cages.Add(new Cage(minWeight, maxWeight, 0));
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
            bool isScheduled = false;
            int c;

            for (c = 0; c < cages.Count && isScheduled == false; c++)
            {
                if (petWeight > cages[c].CageWeightMin && petWeight < cages[c].CageWeightMax &&  )
                {
                    animals.Add(new Animal(petName, petWeight, false, startDay, lengthOfStay, cages[c]));
                    for(int i = 0; i < lengthOfStay; i++)
                    {
                        
                    }
                    cages[c].Ssd = startDay;
                }
            }
            if (isScheduled)
            {
                Console.WriteLine($"{petName} is scheduled for {c}");
            }
            else
            {
                Console.WriteLine($"{petName} could not be scheduled on that day");
            }
        }

        public static void Info(int dayNumber)
        {
            Console.WriteLine("Animals scheduled on that day:");
            foreach (Animal a in animals)
            {
                if (dayNumber == a.Ssd)
                {
                    Console.WriteLine($"{a.ToString()}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("Cages empty on that day");
            foreach (Cage c in cages)
            {
                if (dayNumber == c.Ssd)
                {                  
                    Console.WriteLine($"{c.ToString()}");
                }
            }
        }

        public static void IsScheduled(string name)
        {
            foreach (Animal a in animals)
            {
                if (name == a.Name)
                {
                    Console.WriteLine($"{name} scheduled for day {a.Ssd}");
                }
                else
                {
                    Console.WriteLine($"{name} not scheduled");
                }
            }
        }
    }
}

