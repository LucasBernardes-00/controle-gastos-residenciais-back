using Domain.Entities;

namespace WebApplication1.DTOs.Request
{
    public class TransactionUpdateRequest
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public eTransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PersonId { get; set; }
    }
}
