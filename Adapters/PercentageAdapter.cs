using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for PercentageAdapter.
	/// </summary>
	public class PercentageAdapter : Adapter
	{
		private double max;
		private double min;
		private bool reverse;

		public PercentageAdapter()
		{
			max = 100;
			min = 0;
		}

		public PercentageAdapter(string aname):this()
		{
			name = aname;
		}

		public bool Reverse
		{
			get
			{
				return reverse;
			}
			set
			{
				reverse = value;
			}
		}

		public double Max
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

		public double Min
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

		public override int TranslateValue (double input)
		{
			// simply translate value into a value for the device
			input = input > max ? max : input;
			input = input < min ? min : input;
			if (reverse)
			{
				input = max - input;
			}
			return (int)(((input - min) / max) * (device.Max - device.Min) + device.Min);
		}

	}
}
