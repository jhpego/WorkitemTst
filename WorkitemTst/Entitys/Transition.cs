using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Transition : BaseEntity
    {
        public string Name { get; set; }

        //[ForeignKey("WorkflowId")]
        //public Workflow Workflow { get; set; }

        [ForeignKey("InitialStatusId")]
        public Status? InitialStatus { get; set; }

        public int? InitialStatusId { get; set; }

        [ForeignKey("NextStatusId")]
        public Status NextStatus { get; set; }

        public int NextStatusId { get; set; }

    }
}