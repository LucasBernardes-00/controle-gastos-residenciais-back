using Domain.Entities;

namespace WebApplication1.DTOs.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; init; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public eTransactionType Type { get; set; }
        public Category Category { get; set; }
        public Person Person { get; set; }
    }
}
