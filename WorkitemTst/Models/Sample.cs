using System.ComponentModel.DataAnnotations;

namespace WorkitemTst.Models
{
    public class Sample: BaseEntity
    {

        public int identifier { get; set; }
        public string Name { get; set; }
    }
}
