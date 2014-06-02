using System.ComponentModel.DataAnnotations;

namespace WorkTimeModel
{
    public class PojoBase
    {
        [Key]
        public int ID { get; set; }

        public bool IsDeleted { get; set; }
    }
}