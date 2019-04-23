using System;
using System.Runtime.InteropServices;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for DiskMonitor.
	/// </summary>
	public struct MemoryStatus 
	{

		public uint Length; //Length of struct
		public uint MemoryLoad; //Value from 0-100 represents memory usage
		public uint TotalPhysical;
		public uint AvailablePhysical;
		public uint TotalPageFile;
		public uint AvailablePageFile;
		public uint TotalVirtual;
		public uint AvailableVirtual;

	}

	public class MemoryMonitor : Monitor
	{
		private System.Diagnostics.PerformanceCounter pm;

		[DllImport("kernel32.dll")]
		public static extern void GlobalMemoryStatus(ref MemoryStatus stat);

		public MemoryMonitor()
		{
			pm = new System.Diagnostics.PerformanceCounter("Memory", "Pages/sec");
			frequency = 500;			
		}

		protected override double GetNextValue()
		{
			//MemoryStatus stat = new MemoryStatus();
			//GlobalMemoryStatus(ref stat);
			//return stat.MemoryLoad;
			return pm.NextValue();
		}
	}
}
