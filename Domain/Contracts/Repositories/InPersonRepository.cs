using Domain.DTOs;
using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface InPersonRepository
    {
        Task Save(Person person);
        Task<List<Person>> GetAll();
        Task Update(Person person);
        Task<Person> GetById(Guid id);
        Task Delete(Guid id);
        Task<List<PersonBalance>> ReportWithTransactions();
    }
}
