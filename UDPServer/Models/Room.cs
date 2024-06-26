using System.Net;

namespace UDPServer.Models
{
    public class Room
    {
        public string RoomName { get; set; }
        public List<IPEndPoint> Clients { get; private set; } = new List<IPEndPoint>();

        public Room(string roomName)
        {
            RoomName = roomName;
        }
    }
}
