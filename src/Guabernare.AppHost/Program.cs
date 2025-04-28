var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var guarbenareApi = builder.AddProject<Projects.Gubernare_Api>("ApiDefault")
    .WithReference(cache)
    .WaitFor(cache)
    .WithExternalHttpEndpoints();

builder.AddNpmApp("angular", "../Gubernare.WebApp")
    .WithReference(guarbenareApi)
    .WithReference(cache)
    .WaitFor(guarbenareApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();