namespace WorkitemTst.Models
{
    public class WIType : BaseEntity
    {
        public string Name { get; set; }
        public string InternalCode { get; set; }

        //public WIForm Form { get; set; }

        public List<WIField> Fields { get; set; }

        public IEnumerable<WITypeRelation> Relations { get; set; }
    }

}
