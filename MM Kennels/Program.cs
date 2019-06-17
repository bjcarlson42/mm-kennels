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
                        cages.Add(new Cage(minWeight, maxWeight, i++, false));
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
            var occupiedCages = from a in animals
                                where startDay < a.StartDate + a.LengthOfStay && startDay + lengthOfStay > a.StartDate
                                select a.Cage;
            var query = from cage in cages
                        where petWeight > cage.CageWeightMin && petWeight < cage.CageWeightMax
                        where !occupiedCages.Contains(cage)
                        select cage;
            var c = query.FirstOrDefault();

            if (c != null)
            {
                animals.Add(new Animal(petName, petWeight, startDay, lengthOfStay, c));
                Console.WriteLine($"{petName} is scheduled for cage {c}");
            }
            else
            {
                Console.WriteLine($"{petName} can not be scheduled for that day.");
            }
        }


        public static void Info(int dayNumber)
        {
            List<Cage> OccupiedCages = new List<Cage>();
            Console.WriteLine("Animals scheduled on that day:");
            foreach (Animal a in animals)
            {
                if (a.StartDate <= dayNumber && a.StartDate + a.LengthOfStay > dayNumber)
                {
                    Console.WriteLine(a);
                    OccupiedCages.Add(a.Cage);
                }
            }

            Console.WriteLine();

            Console.WriteLine("Cages empty on that day:");
            foreach (var c in cages)
            {
                if (!OccupiedCages.Contains(c))
                {
                    Console.WriteLine(c);
                }
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
            if (scheduled)
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

