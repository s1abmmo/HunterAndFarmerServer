using MainServer.Models;

namespace MainServer.Services
{
    public interface IRoomService
    {
        string CreateRoom(string name, string tokenPlayerAsHost);
        void AddPlayerToRoom(string roomId, Player player);
        void RemovePlayerFromRoom(string roomId, string playerId);
        Room? GetRoom(string roomId);
        List<Room> GetAllRooms();
        void SetPlayerReadyStatus(string playerToken, string roomId, bool isReady);
        void StartGame(string roomId, string hostToken);
    }
}
