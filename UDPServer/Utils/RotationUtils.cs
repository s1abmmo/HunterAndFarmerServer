using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer.Utils
{
    public class RotationUtils
    {
        private readonly static Vector3 axis = Vector3.UnitY;
        public static Quaternion AngleAxisToQuaternion(float angleInDegrees)
        {
            // Chuyển đổi góc quay từ độ sang radian
            float angleInRadians = MathF.PI / 180.0f * angleInDegrees;

            // Tạo Quaternion từ góc quay và trục quay
            return Quaternion.CreateFromAxisAngle(axis, angleInRadians);
        }

        public static void QuaternionToAngleAxis(Quaternion quaternion, out Vector3 axis, out float angleInDegrees)
        {
            // Đảm bảo quaternion được chuẩn hóa
            quaternion = Quaternion.Normalize(quaternion);

            // Tính toán góc quay (radian)
            float angleInRadians = 2.0f * MathF.Acos(quaternion.W);

            // Tính toán giá trị sin(theta / 2)
            float sinThetaOver2 = MathF.Sqrt(1.0f - quaternion.W * quaternion.W);

            // Trường hợp đặc biệt khi sin(theta / 2) rất nhỏ
            if (sinThetaOver2 < 0.001f)
            {
                axis = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);
            }
            else
            {
                axis = new Vector3(quaternion.X / sinThetaOver2, quaternion.Y / sinThetaOver2, quaternion.Z / sinThetaOver2);
            }

            // Chuyển đổi góc quay từ radian sang độ
            angleInDegrees = angleInRadians * (180.0f / MathF.PI);
        }

    }
}
