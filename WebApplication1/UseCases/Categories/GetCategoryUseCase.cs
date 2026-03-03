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
