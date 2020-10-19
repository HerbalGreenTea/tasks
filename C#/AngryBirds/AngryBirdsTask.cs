using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
        // Ниже — это XML документация, её использует ваша среда разработки, 
        // чтобы показывать подсказки по использованию методов. 
        // Но писать её естественно не обязательно.
        /// <param name="v">Начальная скорость</param>
        /// <param name="distance">Расстояние до цели</param>
        /// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>

        public static double FindSightAngle(double v, double distance)
        {
            const double G = 9.8;
            double angleDeg = Math.Asin((distance * G) / v * v) / 2; // угол в градусах
            double angleRad = (angleDeg / 180) * Math.PI; // угол в радианах 

            if ((angleRad <= Math.PI / 2) && (angleRad >= 0))
                return angleRad;

            return double.NaN;
        }
    }
}