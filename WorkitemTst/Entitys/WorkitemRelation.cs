using System.ComponentModel.DataAnnotations.Schema;
using WorkitemTst.Models;

namespace WorkitemTst.Entitys
{
    public class WorkitemTypeRelation : BaseEntity
    {
        [ForeignKey("SourceWorkitemTypeId")]
        public WorkitemType SourcetWorkitemType { get; set; }

        [ForeignKey("TargetWorkitemTypeId")]
        public WorkitemType TargetWorkitemType { get; set; }

        public int TargetWorkitemTypeId { get; set; }

        public int SourceWorkitemTypeId { get; set; }

        public WorkitemRelationKind RelationMode { get; set; }
    }


    public class WorkitemRelation : BaseEntity
    {
        [ForeignKey("SourceWorkitemId")]
        public Workitem SourceWorkitem { get; set; }

        [ForeignKey("TargetWorkitemId")]
        public Workitem TargetWorkitem { get; set; }

        [ForeignKey("WorkitemTypeRelationId")]
        public WorkitemTypeRelation Relation { get; set; }


        public int TargetWorkitemId { get; set; }

        public int SourceWorkitemId { get; set; }

        public int WorkitemTypeRelationId { get; set; }



    }
}
