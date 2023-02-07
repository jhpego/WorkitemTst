using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class WorkitemType : BaseEntity
    {
        public string Name { get; set; }

        public List<WorkitemField> Fields { get; set; }

        //public IEnumerable<WorkitemTypeRelation> Relations { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow Workflow { get; set; }

        public int WorkflowId { get; set; }


    }

}
