using System.IO;
using Symbol.Audio;

namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Motorola / Symbol implementation of <see cref="ISoundPlayer"/> that uses the
    /// their proprietary Audio API
    /// </summary>
    public class SymbolSoundPlayer : ISoundPlayer
    {
        private readonly Controller controller;

        /// <summary>
        /// Creates an instance of <see cref="SymbolSoundPlayer"/>
        /// </summary>
        public SymbolSoundPlayer()
        {
            if (Device.AvailableDevices.Length == 0) return;
            if (Device.AvailableDevices[0].AudioType != AudioType.StandardAudio) return;

            controller = new StandardAudio(Device.AvailableDevices[0]);
            controller.BeeperVolume = 1;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (controller != null)
                controller.Dispose();
        }

        #endregion

        #region Implementation of ISoundPlayer

        /// <summary>
        /// Plays the audio file using the Motorola/Symbol Audio API
        /// </summary>
        /// <param name="fileName">Audio file (absolute path)</param>
        public void Play(string fileName)
        {
            if (controller != null)
                controller.PlayAudio(500, 500, fileName);
        }

        /// <summary>
        /// Plays the data contained in the stream using the default sound player
        /// </summary>
        /// <param name="stream">Data stream</param>
        public void Play(Stream stream)
        {
            using (var sound = SoundPlayerFactory.CreateDefault())
                sound.Play(stream);
        }

        #endregion
    }
}