using System;
using System.Runtime.InteropServices;

namespace ChristianHelle.Framework.WindowsMobile.Core
{
    /// <summary>
    /// Managed class in charge of handling unamanaged calls that are window-related.
    /// </summary>
    public class SystemWindow
    {
        #region Set Foreground Window
        [DllImport("coredll.dll", EntryPoint = "SetForegroundWindow")]
        static extern bool SetForegroundWindowCE(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        static extern bool SetForegroundWindowNT(IntPtr handle);

        /// <summary>
        /// The SetForegroundWindow function puts the thread that created the specified window into the 
        /// foreground and activates the window. Keyboard input is directed to the window, and various 
        /// visual cues are changed for the user. The system assigns a slightly higher priority to the 
        /// thread that created the foreground window than it does to other threads. 
        /// </summary>
        /// <returns>Returns true if the operation is successful.</returns>
        public static bool SetForegroundWindow(IntPtr handle)
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                return SetForegroundWindowCE(handle);
            return SetForegroundWindowNT(handle);
        }

        #endregion

        #region Get Foreground window
        [DllImport("coredll.dll", EntryPoint = "GetForegroundWindow")]
        static extern IntPtr GetForegroundWindowCE();

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        static extern IntPtr GetForegroundWindowNT();

        /// <summary>
        /// The GetForegroundWindow function returns a handle to the foreground window.
        /// </summary>
        public static IntPtr GetForegroundWindow()
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                return GetForegroundWindowCE();
            else
                return GetForegroundWindowNT();
        }
        #endregion

        #region Set taskbar visibility
        /// <summary>
        /// Set the visibility of the entire taskbar including the SIP button and the start icon.
        /// </summary>
        /// <param name="hwnd">The handle to the currently visible window.</param>
        /// <param name="showTaskbar">True if the taskbar should be shown, false if it should be hidden.</param>
        /// <returns>Returns TRUE if the operation was successful, otherwise FALSE.</returns>
        public static void SetTaskBarVisibility(IntPtr hwnd, bool showTaskbar)
        {
            if (Environment.OSVersion.Platform != PlatformID.WinCE)
                return;

            try
            {
                if (showTaskbar)
                    SHFullScreen(hwnd, SHOWTASKBAR | SHOWSIPBUTTON | SHOWSTARTICON);
                else
                    SHFullScreen(hwnd, HIDETASKBAR | HIDESIPBUTTON | HIDESTARTICON);
            }
            catch (MissingMethodException) { }
        }

        [DllImport("aygshell.dll")]
        static extern uint SHFullScreen(IntPtr hwndRequester, uint state);

        const uint SHOWTASKBAR = 0x0001;
        const uint HIDETASKBAR = 0x0002;
        const uint SHOWSIPBUTTON = 0x0004;
        const uint HIDESIPBUTTON = 0x0008;
        const uint SHOWSTARTICON = 0x0010;
        const uint HIDESTARTICON = 0x0020;
        #endregion

        #region Find Window

        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindowCE(string name, string caption);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindowNT(string name, string caption);

        /// <summary>
        /// The FindWindow function retrieves a handle to the top-level window whose class name and window name 
        /// match the specified strings. This function does not search child windows. 
        /// This function does not perform a case-sensitive search.
        /// </summary>
        /// <returns>Returns true if the operation is successful.</returns>
        public static IntPtr FindWindow(string name, string caption)
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                return FindWindowCE(name, caption);
            return FindWindowNT(name, caption);
        }

        #endregion
    }
}
