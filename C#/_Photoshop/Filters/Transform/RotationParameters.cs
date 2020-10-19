using System;
using System.Drawing;
using System.Runtime.Remoting.Messaging;

namespace MyPhotoshop
{
	public class RotationParameters : IParameters
	{
		[ParameterInfo(Name = "Угол", MaxValue = 360, MinValue = 0, Increment = 5, DefaultValue = 0)]
		public double Angle { get; set; }
    }
}

