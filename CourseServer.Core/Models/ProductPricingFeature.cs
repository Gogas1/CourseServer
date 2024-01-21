namespace CourseServer.Core.Models
{
    public class ProductPricingFeature
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
    }
}
