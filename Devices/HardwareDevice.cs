using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for HardwareDevice.
	/// </summary>
	/// 

	public enum HardwareDeviceDirection
	{
		Input = 0,
		Output = 1
	}

	public class HardwareDevice
	{

		protected HardwareDeviceDirection miDeviceDirection;		// input or output
		protected int miResolution;									// bits supported
		protected int miHardwareValue;								// actual value to output to device
		protected string name;
		protected int max;
		protected int min;
		protected int address;
		protected int port;
		protected USBAdapter usb;

		public HardwareDevice()
		{
			address = 0;
		}

		public HardwareDevice(string aname) : this()
		{
			name = aname;
		}

		public USBAdapter USB
		{
			set
			{
				usb = value;
			}
		}

		public int Address
		{
			set
			{
				address = value;
			}
			get
			{
				return address;
			}
		}

		public int Port
		{
			set
			{
				port = value;
			}
			get
			{
				return port;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public HardwareDeviceDirection DeviceDirection
		{
			get
			{
				return miDeviceDirection;
			}
		}

		public int Resolution
		{
			get
			{
				return miResolution;
			}
		}

		public int HardwareValue
		{
			get
			{
				return miHardwareValue;
			}
			set
			{
				if (value > max) 
				{
					miHardwareValue = max;
				} 
				else if (value < min) 
				{
					miHardwareValue = min;
				} 
				else 
				{
					miHardwareValue = value;
				}
				usb.SetValue(port, address, miHardwareValue);
			}
		}

		public int Max
		{
			get
			{
				return max;
			}
			set
			{
				max = value;
			}
		}

		public int Min
		{
			get
			{
				return min;
			}
			set
			{
				min = value;
			}
		}

		public virtual void Reset()
		{
			HardwareValue = 0;
		}
	
	}
}
