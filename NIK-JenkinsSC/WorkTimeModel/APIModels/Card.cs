using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeModel.APIModels
{
    public class Card : PojoBase
    {
        [Required]
        [Display(Name = "Card code")]
        public int Code { get; set; }

        [Display(Name = "Anti Passback")]
        public bool AntiPassBack { get; set; }

        [Display(Name = "User address")]
        public int UserAddress { get; set; }

        [Display(Name = "Site Code")]
        public int SiteCode { get; set; }

        [Display(Name = "Pin")]
        public int PinCode { get; set; }

        [Display(Name = "Acess mode")]
        public int Mode { get; set; }

        [Display(Name = "Time Zone")]
        public int TimeZone { get; set; }
        
    }
}
