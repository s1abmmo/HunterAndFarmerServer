using System.Numerics;

namespace UDPServer.Utils
{
    public class MovementUtils
    {
        public static Vector3 CalculateNewPosition(Quaternion rotation, float speed, Vector3 currentPosition, float deltaTime)
        {
            // Định nghĩa hướng forward
            Vector3 forward = new Vector3(0, 0, 1);

            // Hướng di chuyển từ Quaternion Rotation
            Vector3 direction = Vector3.Transform(forward, rotation);

            // Vị trí mới = Vị trí hiện tại + (Hướng di chuyển * Tốc độ * Thời gian)
            Vector3 newPosition = currentPosition + direction * speed * deltaTime;

            return newPosition;
        }

    }
}
