using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Workitem : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public int? StatusId { get; set; }

        public IEnumerable<WorkitemValue> Values { get; set; }

        public WorkitemType WorkitemType { get; set; }

        [ForeignKey("InteractionId")]
        public Iteration? Interaction { get; set; }

        public int? IterationId { get; set; }


        [ForeignKey("AreaId")]
        public Area? Area { get; set; }

        public int? AreaId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedMoment { get; set; }

        public virtual Effort Effort { get; set; }
    }
}