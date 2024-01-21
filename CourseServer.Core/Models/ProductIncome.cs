namespace CourseServer.Core.Models
{
    public class ProductIncome
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public double Amount { get; set; }
    }
}
