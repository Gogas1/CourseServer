using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.Networking
{
    public class Server
    {
        private volatile bool _Stop;

        public bool Stop 
        { 
            get => _Stop; 
            set => _Stop = value; 
        }

        private readonly int _awaiterTimeoutMS;
        private readonly string _host;
        private readonly int _port;
        private readonly int _maxConcurrentListeners;
        private readonly TcpListener _listener;

        private bool IsRunning;
        private List<Task> AwaiterTasks = new List<Task>();

        private readonly Action<TcpClient> OnHandleConnection;
        private readonly Action<string> OnLog;

        public Server(Action<string> onLog, Action<TcpClient> connectionHandler, string host, int port, int maxConcurrentListeners, int awaiterTimeoutMS)
        {
            OnLog = onLog;
            OnHandleConnection = connectionHandler;
            _host = host;
            _port = port;
            _maxConcurrentListeners = maxConcurrentListeners;
            _awaiterTimeoutMS = awaiterTimeoutMS;

            _listener = new TcpListener(IPAddress.Parse(host), port);
        }

        public void Run()
        {
            if (IsRunning) 
                return;

            IsRunning = true;
            _listener.Start();
            Stop = false;

            while (!Stop)
            {
                AwaitConnection();
            }

            IsRunning = false;
        }

        private void AwaitConnection()
        {
            while (AwaiterTasks.Count < _maxConcurrentListeners)
            {
                var awaiterTask = Task.Run(async () =>
                {
                    OnLog.Invoke("Listening on thread " + Thread.CurrentThread.ManagedThreadId.ToString());
                    ProcessAcceptedClient(await _listener.AcceptTcpClientAsync());
                });
                AwaiterTasks.Add(awaiterTask);
            }
            int RemoveAtIndex = Task.WaitAny(AwaiterTasks.ToArray(), _awaiterTimeoutMS);
            if(RemoveAtIndex > 0) AwaiterTasks.RemoveAt(RemoveAtIndex);
        }

        private void ProcessAcceptedClient(TcpClient client)
        {
            OnLog.Invoke("Connection on thread" + Thread.CurrentThread.ManagedThreadId.ToString());
            if (!client.Connected) return;

            OnHandleConnection.Invoke(client);
        }
    }
}
