using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for ActiveWireAdapter.
	/// </summary>
	public class ActiveWireAdapter : USBAdapter
	{
		private AxAWUSBIOLib.AxAwusbIO axUsb;
		protected int[] outPinValues;				// current state of output pins
		private static int[] pinAddresses;		// the numeric values of the pins
		private static int[] latchpins;			// 4 pins are used to latch 

		public ActiveWireAdapter()
		{
			// fill in pin values, addressed from 1 to 65580
			pinAddresses = new int[16];
			for (int x = 0; x < pinAddresses.Length; x++) 
			{
				if (x == 0) 
				{
					pinAddresses[x] = 1;
				}
				else
				{
					pinAddresses[x] = pinAddresses[x-1] * 2;
				}
			}
			// fill in latchpins
			latchpins = new int[4];
			latchpins[0] = 8;
			latchpins[1] = 9;
			latchpins[2] = 10;
			latchpins[3] = 13;
			// init array to hold latchvalues
			outPinValues = new int[4];
		}

		public AxAWUSBIOLib.AxAwusbIO ActiveXObject
		{
			set
			{
				axUsb = value;
			}
		}

		public override string Open()
		{
			string s;
			s = axUsb.Open(0);
			if (s != "") 
			{
				return s;
			}
			else
			{
				// open all ports for output and set to 0
				axUsb.EnablePort(65535);
				return axUsb.OutPort(0);
			}
		}

		public override string Close()
		{
			// set everything back to 0 and close
			axUsb.OutPort(pinAddresses[latchpins[0]] + pinAddresses[latchpins[1]] + pinAddresses[latchpins[2]] + pinAddresses[latchpins[3]]);
			axUsb.OutPort(0);
			return axUsb.Close();
		}

		public override string SetValue(int port, int address, int input)
		{
			int mask;
			switch (address) 
			{
				case 0:
					// low 5 bits, I/O pins 0-4, used for 5 bit output
					// save bits 5-7 and set new value
					mask = pinAddresses[5] + pinAddresses[6] + pinAddresses[7];
					break;
				case 1:
					// I/O pin 5, single bit output
					mask = pinAddresses[0] + pinAddresses[1] + pinAddresses[2] + pinAddresses[3] + pinAddresses[4] + pinAddresses[6] + pinAddresses[7];
					input = input * pinAddresses[5];
					break;
				case 2:
					// I/O pin 6, single bit output
					mask = pinAddresses[0] + pinAddresses[1] + pinAddresses[2] + pinAddresses[3] + pinAddresses[4] + pinAddresses[5] + pinAddresses[7];
					input = input * pinAddresses[6];
					break;
				case 3:
					// I/O pin 7, single bit output
					mask = pinAddresses[0] + pinAddresses[1] + pinAddresses[2] + pinAddresses[3] + pinAddresses[4] + pinAddresses[5] + pinAddresses[6];
					input = input * pinAddresses[7];
					break;
				default:
					return "";
			}
			outPinValues[port] = (outPinValues[port] & mask) + input;
			// add in latchpin
			int output;
			int latchadd = pinAddresses[latchpins[port]];
			output = outPinValues[port] + latchadd;
			axUsb.OutPort(output);
			// now set latch low to save
			output = outPinValues[port];
			return axUsb.OutPort(output);
		}

	}
}
