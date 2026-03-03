using Domain.Contracts.Repositories;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Categories
{
    public class GetAllCategoryUseCase
    {
        private readonly InCategoryRepository _repository;

        public GetAllCategoryUseCase(InCategoryRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Obter todas as categorias existentes.
        */
        public async Task<List<CategoryResponse>> Execute()
        {
            var category = await _repository.GetAll();
            return category.Select(c => new CategoryResponse { Id = c.Id, Description = c.Description, Goal = c.Goal }).ToList();
        }
    }
}
