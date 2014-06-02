using System;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    public class ComeCorrection
    {
        
       public static void Correction<TContext>(Event cevent, Site site, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
           var randomTime = new Random();
           var lasted = EventQuery.GetLastOnDay(worker, "Entry");
           DateTime timeStamp;
           if (lasted == null) { 
            timeStamp = new DateTime(
                cevent.EventDate.Year,
                cevent.EventDate.Month,
                cevent.EventDate.Day,
                6, randomTime.Next(50, 55), 2 );
           }
           else
           {
               timeStamp = lasted.TimeStamp.AddSeconds(randomTime.Next(0, 59));
           }
           string style = WorkStyleDetails.GetStyle(cevent, worker); 
           var person = worker.PersonRepository.GetByID(cevent.UserAddress);
           EventDeepLogger.Log(cevent, site, worker, true, false);
            if (person != null)
            {
                
                var savedEvent = new WorkTimeEvent
                {
                    Direction = cevent.DirectionType.ToString(),
                    EventType = cevent.TransactionType.ToString(),
                    Person = person,
                    PersonID = person.ID,
                    Site = site,
                    SiteID = site.ID,
                    TimeStamp = timeStamp
                };

                if (style == "F" || style == "FE")
                {
                    var secondf = new Random();
                    var minutef = new Random();
                    var timeStampf = new DateTime(
                        cevent.EventDate.Year,
                        cevent.EventDate.Month,
                        cevent.EventDate.Day,
                        15,
                        minutef.Next(45, 59),
                        secondf.Next(1, 59)
                        );
                    savedEvent.TimeStamp = timeStampf;

                }


                worker.WorkTimeRepository.InsertEvent(savedEvent, false, true);
                worker.Save();
            }
        }

    }
}
