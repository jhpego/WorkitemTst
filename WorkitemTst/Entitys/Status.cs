using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow Workflow { get; set; }

        public int WorkflowId { get; set; }

    }
}