using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }

        public Area ParentAreaId { get; set; }

    }
}
