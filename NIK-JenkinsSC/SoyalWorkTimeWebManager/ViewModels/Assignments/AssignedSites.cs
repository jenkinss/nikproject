using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoyalWorkTimeWebManager.ViewModels
{
    public class AssignedSites
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public bool Assigned { get; set; }
    }
}