using System.ComponentModel.DataAnnotations;

namespace WorkitemTst.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
