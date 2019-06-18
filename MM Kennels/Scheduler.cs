using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace MM_Kennels
{
    class Scheduler
    {
        private readonly KennelDatabase _database;

        public Scheduler(KennelDatabase database)
        {
            _database = database;
        }

        public AnimalResult GetAnimal(string name)
        {
            AnimalResult animal = null;
            foreach (Animal a in _database.Animals)
            {
                if (name == a.Name)
                {
                    animal = new AnimalResult(name, a.Weight, a.StartDate, a.LengthOfStay);
                }               
            }
            return animal;
        }

        public IEnumerable<CageResult> GetDay(int day)
        {
            List<CageResult> cageResult = new List<CageResult>();
            AnimalResult animal = null;
            List<Cage> OccupiedCages = new List<Cage>();

            //animals scheduled
            foreach (Animal a in _database.Animals)
            {
                if (a.StartDate <= day && a.StartDate + a.LengthOfStay > day)
                {
                    animal = new AnimalResult(a.Name, a.Weight, a.StartDate, a.LengthOfStay);
                    cageResult.Add(new CageResult(a.Cage.ID, a.Cage.CageWeightMin, a.Cage.CageWeightMax, animal));
                    OccupiedCages.Add(a.Cage);
                }
            }

            foreach(var c in _database.Cages)
            {
                if (!OccupiedCages.Contains(c))
                {
                    cageResult.Add(new CageResult(c.ID, c.CageWeightMin, c.CageWeightMax, null));                                 
                } 
            }

            return cageResult;
        }

        public CageResult ScheduleAnimal(string name, int weight, int startDay, int numDays)
        {
            var occupiedCages = from a in _database.Animals
                                where startDay < a.StartDate + a.LengthOfStay && startDay + numDays > a.StartDate
                                select a.Cage;
            var query = from cage in _database.Cages
                        where weight > cage.CageWeightMin && weight < cage.CageWeightMax
                        where !occupiedCages.Contains(cage)
                        select cage;
            var c = query.FirstOrDefault();

            CageResult cageResult = null;
            AnimalResult animal = null;

            if (c != null)
            {
                _database.Animals.Add(new Animal(name, weight, startDay, numDays) { Cage = c});

                animal = new AnimalResult(name, weight, startDay, numDays);
                cageResult = new CageResult(c.ID, c.CageWeightMin, c.CageWeightMax, animal); 
            }
            return cageResult;
        }
    }
}
