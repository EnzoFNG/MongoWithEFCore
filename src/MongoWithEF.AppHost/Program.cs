var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MongoWithEF_Api>("mongowithef-api");

builder.Build().Run();
