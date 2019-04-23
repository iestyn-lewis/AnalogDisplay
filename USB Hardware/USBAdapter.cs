using System;

namespace AnalogDisplay
{
	/// <summary>
	/// A wrapper to encapsulate the functionality of various USB adaptors
	/// </summary>
	public class USBAdapter
	{
		protected string name;

		public USBAdapter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public virtual string Open()
		{
			return "";
		}

		public virtual string Close()
		{
			return "";
		}

		public virtual string SetValue(int port, int address, int input)
		{
			return "";
		}

		public virtual int ReadValue(int address)
		{
			return 0;
		}
	}
}
