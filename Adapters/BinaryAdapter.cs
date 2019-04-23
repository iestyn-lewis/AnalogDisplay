using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for BinaryAdapter.
	/// </summary>
	public class BinaryAdapter : Adapter
	{
		private double onat;   // above this value, return 1, otherwise return 0.
		private double offat;

		public BinaryAdapter()
		{
			onat = 0;
			offat = 100;
		}

		public BinaryAdapter(string aname):this()
		{
			name = aname;
		}

		public double OnAt
		{
			get 
			{
				return onat;
			}
			set 
			{
				onat = value;
			}
		}

		public double OffAt
		{
			get 
			{
				return offat;
			}
			set 
			{
				offat = value;
			}
		}

		public override int TranslateValue (double input)
		{
			// simply translate value into yes-no
			if (input > onat && input < offat) 
			{
				return 1;
			} 
			else
			{
				return 0;
			}
		}

	}
}
