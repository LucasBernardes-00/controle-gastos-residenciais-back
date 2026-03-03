namespace Domain.Entities
{
    public enum eTransactionType
    {
        DESPESA = 1,
        RECEITA = 2
    }

    public class Transaction
    {
        public Guid Id { get; init; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public eTransactionType Type { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}
