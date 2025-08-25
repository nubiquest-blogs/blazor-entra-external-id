using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Backend>("backend");

builder.AddProject<ExternalIdApp>("frontend")
    .WithReference(backend);

builder.Build().Run();