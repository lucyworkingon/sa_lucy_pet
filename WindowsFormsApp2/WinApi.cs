using System;
using System.Runtime.InteropServices;

namespace WindowsFormsApp2
{
	internal class WinApi
	{
		protected const int PROCESS_VM_READ = 16;

		protected const int PROCESS_VM_WRITE = 32;

		protected const int PROCESS_VM_OPERATION = 8;

		protected const uint WM_KEYDOWN = 256;

		protected const uint WM_KEYUP = 257;

		protected const uint WM_MOUSEMOVE = 512;

		protected const uint WM_LBUTTONDOWN = 513;

		protected const uint WM_LBUTTONUP = 514;

		public WinApi()
		{
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		protected static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		protected static extern IntPtr OpenProcess(int dwDesiredAccess, IntPtr bInheritHandle, int dwProcessId);

		[DllImport("User32.Dll", CharSet=CharSet.None, EntryPoint="PostMessageA", ExactSpelling=false, SetLastError=true)]
		protected static extern bool PostMessage(IntPtr hProcess, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
        //		protected static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] object lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
        protected static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		protected static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);
	}
	
}