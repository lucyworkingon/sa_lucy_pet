using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;

namespace WindowsFormsApp2
{
	internal class WinApiWrapper : WinApi
	{
		private static WinApiWrapper instance;

		private Random rand = new Random();

		private int id;

		private IntPtr process;

		private IntPtr handle;

		private IntPtr baseAddress;

		private int memorySize;

		private IntPtr enemyAddress;

		private IntPtr gameStatusAddress;

		private IntPtr battleStatusAddress;

		private IntPtr petUseFlagAddress;

		private IntPtr battleButtonAddress;

		private IntPtr battleCommandPointerAddress1;

		private IntPtr battleCommandPointerAddress2;

		private IntPtr autoEncounterAddress;

		private IntPtr autoBattleAddress;

		private IntPtr menuFlagAddress;

		private Point mouse;

		protected WinApiWrapper(Process instance)
		{
            IntPtr intPtr;
            this.id = instance.Id;
            this.handle = instance.MainWindowHandle;
            this.process = WinApi.OpenProcess(56, IntPtr.Zero, this.id);
            this.baseAddress = instance.MainModule.BaseAddress;
            this.memorySize = instance.MainModule.ModuleMemorySize;
            byte[] numArray = new byte[this.memorySize];
            WinApi.ReadProcessMemory(this.process, this.baseAddress, numArray, this.memorySize, out intPtr);
            this.enemyAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.enemySignature);
            this.gameStatusAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.gameStatusSignature);
            this.petUseFlagAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.petUseFlagSignature);
            this.battleButtonAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.battleButtonSignature);
            this.menuFlagAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.menuFlagSignature);
            this.autoEncounterAddress = Scanner.findSignature(numArray, this.baseAddress, Constant.autoBattleFlagSignature);
            this.battleCommandPointerAddress1 = Scanner.findSignature(numArray, this.baseAddress, Constant.battleCommandPointerSignature1);
            this.battleCommandPointerAddress2 = Scanner.findSignature(numArray, this.baseAddress, Constant.battleCommandPointerSignature2);
            this.enemyAddress = this.dereference(this.enemyAddress, 4) + Offset.enemyStart;
            this.gameStatusAddress = this.dereference(this.gameStatusAddress, 4);
            this.battleStatusAddress = IntPtr.Subtract(this.gameStatusAddress, 4);
            this.battleButtonAddress = this.dereference(this.battleButtonAddress, 4);
            this.menuFlagAddress = this.dereference(this.menuFlagAddress, 4);
            this.autoEncounterAddress = this.dereference(this.autoEncounterAddress, 4);
            this.autoBattleAddress = this.autoEncounterAddress + 32;
            Offset.petStructSize = this.readInt(IntPtr.Subtract(this.petUseFlagAddress, 4 + Constant.petUseFlagSignature.offset));
            this.petUseFlagAddress = this.dereference(this.petUseFlagAddress, 4);
        }

		public void animationHackLoop()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (true)
			{
				try
				{
					int batleStatus = this.getBatleStatus();
					if (batleStatus != 5 && batleStatus != 2)
					{
						stopwatch.Restart();
					}
					else if (stopwatch.ElapsedMilliseconds >= (long)1000)
					{
						this.writeInt(this.battleStatusAddress, 7);
						stopwatch.Restart();
					}
					Thread.Sleep(100);
				}
				catch (Exception exception)
				{
				}
			}
		}

		public void catchEnemy(Enemy enemy)
		{
			if (!this.isCatchAlreadyOn())
			{
				this.moveMouseAndClick(Constant.catchPosition);
			}
			this.moveMouseAndClick(enemy.getPosition());
		}

		private void clickMouse()
		{
			WinApi.PostMessage(this.handle, 513, IntPtr.Zero, (IntPtr)(this.mouse.Y << 16 | this.mouse.X & 65535));
			Thread.Sleep(100);
			WinApi.PostMessage(this.handle, 514, IntPtr.Zero, (IntPtr)(this.mouse.Y << 16 | this.mouse.X & 65535));
			Thread.Sleep(50);
		}

		public void clickRunButton()
		{
			this.moveMouseAndClick(Constant.runPosition);
		}

		private IntPtr dereference(IntPtr address, int size = 4)
		{
			IntPtr intPtr;
			byte[] numArray = new byte[size];
			WinApi.ReadProcessMemory(this.process, address, numArray, (int)numArray.Length, out intPtr);
			if (size == 2)
			{
				return (IntPtr)BitConverter.ToInt16(numArray, 0);
			}
			return (IntPtr)BitConverter.ToInt32(numArray, 0);
		}

		public int getBatleStatus()
		{
			return this.readInt(this.battleStatusAddress);
		}

		public int getGameStatus()
		{
			return this.readInt(this.gameStatusAddress);
		}

		public static WinApiWrapper getInstance(Process process)
		{
			if (WinApiWrapper.instance == null)
			{
				WinApiWrapper.instance = new WinApiWrapper(process);
			}
			return WinApiWrapper.instance;
		}

		public int getPetCount()
		{
			int num = 0;
			for (int i = 0; i < 5; i++)
			{
				if (this.readShort(this.petUseFlagAddress + i * Offset.petStructSize) == 1)
				{
					num++;
				}
			}
			return num;
		}

		public void initMouse()
		{
			this.mouse = new Point(this.rand.Next(0, 1024), this.rand.Next(0, 768));
			Rectangle rectangle = new Rectangle(this.mouse, new Size(1, 1));
			WinApi.PostMessage(this.handle, 512, IntPtr.Zero, (IntPtr)(rectangle.Y << 16 | rectangle.X & 65535));
		}

		public bool isAutoBattleAlreadyOn()
		{
			return this.readInt(this.autoBattleAddress) == 516980;
		}

		public bool isAutoEncounterAlreadyOn()
		{
			return this.readInt(this.autoEncounterAddress) == 1;
		}

		public bool isCatchAlreadyOn()
		{
			return this.readInt(this.battleButtonAddress) == 2;
		}

		public bool isCommandInputNow()
		{
			return this.readInt(this.battleStatusAddress) == 4;
		}

		public bool isOnBattleNow()
		{
			return this.getGameStatus() == 10;
		}

		public void moveMouse(Rectangle rect)
		{
			Point point = new Point()
			{
				X = this.rand.Next(rect.X, rect.Right),
				Y = this.rand.Next(rect.Y, rect.Bottom)
			};
			while (this.mouse.X != point.X || this.mouse.Y != point.Y)
			{
				if (this.mouse.X > point.X)
				{
					ref Point x = ref this.mouse;
					x.X = x.X - 1;
				}
				if (this.mouse.X < point.X)
				{
					ref Point pointPointer = ref this.mouse;
					pointPointer.X = pointPointer.X + 1;
				}
				if (this.mouse.Y > point.Y)
				{
					ref Point y = ref this.mouse;
					y.Y = y.Y - 1;
				}
				if (this.mouse.Y < point.Y)
				{
					ref Point y1 = ref this.mouse;
					y1.Y = y1.Y + 1;
				}
				WinApi.PostMessage(this.handle, 512, IntPtr.Zero, (IntPtr)(this.mouse.Y << 16 | this.mouse.X & 65535));
				Thread.Sleep(1);
			}
		}

		public void moveMouseAndClick(Rectangle rect)
		{
			this.moveMouse(rect);
			this.clickMouse();
		}

		public void openPetStorage()
		{
			this.setMenuFlag(WinApiWrapper.MenuFlag.None);
			this.moveMouseAndClick(Constant.storagePosition[0]);
			this.moveMouseAndClick(Constant.storagePosition[1]);
		}

		private byte[] readArray(IntPtr address, int size)
		{
			IntPtr intPtr;
			byte[] numArray = new byte[size];
			WinApi.ReadProcessMemory(this.process, address, numArray, (int)numArray.Length, out intPtr);
			return numArray;
		}

		private int readInt(IntPtr address)
		{
			IntPtr intPtr;
			byte[] numArray = new byte[4];
			WinApi.ReadProcessMemory(this.process, address, numArray, (int)numArray.Length, out intPtr);
			return BitConverter.ToInt32(numArray, 0);
		}

		private short readShort(IntPtr address)
		{
			IntPtr intPtr;
			byte[] numArray = new byte[2];
			WinApi.ReadProcessMemory(this.process, address, numArray, (int)numArray.Length, out intPtr);
			return BitConverter.ToInt16(numArray, 0);
		}

		public void refreshEnemy(ref Enemy enemy, int offset)
		{
			IntPtr intPtr = this.dereference(this.enemyAddress + offset * 4, 4);
			byte[] numArray = this.readArray(IntPtr.Add(intPtr, Offset.name), 32);
			enemy.setName(Encoding.GetEncoding(949).GetString(numArray, 0, (int)numArray.Length).Replace("\0", string.Empty));
			enemy.setHp(this.readInt(IntPtr.Add(intPtr, Offset.hp)));
			enemy.setLevel(this.readInt(IntPtr.Add(intPtr, Offset.level)));
			enemy.setStatus(this.readInt(IntPtr.Add(intPtr, Offset.status)));
			enemy.setX(this.readInt(IntPtr.Add(intPtr, Offset.x)));
			enemy.setY(this.readInt(IntPtr.Add(intPtr, Offset.y)));
		}

		public void removeAnimation(bool toggle)
		{
			if (toggle)
			{
				this.writeInt(this.battleCommandPointerAddress1, -1);
				this.writeInt(this.battleCommandPointerAddress2, -1);
				return;
			}
			this.writeInt(this.battleCommandPointerAddress1, 0);
			this.writeInt(this.battleCommandPointerAddress2, 0);
		}

		public void setAutoBattle(bool toggle)
		{
			if (toggle)
			{
				if (!this.isAutoBattleAlreadyOn())
				{
					this.setMenuFlag(WinApiWrapper.MenuFlag.None);
					this.moveMouseAndClick(Constant.autoBattlePosition);
					return;
				}
			}
			else if (this.isAutoBattleAlreadyOn())
			{
				this.setMenuFlag(WinApiWrapper.MenuFlag.None);
				this.moveMouseAndClick(Constant.autoBattlePosition);
			}
		}

		public void setAutoEncounter(bool toggle)
		{
            Console.WriteLine(this.isAutoEncounterAlreadyOn());
			if (toggle)
			{
				if (!this.isAutoEncounterAlreadyOn())
				{
					this.setMenuFlag(WinApiWrapper.MenuFlag.None);
					this.moveMouseAndClick(Constant.autoEncounterPosition[0]);
					this.moveMouseAndClick(Constant.autoEncounterPosition[1]);
					return;
				}
			}
			else //if (this.isAutoEncounterAlreadyOn())
			{
				this.setMenuFlag(WinApiWrapper.MenuFlag.None);
				this.moveMouseAndClick(Constant.autoEncounterPosition[0]);
				this.moveMouseAndClick(Constant.autoEncounterPosition[1]);
			}
		}

		private void setMenuFlag(WinApiWrapper.MenuFlag menu)
		{
			this.writeInt(this.menuFlagAddress, 0);
			Thread.Sleep(250);
			this.writeInt(this.menuFlagAddress, (int)menu);
			Thread.Sleep(1000);
		}

		public void storePetToStorage()
		{
			while (this.getPetCount() > 1)
			{
				this.moveMouseAndClick(Constant.storagePosition[2]);
			}
		}

		private void writeInt(IntPtr address, int value)
		{
			int num;
			WinApi.WriteProcessMemory(this.process, address, BitConverter.GetBytes(value), (int)BitConverter.GetBytes(value).Length, out num);
		}

		private enum BattleStatus
		{
			None,
			BattleWait,
			BattleCommand
		}

		private enum GameStatus
		{
			Game = 9,
			Battle = 10
		}

		public enum MenuFlag
		{
			Esc = -2147483648,
			None = 0,
			Pet = 268435456
		}
	}
}