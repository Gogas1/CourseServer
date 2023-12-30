namespace CourseServer.Core.Models
{
    public class ProductIncome
    {
        public Product Product { get; set; } = new Product();
        public decimal Price { get; set; }
        public double Amount { get; set; } 
    }
}
