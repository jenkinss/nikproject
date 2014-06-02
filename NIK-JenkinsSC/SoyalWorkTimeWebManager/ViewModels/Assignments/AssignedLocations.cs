using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace SoyalWorkTimeWebManager.ViewModels.Assignments
{
    public class AssignedLocations
    {
        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public bool Assigned { get; set; }
    }
}