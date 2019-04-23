using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for CPUMonitor.
	/// </summary>
	public class NetworkMonitor : Monitor
	{
		private System.Diagnostics.PerformanceCounter pm;

		public NetworkMonitor()
		{
            //System.Diagnostics.PerformanceCounterCategory cat = new System.Diagnostics.PerformanceCounterCategory("Network Interface");
            //string[] names = cat.GetInstanceNames();
			pm = new System.Diagnostics.PerformanceCounter();
			pm = new System.Diagnostics.PerformanceCounter("Network Interface", "Bytes Total/sec");
            pm.InstanceName = "Broadcom NetXtreme 57xx Gigabit Controller - Packet Scheduler Miniport";
			//pm.InstanceName = "Cisco Systems VPN Adapter - Deterministic Network Enhancer Miniport";
			//pm.InstanceName = "Broadcom 570x Gigabit Integrated Controller - Deterministic Network Enhancer Miniport";
			//pm.InstanceName = "Intel[R] PRO_Wireless LAN 2100 3A Mini PCI Adapter - Deterministic Network Enhancer Miniport";
			frequency = 500;
		}

		protected override double GetNextValue()
		{
            double ret = 0;
            try
            {
                ret = pm.NextValue();
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
		}
	}
}
