using CourseServer.Api.Commands;
using CourseServer.Api.Networking;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Text.Json;

namespace CourseServer.Api.ClientControl
{
    public class ClientController : IHostedService
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ServerController _serverController;
        private readonly CommandController _commandController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private List<Client> clients = new List<Client>();

        public ClientController(ServerController serverController, CommandController commandController, ILogger<ClientController> logger)
        {
            _serverController = serverController;
            _commandController = commandController;
            _logger = logger;

            _serverController.OnClientHandle += AddClient;
            _serverController.OnLog += Log;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _serverController.RunServer();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _serverController.StopServer();

            return Task.CompletedTask;
        }

        private void AddClient(TcpClient client)
        {
            Client newClient = new Client(client, HandleDisconnect, HandleReceivedMessage, _cancellationTokenSource.Token);
        }

        private void HandleDisconnect(Guid id)
        {
            clients.RemoveAll(x => x.Id == id);
        }

        private async void HandleReceivedMessage(ClientMessage message)
        {
            Client client = message.Client;
            MasterMessage? data = JsonSerializer.Deserialize<MasterMessage>(message.Message);

            if (data != null)
            {
                var answer = await _commandController.SendCommand(data.Command, data.CommandData);

                client.SendMessage(JsonSerializer.Serialize(answer));
            }
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
