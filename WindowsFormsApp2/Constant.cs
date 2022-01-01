using System;
using System.Drawing;

namespace WindowsFormsApp2
{
	internal class Constant
	{
		public static string exeName;

		public static WindowsFormsApp2.Signature enemySignature;

		public static WindowsFormsApp2.Signature gameStatusSignature;

		public static WindowsFormsApp2.Signature petUseFlagSignature;

		public static WindowsFormsApp2.Signature battleButtonSignature;

		public static WindowsFormsApp2.Signature menuFlagSignature;

		public static WindowsFormsApp2.Signature autoBattleFlagSignature;

		public static WindowsFormsApp2.Signature battleCommandPointerSignature1;

		public static WindowsFormsApp2.Signature battleCommandPointerSignature2;

		public static Rectangle[] enemiesPosition;

		public static Rectangle catchPosition;

		public static Rectangle runPosition;

		public static Rectangle autoBattlePosition;

		public static Rectangle[] autoEncounterPosition;

		public static Rectangle[] storagePosition;

		static Constant()
		{
			Constant.exeName = "LucyStoneAge";
			Constant.enemySignature = new WindowsFormsApp2.Signature("xxxxx?xxx????xx?xx?xx?xxx????xx?xxx", new byte[] { 131, 236, 12, 139, 69, 0, 139, 12, 133, 0, 0, 0, 0, 139, 81, 0, 137, 85, 0, 139, 69, 0, 139, 12, 133, 0, 0, 0, 0, 139, 81, 0, 131, 234, 80 }, 9);
			Constant.gameStatusSignature = new WindowsFormsApp2.Signature("xxxxxxxx????xx?xx", new byte[] { 85, 139, 236, 131, 236, 28, 131, 61, 0, 0, 0, 0, 9, 116, 0, 51, 192 }, 8);
			Constant.petUseFlagSignature = new WindowsFormsApp2.Signature("xxx????xxxxx????xx????xxxxxx????xxxx????", new byte[] { 15, 191, 145, 0, 0, 0, 0, 131, 250, 1, 15, 133, 0, 0, 0, 0, 199, 133, 0, 0, 0, 0, 0, 0, 0, 0, 139, 133, 0, 0, 0, 0, 15, 191, 12, 69, 0, 0, 0, 0 }, 3);
			Constant.battleButtonSignature = new WindowsFormsApp2.Signature("xxxxx????xxxxxx????xxxxxx????xxxxx?xx????xxx", new byte[] { 193, 226, 0, 199, 130, 0, 0, 0, 0, 1, 0, 0, 0, 199, 5, 0, 0, 0, 0, 1, 0, 0, 0, 199, 133, 0, 0, 0, 0, 0, 0, 0, 0, 235, 0, 139, 133, 0, 0, 0, 0, 131, 192, 1 }, 15);
			Constant.menuFlagSignature = new WindowsFormsApp2.Signature("xxxxxxx????xxxxxx?xxxxxxxx", new byte[] { 85, 139, 236, 131, 236, 16, 161, 0, 0, 0, 0, 37, 0, 0, 0, 64, 117, 0, 185, 4, 0, 0, 0, 107, 209, 0 }, 7);
			Constant.autoBattleFlagSignature = new WindowsFormsApp2.Signature("xx????xx?xx?xxxxx?xx?xxxxxxxx?x", new byte[] { 131, 61, 0, 0, 0, 0, 0, 116, 0, 199, 69, 0, 4, 0, 0, 0, 235, 0, 199, 69, 0, 0, 0, 0, 0, 106, 1, 139, 77, 0, 232 }, 2);
			Constant.battleCommandPointerSignature1 = new WindowsFormsApp2.Signature("xx????????x????xxx?xx????xx????xx?x????xx?xxxxx", new byte[] { 199, 5, 0, 0, 0, 0, 0, 0, 0, 0, 233, 0, 0, 0, 0, 131, 125, 228, 0, 15, 133, 0, 0, 0, 0, 131, 61, 0, 0, 0, 0, 0, 116, 0, 161, 0, 0, 0, 0, 139, 72, 0, 186, 4, 0, 0, 0 }, 6);
			Constant.battleCommandPointerSignature2 = new WindowsFormsApp2.Signature("xxxx?xx?xxx????xx????x????xx????????xx?xxxxxxxxxxxxxxxxxxxxxx", new byte[] { 209, 224, 139, 77, 0, 139, 81, 0, 139, 4, 133, 0, 0, 0, 0, 137, 130, 0, 0, 0, 0, 233, 0, 0, 0, 0, 199, 5, 0, 0, 0, 0, 0, 0, 0, 0, 139, 69, 0, 139, 229, 93, 195, 204, 204, 204, 204, 204, 85, 139, 236, 131, 236, 8, 184, 1, 0, 0, 0, 133, 192 }, 32);
			Constant.enemiesPosition = new Rectangle[] { new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 10, 10) };
			Constant.catchPosition = new Rectangle(862, 26, 64, 58);
			Constant.runPosition = new Rectangle(930, 87, 64, 58);
			Constant.autoBattlePosition = new Rectangle(986, 704, 28, 28);
			Constant.autoEncounterPosition = new Rectangle[] { new Rectangle(734, 745, 140, 25), new Rectangle(695, 532, 88, 88) };
			Constant.storagePosition = new Rectangle[] { new Rectangle(296, 745, 140, 25), new Rectangle(915, 302, 39, 84), new Rectangle(535, 323, 188, 47) };
		}

		public Constant()
		{
		}
	}
}