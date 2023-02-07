using System.ComponentModel.DataAnnotations;

namespace WorkitemTst.Entitys
{
    public class Sample : BaseEntity
    {

        public int identifier { get; set; }
        public string Name { get; set; }
    }
}
