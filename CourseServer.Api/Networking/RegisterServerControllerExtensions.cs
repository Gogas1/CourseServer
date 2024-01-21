using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseServer.Api.Networking
{
    public static class RegisterServerControllerExtensions
    {
        public static void AddServerControllerSingleton(this HostApplicationBuilder builder)
        {
            string host = builder.Configuration.GetServerSetting("host").ToString();
            int port = builder.Configuration.GetServerSetting("port").ToInt();
            int maxListeners = builder.Configuration.GetServerSetting("maxConcurrentListeners").ToInt();
            int awaiterTimeoutMS = builder.Configuration.GetServerSetting("awaiterTimeoutMS").ToInt();

            builder.Services.AddSingleton(new ServerController(host, port, maxListeners, awaiterTimeoutMS));
        }

        public static ServerSetting GetServerSetting(this IConfiguration configuration, string setting)
        {
            return new ServerSetting(configuration.GetSection("ServerSettings")[setting] ?? throw new ArgumentNullException("Target server setting is missing"));
        }

    }

    public class ServerSetting
    {
        private readonly string _setting;
        public ServerSetting(string setting)
        {
            _setting = setting;
        }

        public override string ToString()
        {
            return _setting;
        }

        public int ToInt()
        {
            return int.Parse(_setting);
        }
    }
}
