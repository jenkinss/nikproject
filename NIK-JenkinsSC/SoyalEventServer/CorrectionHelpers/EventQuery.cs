using System;
using System.Linq;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    public class EventQuery
    {
        public static WorkTimeEvent GetLastOnDay<TContext>(EFUnitOfWork<TContext> worker, string direction) where TContext : WorkTimeManagerContext
        {
            return worker.WorkTimeRepository.Get(_ => _.TimeStamp >= DateTime.Today && _.Direction == direction).OrderByDescending(@event => @event.TimeStamp).First();
        }

        public static bool IsFirst<TContext>(Event cevent, EFUnitOfWork<TContext> worker, DirectionType direction) where TContext : WorkTimeManagerContext
        {
            bool result = false;

            var person = worker.PersonRepository.GetByID(cevent.UserAddress);
            int personid = person.ID;

            DateTime s = cevent.EventDate.Date;
            DateTime e = s.AddDays(1);
            string simpleDirection = direction.ToString();
            var events = worker.WorkTimeRepository.Get(_ => _.PersonID == personid && _.TimeStamp > s && _.TimeStamp < e && _.Direction == simpleDirection);

            if (events != null && !events.Any())
                result = true;

            return result;
        }
    }
}
