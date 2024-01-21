using System.Net.Sockets;
using System.Text;

namespace CourseServer.Api.ClientControl
{
    public class Client
    {
        private TcpClient _client;
        private readonly CancellationToken connectionCT;

        public Guid Id { get; set; }

        public Action<Guid> OnDisconnect { get; set; }
        public Action<ClientMessage> OnReceive { get; set; }

        public Client(TcpClient client, Action<Guid> onDisconnect, Action<ClientMessage> onReceive, CancellationToken connectionCT)
        {
            _client = client;

            Id = Guid.NewGuid();
            OnDisconnect = onDisconnect ?? throw new ArgumentNullException(nameof(onDisconnect));
            OnReceive = onReceive ?? throw new ArgumentNullException(nameof(onReceive));

            this.connectionCT = connectionCT;

            Listen();
        }

        public void Listen()
        {
            var stream = _client.GetStream();

            if (!stream.CanWrite && !stream.CanRead) return;

            try
            {
                byte[] buffer = new byte[4096];
                stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
            catch (IOException ex)
            {
                if (_client.Connected && !connectionCT.IsCancellationRequested)
                {
                    Listen();
                }
            }
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            byte[] buffer = (byte[])asyncResult.AsyncState;

            try
            {
                var stream = _client.GetStream();

                int bytesReaded = stream.EndRead(asyncResult);
                if (bytesReaded > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer);
                    data = string.Join("", data.Split('\0'));

                    OnReceive?.Invoke(new ClientMessage(data, this));

                    if (_client.Connected && !connectionCT.IsCancellationRequested)
                    {
                        Listen();
                    }
                }
                else
                {
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                if (_client.Connected && !connectionCT.IsCancellationRequested)
                {
                    var stream = _client.GetStream();

                    stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                }
            }
        }

        public void SendMessage(string message)
        {
            var stream = _client.GetStream();

            try
            {
                if (stream.CanWrite && _client.Connected && !connectionCT.IsCancellationRequested)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.BeginWrite(data, 0, data.Length, SendingCallback, null);
                }
                else
                {
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                if (!_client.Connected || connectionCT.IsCancellationRequested)
                {
                    Disconnect();
                }
            }
        }

        private void SendingCallback(IAsyncResult asyncResult)
        {
            var stream = _client.GetStream();

            try
            {
                stream.EndWrite(asyncResult);
            }
            catch (IOException ex)
            {
                if (!_client.Connected || connectionCT.IsCancellationRequested)
                {
                    Disconnect();
                }
            }
        }

        private void Disconnect()
        {
            _client.Close();
            OnDisconnect.Invoke(Id);
        }
    }
}
