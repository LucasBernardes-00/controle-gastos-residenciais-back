using Domain.Entities;

namespace WebApplication1.DTOs.Response
{
    public class CategoryResponse
    {
        public Guid Id { get; init; }
        public string Description { get; set; }
        public eGoal Goal { get; set; }
    }
}
