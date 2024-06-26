using System.Net.Sockets;
using System.Net;
using System.Text;
using UDPServer.Utils;
using System.Numerics;

namespace UDPServer.Services
{
    public class UdpServer
    {
        private readonly UdpClient _udpClient;
        private readonly RoomManager _roomManager;

        public UdpServer()
        {
            _udpClient = new UdpClient(11000);
            _roomManager = new RoomManager();
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Server is running...");
            _ = Task.Run(() => BroadcastUpdates());

            while (true)
            {
                var receivedResults = await _udpClient.ReceiveAsync();
                string receivedData = Encoding.UTF8.GetString(receivedResults.Buffer);
                HandleReceivedData(receivedData, receivedResults.RemoteEndPoint);
            }
        }

        private void HandleReceivedData(string data, IPEndPoint clientEndPoint)
        {
            if (!data.Contains("STOP"))
            {
                Console.WriteLine($"Received data: {data}");

            }
            var dataParts = data.Split('|');
            if (dataParts.Length > 2)
            {
                string command = dataParts[0];
                string roomId = dataParts[1];
                string payload = dataParts[2];

                switch (command)
                {
                    case "CREATE_ROOM":
                        string _roomId = _roomManager.CountRooms().ToString();
                        _roomManager.CreateRoom(_roomId);
                        break;
                    case "JOIN":
                        _roomManager.JoinRoom(roomId, clientEndPoint);
                        break;
                    case "LEAVE":
                        _roomManager.LeaveRoom(roomId, clientEndPoint);
                        break;
                    case "MOVE":
                        Console.WriteLine("set move case");
                        _roomManager.UpdatePlayerRotation(roomId, clientEndPoint, Convert.ToInt64(payload));
                        break;
                    case "STOPMOVE":
                        _roomManager.UpdatePlayerStateStopMove(roomId, clientEndPoint);
                        break;
                    case "ATTACK":
                        break;
                    case "CUTTREE":
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }

        private async Task BroadcastUpdates()
        {
            while (true)
            {
                foreach (var room in _roomManager.GetAllRooms())
                {

                    // Cập nhật vị trí mới của các player
                    List<string> playersInfo = new List<string>();
                    foreach (var player in room.Players)
                    {
                        room.UpdatePlayerPosition(player.Key);

                        RotationUtils.QuaternionToAngleAxis(player.Value.Rotation, out Vector3 axis, out float angle);

                        room.UpdatePlayerAngle(player.Key, angle);

                        playersInfo.Add($"{player.Value.Id}/{player.Value.PlayerState}/{player.Value.Position.Z},{player.Value.Position.X}/{player.Value.Angle}");
                    }

                    //Sau đấy gửi cho các player trong room thông tin của các player trong room: ID nhân vật, animation state, vị trí hiện tại, hướng quay mặt angle
                    foreach (var player in room.Players)
                    {
                        try
                        {

                            string response = string.Join('|', playersInfo);

                            byte[] responseData = Encoding.UTF8.GetBytes(response);
                            Console.WriteLine($"response: {response}");
                            await _udpClient.SendAsync(responseData, responseData.Length, player.Key);
                        }
                        catch (SocketException ex)
                        {
                            Console.WriteLine($"SocketException: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception: {ex.Message}");
                        }
                    }
                }

                await Task.Delay(15);
            }
        }
    }
}
