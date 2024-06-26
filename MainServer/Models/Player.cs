namespace MainServer.Models
{
    public class Player(string token)
    {
        public string Token { get; set; } = token;
        public string? AtRoomId { get; set; }
        public bool IsReady { get; set; } = false;
    }
}
