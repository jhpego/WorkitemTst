namespace WorkitemTst.Models
{
    public class WIForm : BaseEntity
    {
        public IEnumerable<WIField> Fields { get; set; }
    }
}
