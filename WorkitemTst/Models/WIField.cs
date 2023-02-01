using System.Collections;

namespace WorkitemTst.Models
{
    public class WIField: BaseEntity
    {
        public int Order { get; set; }
        //public IEnumerable<WIValue> Value { get; set; }
        public WIFieldType FieldType { get; set; }

        public string Name { get; set; }

    }
}
