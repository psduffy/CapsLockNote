﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapsLockNote
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        const int VK_CAPITAL = 0x14;
        private static bool _isCapsLockOn = false;
        private static CapsNotifier _currentNotifier;

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
                // Caps Lock state has changed
                _isCapsLockOn = currentCapsLockState;

                string message = _isCapsLockOn ? "AA\nCAPS LOCK ON" : "aa\ncaps lock off";

                // Close the current notification if it exists
                if (_currentNotifier != null && !_currentNotifier.IsDisposed)
                {
                    _currentNotifier.Close();
                }

                // Show new notification
                _currentNotifier = new CapsNotifier(message);
                _currentNotifier.Show();
            }
        }

        private static bool IsCapsLockOn()
        {
            return (GetKeyState(VK_CAPITAL) & 0x0001) != 0;
        }
    }
}