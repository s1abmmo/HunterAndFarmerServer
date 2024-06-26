namespace UDPServer.Models
{
    public class Tree
    {
        public required Position Position { get; set; }
        public DateTime? CutOffAt { get; set; }
        public int TreeType { get; set; }
    }
}
