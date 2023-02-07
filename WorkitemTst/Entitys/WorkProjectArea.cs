using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class WorkProjectArea : BaseEntity
    {
        public Area Area { get; set; }

        public WorkProject WorkProject { get; set; }

    }
}
