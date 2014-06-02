using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeModel
{
    public class FacilityLocation: NamedPojoBase
    {
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        
        [Display(Name = "Sites")]
        public virtual ICollection<Site> Sites { get; set; }

        [Display(Name = "People")]
        public virtual ICollection<Person> People { get; set; }

        [Display(Name = "Groups")]
        public virtual ICollection<PersonGroup> PeopleGroups { get; set; }
    }
}
