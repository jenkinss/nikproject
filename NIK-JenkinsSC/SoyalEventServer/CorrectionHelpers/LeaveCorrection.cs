using System;
using System.Linq;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    public class LeaveCorrection
    {
        public static void Correction<TContext>(Event cevent, Site site, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {

            //az első exit event korrekció
            var person = worker.PersonRepository.GetByID(cevent.UserAddress);
            var rand = new Random();
            if (person != null)
            {
                // valós event, ami csak loggolva van
                EventDeepLogger.Log(cevent, site, worker, true, false);
                
                // weboldalon látható event, dátum nélkül. Azt a WorkStyle-nak megfelelően kapja meg.
                var savedEvent = new WorkTimeEvent
                {
                    Direction = cevent.DirectionType.ToString(),
                    EventType = cevent.TransactionType.ToString(),
                    Person = person,
                    PersonID = person.ID,
                    Site = site,
                    SiteID = site.ID,
                };
               

                var firstEnter = GetFirstEnter(cevent, person, worker);
                string style = WorkStyleDetails.GetStyle(cevent, worker);
                switch (style)
                {
                    case "A":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(9);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        break;
                    case "B":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(4);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        break;
                    case "C":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(7);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        break;
                    case "D":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.DayOfWeek.ToString() == "Friday" ? firstEnter.TimeStamp.AddHours(9) : firstEnter.TimeStamp.AddHours(8);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        break;
                    case "F":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(9);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        break;
                    case "AE":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(9);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        // generate lunch
                        LunchGenerator.Generate(cevent, person, site, worker);
                        break;
                    case "CE":
                         savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(7);
                         savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                         savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        // generate lunch
                        LunchGenerator.Generate(cevent, person, site, worker);
                        break;
                    case "DE":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.DayOfWeek.ToString() == "Friday" ? firstEnter.TimeStamp.AddHours(9) : firstEnter.TimeStamp.AddHours(8);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        // generate lunch
                        LunchGenerator.Generate(cevent, person, site, worker);
                        break;
                    case "FE":
                        savedEvent.TimeStamp = firstEnter.TimeStamp.AddHours(9);
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddMinutes(rand.Next(11, 16));
                        savedEvent.TimeStamp = savedEvent.TimeStamp.AddSeconds(rand.Next(1, 30));
                        // generate lunch
                        LunchGenerator.Generate(cevent, person, site, worker);
                        break;

                }
                worker.WorkTimeRepository.InsertEvent(savedEvent, false, true);
                worker.Save();
            }
        }

        public static WorkTimeEvent GetFirstEnter<TContext>(Event cevent, Person person, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            DateTime s = cevent.EventDate.Date;
            DateTime e = cevent.EventDate.AddDays(1);
            string direction = DirectionType.Entry.ToString();

            var personID = person.ID;
            var result =
                worker.WorkTimeRepository.Get(
                    _ => _.PersonID == personID && _.TimeStamp > s && _.TimeStamp < e && _.Direction == direction).Min();
            return result;
        }
    }
}
