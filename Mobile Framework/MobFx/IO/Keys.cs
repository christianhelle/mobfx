using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Helper class for sending keyboard events and locking the keyboard
    /// </summary>
    public static class MobileKeys
    {
        #region DeviceKeys enum

        /// <summary>
        /// Windows Mobile device specific keys
        /// </summary>
        public enum DeviceKeys
        {
            /// <summary>
            /// Hold Button
            /// </summary>
            Hold = 0x8000,
            /// <summary>
            /// Application shortcut 1 Button
            /// </summary>
            App1 = 0xc1,
            /// <summary>
            /// Application shortcut 2 Button
            /// </summary>
            App2 = 0xc2,
            /// <summary>
            /// Application shortcut 3 Button
            /// </summary>
            App3 = 0xc3,
            /// <summary>
            /// Application shortcut 4 Button
            /// </summary>
            App4 = 0xc4,
            /// <summary>
            /// Application shortcut 5 Button
            /// </summary>
            App5 = 0xc5,
            /// <summary>
            /// Application shortcut 6 Button
            /// </summary>
            App6 = 0xc6,
            /// <summary>
            /// Application shortcut 7 Button
            /// </summary>
            App7 = 0xc7,
            /// <summary>
            /// Application shortcut 8 Button
            /// </summary>
            App8 = (Keys.F17 | Keys.H),
            /// <summary>
            /// Application shortcut 9 Button
            /// </summary>
            App9 = (Keys.F18 | Keys.H),
            /// <summary>
            /// Application shortcut 10 Button
            /// </summary>
            App10 = (Keys.F19 | Keys.H),
            /// <summary>
            /// Application shortcut 11 Button
            /// </summary>
            App11 = (Keys.F20 | Keys.H),
            /// <summary>
            /// Application shortcut 12 Button
            /// </summary>
            App12 = (Keys.F21 | Keys.H),
            /// <summary>
            /// Application shortcut 13 Button
            /// </summary>
            App13 = (Keys.F22 | Keys.H),
            /// <summary>
            /// Application shortcut 14 Button
            /// </summary>
            App14 = (Keys.F23 | Keys.H),
            /// <summary>
            /// Application shortcut 15 Button
            /// </summary>
            App15 = (Keys.F24 | Keys.H),
            /// <summary>
            /// Action Button
            /// </summary>
            Action = Keys.F23,
            /// <summary>
            /// Back button
            /// </summary>
            Back = Keys.Escape,
            /// <summary>
            /// Done button
            /// </summary>
            Done = Keys.F6,
            /// <summary>
            /// DPad
            /// </summary>
            DPad = Keys.F21,
            /// <summary>
            /// End Call Button
            /// </summary>
            End = Keys.F4,
            /// <summary>
            /// Flip button
            /// </summary>
            Flip = Keys.F17,
            /// <summary>
            /// Home button
            /// </summary>
            Home = Keys.LWin,
            /// <summary>
            /// Power button
            /// </summary>
            Power = Keys.F18,
            /// <summary>
            /// Record button
            /// </summary>
            Record = Keys.F10,
            /// <summary>
            /// Red key
            /// </summary>
            RedKey = Keys.F19,
            /// <summary>
            /// Rocker
            /// </summary>
            Rocker = Keys.F20,
            /// <summary>
            /// Soft key 1
            /// </summary>
            Softkey1 = Keys.F1,
            /// <summary>
            /// Soft key 2
            /// </summary>
            Softkey2 = Keys.F2,
            /// <summary>
            /// Star
            /// </summary>
            Star = Keys.F8,
            /// <summary>
            /// Symbol
            /// </summary>
            Symbol = Keys.F11,
            /// <summary>
            /// Talk button
            /// </summary>
            Talk = Keys.F3,
            /// <summary>
            /// Voice dial button
            /// </summary>
            VoiceDial = Keys.F24,
            /// <summary>
            /// Volume down
            /// </summary>
            VolumeDown = Keys.F7,
            /// <summary>
            /// Volume up
            /// </summary>
            VolumeUp = Keys.F6
        }

        #endregion

        private const int KEYEVENTF_KEYDOWN = 0x0000;
        private const int KEYEVENTF_KEYUP = 0x0002;

        [DllImport("coredll.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// Sends a single character to keybd_event()
        /// </summary>
        /// <param name="key">Hardware button containing a single char</param>
        public static void SendMobileKey(DeviceKeys key)
        {
            keybd_event((byte) key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event((byte) key, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// Sends a single character to keybd_event()
        /// </summary>
        /// <param name="key">Key code</param>
        public static void SendKey(Keys key)
        {
            keybd_event((byte) key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event((byte) key, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// Sends a single character to keybd_event() with the shift key pressed down
        /// </summary>
        /// <param name="key">Key code</param>
        public static void SendUpperCaseKey(Keys key)
        {
            keybd_event((byte)Keys.ShiftKey, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event((byte)Keys.ShiftKey, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// Lock All Hardware buttons using the GAPI
        /// </summary>
        public static bool LockHardwareButtons()
        {
            try
            {
                GXOpenInput();
                return true;
            }
            catch (MissingMethodException)
            {
                return false;
            }
        }

        /// <summary>
        /// Unlock all hardware buttons using the GAPI
        /// </summary>
        public static bool UnlockHardwareButtons()
        {
            try
            {
                GXCloseInput();
                return true;
            }
            catch (MissingMethodException)
            {
                return false;
            }
        }

        [DllImport("gx.dll", EntryPoint = "#9")]
        private static extern int GXOpenInput();

        [DllImport("gx.dll", EntryPoint = "#3")]
        private static extern int GXCloseInput();
    }
}