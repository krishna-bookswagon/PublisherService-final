using PublisherService_demo.Models;

namespace PublisherService_demo.Repositories
{
    // This is a CONTRACT - it defines WHAT operations exist
    // The actual implementation is in PublisherRepository.cs
    public interface IPublisherRepository
    {
        // Find one publisher by ID
        Task<Publisher?> GetByIdAsync(int id);

        // Find publishers by name with pagination
        Task<List<Publisher>> GetByNameAsync(string name, int pageNumber, int pageSize);

        // Get total count of name matches (needed for HasNextPage)
        Task<int> GetCountByNameAsync(string name);
    }
}