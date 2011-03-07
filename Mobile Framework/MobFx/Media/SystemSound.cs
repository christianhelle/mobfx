using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Represents a system sound type
    /// </summary>
    public class SystemSound
    {
        private readonly int soundType;

        internal SystemSound(int soundType)
        {
            this.soundType = soundType;
        }

        [DllImport("coredll.dll", EntryPoint = "MessageBeep", SetLastError = true)]
        private static extern void MessageBeepCE(int type);

        [DllImport("User32.dll", EntryPoint = "MessageBeep", SetLastError = true)]
        private static extern void MessageBeepNT(int type);

        /// <summary>
        /// Plays the system sound type.
        /// </summary>
        public void Play()
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.WinCE)
                    MessageBeepCE(soundType);
                else
                    MessageBeepNT(soundType);
            }
            catch (MissingMethodException)
            {
                Debug.WriteLine("MissingMethodException on MessageBeep");
            }
        }
    }
}