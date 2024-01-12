namespace CourseServer.Core.Models
{
    public class Outgoing
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Manager { get; set; } = string.Empty;
        public List<ProductOutgoing> OutgoingProducts { get; set; } = new();
    }
}
