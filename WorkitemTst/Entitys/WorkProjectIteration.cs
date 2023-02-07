using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class WorkProjectIteration : BaseEntity
    {
        public Iteration Iteration { get; set; }

        public WorkProject WorkProject { get; set; }

    }
}
