using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace AnalogDisplay
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar statusBar1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// other variables

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnSetHard;
		private System.Windows.Forms.TextBox txtHardValue;
		private System.Windows.Forms.Button button1;

		private ActiveWireAdapter aw;
		private PercentageAdapter pct;
		private HWDeviceMeter[] meters;
		private SingleLEDDevice[] yellowlights;
		private SingleLEDDevice[] redlights;
		private SingleLEDDevice[] indicators;
		private AxAWUSBIOLib.AxAwusbIO axAwusbIO1;
		private System.Windows.Forms.Button btnMonitor;
		private CPUMonitor cpu;
		private Hashtable monitors;
		private Hashtable adapters;
		private string currAdapter;
		private System.Windows.Forms.TextBox txtAdapterValue;
		private System.Windows.Forms.TextBox txtMonitorValue;
        private Button button2;
		private string currMonitor;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// initialize USB adapter and open
			aw = new ActiveWireAdapter();
			aw.ActiveXObject = axAwusbIO1;
			string err = aw.Open();
			// init hardware 
			meters = new HWDeviceMeter[4];
			yellowlights = new SingleLEDDevice[4];
			redlights = new SingleLEDDevice[4];
			indicators = new SingleLEDDevice[4];
			for (int x=0;x<4;x++) 
			{
				meters[x] = new HWDeviceMeter(Convert.ToString(x));
				meters[x].USB = aw;
				meters[x].Port = x;
				meters[x].Address = 0;
				yellowlights[x] = new SingleLEDDevice(Convert.ToString(x));
				yellowlights[x].USB = aw;
				yellowlights[x].Port = x;
				yellowlights[x].Address = 1;
				redlights[x] = new SingleLEDDevice(Convert.ToString(x));
				redlights[x].USB = aw;
				redlights[x].Port = x;
				redlights[x].Address = 2;
				indicators[x] = new SingleLEDDevice(Convert.ToString(x));
				indicators[x].USB = aw;
				indicators[x].Port = x;
				indicators[x].Address = 3;
			}

			// set up adapters and monitors
			monitors = new Hashtable();
			adapters = new Hashtable();

			monitors.Add("EmailMonitor", new EmailMonitor());
			adapters.Add("EmailAdapter", new BinaryAdapter("EmailAdapter"));

			monitors.Add("CPUMonitor", new CPUMonitor());
			adapters.Add("CPUPercentageAdapter", new PercentageAdapter("CPUPercentageAdapter"));
			adapters.Add("CPURedAdapter", new BinaryAdapter("CPURedAdapter"));
			adapters.Add("CPUYellowAdapter", new BinaryAdapter("CPUYellowAdapter"));

			monitors.Add("NetworkMonitor", new NetworkMonitor());
			adapters.Add("NetworkAdapter", new PercentageAdapter("NetworkAdapter"));
			adapters.Add("NetRedAdapter", new BinaryAdapter("NetRedAdapter"));
			adapters.Add("NetYellowAdapter", new BinaryAdapter("NetYellowAdapter"));

			monitors.Add("DiskMonitor", new DiskMonitor());
			adapters.Add("DiskPercentageAdapter", new PercentageAdapter("DiskPercentageAdapter"));
			adapters.Add("DiskRedAdapter", new BinaryAdapter("DiskRedAdapter"));
			adapters.Add("DiskYellowAdapter", new BinaryAdapter("DiskYellowAdapter"));

			monitors.Add("MemoryMonitor", new MemoryMonitor());
			adapters.Add("MemoryAdapter", new PercentageAdapter("MemoryAdapter"));
			adapters.Add("MemRedAdapter", new BinaryAdapter("MemRedAdapter"));
			adapters.Add("MemYellowAdapter", new BinaryAdapter("MemYellowAdapter"));

			currAdapter = "EmailAdapter";
			currMonitor = "EmailMonitor";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 0;		
			((BinaryAdapter)adapters[currAdapter]).OffAt = 20000;		
			((Adapter)adapters[currAdapter]).Device = indicators[0];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			
			// setup of adapters and monitors
			currAdapter = "CPUPercentageAdapter";
			currMonitor = "CPUMonitor";
			((Adapter)adapters[currAdapter]).Device = meters[0];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "CPUYellowAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 50;		
			((BinaryAdapter)adapters[currAdapter]).OffAt = 85;		
			((Adapter)adapters[currAdapter]).Device = yellowlights[0];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "CPURedAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 75;		
			((BinaryAdapter)adapters[currAdapter]).OffAt = 110;		
			((Adapter)adapters[currAdapter]).Device = redlights[0];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);

			currAdapter = "NetworkAdapter";
			currMonitor = "NetworkMonitor";
			// set max-min for network adapter
			((PercentageAdapter)adapters[currAdapter]).Max = 120000;		// 10 Mbit/sec in Bytes/sec
			((Adapter)adapters[currAdapter]).Device = meters[1];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);			
			currAdapter = "NetYellowAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = (50 * 120000) / 100;		
			((BinaryAdapter)adapters[currAdapter]).OffAt = (85 * 120000) / 100;		
			((Adapter)adapters[currAdapter]).Device = yellowlights[1];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "NetRedAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = (75 * 120000) / 100;		
			((BinaryAdapter)adapters[currAdapter]).OffAt = (200 * 120000) / 100;		
			((Adapter)adapters[currAdapter]).Device = redlights[1];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			
			currAdapter = "MemoryAdapter";
			currMonitor = "MemoryMonitor";
			((Adapter)adapters[currAdapter]).Device = meters[2];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "MemYellowAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 50;
			((BinaryAdapter)adapters[currAdapter]).OffAt = 10000;
			((Adapter)adapters[currAdapter]).Device = yellowlights[2];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "MemRedAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 75;
			((BinaryAdapter)adapters[currAdapter]).OffAt = 110;
			((Adapter)adapters[currAdapter]).Device = redlights[2];
			//((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);

			currAdapter = "DiskPercentageAdapter";
			currMonitor = "DiskMonitor";
			((Adapter)adapters[currAdapter]).Device = meters[3];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);			
			((PercentageAdapter)adapters[currAdapter]).Max = 4400000;		// 10 Mbit/sec in Bytes/sec
			currAdapter = "DiskYellowAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 50 * 4400000 / 100;
			((BinaryAdapter)adapters[currAdapter]).OffAt = 85 * 4400000 / 100;
			((Adapter)adapters[currAdapter]).Device = yellowlights[3];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);
			currAdapter = "DiskRedAdapter";
			((BinaryAdapter)adapters[currAdapter]).OnAt = 75 * 4400000 / 100;
			((BinaryAdapter)adapters[currAdapter]).OffAt = 110 * 4400000 / 100;
			((Adapter)adapters[currAdapter]).Device = redlights[3];
			((Monitor)monitors[currMonitor]).AddSubscriber((Adapter)adapters[currAdapter]);

			currMonitor = "CPUMonitor";
			
			foreach( Monitor m in monitors.Values )
			{
				m.Start();
			}
			// add listener to monitor so we can update UI with current values
			currMonitor = "DiskMonitor";
			((Monitor)monitors[currMonitor]).Tick += new Monitor.TickHandler(Form1_Tick);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				foreach(Monitor m in monitors.Values)
				{
					m.Stop();
				}
				aw.Close();
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSetHard = new System.Windows.Forms.Button();
            this.txtHardValue = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAdapterValue = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.txtMonitorValue = new System.Windows.Forms.TextBox();
            this.axAwusbIO1 = new AxAWUSBIOLib.AxAwusbIO();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAwusbIO1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 421);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(432, 24);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 248);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Meter 1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSetHard);
            this.groupBox4.Controls.Add(this.txtHardValue);
            this.groupBox4.Location = new System.Drawing.Point(16, 176);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 56);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Hardware";
            // 
            // btnSetHard
            // 
            this.btnSetHard.Location = new System.Drawing.Point(112, 16);
            this.btnSetHard.Name = "btnSetHard";
            this.btnSetHard.Size = new System.Drawing.Size(72, 24);
            this.btnSetHard.TabIndex = 5;
            this.btnSetHard.Text = "Set Value";
            this.btnSetHard.Click += new System.EventHandler(this.btnSetHard_Click);
            // 
            // txtHardValue
            // 
            this.txtHardValue.Location = new System.Drawing.Point(16, 16);
            this.txtHardValue.Name = "txtHardValue";
            this.txtHardValue.Size = new System.Drawing.Size(88, 20);
            this.txtHardValue.TabIndex = 4;
            this.txtHardValue.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txtAdapterValue);
            this.groupBox3.Location = new System.Drawing.Point(16, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 48);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Adapter";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 24);
            this.button1.TabIndex = 7;
            this.button1.Text = "Set Value";
            // 
            // txtAdapterValue
            // 
            this.txtAdapterValue.Location = new System.Drawing.Point(16, 16);
            this.txtAdapterValue.Name = "txtAdapterValue";
            this.txtAdapterValue.Size = new System.Drawing.Size(88, 20);
            this.txtAdapterValue.TabIndex = 6;
            this.txtAdapterValue.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMonitor);
            this.groupBox2.Controls.Add(this.txtMonitorValue);
            this.groupBox2.Location = new System.Drawing.Point(16, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 56);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monitor";
            // 
            // btnMonitor
            // 
            this.btnMonitor.Location = new System.Drawing.Point(112, 24);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(72, 24);
            this.btnMonitor.TabIndex = 8;
            this.btnMonitor.Text = "On/Off";
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // txtMonitorValue
            // 
            this.txtMonitorValue.Location = new System.Drawing.Point(16, 24);
            this.txtMonitorValue.Name = "txtMonitorValue";
            this.txtMonitorValue.Size = new System.Drawing.Size(88, 20);
            this.txtMonitorValue.TabIndex = 7;
            this.txtMonitorValue.Text = "0";
            // 
            // axAwusbIO1
            // 
            this.axAwusbIO1.Enabled = true;
            this.axAwusbIO1.Location = new System.Drawing.Point(8, 360);
            this.axAwusbIO1.Name = "axAwusbIO1";
            this.axAwusbIO1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAwusbIO1.OcxState")));
            this.axAwusbIO1.Size = new System.Drawing.Size(64, 50);
            this.axAwusbIO1.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(248, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(432, 445);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.axAwusbIO1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar1);
            this.Name = "Form1";
            this.Text = "Dexter Dashboard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAwusbIO1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>		
        [STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnMonitor_Click(object sender, System.EventArgs e)
		{
			Monitor m = (Monitor)monitors[currMonitor];
			if (m.Monitoring) 
			{
				m.Stop();
			} 
			else 
			{
				m.Start();
			}
		}

		private void btnSetHard_Click(object sender, System.EventArgs e)
		{
			meters[0].HardwareValue = Convert.ToInt32( txtHardValue.Text);
		}

		private void Form1_Tick(Monitor m, EventArgs e)
		{
			txtMonitorValue.Text = m.CurrentValue.ToString();
			txtAdapterValue.Text = ((Adapter)adapters[currAdapter]).CurrentValue.ToString();
			txtHardValue.Text = meters[0].HardwareValue.ToString();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WebFetch.grabPage("http://www.wunderground.com/cgi-bin/findweather/getForecast?query=30092"));
        }
	}
}
