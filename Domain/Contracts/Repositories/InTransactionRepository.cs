using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface InTransactionRepository
    {
        Task Save(Transaction transaction);
        Task<List<Transaction>> GetAll();
        Task Update(Transaction transaction);
        Task<Transaction> GetById(Guid id);
        Task Delete(Guid id);
        Task DeleteByPerson(Guid personId);
    }
}
