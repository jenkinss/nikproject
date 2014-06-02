using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoyalWorkTimeWebManager.ViewModels.Assignments
{
    public class AssignedGroups
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public bool Assigned { get; set; }
    }
}