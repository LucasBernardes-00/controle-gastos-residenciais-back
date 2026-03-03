using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;

namespace WebApplication1.UseCases.Transactions
{
    public class DeleteTransactionUseCase
    {
        private readonly InTransactionRepository _repository;

        public DeleteTransactionUseCase(InTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Guid id)
        {
            var validation = new ValidationResult();

            var transaction = await _repository.GetById(id);
            if (transaction == null)
            {
                validation.AddError("Transação não encontrada para remoção.");
                throw new InputException(validation.Errors);
            }

            await _repository.Delete(id);
        }
    }
}
