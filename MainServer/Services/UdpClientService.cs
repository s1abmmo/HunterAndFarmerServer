using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MainServer.Services
{
    public class UdpClientService
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endPoint;

        public UdpClientService(string serverAddress, int serverPort)
        {
            _udpClient = new UdpClient();
            _endPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
        }

        public async Task SendMessageAsync(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await _udpClient.SendAsync(bytes, bytes.Length, _endPoint);
        }
    }
}
