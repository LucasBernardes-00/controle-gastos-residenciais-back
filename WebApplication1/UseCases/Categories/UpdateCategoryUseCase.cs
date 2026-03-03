using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using WebApplication1.DTOs.Request;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Categories
{
    public class UpdateCategoryUseCase
    {
        private readonly InCategoryRepository _repository;

        public UpdateCategoryUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Atualizar os detalhes de uma categoria existente.
         * 
         * @param id: O identificador único da categoria a ser atualizada.
         * @param category: Objeto contendo a nova descrição e finalidade da categoria.
         * @return: Retorna um objeto contendo o ID, descrição e finalidade da categoria atualizada.
         * @throws InputException: Lançada quando a categoria com o ID fornecido não é encontrada, ou quando a descrição é vazia ou nula, ou quando a finalidade é inválida.
        */
        public async Task<CategoryResponse> Execute(Guid id, CategoryUpdateRequest category)
        {
            var validation = new ValidationResult();

            var existingCategory = await _repository.GetById(id);
            if (existingCategory == null)
            {
                validation.AddError("Categoria não encontrada.");
                throw new InputException(validation.Errors);
            }

            Validate(category, validation);

            if (!validation.IsValid)
            {
                throw new InputException(validation.Errors);
            }

            existingCategory.Description = category.Description;
            existingCategory.Goal = category.Goal;

            await _repository.Update(existingCategory);

            return new CategoryResponse { Id = id, Description = category.Description, Goal = category.Goal };
        }

        private void Validate(CategoryUpdateRequest category, ValidationResult validation)
        {
            if (string.IsNullOrWhiteSpace(category.Description))
            {
                validation.AddError("A descrição é obrigatória.");
            }
            if (!Enum.IsDefined(typeof(eGoal), category.Goal))
            {
                validation.AddError("A finalidade informada é inválida. Use: Despesa, Receita ou Ambas.");
            }
        }
    }
}
