using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Worklog : BaseEntity
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("WorkitemId")]
        public Workitem Workitem { get; set; }

        public int WorkitemId { get; set; }

        public int? UserId { get; set; }
    }
}

