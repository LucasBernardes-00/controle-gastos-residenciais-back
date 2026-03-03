using Domain.Contracts.Repositories;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Persons
{
    public class GetAllUseCase
    {
        private readonly InPersonRepository _repository;

        public GetAllUseCase(InPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonResponse>> Execute()
        {
            var people = await _repository.GetAll();
            return people.Select(p => new PersonResponse { Id = p.Id, Name = p.Name, Birthday = p.Birthday }).ToList();
        }
    }
}
