using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace MM_Kennels
{
    class Program
    {
        private readonly Scheduler _scheduler;
        private readonly KennelDatabase _database;

        private Program()
        {
            _database = new KennelDatabase();
            _scheduler = new Scheduler(_database);
        }

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

            var program = new Program();

            program.InitializeDatabase(args[0]);
            program.Run();
        }

        private void InitializeDatabase(string cageFile)
        {
            _database.Animals.Clear();
            _database.Cages.Clear();

            using (var reader = new StreamReader(cageFile))
            {
                string line;
                int id = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(' ');

                    if ((values.Length == 2) &&
                        int.TryParse(values[0], out var minWeight) &&
                        int.TryParse(values[1], out var maxWeight))
                        _database.Cages.Add(new Cage(id++, minWeight, maxWeight));
                }
            }
        }

        private void Run()
        {
            string request;

            // Unix EOF
            var exitCommand = "Ctrl+D";

            // Windows EOF
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                exitCommand = "Ctrl+Z";

            Console.WriteLine($"Enter commands, one per line (press {exitCommand} to exit):");
            Console.WriteLine();

            // Read commands until EOF or empty line
            while (!string.IsNullOrEmpty(request = Console.ReadLine()))
            {
                var values = request.Split(' ');

                switch (values[0])
                {
                    case "animal":
                        if (values.Length != 2)
                            goto default;

                        FindAndPrintAnimal(values[1]);
                        break;

                    case "day":
                        if ((values.Length != 2) ||
                            !int.TryParse(values[1], out var day))
                            goto default;

                        PrintDay(day);
                        break;

                    case "schedule":
                        if ((values.Length != 5) ||
                            !int.TryParse(values[2], out var weight) ||
                            !int.TryParse(values[3], out var startDay) ||
                            !int.TryParse(values[4], out var numDays))
                            goto default;

                        ScheduleAnimal(values[1], weight, startDay, numDays);
                        break;

                    default:
                        Console.WriteLine($"Invalid request: {request}");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print the information about an animal if it is in the schedule.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        private void FindAndPrintAnimal(string name)
        {
            var animal = _scheduler.GetAnimal(name);

            if (animal != null)
                PrintAnimal(animal);
            else
                Console.WriteLine($"Error: {name} not in list!");
        }

        /// <summary>
        /// Prints a given animal's name, weight, arrival and departure.
        /// </summary>
        /// <param name="animal">The animal to print.</param>
        private void PrintAnimal(AnimalResult animal)
        {
            Console.WriteLine($"{animal.Name} {animal.Weight} {animal.StartDay} {animal.EndDay}");
        }

        /// <summary>
        /// Print information about all animals and cages on a given day.  If
        /// a cage is empty on the day, give the weight range.  If the cage
        /// is full, give the animal name, weight, arrival and departure.
        /// </summary>
        /// <param name="day">The day to check.</param>
        private void PrintDay(int day)
        {
            var cages = _scheduler.GetDay(day);

            foreach (var cage in cages)
                if (cage.Animal != null)
                    PrintAnimal(cage.Animal);
                else
                    Console.WriteLine($"{cage.MinWeight}-{cage.MaxWeight}");
        }

        /// <summary>
        /// Add an animal to the schedule, if possible.
        /// </summary>
        private void ScheduleAnimal(string name, int weight, int startDay, int numDays)
        {
            var cage = _scheduler.ScheduleAnimal(name, weight, startDay, numDays);

            if (cage != null)
                Console.WriteLine($"{name} is scheduled for cage {cage.Id}");
            else
                Console.WriteLine($"{name} could not be scheduled on that day");
        }
    }
}

