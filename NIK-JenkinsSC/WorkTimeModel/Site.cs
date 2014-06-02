using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeModel.APIModels;

namespace WorkTimeModel
{
    public class Site : NamedPojoBase
    {
        [Required]
        [Display(Name = "Site Description")]
        public string Description { get; set; }

        public int? NodeID { get; set; }    

        [Display(Name = "Node")]
        public virtual Node Node { get; set; }    

    }
}
