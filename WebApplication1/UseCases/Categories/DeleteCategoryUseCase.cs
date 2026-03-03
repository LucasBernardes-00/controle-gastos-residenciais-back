using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;

namespace WebApplication1.UseCases.Categories
{
    public class DeleteCategoryUseCase
    {
        private readonly InCategoryRepository _repository;

        public DeleteCategoryUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Remover uma categoria existente.
         *
         * @param id O ID da categoria a ser removida.
         * @throws InputException Se a categoria não for encontrada para remoção.
         */
        public async Task Execute(Guid id)
        {
            var validation = new ValidationResult();

            var existingCategory = await _repository.GetById(id);
            if (existingCategory == null)
            {
                validation.AddError("Categoria não encontrada para remoção.");
                throw new InputException(validation.Errors);
            }
            await _repository.Delete(id);
        }
    }
}
