#region Imported Namespaces

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Extends the existing <see cref="ListView"/> control by exposing 
    /// the extended list view styles
    /// </summary>
    public class ListViewEx : ListView
    {
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_GETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 55;
        private const uint LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        private const uint LVS_EX_DOUBLEBUFFER = 0x00010000;
        private const uint LVS_EX_GRADIENT = 0x20000000;
        private const uint LVS_EX_GRIDLINES = 0x00000001;

        private bool doubleBuffering;
        private bool gradient;
        private bool gridLines;

        /// <summary>
        /// Shows or hides the grid lines
        /// </summary>
        public bool GridLines
        {
            get { return gridLines; }
            set
            {
                gridLines = value;
                SetStyle(LVS_EX_GRIDLINES, gridLines);
            }
        }

        /// <summary>
        /// Enables or disables double buffering
        /// </summary>
        public bool DoubleBuffering
        {
            get { return doubleBuffering; }
            set
            {
                doubleBuffering = value;
                SetStyle(LVS_EX_DOUBLEBUFFER, doubleBuffering);
            }
        }

        /// <summary>
        /// Enables or disables the gradient background
        /// </summary>
        public bool Gradient
        {
            get { return gradient; }
            set
            {
                gradient = value;
                SetStyle(LVS_EX_GRADIENT, gradient);
            }
        }

        [DllImport("coredll.dll")]
        private static extern uint SendMessage(IntPtr hwnd, uint msg, uint wparam, uint lparam);

        private void SetStyle(uint style, bool enable)
        {
            if (Environment.OSVersion.Platform != PlatformID.WinCE)
                return;

            var currentStyle = SendMessage(
                Handle,
                LVM_GETEXTENDEDLISTVIEWSTYLE,
                0,
                0);

            if (enable)
                SendMessage(
                    Handle,
                    LVM_SETEXTENDEDLISTVIEWSTYLE,
                    0,
                    currentStyle | style);
            else
                SendMessage(
                    Handle,
                    LVM_SETEXTENDEDLISTVIEWSTYLE,
                    0,
                    currentStyle & ~style);
        }

        #region Code Behind Generation

        private bool ShouldSerializeGradient()
        {
            return gradient;
        }

        private bool ShouldSerializeDoubleBuffering()
        {
            return doubleBuffering;
        }

        private bool ShouldSerializeGridLines()
        {
            return gridLines;
        }

        #endregion
    }
}