using System;
using System.Collections;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for Monitor.
	/// </summary>
	public class Monitor
	{
		protected int frequency;			// frequency in milliseconds to pause between polls
		protected Hashtable subscribers;
		protected System.Windows.Forms.Timer timer;
		protected bool monitoring;
		protected double currentValue;
		
		// events
		public event TickHandler Tick;
		public EventArgs e = null;
		public delegate void TickHandler(Monitor m, EventArgs e);

		public Monitor()
		{
			subscribers = new Hashtable();
		}

		public bool Monitoring 
		{
			get 
			{
				return monitoring;
			}
		}

		public virtual void Start()
		{
			timer = new System.Windows.Forms.Timer();
			timer.Interval = frequency;
			timer.Tick+= new EventHandler(Timer_Tick);
			timer.Start();
			monitoring = true;
		}

		public virtual void Stop()
		{
			if (timer != null) 
			{
				timer.Stop();
				timer.Dispose();
				timer = null;
				monitoring = false;

			}
		}

		public virtual void AddSubscriber(Adapter adapter)
		{
			subscribers.Add(adapter.Name, adapter);
		}

		public virtual void RemoveSubscriber(Adapter adapter)
		{
			if (subscribers.ContainsKey(adapter.Name))
			{
				subscribers.Remove(adapter.Name);
			}
		}

		protected virtual void Timer_Tick(object sender,EventArgs eArgs)
		{
			if (sender == timer) 
			{
				currentValue = GetNextValue();
				foreach (Adapter a in subscribers.Values)
				{
					a.SetDeviceValue(currentValue);
				}
				if (Tick != null) 
				{
					Tick(this, e);
				}
			}
		}

		protected virtual double GetNextValue()
		{
			return 0.0;
		}

		public double CurrentValue
		{
			get
			{
				return currentValue;
			}
		}
	}
}
