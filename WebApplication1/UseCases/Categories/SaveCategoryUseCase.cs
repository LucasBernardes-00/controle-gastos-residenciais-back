using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using WebApplication1.DTOs.Request;
using WebApplication1.DTOs.Response;
using Category = Domain.Entities.Category;

namespace WebApplication1.UseCases.Categories
{
    public class SaveCategoryUseCase
    {
        private readonly InCategoryRepository _repository;
        
        public SaveCategoryUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Criar uma nova categoria.
         * 
         * @param category: Objeto contendo a descrição e a finalidade da categoria a ser criada.
         * @return: Retorna um objeto contendo o ID, descrição e finalidade da categoria criada.
         * @throws InputException: Lançada quando a descrição é vazia ou nula, ou quando a finalidade é inválida.
        */
        public async Task<CategoryResponse> Execute(CategorySaveRequest category)
        {
            var validation = new ValidationResult();
            Validate(category, validation);

            if (!validation.IsValid)
            {
                throw new InputException(validation.Errors);
            }

            var newCategory = new Category { Id = Guid.NewGuid(), Description = category.Description, Goal = category.Goal };

            await _repository.Save(newCategory);
            return new CategoryResponse { Id = newCategory.Id, Description = newCategory.Description, Goal = newCategory.Goal};
        }

        private void Validate(CategorySaveRequest category, ValidationResult validation)
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
