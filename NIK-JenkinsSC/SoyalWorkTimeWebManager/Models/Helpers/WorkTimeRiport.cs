using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SoyalWorkTimeWebManager.DAL;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Models.Helpers
{
    public class HourOfDay
    {
        public int Date { get; set; }

        public int Hour { get; set; }

        public string Absence { get; set; }
    }

    public class WorkTimeRiport
    {
        public WorkTimeRiport(Person person, List<HourOfDay> hourOfDays)
        {
            HourOfDays = new List<HourOfDay>();
            Name = person.Name;
            NameUa = person.NameUa;
            Post = person.Post;
            CardBoardNumber = person.CardBoardNumber;
            HourOfDays = hourOfDays;
        }

        public string Name { get; set; }

        public string NameUa { get; set; }

        public string Post { get; set; }

        public int CardBoardNumber { get; set; }

        public List<HourOfDay> HourOfDays { get; set; }

        public int WorkDays
        {
            get
            {
                var result = 0;
                if (HourOfDays != null)
                {
                    result = HourOfDays.Count(_ => _.Hour > 0 && _.Absence=="");
                }
                return result;
            }
        }

        public int FreeDays
        {
            get
            {
                var result = 0;
                if (HourOfDays != null)
                {
                    result = HourOfDays.Count(_ => _.Hour == 0 && _.Absence == "");
                }
                return result;
            }
        }

        public int WorkHours
        {
            get
            {
                var result = 0;
                if (HourOfDays != null)
                {
                    result += HourOfDays.Sum(day => day.Hour);
                }
                return result;
            }
        }

        public int DaysInMonth
        {
            get
            {
                var result = 0;
                if (HourOfDays != null)
                {
                    result = HourOfDays.Count;
                }
                return result;
            }
        }
        
    }
}