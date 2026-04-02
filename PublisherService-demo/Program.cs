using Microsoft.EntityFrameworkCore;
using PublisherService_demo.Data;
using PublisherService_demo.GraphQL;
using PublisherService_demo.Repositories;
using PublisherService_demo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ActiveConnectionString")));

// Add Repository & Service
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<PublisherQuery>();

var app = builder.Build();

// Map GraphQL endpoint
app.MapGraphQL();

app.Run();