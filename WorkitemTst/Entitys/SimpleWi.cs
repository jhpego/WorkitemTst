using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class SimpleWi : BaseEntity
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string Hash { get; set; }

    }
}
