using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UDPServer.Models;

namespace UDPServer.Services
{
    public class RoomManager
    {
        private readonly ConcurrentDictionary<string, Room> _rooms = new ConcurrentDictionary<string, Room>();

        public void CreateRoom(string roomId)
        {
            _rooms.GetOrAdd(roomId, new Room(roomId));
        }

        public void JoinRoom(string roomId, IPEndPoint clientEndPoint)
        {
            if (!_rooms.Keys.Contains(roomId))
            {
                Console.WriteLine($"add room {roomId}");
                var room = _rooms.GetOrAdd(roomId, new Room(roomId));
                room.AddClient(clientEndPoint);
            }
        }

        public void LeaveRoom(string roomName, IPEndPoint clientEndPoint)
        {
            if (_rooms.TryGetValue(roomName, out Room room))
            {
                room.RemoveClient(clientEndPoint);
            }
        }

        public void UpdatePlayerRotation(string roomId, IPEndPoint clientEndPoint, float angle)
        {
            if (_rooms.TryGetValue(roomId, out Room room))
            {
                room.UpdatePlayerRotation(clientEndPoint, angle);
            }
        }

        public void UpdatePlayerStateStopMove(string roomId, IPEndPoint clientEndPoint)
        {
            if (_rooms.TryGetValue(roomId, out Room room))
            {
                room.StopMovePlayer(clientEndPoint);
            }
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _rooms.Values;
        }

        public int CountRooms()
        {
            return _rooms.Count;
        }

    }
}
