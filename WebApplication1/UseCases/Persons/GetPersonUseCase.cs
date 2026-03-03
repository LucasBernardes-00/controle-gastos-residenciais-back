using Domain.Common;
using Domain.Contracts.Repositories;
using Domain.Exceptions;
using WebApplication1.DTOs.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebApplication1.UseCases.Persons
{
    public class GetPersonUseCase
    {
        private readonly InPersonRepository _repository;

        public GetPersonUseCase(InPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonResponse> Execute(Guid id)
        {
            var validation = new ValidationResult();

            var person = await _repository.GetById(id);
            if (person == null)
            {
                validation.AddError("Pessoa não encontrada.");
                throw new InputException(validation.Errors);
            }
            return new PersonResponse { Id = person.Id, Name = person.Name, Birthday = person.Birthday };
        }
    }
}
