using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;
using Infra;

namespace WebApplication1.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        private readonly InPersonRepository _repository;
        private readonly InTransactionRepository _transactionrepository;

        private readonly AppDbContext _context;

        public DeletePersonUseCase(
            InPersonRepository repository,
            InTransactionRepository transactionRepository,
            AppDbContext context
        )
        {
            _repository = repository;
            _transactionrepository = transactionRepository;
            _context = context;
        }

        public async Task Execute(Guid id)
        {
            var validation = new ValidationResult();

            var person = await _repository.GetById(id);
            if (person == null)
            {
                validation.AddError("A pessoa informada não existe.");
                throw new InputException(validation.Errors);
            }

            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await DeleteAllTransactions(id);
                await _repository.Delete(id);

                await dbTransaction.CommitAsync();
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                validation.AddError("Falha ao tentar remover pessoa.");
                throw new InputException(validation.Errors);
            }
        }

        private async Task DeleteAllTransactions(Guid id)
        {
            await _transactionrepository.DeleteByPerson(id);
        }
    }
}
