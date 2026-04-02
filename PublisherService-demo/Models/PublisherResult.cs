namespace PublisherService_demo.Models
{
    // Used when searching by ID → always returns 1 or error
    public class SinglePublisherResult
    {
        public Publisher? Publisher { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        // Factory method: call this when ID search succeeds
        public static SinglePublisherResult Success(Publisher publisher) =>
            new SinglePublisherResult
            {
                Publisher = publisher,
                IsSuccess = true
            };

        // Factory method: call this when something goes wrong
        public static SinglePublisherResult Failure(string errorMessage) =>
            new SinglePublisherResult
            {
                ErrorMessage = errorMessage,
                IsSuccess = false
            };
    }

    // Used when searching by Name → returns many with pagination info
    public class PublisherListResult
    {
        public List<Publisher> Publishers { get; set; } = new();
        public int TotalCount { get; set; }      // Total records matching search
        public int PageNumber { get; set; }      // Which page are we on?
        public int PageSize { get; set; }        // How many per page?
        public bool HasNextPage { get; set; }    // Is there a page after this?
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        // Factory method: call this when name search succeeds
        public static PublisherListResult Success(
            List<Publisher> publishers,
            int totalCount,
            int pageNumber,
            int pageSize) =>
            new PublisherListResult
            {
                Publishers = publishers,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                // If total records > what we've shown so far → there's a next page
                HasNextPage = (pageNumber * pageSize) < totalCount,
                IsSuccess = true
            };

        // Factory method: call this when something goes wrong
        public static PublisherListResult Failure(string errorMessage) =>
            new PublisherListResult
            {
                ErrorMessage = errorMessage,
                IsSuccess = false
            };
    }
}