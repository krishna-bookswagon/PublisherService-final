using PublisherService_demo.Models;

namespace PublisherService_demo.Services
{
    // CONTRACT: defines WHAT business operations exist
    public interface IPublisherService
    {
        Task<SinglePublisherResult> GetPublisherByIdAsync(int id);

        Task<PublisherListResult> GetPublishersByNameAsync(
            string name,
            int pageNumber,
            int pageSize);
    }
}