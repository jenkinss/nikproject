using System;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    public class LunchGenerator
    {
        public static void Generate<TContext>(Event cevent, Person person, Site site, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            var currentDate = cevent.EventDate.Date;
            var rtime = new Random();
            //----------------------------------------------------------------------
            var leaveStamp = new DateTime(
                currentDate.Year,
                currentDate.Month,
                currentDate.Day,
                11,
                00,
                00
                );
            var l = leaveStamp.AddMinutes(rtime.Next(1, 10));
            var leave = l.AddSeconds(rtime.Next(1, 59));

            var comeStamp = new DateTime(
                currentDate.Year,
                currentDate.Month,
                currentDate.Day,
                11,
                59,
                00
                );
            var c = comeStamp.AddMinutes(rtime.Next(1, 10));
            var come = c.AddSeconds(rtime.Next(1, 59));

            var leaveStampf = new DateTime(
                currentDate.Year,
                currentDate.Month,
                currentDate.Day,
                20,
                00,
                00
                );
            var lf = leaveStampf.AddMinutes(rtime.Next(1, 20));
            var leavef = lf.AddSeconds(rtime.Next(1, 59));

            var comeStampf = new DateTime(
                currentDate.Year,
                currentDate.Month,
                currentDate.Day,
                20,
                40,
                00
                );
            var cf = comeStampf.AddMinutes(rtime.Next(1, 20));
            var comef = cf.AddSeconds(rtime.Next(1, 59));


            //----------------------------------------------------------------------
            var lunchLeave = new WorkTimeEvent
            {
                Direction = "EXIT",
                EventType = "NORMAL_ACCESS",
                Person = person,
                PersonID = person.ID,
                Site = site,
                SiteID = site.ID,
                EventNote = "->",
                TimeStamp = leave

            };

            var comeBack = new WorkTimeEvent
            {
                Direction = "ENTRY",
                EventType = "NORMAL_ACCESS",
                Person = person,
                PersonID = person.ID,
                Site = site,
                SiteID = site.ID,
                EventNote = "<-",
                TimeStamp = come

            };
            string style = WorkStyleDetails.GetStyle(cevent, worker);
            if (style=="FE")
            {
                lunchLeave.TimeStamp = leavef;
                lunchLeave.TimeStamp = comef;
            }


            worker.WorkTimeRepository.InsertEvent(lunchLeave, true, true);
            worker.WorkTimeRepository.InsertEvent(comeBack, true, true);
            worker.Save();
        }
    }
}
