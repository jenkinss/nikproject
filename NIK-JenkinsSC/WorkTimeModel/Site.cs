using System.ComponentModel.DataAnnotations;
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
