namespace MainServer.Models.Dtos
{
    public class RoomDetailDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required List<WaitingPlayerDto> Players { get; set; }
        public required string TokenPlayerAsHost {  get; set; }
        public DateTime? StartTime { get; set; }
    }
}
