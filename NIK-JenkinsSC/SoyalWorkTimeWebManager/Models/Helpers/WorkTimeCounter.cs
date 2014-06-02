using System;
using System.Collections.Generic;
using System.Linq;
using SoyalWorkTimeWebManager.DAL;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Models.Helpers
{
    public class WorkTimeCounter
    {
        private const string NORMAL_ACCESS = "NORMAL_ACCESS";
        private const string EXIT = "EXIT"; 
        private const string ENTRY = "ENTRY";

        public static HourOfDay Compute<TContext>(Person person, DateTime date, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            var result = new HourOfDay
            {
                Absence = "",
                Date = date.Day,
                Hour = 0
            };

            DateTime end = date.AddDays(1);

            List<WorkTimeEvent> eventsOnDay = worker.WorkTimeRepository.Get(_ => _.PersonID==person.ID && _.TimeStamp >= date && _.TimeStamp < end).ToList();

            if (eventsOnDay.Any())
            {
                eventsOnDay.OrderBy(_ => _.TimeStamp);

                if (eventsOnDay.Count == 2)
                {
                    var startDay = eventsOnDay.First(_=>_.Direction==ENTRY);
                    var endDay = eventsOnDay.Last(_ => _.Direction == EXIT);
                    if (startDay!=null && endDay!=null && startDay.EventType == NORMAL_ACCESS && endDay.EventType==NORMAL_ACCESS)
                    {
                        TimeSpan h = startDay.TimeStamp - endDay.TimeStamp;
                        result.Hour = Math.Abs(h.Hours) > 0 ? Math.Abs(h.Hours) - 1 : 0;
                    }
                    else
                    {
                        result.Absence += startDay.EventType[0];
                        result.Hour = 0;
                    }
                }
                else if (eventsOnDay.Count == 4)
                {
                    var startDay = eventsOnDay.First(_ => _.Direction == ENTRY);
                    var endDay = eventsOnDay.Last(_ => _.Direction == EXIT);
                    if (startDay != null && endDay != null && startDay.EventType == NORMAL_ACCESS && endDay.EventType == NORMAL_ACCESS)
                    {
                        TimeSpan h = startDay.TimeStamp - endDay.TimeStamp;
                        result.Hour = Math.Abs(h.Hours) > 0 ? Math.Abs(h.Hours) - 1 : 0;
                    }
                    else
                    {
                        result.Absence += startDay.EventType[0];
                        result.Hour = 0;
                    }
                }
            }

            return result;
        }
    }
}