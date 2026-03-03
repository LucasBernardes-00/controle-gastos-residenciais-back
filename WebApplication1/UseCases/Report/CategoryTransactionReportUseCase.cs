using Domain.Contracts.Repositories;
using Domain.DTOs;

namespace WebApplication1.UseCases.Report
{
    public class CategoryTransactionReportUseCase
    {
        private readonly InCategoryRepository _repository;

        public CategoryTransactionReportUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryBalance>> Execute()
        {
            return await _repository.ReportWithTransactions();
        }
    }
}
