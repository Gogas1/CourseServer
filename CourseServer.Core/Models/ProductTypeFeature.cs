namespace CourseServer.Core.Models
{
    public class ProductTypeFeature
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string TypeFeature { get; set; }
    }
}
