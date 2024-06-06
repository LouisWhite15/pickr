var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres");

// Databases
var catalogDb = postgres.AddDatabase("catalogdb");
var selectionsDb = postgres.AddDatabase("selectionsdb");

// Services
var catalogApi = builder.AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(rabbitMq)
    .WithReference(catalogDb);

var selectionsApi = builder.AddProject<Projects.Selections_API>("selections-api")
    .WithReference(rabbitMq)
    .WithReference(selectionsDb);

builder.AddNpmApp("react", "../web-app")
    .WithReference(catalogApi)
    .WithReference(selectionsApi)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();