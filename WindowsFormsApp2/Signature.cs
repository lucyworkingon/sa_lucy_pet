using System;

namespace WindowsFormsApp2
{
	public class Signature
	{
		public string mask;

		public byte[] data;

		public int offset;

		public Signature(string mask, byte[] data, int offset)
		{
			this.mask = mask;
			this.data = data;
			this.offset = offset;
		}
	}
}