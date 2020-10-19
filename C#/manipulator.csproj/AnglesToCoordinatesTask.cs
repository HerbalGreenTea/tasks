using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            elbow = elbow - (Math.PI - shoulder);
            wrist = wrist - (Math.PI - elbow);


            var x1 = CalculeteCoordinatesX(shoulder, Manipulator.UpperArm);
            var y1 = CalculeteCoordinatesY(shoulder, Manipulator.UpperArm);
            var x2 = x1 + CalculeteCoordinatesX(elbow, Manipulator.Forearm);
            var y2 = y1 + CalculeteCoordinatesY(elbow, Manipulator.Forearm);
            var x3 = x2 + CalculeteCoordinatesX(wrist, Manipulator.Palm);
            var y3 = y2 + CalculeteCoordinatesY(wrist, Manipulator.Palm);

            var elbowPos = new PointF((float)x1 ,(float)y1);
            var wristPos = new PointF((float)x2, (float)y2);
            var palmEndPos = new PointF((float)x3, (float)y3);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        public static double CalculeteCoordinatesX (double angle, double line)
        {
            return Math.Cos(angle) * line;
        }

        public static double CalculeteCoordinatesY(double angle, double line)
        {
            return Math.Sin(angle) * line;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(Manipulator.UpperArm, CalculateHypotenuse(joints[0].X, 0, joints[0].Y, 0));
        } 

        public double CalculateHypotenuse(float x1, float x0, float y1, float y0)
        {
            return Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
        }
    }
}