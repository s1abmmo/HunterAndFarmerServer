namespace MainServer.Models.Dtos
{
    public class RoomDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int WaitingPlayersCount { get; set; }
    }
}
