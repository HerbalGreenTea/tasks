using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static float Upper = Manipulator.UpperArm;
        public static float Forearm = Manipulator.Forearm;
        public static float Palm = Manipulator.Palm;

        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristY = Math.Sin(alpha) * Manipulator.Palm;
            var wristX = Math.Sin(Math.PI / 2 - alpha) * Palm;
            wristX = x - wristX;
            wristY = y + wristY;

            var elbow = TriangleTask.GetABAngle(Upper, Forearm, Math.Sqrt(wristX * wristX + wristY * wristY));
            var angle = TriangleTask.GetABAngle(Math.Sqrt(wristX * wristX + wristY * wristY), Upper, Forearm);
            var lineAngle = Math.Atan2(wristY, wristX);

            var shoulder = angle + lineAngle;
            var wrist = -alpha - shoulder - elbow;

            if (shoulder != double.NaN && elbow != double.NaN && wrist != double.NaN)
                return new[]
                {
                    shoulder, elbow, wrist
                };
            else
                return new[]
                {
                    double.NaN, double.NaN, double.NaN
                };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [TestCase(new double[] { Math.PI / 2, Math.PI / 2, -3 * Math.PI / 2}, 120d, 60d, Math.PI / 2)]
        public void TestMoveManipulatorTo(double[] expectedAngle, double x, double y, double alpha)
        {
            var actualAngle = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(expectedAngle[i], actualAngle[i], 0.3); 
        } 
    }
}