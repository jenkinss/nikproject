using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    class EventDeepLogger
    {
        public static void Log<TContext>(Event cevent, Site site, EFUnitOfWork<TContext> worker, bool inserttolog, bool inserttowork) where TContext : WorkTimeManagerContext
        {
            var person = worker.PersonRepository.GetByID(cevent.UserAddress);
            if (person != null)
            {
                var loggedEvent = new WorkTimeEvent
                {
                    Direction = cevent.DirectionType.ToString(),
                    EventType = cevent.TransactionType.ToString(),
                    Person = person,
                    PersonID = person.ID,
                    Site = site,
                    SiteID = site.ID,
                    TimeStamp = cevent.EventDate
                };
                worker.WorkTimeRepository.InsertEvent(loggedEvent, inserttolog, inserttowork);
                worker.Save();
            }
        }
    }
}
