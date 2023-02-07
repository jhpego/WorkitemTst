using System.Collections;

namespace WorkitemTst.Entitys
{
    public class WorkitemField : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public WorkitemFieldKind FieldMode { get; set; }
    }
}
