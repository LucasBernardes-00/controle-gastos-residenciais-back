namespace Domain.DTOs
{
    public class CategoryBalance
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
    }
}
