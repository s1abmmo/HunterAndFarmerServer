using MainServer.Enums;

namespace MainServer.Models
{
    public class Room
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public required string TokenPlayerAsHost {  get; set; }
        public DateTime? PlayStartTime { get; set; }
        public RoomStateEnum RoomState { get; set; } = RoomStateEnum.WAITING;
    }
}
