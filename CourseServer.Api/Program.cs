using CourseServer.Api.ClientControl;
using CourseServer.Api.Commands;
using CourseServer.Api.Networking;
using CourseServer.Core;
using CourseServer.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Sqlite") ?? throw new Exception("No connection string");
builder.Services.RegisterSqliteDbContext(connectionString);

builder.AddServerControllerSingleton();

builder.Services.RegisterCommands();
builder.Services.AddCoreServices();
builder.Services.AddRepos();

builder.Services.AddHostedService<ClientController>();

var host = builder.Build();
host.Services.MigrateDb();

host.Run();