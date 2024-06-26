using MainServer.Models;

namespace MainServer.Services
{
    public interface IPlayerService
    {
        public Player RegisterPlayer();
        Player? GetPlayerByToken(string token);
        void RemovePlayerAtRoom(string token);
    }
}
