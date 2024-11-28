var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BuberDinner_Api>("BuberDinnerApi");

builder.Build().Run();