namespace CourseServer.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductPricingFeature? PricingFeature { get; set; }
        public ProductTypeFeature? TypeFeature { get; set; }
    }
}
