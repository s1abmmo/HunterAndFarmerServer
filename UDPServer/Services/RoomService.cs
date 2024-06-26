using System.Net;
using UDPServer.Models;
using System.Collections.Concurrent;
using System.Numerics;
using UDPServer.Utils;

namespace UDPServer.Services
{
    public class Room
    {
        public string RoomId { get; set; }
        public ConcurrentDictionary<IPEndPoint, Player> Players = new ConcurrentDictionary<IPEndPoint, Player>();

        public Room(string roomId)
        {
            RoomId = roomId;
        }

        public void AddClient(IPEndPoint client)
        {
            if (!Players.Keys.Contains(client))
            {
                Players.TryAdd(client, new Player("123", 0.15f, client));
            }
        }

        public void RemoveClient(IPEndPoint client)
        {
            if (Players.Keys.Contains(client))
            {
                Players.TryRemove(client, out _);
            }
        }

        public void UpdatePlayerRotation(IPEndPoint client, float angle)
        {
            if (Players.ContainsKey(client))
            {
                Console.WriteLine("set rotation");
                Players[client].Rotation = RotationUtils.AngleAxisToQuaternion(angle);
                Players[client].PlayerState = Enums.PlayerStateEnum.RUNNING;
            }
        }

        public void UpdatePlayerPosition(IPEndPoint client)
        {
            if (Players.ContainsKey(client))
            {
                if (Players[client].PlayerState == Enums.PlayerStateEnum.RUNNING)
                {
                    Vector3 newPosition = MovementUtils.CalculateNewPosition(Players[client].Rotation, Players[client].Speed, Players[client].Position, ((float)(DateTime.Now - Players[client].LastTimeUpdatePosition).TotalSeconds));
                    Players[client].Position = newPosition;
                    Players[client].LastTimeUpdatePosition = DateTime.Now;
                }
            }
        }

        public void StopMovePlayer(IPEndPoint client)
        {
            if (Players.ContainsKey(client))
            {
                Players[client].PlayerState = Enums.PlayerStateEnum.IDLE;
                Players[client].LastTimeUpdatePosition = DateTime.Now;
            }

        }

        public void UpdatePlayerAngle(IPEndPoint client, float angle)
        {
            if (Players.ContainsKey(client))
            {
                Players[client].Angle = angle;
            }
        }

    }
}
