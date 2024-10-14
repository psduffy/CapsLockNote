using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapsLockNote
{
    public partial class CapsNotifier : Form
    {
        private Timer _closeTimer;

        public CapsNotifier(string message)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.Azure;
            this.StartPosition = FormStartPosition.Manual;

            // Use CS_DROPSHADOW style for the shadow effect

            // Create and configure the label
            SmoothLabel labelNotification = new SmoothLabel();
            labelNotification.Text = message;
            labelNotification.Font = new Font("Calibri", 18, FontStyle.Bold);
            labelNotification.AutoSize = true;
            labelNotification.TextAlign = ContentAlignment.MiddleCenter;
            labelNotification.ForeColor = Color.Black; // Ensure text is black

            // Adjust form size to fit the label properly
            this.Controls.Add(labelNotification);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Add some padding around the label for better visual appeal
            this.Padding = new Padding(30);

            // Determine which screen the mouse is currently on
            var screen = Screen.FromPoint(Cursor.Position);

            // Set the location of the form to the bottom center of the current screen
            int screenWidth = screen.WorkingArea.Width;
            int screenHeight = screen.WorkingArea.Height;
            int screenLeft = screen.WorkingArea.Left;
            int screenTop = screen.WorkingArea.Top;

            this.Location = new Point(
                screenLeft + (screenWidth - this.Width) / 2,
                screenTop + screenHeight - this.Height - 100
            );

            // Timer settings to close the form after a short delay
            _closeTimer = new Timer();
            _closeTimer.Interval = 2000;
            _closeTimer.Tick += CloseTimer_Tick;
            _closeTimer.Start();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            _closeTimer.Stop();
            _closeTimer.Dispose();
            this.Close();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }
    }

    public class SmoothLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            base.OnPaint(e);
        }
    }
}
