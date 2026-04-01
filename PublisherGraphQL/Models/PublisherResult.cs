namespace PublisherGraphQL.Models
{
    public class PublisherResult
    {
        public Publisher? SinglePublisher { get; set; }
        public List<Publisher>? MultiplePublishers { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }
        public bool IsSuccess { get; set; }

        // When found by ID
        public static PublisherResult FoundSingle(Publisher publisher)
        {
            return new PublisherResult
            {
                SinglePublisher = publisher,
                IsSuccess = true
            };
        }

        // When found by Name
        public static PublisherResult FoundMultiple(List<Publisher> publishers)
        {
            return new PublisherResult
            {
                MultiplePublishers = publishers,
                IsSuccess = true
            };
        }

        // When error occurs
        public static PublisherResult Failure(string message, string code)
        {
            return new PublisherResult
            {
                ErrorMessage = message,
                ErrorCode = code,
                IsSuccess = false
            };
        }
    }
}
