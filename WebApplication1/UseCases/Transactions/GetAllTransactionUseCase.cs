using Domain.Contracts.Repositories;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Transactions
{
    public class GetAllTransactionUseCase
    {
        private readonly InTransactionRepository _repository;

        public GetAllTransactionUseCase(InTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactionResponse>> Execute()
        {
            var transaction = await _repository.GetAll();
            return transaction.Select(t => new TransactionResponse { Id = t.Id, Description = t.Description, Amount = t.Amount, Type = t.Type, Category = t.Category, Person = t.Person }).ToList();
        }
    }
}
