namespace WorkitemTst.Models
{
    public class Workitem: BaseEntity
    {


        public IEnumerable<WIValue> Values { get; set; }
        public WIType WIType { get; set; }

        public IEnumerable<WIRelation> Relations { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }
    }
}