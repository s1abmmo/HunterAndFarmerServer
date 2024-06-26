using MainServer.Models;
using MainServer.Utilities;

namespace MainServer.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly List<Player> _players = new List<Player>();
        public Player RegisterPlayer()
        {
            Player player = new Player(RandomTokenGenerator.GenerateRandomToken(10));
            _players.Add(player);
            return player;
        }

        public Player GetPlayerByToken(string token)
        {
            var player = _players.Find(p => p.Token == token);
            if (player == null)
            {
                throw new Exception("Player not exists");
            }
            return player;
        }

        public void SetPlayerAtRoomId(string token, string roomId)
        {
            int index = _players.FindIndex(p => p.Token == token);
            _players[index].AtRoomId = roomId;
        }

        public void RemovePlayerAtRoom(string token)
        {
            int index = _players.FindIndex(p => p.Token == token);
            _players[index].AtRoomId = null;
        }

    }
}
