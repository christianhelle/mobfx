#region Imported Namespaces

using System;
using System.Drawing;
using Microsoft.WindowsCE.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// Provides factory methods for creating specialized fonts
    /// </summary>
    public static class FontFactory
    {
        /// <summary>
        /// Creates a rotated font by using the LOGFONT structure
        /// </summary>
        /// <param name="fontName">Face of the font</param>
        /// <param name="angleInDegrees">Angle in degrees</param>
        /// <returns>Returns a <see cref="Font"/></returns>
        public static Font CreateRotatedFont(string fontName, int angleInDegrees)
        {
            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                return CreateRotatedFontCE(fontName, angleInDegrees);
            return CreateRotatedFontNT(fontName, angleInDegrees);
        }

        /// <summary>
        /// Runtime
        /// </summary>
        private static Font CreateRotatedFontCE(string fontName, int angleInDegrees)
        {
            var logf = new LogFont();

            logf.Height = (int)(-18f * Dpi.DeviceDpi / 96);
            logf.Orientation = logf.Escapement = angleInDegrees * 10;
            logf.FaceName = fontName;
            logf.CharSet = LogFontCharSet.Default;
            logf.OutPrecision = LogFontPrecision.Default;
            logf.ClipPrecision = LogFontClipPrecision.Default;
            logf.Quality = LogFontQuality.ClearType;
            logf.PitchAndFamily = LogFontPitchAndFamily.Default;

            return Font.FromLogFont(logf);
        }

        /// <summary>
        /// Design time
        /// </summary>
        private static Font CreateRotatedFontNT(string fontName, int angleInDegrees)
        {
            var logf = new Native.LOGFONT();

            using (var buffer = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(buffer))
                logf.Height = (int)(-18f * g.DpiY / 96);

            logf.Orientation = logf.Escapement = angleInDegrees * 10;
            logf.FaceName = fontName;
            logf.CharSet = Native.LogFontCharSet.Default;
            logf.OutPrecision = Native.LogFontPrecision.Default;
            logf.ClipPrecision = Native.LogFontClipPrecision.Default;
            logf.Quality = Native.LogFontQuality.ClearType;
            logf.PitchAndFamily = Native.LogFontPitchAndFamily.Default;

            return Font.FromLogFont(logf);
        }
    }
}