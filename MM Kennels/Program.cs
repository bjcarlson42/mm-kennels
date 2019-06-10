using System;
using System.Collections.Generic;
using System.IO;

namespace MM_Kennels
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cage> cages = new List<Cage>();
            List<Animal> animals = new List<Animal>();

            //StreamReader sr = new StreamReader(args[1]);
            //string text = sr.ReadLine();
            //foreach (char t in text)
            //{
            //    int min = int.Parse(args[1]);
            //    int max = int.Parse(args[2]);
            //    int ds = 0; //ds = day scheduled

            //    cages.Add(new Cage(min, max, ds, false));
            //}

            Console.WriteLine("Loading cages...");

            foreach (Cage c in cages)
            {
                Console.WriteLine(c);
            }

            bool run = true;
            while (run)
            {
                if (args[0].ToLower() == "schedule")
                {
                    //Schedule(args[1], args[2], args[3], args[4]);
                }
                else if (args[0].ToLower() == "day")
                {

                }
                else if (args[0].ToLower() == "animal")
                {
                    //IsScheduled(args[0], args);
                    Console.WriteLine(IsScheduled(args[1], args));

                }
                else
                {
                    Console.WriteLine("Please enter a valid command.");
                    Console.ReadKey();
                }

                Console.WriteLine("Do you want to enter another command? Enter \"y\" or \"n\" and press enter");
                Console.ReadLine();
                if(Console.ReadLine() == "y")
                {
                    run = true;
                    Console.WriteLine("Enter Command...");
                }
                else
                {
                    run = false;
                }
            }
        }

        public static void Schedule(string petName, int petWeight, int startDay, int lengthOfStay)
        {

        }

        public static string Info(int dayNumber)
        {
            string info = "";

            return info;
        }

        public static bool IsScheduled(string name, string[] args)
        {
            name = args[1];

            Animal animal = new Animal(args[1], 100, false);

            return animal.inCage;
        }
    }
}

