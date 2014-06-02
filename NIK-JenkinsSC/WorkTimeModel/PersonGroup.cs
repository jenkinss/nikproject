using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeModel
{
    public class PersonGroup: NamedPojoBase
    {
        [Display(Name = "Members")]
        public ICollection<Person> Persons { get; set; }

        public int? LocationID { get; set; }

        [Display(Name = "Location")]
        public virtual  FacilityLocation Location { get; set; }

    }
}
