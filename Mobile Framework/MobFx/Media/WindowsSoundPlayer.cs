using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Represents a Sound object to play sound data
    /// </summary>
    public class WindowsSoundPlayer : ISoundPlayer
    {
        [Flags]
        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int PlaySoundCE(string szSound, IntPtr hMod, int flags);

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int PlaySoundBytesCE(byte[] szSound, IntPtr hMod, int flags);

        [DllImport("Winmm.dll", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int PlaySoundNT(string szSound, IntPtr hMod, int flags);

        [DllImport("Winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int PlaySoundBytesNT(byte[] szSound, IntPtr hMod, int flags);

        /// <summary>
        /// Play the sound
        /// </summary>
        /// <param name="fileName">File to play</param>
        public void Play(string fileName)
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                PlaySoundCE(fileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
            else
                PlaySoundNT(fileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
        }

        /// <summary>
        /// Play the sound
        /// </summary>
        /// <param name="stream">Sound file data</param>
        public void Play(Stream stream)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);

            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                PlaySoundBytesCE(buffer, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_MEMORY));
            else
                PlaySoundBytesNT(buffer, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_MEMORY));
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion
    }
}
