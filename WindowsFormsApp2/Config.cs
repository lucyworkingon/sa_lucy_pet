using System;

namespace WindowsFormsApp2
{
	internal class Config
	{
		private string name;

		private int minHp;

		private int maxHp;

		public Config(string name, string maskedTextBox)
		{
			this.name = name;
			string[] strArrays = maskedTextBox.Split(new char[] { '-' });
			this.minHp = int.Parse(strArrays[0]);
			this.maxHp = int.Parse(strArrays[1]);
		}

		public int getMaxHp()
		{
			return this.maxHp;
		}

		public int getMinHp()
		{
			return this.minHp;
		}

		public string getName()
		{
			return this.name;
		}

		public bool isInHpRange(int hp)
		{
			if (this.minHp > hp)
			{
				return false;
			}
			return hp <= this.maxHp;
		}

		public bool isNameMatch(string petname)
		{

			if (this.name.Length == 0)
			{
				return true;
			}
			if (name.Contains(","))
				{
				char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

				string[] words = name.Split(delimiterChars);
				bool flag = false;
                foreach (var item in words)
                {
					flag |= petname.Equals(item);

				}
				return flag;
			}
            else
            {

				return this.name.Equals(petname);
			}

		}
	}
}