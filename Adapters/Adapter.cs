using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for Adapter.
	/// </summary>
	public class Adapter
	{
		protected HardwareDevice device;
		protected string name;
		protected double currValue;
		
		public Adapter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Adapter(string aname)
		{
			name = aname;
		}

		public double CurrentValue
		{
			get
			{
				return currValue;
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
		
		public HardwareDevice Device
		{
			get
			{
				return device;
			}
			set
			{
				device = value;
			}
		}

		public virtual void SetDeviceValue (double input)
		{
			// by default, pass cast value straight into hardware device
			currValue = input;
			device.HardwareValue  = TranslateValue(input);
		}

		public virtual int TranslateValue (double input)
		{
			return (int) input;
		}
	}
}
