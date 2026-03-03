namespace Domain.Entities
{
    public class Person
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
