﻿using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            var cos = (a * a + b * b - c * c) / (2.0 * a * b);

            if (Math.Abs(cos) <= 1 && a != 0 && b != 0 && c < a + b && c >= 0)
                return Math.Round(Math.Acos(cos), 15);
            else
                return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var actualAngle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(actualAngle, Math.Round(expectedAngle, 15));
        } 
    }
}