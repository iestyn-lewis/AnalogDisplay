using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for CPUMonitor.
	/// </summary>
	public class CPUMonitor : Monitor
	{
		private System.Diagnostics.PerformanceCounter pm;

		public CPUMonitor()
		{
			pm = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time");
			pm.InstanceName = "_Total";
			frequency = 500;
		}

		protected override double GetNextValue()
		{
			return pm.NextValue();
		}
	}
}
