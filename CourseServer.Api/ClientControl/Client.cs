using System.Net.Sockets;
using System.Text;

namespace CourseServer.Api.ClientControl
{
    public class Client
    {
        private TcpClient _client;

        public Guid Id { get; set; }
        public bool IsAlive;

        public Action<Guid> OnDisconnect { get; set; }
        public Action<string> OnReceive { get; set; }

        public Client(TcpClient client, Action<Guid> onDisconnect, Action<string> onReceive)
        {
            _client = client;

            Id = Guid.NewGuid();
            OnDisconnect = onDisconnect ?? throw new ArgumentNullException(nameof(onDisconnect));
            OnReceive = onReceive ?? throw new ArgumentNullException(nameof(onReceive));
        }

        public void SendMessage(string message)
        {
            if (_client.Connected)
            {
                using (var stream = _client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.BeginWrite(data, 0, data.Length, SendMessageCallback, data);
                }
            }
            Disconnect();
        }

        public void SendMessageCallback(IAsyncResult asyncResult)
        {
            try
            {
                using (var stream = _client.GetStream())
                {
                    stream.EndWrite(asyncResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BeginRead()
        {
            if (_client.Connected)
            {
                using (var stream = _client.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                }
            }
            Disconnect();
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            try
            {
                var buffer = (byte[])asyncResult.AsyncState;

                using (var stream = _client.GetStream())
                {
                    int bytesRead = stream.EndRead(asyncResult);

                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        BeginRead();
                    }
                    if (!_client.Connected)
                    {
                        Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Disconnect()
        {
            _client.Close();
            OnDisconnect.Invoke(Id);
        }
    }
}
