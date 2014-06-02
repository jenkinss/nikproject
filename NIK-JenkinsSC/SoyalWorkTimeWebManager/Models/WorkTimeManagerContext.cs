using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorkTimeModel;
using WorkTimeModel.APIModels;


namespace SoyalWorkTimeWebManager.Models
{
    public class WorkTimeManagerContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonGroup> PersonGroups { get; set; }
        public DbSet<FacilityLocation> Locations { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<WTCard> Cards { get; set; }
        public DbSet<WorkTimeEvent> WorkTimeEvents { get; set; }
        public DbSet<LoggedEvent> LoggedEvents { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<WTEventType> WtEventTypes { get; set; }
    }
}