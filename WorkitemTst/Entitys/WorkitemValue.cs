using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace WorkitemTst.Entitys
{
    public class WorkitemValue : BaseEntity
    {
        public string Value { get; set; }

        public WorkitemField Field { get; set; }



    }
}
