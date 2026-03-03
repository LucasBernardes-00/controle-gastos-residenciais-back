using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Categories
{
    public class GetCategoryUseCase
    {
        private readonly InCategoryRepository _repository;

        public GetCategoryUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Obter os detalhes de uma categoria específica.
         * 
         * @param id: O identificador único da categoria a ser obtida.
         * @return: Um objeto CategoryResponse contendo os detalhes da categoria.
         * @throws InputException: Lançada quando a categoria com o ID fornecido não é encontrada.
        */
        public async Task<CategoryResponse> Execute(Guid id)
        {
            var validation = new ValidationResult();

            var category = await _repository.GetById(id);
            if (category == null)
            {
                validation.AddError("Categoria não encontrada.");
                throw new InputException(validation.Errors);
            }

            return new CategoryResponse { 
                Id = category.Id, 
                Description = category.Description, 
                Goal = category.Goal 
            };
        }
    }
}
