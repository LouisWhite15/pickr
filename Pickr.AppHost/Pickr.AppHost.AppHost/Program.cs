using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitMq = builder.AddRabbitMQ("eventbus");

// Services
builder.AddProject<Selection_API>("selection-api")
    .WithReference(rabbitMq);

builder.Build().Run();