using System.ComponentModel.DataAnnotations;
using WorkTimeModel.APIModels;

namespace WorkTimeModel
{
    public class WTCard : Card
    {
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
