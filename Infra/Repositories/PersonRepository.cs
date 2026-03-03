using Domain.Contracts.Repositories;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PersonRepository : InPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save(Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Person>> GetAll()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task Update(Person person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetById(Guid id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task Delete(Guid id)
        {
            await _context.Person.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<PersonBalance>> ReportWithTransactions()
        {
            var results = await _context.Database
                .SqlQuery<PersonBalance>(@$"
                    SELECT 
                        p.Id, 
                        p.Name,
                        COALESCE(SUM(CASE WHEN t.Type = 'RECEITA' THEN t.Amount ELSE 0 END), 0) as Income,
                        COALESCE(SUM(CASE WHEN t.Type = 'DESPESA' THEN t.Amount ELSE 0 END), 0) as Expense,
                        COALESCE(SUM(CASE WHEN t.Type = 'RECEITA' THEN t.Amount ELSE -t.Amount END), 0) as Balance
                    FROM Person p
                    LEFT JOIN Transaction t ON p.Id = t.PersonId
                    GROUP BY p.Id, p.Name")
                .ToListAsync();
            return results;
        }
    }
}
