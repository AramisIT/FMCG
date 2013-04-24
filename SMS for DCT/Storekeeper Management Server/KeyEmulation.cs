using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StorekeeperManagementServer
{
    class KeyEmulation
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, UInt32 dwFlags, IntPtr dwExtraInfo);
        public static UInt32 KEYEVENTF_EXTENDEDKEY = 1;
        public static UInt32 KEYEVENTF_KEYUP = 2;

        public static void PressKey(Keys key)
        {
            keybd_event(key, 0x45, KEYEVENTF_EXTENDEDKEY | 0, (IntPtr)0);
            keybd_event(key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (IntPtr)0);
        }
    }
}
