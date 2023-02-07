using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class WorkProjectWorkitemType : BaseEntity
    {
        public WorkitemType WorkitemType { get; set; }

        public WorkProject WorkProject { get; set; }

    }
}
