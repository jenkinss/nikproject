using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeModel.APIModels
{
    public class Node : NamedPojoBase
    {
        [Required]
        [Display(Name="Node Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "IP Address")]
        public string IP { get; set; }

        [Required]
        [Display(Name = "Port")]
        public Int32 Port { get; set; }

        [Required]
        [Display(Name = "Node Number")]
        public string Number { get; set; }

    }
}
