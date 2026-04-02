using PublisherService_demo.Models;
using PublisherService_demo.Repositories;

namespace PublisherService_demo.Services
{
    // IMPLEMENTATION: defines HOW business rules are applied
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;

        // Repository is injected - we don't create it manually
        public PublisherService(IPublisherRepository repository)
        {
            _repository = repository;
        }

        public async Task<SinglePublisherResult> GetPublisherByIdAsync(int id)
        {
            // Business Rule: ID must be valid
            if (id <= 0)
                return SinglePublisherResult.Failure(
                    "Publisher ID must be greater than 0.");

            var publisher = await _repository.GetByIdAsync(id);

            // Business Rule: return error if not found
            if (publisher == null)
                return SinglePublisherResult.Failure(
                    $"No publisher found with ID {id}.");

            return SinglePublisherResult.Success(publisher);
        }

        public async Task<PublisherListResult> GetPublishersByNameAsync(
            string name,
            int pageNumber,
            int pageSize)
        {
            // Business Rule: page values must be valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Get total count first (for pagination calculation)
            var totalCount = await _repository.GetCountByNameAsync(name);

            // Business Rule: return error if nothing found
            if (totalCount == 0)
                return PublisherListResult.Failure(
                    $"No publishers found with name containing '{name}'.");

            var publishers = await _repository.GetByNameAsync(
                name, pageNumber, pageSize);

            return PublisherListResult.Success(
                publishers, totalCount, pageNumber, pageSize);
        }
    }
}