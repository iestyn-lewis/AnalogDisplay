using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for HWDeviceMeter.
	/// </summary>
	public class HWDeviceMeter : HardwareDevice
	{
		
		public HWDeviceMeter():base()
		{
			miDeviceDirection = HardwareDeviceDirection.Output;
			miResolution = 1;		
			// 5-bit
			max = 31;
			min = 0;
		}

		public HWDeviceMeter(string aname):this()
		{
			name = aname;
		}

	}
}
