using WarehouseOptimizer.Worker.ServiceExtensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
