using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.Networking
{
    public class Listener
    {
        private volatile bool _Stop;
        public bool Stop
        {
            get => _Stop;
            set => _Stop = value;
        }

        private readonly string _host;
        private readonly int _port;
        private readonly TcpListener _listener;

        private bool IsRunning;

        private readonly Action<TcpClient> OnHandleConnection;

        public Listener(Action<TcpClient> connectionHandler, string host, int port)
        {
            OnHandleConnection = connectionHandler;
            _host = host;
            _port = port;

            _listener = new TcpListener(IPAddress.Parse(host), port);
        }

        public void Run()
        {
            if (IsRunning)
                return;

            IsRunning = true;
            _listener.Start();
            Stop = false;

            StartAccepting();
        }

        public void StartAccepting()
        {
            _listener.BeginAcceptTcpClient(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            var client = _listener.EndAcceptTcpClient(asyncResult);

            OnHandleConnection?.Invoke(client);
            if(!Stop)
            {
                StartAccepting();
            }
            else
            {
                _listener.Stop();
            }
        }        
    }
}
