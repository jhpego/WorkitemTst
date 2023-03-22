using System.ComponentModel.DataAnnotations.Schema;

namespace WorkitemTst.Entitys
{
    public class SimpleWit : BaseEntity
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string Hash { get; set; }

        public override bool Equals(object? obj)
        {
            return this.Name == ((SimpleWit)obj).Name;
            //return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
            //return base.GetHashCode();
        }
    }
}
