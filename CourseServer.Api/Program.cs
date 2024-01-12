using CourseServer.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CourseServer.Api.Commands;
using CourseServer.Api.Networking;
using CourseServer.Api.ClientControl;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Sqlite") ?? throw new Exception("No connection string");
builder.Services.RegisterSqliteDbContext(connectionString);

builder.AddServerControllerSingleton();

builder.Services.RegisterCommands();
builder.Services.AddSingleton<CommandController>();

builder.Services.AddHostedService<ClientController>();

var host =  builder.Build();
host.Run();