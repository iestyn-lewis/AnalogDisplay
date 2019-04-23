using System;
using Microsoft.Win32;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for CPUMonitor.
	/// </summary>
	public class EmailMonitor : Monitor
	{
		// email
		RegistryKey oKey; 

		public EmailMonitor()
		{
			oKey = Registry.CurrentUser;
			oKey = oKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\UnreadMail\ilewis@pharm.emory.edu");
			frequency = 500;
		}

		protected override double GetNextValue()
		{
            double ret = 0;
            if (oKey != null)
            {
                ret = Convert.ToDouble(oKey.GetValue("MessageCount"));
            }
            return ret;
		}
	}
}
