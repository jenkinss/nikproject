using Microsoft.Build.Framework;

namespace WorkTimeModel
{
    public class NamedPojoBase : PojoBase
    {
        [Required]
        public string Name { get; set; }
    }
}