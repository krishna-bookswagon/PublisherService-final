using Microsoft.EntityFrameworkCore;
using PublisherService_demo.Data;
using PublisherService_demo.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ActiveConnectionString")));

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<PublisherQuery>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Map GraphQL endpoint
app.MapGraphQL();

app.Run();