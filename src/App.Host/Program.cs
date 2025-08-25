using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Backend>("Backend");

builder.AddProject<ExternalIdApp>("Frontend")
    .WithReference(backend);

builder.Build().Run();