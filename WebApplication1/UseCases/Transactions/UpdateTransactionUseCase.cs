using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using WebApplication1.DTOs.Request;
using Category = Domain.Entities.Category;
using Person = Domain.Entities.Person;

namespace WebApplication1.UseCases.Transactions
{
    public class UpdateTransactionUseCase
    {
        private readonly InTransactionRepository _repository;
        private readonly InCategoryRepository _repositoryCategory;
        private readonly InPersonRepository _repositoryPerson;

        public UpdateTransactionUseCase(
            InTransactionRepository repository,
            InCategoryRepository repositoryCategory,
            InPersonRepository repositoryPerson
        )
        {
            _repository = repository;
            _repositoryCategory = repositoryCategory;
            _repositoryPerson = repositoryPerson;
        }

        /*
         * Objetivo: Atualizar uma transação existente.
         * 
         * @param id: Identificador da transação a ser atualizada.
         * @param req: Objeto contendo os dados para atualização da transação.
         * @throws InputException: Lançada quando os dados de entrada são inválidos, quando a categoria e a pessoa relacionadas a transação não são encontradas, quando o valor(amout) é menor que zero, quando o tipo escolhido não é válido, quando sua descrição está vazia ou nula, quando o tipo da categoria não bate com o tipo da transação ou quando um menor de idade estiver tentando cadastrar uma receita.
         */
        public async Task Execute(Guid id, TransactionUpdateRequest req)
        {
            var validation = new ValidationResult();

            var transaction = await GetTransaction(id, validation);
            var category = await GetCategory(req.CategoryId, validation);
            var person = await GetPerson(req.PersonId, validation);

            Validate(req, validation, category, person);

            if (!validation.IsValid)
            {
                throw new InputException(validation.Errors);
            }

            transaction.Description = req.Description;
            transaction.Amount = req.Amount;
            transaction.Type = req.Type;
            transaction.CategoryId = req.CategoryId;
            transaction.PersonId = req.PersonId;

            await _repository.Update(transaction);
        }

        private void Validate(TransactionUpdateRequest req, ValidationResult validation, Category category, Person person)
        {
            if (string.IsNullOrWhiteSpace(req.Description))
                validation.AddError("A descrição é obrigatória.");

            if (req.Amount <= 0)
                validation.AddError("O valor da transação deve ser maior que zero.");

            if (!Enum.IsDefined(typeof(eTransactionType), req.Type))
                validation.AddError("O tipo de transação informado é inválido. Use: Despesa ou Receita.");

            if (category.Id != Guid.Empty)
            {
                if (req.Type == eTransactionType.DESPESA && category.Goal == eGoal.RECEITA)
                    validation.AddError("Uma despesa não pode usar uma categoria de finalidade 'Receita'.");

                if (req.Type == eTransactionType.RECEITA && category.Goal == eGoal.DESPESA)
                    validation.AddError("Uma receita não pode usar uma categoria de finalidade 'Despesa'.");
            }

            if (person.Id != Guid.Empty)
            {
                var age = DateTime.Today.Year - person.Birthday.Year;
                if (person.Birthday.Date > DateTime.Today.AddYears(-age))
                    age--;

                if (age < 18 && req.Type == eTransactionType.RECEITA)
                    validation.AddError("Menores de idade só podem registrar despesas.");
            }
        }

        private async Task<Transaction> GetTransaction(Guid id, ValidationResult validation)
        {
            var transaction = await _repository.GetById(id);
            if (transaction == null)
            {
                validation.AddError("A categoria informada não existe.");
                return new Transaction();
            }
            return transaction;
        }

        private async Task<Category> GetCategory(Guid id, ValidationResult validation)
        {
            var category = await _repositoryCategory.GetById(id);

            if (category is not null) return category;

            validation.AddError("A categoria informada não existe.");
            return new Category();
        }

        private async Task<Person> GetPerson(Guid id, ValidationResult validation)
        {
            var person = await _repositoryPerson.GetById(id);

            if (person is not null) return person;

            validation.AddError("A pessoa informada não existe.");
            return new Person();
        }
    }
}
