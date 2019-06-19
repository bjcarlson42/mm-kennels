﻿using System;
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
            AnimalResult result = null;

            //var animal = _database.Animals.Find(name);

            //var animal = _database.Animals.FirstOrDefault(a => a.Name == name);

            //var animal = _database.Animals.Where(a => a.Name == name).FirstOrDefault();

            var query = from a in _database.Animals
                        where a.Name == name
                        select a;
            var animal = query.FirstOrDefault();

            if (animal != null)
                result = new AnimalResult(animal.Name, animal.Weight, animal.StartDate, animal.StartDate + animal.LengthOfStay - 1);

            return result;
        }

        public IEnumerable<CageResult> GetDay(int day)
        {
            List<CageResult> cageResult = new List<CageResult>();
            AnimalResult animal = null;
            List<Cage> OccupiedCages = new List<Cage>();

            //animals scheduled
            var a = _database.Animals.Find(animal.StartDay <= day && animal.StartDay + animal.EndDay > day); 
 
                if (a != null)
                {
                    animal = new AnimalResult(animal.Name, animal.Weight, animal.StartDay, animal.EndDay);
                    cageResult.Add(new CageResult(a.Cage.ID, a.Cage.CageWeightMin, a.Cage.CageWeightMax, animal));
                    OccupiedCages.Add(a.Cage);
                }  

            foreach (var c in _database.Cages)
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
                _database.Animals.Add(new Animal { Cage = c, Name = name, Weight = weight, StartDate = startDay, LengthOfStay = numDays});
                _database.SaveChanges();

                animal = new AnimalResult(name, weight, startDay, numDays);
                cageResult = new CageResult(c.ID, c.CageWeightMin, c.CageWeightMax, animal); 
            }
            return cageResult;
        }
    }
}
