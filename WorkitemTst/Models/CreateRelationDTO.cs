namespace WorkitemTst.Models
{
    public class CreateRelationDTO
    {
        public long SourceWorkitemId { get; set; }

        public long TargetWorkitemId { get; set;}

        public long RelationTypeEnumId { get; set;}


    }
}
