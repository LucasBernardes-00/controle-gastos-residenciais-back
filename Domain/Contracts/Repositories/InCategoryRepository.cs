using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface InCategoryRepository
    {
        Task Save(Category person);
        Task<List<Category>> GetAll();
        Task Update(Category person);
        Task<Category> GetById(Guid id);
        Task Delete(Guid id);
        Task<List<CategoryBalance>> ReportWithTransactions();
    }
}
