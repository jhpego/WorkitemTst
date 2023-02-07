using System.ComponentModel.DataAnnotations;

namespace WorkitemTst.Entitys
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
