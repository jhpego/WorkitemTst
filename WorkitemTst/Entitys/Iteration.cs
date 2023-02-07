using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Iteration : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Active { get; set; }

        [ForeignKey("ParentId")]
        public Iteration Parent { get; set; }

        public int? ParentId { get; set; }


    }
}
