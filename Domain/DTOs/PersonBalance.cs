namespace Domain.DTOs
{
    public class PersonBalance
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
    }
}
