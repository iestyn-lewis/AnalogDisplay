using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for DiskMonitor.
	/// </summary>
	public class DiskMonitor : Monitor
	{
		private System.Diagnostics.PerformanceCounter pm;

		public DiskMonitor()
		{
			pm = new System.Diagnostics.PerformanceCounter("LogicalDisk", "Disk Bytes/sec");
			pm.InstanceName = "_Total";
			frequency = 500;
			
		}

		protected override double GetNextValue()
		{
			return pm.NextValue();
		}
	}
}
