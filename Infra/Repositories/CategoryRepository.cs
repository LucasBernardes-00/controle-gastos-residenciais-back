using Domain.Contracts.Repositories;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class CategoryRepository : InCategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task Update(Category person)
        {
            _context.Category.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task Delete(Guid id)
        {
            await _context.Category.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<CategoryBalance>> ReportWithTransactions()
        {
            var results = await _context.Database
                .SqlQuery<CategoryBalance>(@$"
                    SELECT 
	                    c.Id,
                        c.Description,
                        COALESCE(SUM(CASE WHEN t.Type = 'RECEITA' THEN t.Amount END), 0) AS Income,
                        COALESCE(SUM(CASE WHEN t.Type = 'DESPESA' THEN t.Amount END), 0) AS Expense,
                        COALESCE(SUM(
                            CASE 
                                WHEN t.Type = 'RECEITA' THEN t.Amount 
                                WHEN t.Type = 'DESPESA' THEN -t.Amount 
                                ELSE 0 
                            END
                        ), 0) AS Balance
                    FROM category c
                    LEFT JOIN transaction t ON c.Id = t.CategoryId
                    GROUP BY c.Id, c.Description
                    HAVING Balance <> 0")
                .ToListAsync();
            return results;
        }
    }
}
