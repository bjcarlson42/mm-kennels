using System;
using System.Collections.Generic;
using System.IO;

namespace MM_Kennels
{
    class Program
    {
        static List<Animal> animals = new List<Animal>();
        static List<Cage> cages = new List<Cage>();

        static void Main(string[] args)
        {
            while (true)
            {
                /*
                 * Reading the cages in from a textfile and storing them in order in the cage class
                 */
                string line;
                StreamReader file = new StreamReader(args[0]);
                while ((line = file.ReadLine()) != null)
                {
                    string[] cage = line.Split(new char[] { ' ' });
                    string min = cage[0];
                    string max = cage[1];
                    cages.Add(new Cage(int.Parse(min), int.Parse(max), 0, false));
                }
                file.Close();

                if (args[0].ToLower() == "schedule")
                {
                    continue;
                }
                else if (args[0].ToLower() == "day")
                {
                    continue;
                }
                else if (args[0].ToLower() == "animal")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Please enter a valid command.");
                    Console.ReadKey();
                }
                Console.WriteLine("Program finished...");
            }
        }

        public static void Schedule(string petName, int petWeight, int startDay, int lengthOfStay)
        {
            animals.Add(new Animal(petName, petWeight, false, startDay, lengthOfStay));
        }

        public static void Info(int dayNumber)
        {
            foreach(Animal a in animals)
            {
                if (dayNumber == a.startDate)
                {
                    Console.WriteLine($"{a.ToString()}");
                }
            }

            foreach(Cage c in cages)
            {
                if(c.isOccupied == false)
                {
                    Console.WriteLine($"{c.ToString()}");
                }
            }
        }

        public static void IsScheduled(string name)
        {
            foreach (Animal a in animals)
            {
                if (name == a.name)
                {
                    Console.WriteLine($"Day scheduled: {a.startDate}");
                }
                else
                {
                    Console.WriteLine("Not Scheduled");
                }
            }      
        }
    }
}

