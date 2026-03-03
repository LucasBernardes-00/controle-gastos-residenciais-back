using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Common;
using WebApplication1.DTOs.Request;
using WebApplication1.DTOs.Response;

namespace WebApplication1.UseCases.Persons
{
    public class SavePersonUseCase
    {
        private readonly InPersonRepository _repository;

        public SavePersonUseCase(InPersonRepository repository)
        {
            _repository = repository;
        }

        /*
         * Objetivo: Salvar uma nova pessoa no sistema, garantindo que os dados fornecidos sejam válidos.
         * 
         * @param req: Um objeto PersonSaveRequest contendo os detalhes da pessoa a ser salva, como nome e data de nascimento.
         * @return: Um objeto PersonResponse contendo os detalhes da pessoa salva, incluindo seu identificador único.
         * @throws InputException: Lançada quando os dados fornecidos para salvar a pessoa são inválidos, como nome vazio ou data de nascimento ausente.
        */
        public async Task<PersonResponse> Execute(PersonSaveRequest req)
        {
            var validation = new ValidationResult();
            Validate(req, validation);

            if (!validation.IsValid)
            {
                throw new InputException(validation.Errors);
            }

            var person = new Person { Id = Guid.NewGuid(), Name = req.Name, Birthday = req.Birthday };

            await _repository.Save(person);
            return new PersonResponse { Id = person.Id, Name = person.Name, Birthday = person.Birthday };
        }

        private void Validate(PersonSaveRequest person, ValidationResult validation)
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
