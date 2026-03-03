using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Transactions
{
    public class GetTransactionUseCase
    {
        private readonly InTransactionRepository _repository;

        public GetTransactionUseCase(InTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransactionResponse> Execute(Guid id)
        {
            var validation = new ValidationResult();
            var transaction = await _repository.GetById(id);

            if (transaction == null)
            {
                validation.AddError("A transação informada não existe.");
                throw new InputException(validation.Errors);
            }

            return new TransactionResponse {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Category = transaction.Category,
                Person = transaction.Person
            };
        }
    }
}
