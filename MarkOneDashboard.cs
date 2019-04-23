using System;

namespace AnalogDisplay
{
	/// <summary>
	/// Class describing the Mark One hardware, using an ActiveWire USB board.
	/// 4 Meters
	/// 4 Lights
	/// </summary>
	public class MarkOneDashboard: HardwareDashboard
	{
		private AxAWUSBIOLib.AxAwusbIO axAwusbIO1;

		public MarkOneDashboard()
		{
			//
			// TODO: Add constructor logic here
			//
			msName = "Mark One Analog Dashboard";
		}
	}
}
