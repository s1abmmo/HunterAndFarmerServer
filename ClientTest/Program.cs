using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;

UdpClient udpClient = new UdpClient();
IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 11000);

// Join the room
string joinMessage = "JOIN|room1|";
byte[] joinData = Encoding.UTF8.GetBytes(joinMessage);
await udpClient.SendAsync(joinData, joinData.Length, serverEndPoint);

// Update position
for (int i = 0; i < 10; i++)
{
    var position = new { X = i * 10, Y = i * 20 };
    string positionMessage = $"UPDATE_POSITION|room1|{JsonSerializer.Serialize(position)}";
    byte[] positionData = Encoding.UTF8.GetBytes(positionMessage);
    await udpClient.SendAsync(positionData, positionData.Length, serverEndPoint);

    await Task.Delay(1000); // Simulate delay between movements
}

// Leave the room
string leaveMessage = "LEAVE|room1|";
byte[] leaveData = Encoding.UTF8.GetBytes(leaveMessage);
await udpClient.SendAsync(leaveData, leaveData.Length, serverEndPoint);