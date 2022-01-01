using System;

namespace WindowsFormsApp2
{
	internal class Offset
	{
		public static int enemyStart;

		public static int name;

		public static int hp;

		public static int level;

		public static int status;

		public static int x;

		public static int y;

		public static int petStructSize;

		static Offset()
		{
			Offset.enemyStart = 60;
			Offset.name = 56;
			Offset.hp = 120;
			Offset.level = 136;
			Offset.status = 160;
			Offset.x = 24;
			Offset.y = 28;
			Offset.petStructSize = 0;
		}

		public Offset()
		{
		}
	}
}