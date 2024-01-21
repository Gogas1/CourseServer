using System.Net.Sockets;

namespace CourseServer.Api.Networking
{
    public class ServerController
    {

        private readonly Listener _server;

        public Action<string>? OnLog;
        public Action<TcpClient>? OnClientHandle;

        public ServerController(string host, int port, int maxConcurrentListeners, int awaiterTimeoutMS)
        {
            _server = new Listener(HandleClient, host, port);
        }

        public void RunServer()
        {
            _server.Run();
            OnLog?.Invoke("Server started");
        }

        public void StopServer()
        {
            _server.Stop = true;
            OnLog?.Invoke("Stopping server thread");
        }

        private void HandleClient(TcpClient tcpClient)
        {
            OnClientHandle?.Invoke(tcpClient);
        }

        private void OnMessage(string message)
        {
            OnLog?.Invoke(message);
        }
    }
}
