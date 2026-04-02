using Microsoft.EntityFrameworkCore;
using PublisherService_demo.Models;

namespace PublisherService_demo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Publisher> Table_Publisher { get; set; }

        // It tells EF Core that the PK for the Publisher entity is ID_Publisher and that the corresponding table in the database should be named Table_Publisher.
        // This configuration ensures that EF Core correctly maps the Publisher class to the appropriate database structure.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.ID_Publisher);
                entity.ToTable("Table_Publisher");
            });
        }
    }
}
