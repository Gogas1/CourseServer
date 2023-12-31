using CourseServer.Api.ClientControl;
using System.Net.Sockets;

namespace CourseServer.Api.Networking
{
    public class ServerController
    {

        private readonly Server _server;
        private readonly Thread _serverThread;

        public Action<string>? OnLog;
        public Action<TcpClient>? OnClientHandle;

        public ServerController(string host, int port, int maxConcurrentListeners, int awaiterTimeoutMS)
        {
            _server = new Server(OnMessage, HandleClient, host, port, maxConcurrentListeners, awaiterTimeoutMS);
            _serverThread = new Thread(_server.Run);
        }

        public void RunServer()
        {
            _serverThread.Start();
            OnLog?.Invoke("Server started");
        }

        public void StopServerThread()
        {
            _server.Stop = true;
            OnLog?.Invoke("Stopping server thread");
            _serverThread.Join();
            OnLog?.Invoke("Stopped");
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
