using System;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer
{
    public class Corrector
    {
        public static void EventCorrection<TContext>(Event cevent, Site site, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            /*
             * Ez az első(azaz ma az eseményhez tartozó személynek nincs még eseménye). Ennek megfelelően korrekció történik 
             * Ez nem az első, akkor az esemény számának megfelelően korrekció.
             * 
             */
            // Jelenleg nincs korrekció
            try
            {
                //var worker = new EFUnitOfWork<TContext>();
                var person = worker.PersonRepository.GetByID(cevent.UserAddress);
                var savedEvent = new WorkTimeEvent
                {
                    Direction = cevent.DirectionType.ToString(),
                    EventType = cevent.TransactionType.ToString(),
                    Person = person,
                    PersonID = person.ID,
                    Site = site,
                    SiteID = site.ID,
                    TimeStamp = cevent.EventDate
                };
                
                worker.WorkTimeRepository.Insert(savedEvent);
                worker.Save();
            }
            catch (Exception e)
            {
                
                throw new Exception("Corrector >> "+e.Message);
            }
            
        }
    }
}
