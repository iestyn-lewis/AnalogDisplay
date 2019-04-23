using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for SingleLED.
	/// </summary>
	public class SingleLEDDevice : HardwareDevice
	{
	
		public SingleLEDDevice():base()
		{
			miDeviceDirection = HardwareDeviceDirection.Output;
			miResolution = 1;		// 1-bit
			max = 1;
			min = 0;
		}

		public SingleLEDDevice(string aname):this()
		{
			name = aname;
		}

	}
}