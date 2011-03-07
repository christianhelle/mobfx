using System;
using System.IO;

namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Represents the device sound player
    /// </summary>
    public interface ISoundPlayer : IDisposable
    {
        /// <summary>
        /// Plays the audio file
        /// </summary>
        /// <param name="fileName">Audio file (absolute path)</param>
        void Play(string fileName);

        /// <summary>
        /// Plays the data contained in the stream
        /// </summary>
        /// <param name="stream">Data stream</param>
        void Play(Stream stream);
    }
}
