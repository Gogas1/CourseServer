using CourseServer.Api.Commands;
using CourseServer.Api.Networking;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.ClientControl
{
    public class ClientController : IHostedService
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ServerController _serverController;
        private readonly CommandController _commandController;

        private List<Client> clients = new List<Client>();

        public ClientController(ServerController serverController, CommandController commandController, ILogger<ClientController> logger)
        {
            _serverController = serverController;
            _commandController = commandController;
            _logger = logger;

            _serverController.OnClientHandle += AddClient;
            _serverController.OnLog += Log;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _serverController.RunServer();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _serverController.StopServerThread();

            return Task.CompletedTask;
        }

        private void AddClient(TcpClient client)
        {
            Client newClient = new Client(client, HandleDisconnect, HandleReceivedMessage);
        }

        private void HandleDisconnect(Guid id)
        {
            clients.RemoveAll(x => x.Id == id);
        }

        private void HandleReceivedMessage(string message)
        {
            _commandController.SendCommand(message, "some body");
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
