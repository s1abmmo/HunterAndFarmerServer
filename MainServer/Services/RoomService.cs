using MainServer.Enums;
using MainServer.Models;

namespace MainServer.Services
{
    public class RoomService : IRoomService
    {
        private readonly List<Room> _rooms = new List<Room>();

        public string CreateRoom(string name, string tokenPlayerAsHost)
        {
            var room = new Room
            {
                Id = (_rooms.Count + 1).ToString(),
                Name = name,
                TokenPlayerAsHost = tokenPlayerAsHost
            };
            _rooms.Add(room);
            return room.Id;
        }

        public void AddPlayerToRoom(string roomId, Player player)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null && room.Players.Count < 5)
            {
                room.Players.Add(player);
            }
            else
            {
                throw new Exception("Room full or not exists");
            }
        }

        public void RemovePlayerFromRoom(string roomId, string playerToken)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                var player = room.Players.FirstOrDefault(p => p.Token == playerToken);
                if (player != null)
                {
                    room.Players.Remove(player);
                }
            }
        }

        public Room? GetRoom(string roomId)
        {
            return _rooms.FirstOrDefault(r => r.Id == roomId);
        }
        public List<Room> GetAllRooms()  // Thêm phương thức này
        {
            return _rooms;
        }
        public void SetPlayerReadyStatus(string playerToken, string roomId, bool isReady)
        {
            int roomIndex = _rooms.FindIndex(r => r.Id == roomId);
            if (roomIndex != -1)
            {
                int playerIndex = _rooms[roomIndex].Players.FindIndex(p => p.Token == playerToken);
                if (roomIndex != -1)
                {
                    _rooms[roomIndex].Players[playerIndex].IsReady = isReady;
                }
                else
                {
                    throw new Exception($"Player not in room");
                }
            }
            else
            {
                throw new Exception($"Room not exists");
            }
        }
        public void StartGame(string roomId, string hostToken)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null && room.TokenPlayerAsHost == hostToken)
            {
                room.PlayStartTime = DateTime.UtcNow.AddSeconds(3);
                room.RoomState = RoomStateEnum.PLAYING;
            }
        }
    }
}
