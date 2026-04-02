using PublisherService_demo.Models;
using PublisherService_demo.Services;

namespace PublisherService_demo.GraphQL
{
    public class PublisherQuery
    {
        private readonly IPublisherService _publisherService;

        // Service is injected
        public PublisherQuery(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // Called when: ID is provided and valid
        // Returns: single publisher or error
        public async Task<SinglePublisherResult> GetPublisherById(int id)
        {
            return await _publisherService.GetPublisherByIdAsync(id);
        }

        // Called when: name is provided
        // Returns: list of publishers with pagination or error
        public async Task<PublisherListResult> GetPublishersByName(
            string name,
            int pageNumber = 1,
            int pageSize = 10)
        {
            return await _publisherService.GetPublishersByNameAsync(
                name, pageNumber, pageSize);
        }

        // Called when: nothing is provided
        // Returns: error message
        public SinglePublisherResult GetPublisherError()
        {
            return SinglePublisherResult.Failure(
                "Please provide either ID_Publisher or Company_Name.");
        }
    }
}