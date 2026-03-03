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

        /* 
         * * Objetivo: Obter os detalhes de uma pessoa específica com base no seu identificador único
         * 
         * @param id: O identificador único da pessoa a ser recuperada
         * @return: Um objeto PersonResponse contendo os detalhes da pessoa, ou uma exceção InputException se a pessoa não for encontrada7
         * @throws InputException: Lançada quando a pessoa com o identificador fornecido não é encontrada no repositório
        */
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
