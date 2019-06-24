using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MM_Kennels.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly Scheduler _scheduler;

        public IActionResult Index()
        {
            return View();
        }

        public ScheduleController(Scheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public IActionResult Animal(string name)
        {          
            var animal = _scheduler.GetAnimal(name);
            
            if(name == null)
            {
                return View(animal);
            }
            else
            {
                return View("AnimalNotFound");
            }    
        }

    public IActionResult Day(int day)
        {
            var cages = _scheduler.GetDay(day);

            return View(cages);
        }

        public IActionResult Schedule(string name, int weight, int startDay, int numDays)
        {
            var cage = _scheduler.ScheduleAnimal(name, weight, startDay, numDays);

            return View(cage);
        }
    }
}