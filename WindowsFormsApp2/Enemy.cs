using System;
using System.Drawing;

namespace WindowsFormsApp2
{
	internal class Enemy
	{
		private string name;

		private int hp;

		private int level;

		private bool status;

		private Rectangle position;

		public Enemy(Rectangle position)
		{
			this.position = position;
		}

		public int getHp()
		{
			return this.hp;
		}

		public int getLevel()
		{
			return this.level;
		}

		public string getName()
		{
			return this.name;
		}

		public Rectangle getPosition()
		{
			return this.position;
		}

		public bool getStatus()
		{
			return this.status;
		}

		public void setHp(int hp)
		{
			this.hp = hp;
		}

		public void setLevel(int level)
		{
			this.level = level;
		}

		public void setName(string name)
		{
			this.name = name;
		}

		public void setStatus(int status)
		{
			this.status = (status == 84 || status == 92 ? true : false);
		}

		public void setX(int x)
		{
			this.position.X = x - 10;
		}

		public void setY(int y)
		{
			this.position.Y = y - 10;
		}
	}
}