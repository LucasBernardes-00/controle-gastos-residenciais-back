using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Common;
using WebApplication1.DTOs.Request;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        private readonly InPersonRepository _repository;

        public UpdatePersonUseCase(InPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonResponse> Execute(Guid id, PersonUpdateRequest person)
        {
            var validation = new ValidationResult();

            var existingPerson = await _repository.GetById(id);
            if (existingPerson == null)
            {
                validation.AddError("Pessoa não encontrada.");
                throw new InputException(validation.Errors);
            }

            Validate(person, validation);

            if (!validation.IsValid)
            {
                throw new InputException(validation.Errors);
            }

            existingPerson.Name = person.Name;
            existingPerson.Birthday = person.Birthday;

            await _repository.Update(existingPerson);

            return new PersonResponse { Name = person.Name, Birthday = person.Birthday };
        }

        private void Validate(PersonUpdateRequest person, ValidationResult validation)
        {
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                validation.AddError("O nome é obrigatório.");
            }
            if (person.Birthday == default)
            {
                validation.AddError("A data de nascimento é obrigatória.");
            }
        }
    }
}
