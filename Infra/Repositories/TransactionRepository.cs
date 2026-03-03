using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TransactionRepository : InTransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save(Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Transaction>> GetAll()
        {
            return await _context.Transaction
                .Include(t => t.Person)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Transaction> GetById(Guid id)
        {
            return await _context.Transaction
                .Include(t => t.Person)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Update(Transaction transaction)
        {
            _context.Transaction.Update(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            await _context.Transaction.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteByPerson(Guid personId)
        {
            await _context.Transaction.Where(x => x.PersonId == personId).ExecuteDeleteAsync();
        }
    }
}
