using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeModel
{
    public class WorkTimeEvent : PojoBase
    {
        [Display(Name = "Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public int SiteID { get; set; }

        [Display(Name = "Site")]
        public virtual Site Site { get; set; }

        public int PersonID { get; set; }

        [Display(Name = "Person")]
        public virtual Person Person { get; set; }

        [Display(Name = "Direction")]
        public string Direction { get; set; }

        [Display(Name = "Type")]
        public string EventType { get; set; }
        
        [Display(Name = "Note")]
        public string EventNote { get; set; }
    }
}
