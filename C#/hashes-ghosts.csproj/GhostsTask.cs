using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
	{
		private Random random = new Random();
		private static byte[] content = new byte[] { 1, 0, 0, 1 };

		private static Vector vector = new Vector(1, 1);
		private Segment segment = new Segment(vector, new Vector(0,0));
		private Robot robot = new Robot("0", 1);
		private Cat cat = new Cat("cat", "cat", new DateTime(1,1,1));
		private Document document = new Document("doc", Encoding.UTF8, content);

		public void DoMagic()
		{
			vector.Add(new Vector(5,5));
			Robot.BatteryCapacity = Robot.BatteryCapacity + 1;
			cat.Rename(random.Next(0,1000).ToString());
			content[random.Next(0,content.Length - 1)] = (byte)random.Next(0,50);
		}

		Vector IFactory<Vector>.Create()
		{
			return vector;
		}

		Segment IFactory<Segment>.Create()
		{
			return segment;
		}

		Document IFactory<Document>.Create()
		{
			return document;
		}

		Cat IFactory<Cat>.Create()
		{
			return cat;
		}

		Robot IFactory<Robot>.Create()
		{
			return robot;
		}
	}
}