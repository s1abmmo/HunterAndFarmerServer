using System.Net;
using System.Numerics;
using UDPServer.Enums;

namespace UDPServer.Models
{
    public class Player(string id, float someThreshold, IPEndPoint endPoint)
    {
        public string Id { get; set; } = id;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public float Speed { get; set; } = 1f;
        public float SomeThreshold { get; set; } = someThreshold;
        public IPEndPoint EndPoint { get; set; } = endPoint;
        public PlayerStateEnum PlayerState { get; set; } = PlayerStateEnum.IDLE;
        public Quaternion Rotation { get; set; }
        public float Angle { get; set; }
        public DateTime LastTimeUpdatePosition { get; set; } = DateTime.Now;
    }
}
