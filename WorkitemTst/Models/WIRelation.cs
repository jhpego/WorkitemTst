using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Models
{
    public class WITypeRelation : BaseEntity
    {
        public WIType SourcetWIType { get; set; }

        public WIType TargetWIType { get; set; }

        public int TargetWITypeId { get; set; }

        public int WITypeId { get; set; }

        public WIRelationTypeEnum Relation { get; set; } 
    }


    public class WIRelation : BaseEntity
    {
        [ForeignKey("WorkitemId")]
        public Workitem SourceWorkitem { get; set; }

        [ForeignKey("TargetWorkitemId")]
        public Workitem TargetWorkitem { get; set; }
        public int TargetWorkitemId { get; set; }

        public int WorkitemId { get; set; }

        public WIRelationTypeEnum Relation { get; set; }
        //[ForeignKey("RelationTypeId")]
        //public WITypeRelation RelationType { get; set; }

        //public int RelationTypeId { get; set; }


    }
}
