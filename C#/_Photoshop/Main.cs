using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MyPhotoshop
{
	class MainClass
	{
        [STAThread]
		public static void Main (string[] args)
		{
			var window=new MainWindow();
			window.AddFilter(new PixelFilter<LighteningParameters>(
				"Осветление/затемнеие",
				(pixel, parameters) => pixel * parameters.Coefficient
				));

			window.AddFilter(new PixelFilter<GrayParameters>(
				"Контрастность",
				(pixel, parameters) =>
                {
					var pixelBrightness = (pixel.R + pixel.G + pixel.B) / 3;
					return new Pixel(
						Pixel.Trim(pixel.R + (pixelBrightness - pixel.R) * (1 - parameters.Coefficient)),
						Pixel.Trim(pixel.G + (pixelBrightness - pixel.G) * (1 - parameters.Coefficient)),
						Pixel.Trim(pixel.B + (pixelBrightness - pixel.B) * (1 - parameters.Coefficient))
						);
				}));

            window.AddFilter(new TransformFilter(
                "Отразить по горизонтали",
                size => size,
                (point, size) => new Point(size.Width - point.X - 1, point.Y)
                ));

            window.AddFilter(new TransformFilter(
                "Повернуть",
                size => new Size(size.Height, size.Width),
                (point, size) => new Point(point.Y, point.X)
                ));

            window.AddFilter(new TransformFilter<RotationParameters>("Свободное вращение", new RotateTransformer()));

			Application.Run (window);
		}
	}
}
