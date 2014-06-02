using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeModel.APIModels;

namespace WorkTimeModel
{
    public class WTCard : Card
    {
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
