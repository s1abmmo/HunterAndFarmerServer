namespace MainServer.Models.Dtos
{
    public class WaitingPlayerDto
    {
        public required string TokenPlayer { get; set; }
        public bool IsReady { get; set; }
    }
}
