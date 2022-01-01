using System;

namespace WindowsFormsApp2
{
	internal class Scanner
	{
		public Scanner()
		{
		}

		public static IntPtr findSignature(byte[] data, IntPtr baseAddess, WindowsFormsApp2.Signature signature)
		{
			char[] charArray = signature.mask.TrimEnd(new char[0]).ToCharArray();
			for (int i = 0; i < (int)data.Length; i++)
			{
				bool flag = false;
				int num = 0;
				while (num < (int)charArray.Length)
				{
					if ((charArray[num] != 'x' || signature.data[num] != data[i + num]) && charArray[num] != '?')
					{
						flag = false;
						i += num;
						break;
					}
					else
					{
						flag = true;
						num++;
					}
				}
				if (flag)
				{
					return IntPtr.Add(baseAddess, i + signature.offset);
				}
			}
			return IntPtr.Zero;
		}
	}
}