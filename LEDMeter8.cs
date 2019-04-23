using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Implements a meter from 0 to 8.  Takes a value from 0 to 100,
	/// and outputs a number from 0 to 255 which should be passed to 
	/// the USB device to switch on 0 to 8 LEDs in a bit pattern:
	/// 00001111 turns on the first 4 leds, and is decimal number 15.
	/// values of leds from 0 to 8 are:
	/// 0
	/// 1
	/// 3
	/// 7
	/// 15
	/// 31
	/// 95
	/// 127
	/// 255
	/// </summary>
	public class LEDMeter8
	{

		int[] ledValues;
		float[] inputValues;

		public LEDMeter8()
		{
			//
			// TODO: Add constructor logic here
			//
			ledValues = new int[] {1, 3, 7, 15, 31, 63, 127, 255};
			inputValues = new float[] {12.5f, 25f, 37.5f, 50f, 62.5f, 75f, 87.5f, 100f};
		}

		public int usbValue(float input)
		{
			if (input == 0) 
			{
				return 0;
			}
			for (int i = 0; i < ledValues.Length; i++) 
			{
				if (input <= inputValues[i]) 
				{
					return ledValues[i];
				}
			}
		   return 255; 
		}
	}
}
