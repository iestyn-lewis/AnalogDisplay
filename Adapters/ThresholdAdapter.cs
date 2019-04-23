using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Threshold Adapter takes an input and outputs a series of thresholded values
	/// </summary>
	public class ThresholdAdapter : Adapter
	{
		public ThresholdAdapter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ThresholdAdapter(string aname):this()
		{
			name = aname;
		}

		public override int TranslateValue (double input)
		{
			// simply translate value into a value for the device
			if (input > 0) 
			{
				return (device.Max - device.Min) / 2;
			} 
			else
			{
				return 0;
			}
		}

	}
}
