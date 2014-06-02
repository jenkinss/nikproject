using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkTimeModel
{
    public class Person:NamedPojoBase
    {
        //[Display(Name = "Name")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resources),
        //          ErrorMessageResourceName = "FirstNameRequired")]
        [Required]
        [Display(Name = "Name ukrain")] //, ResourceType = typeof()
        public string NameUa { get; set; }

        [Required]
        [Display(Name = "Cardboard Number")]
        public int CardBoardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth")]
        public DateTime Birth { get; set; }

        public int? CardID { get; set; }

        [Display(Name = "Card")]
        public virtual WTCard Card { get; set; }
        
        [Required]
        [Display(Name = "Professional Class")]
        public string ProfessionalClass { get; set; }

        //beosztás
        [Required]
        [Display(Name = "Position")]
        public string Post { get; set; }

        [Display(Name = "Sub-Unit")]
        public string SubUnit { get; set; }

        
        [Display(Name = "Group")]
        public virtual ICollection<PersonGroup> PersonGroups { get; set; }

        public string WorkStyle { get; set; }

        //telephely
        public virtual ICollection<FacilityLocation> Location { get; set; }

        public bool IsYoung()
        {
            bool result = (DateTime.Today.Year - Birth.Year) < 18;
            return result;
        }


    }
}
