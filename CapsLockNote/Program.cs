using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapsLockNote
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        const int VK_CAPITAL = 0x14;
        private static bool _isCapsLockOn = false;



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            _isCapsLockOn = IsCapsLockOn();

            Timer capsLockTimer = new Timer();
            capsLockTimer.Interval = 100;
            capsLockTimer.Tick += CapsLockTimer_Tick;
            capsLockTimer.Start();

            Application.Run();
        }

        private static void CapsLockTimer_Tick(object sender, EventArgs e)
        {
            bool currentCapsLockState = IsCapsLockOn();
            if (currentCapsLockState != _isCapsLockOn)
            {

                _isCapsLockOn = currentCapsLockState;

                string message = _isCapsLockOn ? "CAPS LOCK ON" : "caps lock off";
                ShowCapsNotification(message);
            }
        }

        private static bool IsCapsLockOn()
        {
            return (GetKeyState(VK_CAPITAL) & 0x0001) != 0;
        }

        private static void ShowCapsNotification(string message)
        {
            CapsNotifier notifierForm = new CapsNotifier(message);
            notifierForm.Show();
        }


    }
}
