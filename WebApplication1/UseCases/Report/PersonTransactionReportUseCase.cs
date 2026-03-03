using Domain.Contracts.Repositories;
using Domain.DTOs;

namespace WebApplication1.UseCases.Report
{
    public class PersonTransactionReportUseCase
    {
        private readonly InPersonRepository _repository;

        public PersonTransactionReportUseCase(InPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonBalance>> Execute()
        {
            return await _repository.ReportWithTransactions();
        }
    }
}
