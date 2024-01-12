namespace CourseServer.Core.Models
{
    public class Income
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public List<ProductIncome> IncomeProducts { get; set; } = new List<ProductIncome>();
        public decimal Summ { get; set; }
    }
}
