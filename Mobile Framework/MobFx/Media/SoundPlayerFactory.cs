using System;
using ChristianHelle.Framework.WindowsMobile.Core;
using ChristianHelle.Framework.WindowsMobile.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Factory class for creating <see cref="ISoundPlayer"/>
    /// </summary>
    public static class SoundPlayerFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ISoundPlayer"/> based on the
        /// OEM information
        /// </summary>
        /// <returns>Returns an instance of <see cref="ISoundPlayer"/></returns>
        public static ISoundPlayer Create()
        {
            try
            {
                var oem = Device.GetManufacturerInfo();
                if (!string.IsNullOrEmpty(oem))
                {
                    oem = oem.ToUpper();
                    if (oem.IndexOf("SYMBOL") > -1)
                        return new SymbolSoundPlayer();
                    if (oem.IndexOf("MOTOROLA") > -1)
                        return new SymbolSoundPlayer();
                }
            }
            catch (Exception ex)
            {
                var handler = new DeviceExceptionHandler(ex);
                handler.HandleException("Unable to create ISoundPlayer", false);
            }

            return CreateDefault();
        }

        /// <summary>
        /// Creates an instance of <see cref="ISoundPlayer"/> using the default sound player (Windows Waveform Audio API)
        /// </summary>
        /// <returns>Returns an instance of <see cref="ISoundPlayer"/></returns>
        public static ISoundPlayer CreateDefault()
        {
            return new WindowsSoundPlayer();
        }
    }
}