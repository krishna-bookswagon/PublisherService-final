using PublisherGraphQL.Data;
using PublisherGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace PublisherGraphQL.GraphQL
{
    public class PublisherQuery
    {

        // Get all publishers
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Publisher> GetPublishers([Service] AppDbContext context)
        {
            return context.Table_Publisher;
        }

        // Get single publisher by ID
        public async Task<Publisher?> GetPublisher(
            int id,
            [Service] AppDbContext context)
        {
            return await context.Table_Publisher
                .Where(p => p.ID_Publisher == id)
                .FirstOrDefaultAsync();
        }

        // Get publishers by id or name with pagination
        public async Task<PublisherResult> SearchPublisher(
            int? id,
            string? name,
            int pageSize,
            int pageNumber,
            [Service] AppDbContext context)
        {
            // Case 1: ID is provided and valid
            if (id.HasValue && id.Value > 0)
            {
                var publisher = await context.Table_Publisher
                    .Where(p => p.ID_Publisher == id.Value)
                    .FirstOrDefaultAsync();

                if (publisher == null)
                    return PublisherResult.Failure(
                        $"Publisher with ID {id} not found.",
                        "NOT_FOUND");

                return PublisherResult.FoundSingle(publisher);
            }

            // Case 2: Name is provided
            if (!string.IsNullOrWhiteSpace(name))
            {
                var publishers = await context.Table_Publisher
                    .Where(p => p.Company_Name != null &&
                           p.Company_Name.Contains(name))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (!publishers.Any())
                    return PublisherResult.Failure(
                        $"No publishers found with name containing '{name}'.",
                        "NOT_FOUND");

                return PublisherResult.FoundMultiple(publishers);
            }

            // Case 3: Nothing provided
            return PublisherResult.Failure(
                "Please provide either ID_Publisher or Company_Name.",
                "BAD_REQUEST");
        }
    }
}
