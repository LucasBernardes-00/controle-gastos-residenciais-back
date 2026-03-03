using Domain.Entities;

namespace WebApplication1.DTOs.Request
{
    public class CategorySaveRequest
    {
        public string Description { get; set; }
        public eGoal Goal { get; set; }
    }
}
