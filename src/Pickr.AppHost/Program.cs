var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres");

// Databases
var catalogDb = postgres.AddDatabase("catalogdb");

// Services
builder.AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(rabbitMq)
    .WithReference(catalogDb);

builder.AddProject<Projects.Selections_API>("selections-api")
    .WithReference(rabbitMq);

builder.Build().Run();