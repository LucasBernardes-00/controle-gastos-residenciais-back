namespace Domain.Entities
{
    public enum eGoal
    {
        DESPESA = 1,
        RECEITA = 2,
        AMBAS = 3
    }

    public class Category
    {
        public Guid Id { get; init; }
        public string Description { get; set; }
        public eGoal Goal { get; set; }
    }
}
