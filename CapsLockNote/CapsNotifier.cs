using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapsLockNote
{
    public partial class CapsNotifier : Form
    {
        private Timer _closeTimer;
        public CapsNotifier(string message)
        {
            InitializeComponent();

            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.BackColor = Color.HotPink;

            Label labelNotification = new Label();
            labelNotification.Text = message;
            labelNotification.Font = new Font("Calibri", 36, FontStyle.Bold);  // Increased font size for visibility
            labelNotification.AutoSize = true;
            labelNotification.TextAlign = ContentAlignment.MiddleCenter;

            // Adjust form size to fit the label properly
            this.Controls.Add(labelNotification);
            this.AutoSize = true; // Let the form size itself based on the content size
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Add some padding around the label so the form doesn't feel too cramped
            this.Padding = new Padding(30);

            // Timer settings
            _closeTimer = new Timer();
            _closeTimer.Interval = 2000; // display notification for 2 seconds
            _closeTimer.Tick += CloseTimer_Tick;
            _closeTimer.Start();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            _closeTimer.Stop();
            _closeTimer.Dispose();
            this.Close();
        }
    }
}
