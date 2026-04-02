using Microsoft.EntityFrameworkCore;
using PublisherService_demo.Data;
using PublisherService_demo.Models;

namespace PublisherService_demo.Repositories
{
    // This is the IMPLEMENTATION - it defines HOW we fetch data
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        // AppDbContext is injected - we don't create it manually
        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        // Fetch single publisher by ID
        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Table_Publisher
                .Where(p => p.ID_Publisher == id)
                .FirstOrDefaultAsync();
        }

        // Fetch publishers by name with pagination
        public async Task<List<Publisher>> GetByNameAsync(
            string name,
            int pageNumber,
            int pageSize)
        {
            return await _context.Table_Publisher
                .Where(p => p.Company_Name != null &&
                            p.Company_Name.Contains(name))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Count total matches for pagination calculation
        public async Task<int> GetCountByNameAsync(string name)
        {
            return await _context.Table_Publisher
                .Where(p => p.Company_Name != null &&
                            p.Company_Name.Contains(name))
                .CountAsync();
        }
    }
}