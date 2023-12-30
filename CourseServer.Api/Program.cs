using CourseServer.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Sqlite") ?? throw new Exception("No connection string");
builder.Services.RegisterSqliteDbContext(connectionString);

var host =  builder.Build();
host.Run();