namespace MainServer.Models.Dtos
{
    public class SetPlayerReadyRequest
    {
        public required string PlayerToken { get; set; }
        public bool IsReady { get; set; }
    }
}
