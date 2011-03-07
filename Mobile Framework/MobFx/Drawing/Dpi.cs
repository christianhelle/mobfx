using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// A Helper object to adjus the sizes of controls based on the DPI.
    /// </summary>
    public class Dpi
    {
        [DllImport("coredll.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        private const int NORMAL_DPI = 96;
        private const int LOGPIXELSX = 88;
        private static readonly int dpi = NORMAL_DPI;
        private static readonly bool hidpi;

        static Dpi()
        {
            try
            {
                dpi = GetDeviceCaps(IntPtr.Zero, LOGPIXELSX);
                if (dpi != NORMAL_DPI)
                {
                    hidpi = true;
                }
            }
            catch
            {
                dpi = NORMAL_DPI;
                hidpi = false;
            }
        }

        /// <summary>
        /// Is the device a High DPI device
        /// </summary>
        public static bool IsHiDpi
        {
            get { return hidpi; }
        }

        /// <summary>
        /// Gets the DPI of the device
        /// </summary>
        public static int DeviceDpi
        {
            get { return dpi; }
        }

        /// <summary>
        /// Adjust the sizes of controls to account for the DPI of the device.		
        /// </summary>
        /// <param name="parent">The parent node of the tree of controls to adjust.</param>
        public static void AdjustAllControls(Control parent)
        {
            if (!hidpi)
                return;

            foreach (Control child in parent.Controls)
            {
                AdjustControl(child);
                AdjustAllControls(child);
            }
        }

        /// <summary>
        /// Adjust the sizes of controls to account for the DPI of the device.		
        /// </summary>
        /// <param name="control">controls to adjust.</param>
        public static void AdjustControl(Control control)
        {
            if (!hidpi)
                return;

            control.Bounds = new Rectangle(
                control.Left * dpi / NORMAL_DPI,
                control.Top * dpi / NORMAL_DPI,
                control.Width * dpi / NORMAL_DPI,
                control.Height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Adjust the sizes of controls to account for the DPI of the device.		
        /// </summary>
        /// <param name="parent">The parent node of the tree of controls to adjust.</param>
        public static void AdjustAllControlLocations(Control parent)
        {
            if (!hidpi)
                return;

            foreach (Control child in parent.Controls)
            {
                AdjustControlLocation(child);
                AdjustAllControlLocations(child);
            }
        }

        /// <summary>
        /// Adjust the location of controls to account for the DPI of the device.
        /// </summary>
        /// <param name="control">control to adjust</param>
        public static void AdjustControlLocation(Control control)
        {
            if (!hidpi)
                return;

            control.Location = new Point(
                control.Location.X * dpi / NORMAL_DPI,
                control.Location.Y * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Adjust the size of controls to account for the DPI of the device.
        /// </summary>
        /// <param name="parent">control to adjust</param>
        public static void AdjustAllControlSizes(Control parent)
        {
            if (!hidpi)
                return;

            foreach (Control child in parent.Controls)
            {
                AdjustControlSize(child);
                AdjustAllControlSizes(child);
            }
        }

        /// <summary>
        /// Adjust the size of controls to account for the DPI of the device.
        /// </summary>
        /// <param name="control">control to adjust</param>
        public static void AdjustControlSize(Control control)
        {
            if (!hidpi)
                return;

            control.Size = new Size(
                control.Width * dpi / NORMAL_DPI,
                control.Height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Get a Rectangle to account for the DPI of the device.
        /// </summary>
        /// <param name="left">X-coordinate of the Rectangle</param>
        /// <param name="top">Y-coordinate of the Rectangle</param>
        /// <param name="width">Width of the Rectangle</param>
        /// <param name="height">Height of the Rectangle</param>
        /// <returns>A Rectangle scaled to fit regarding the DPI of the device</returns>
        public static Rectangle ScaleRectangle(int left, int top, int width, int height)
        {
            if (!hidpi)
                return new Rectangle(left, top, width, height);

            return new Rectangle(
                left * dpi / NORMAL_DPI,
                top * dpi / NORMAL_DPI,
                width * dpi / NORMAL_DPI,
                height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Get a Rectangle to amount for the DPI of the device
        /// </summary>
        /// <param name="rect">Source Rectangle</param>
        /// <returns>A Rectangle scaled to fit regarding the DPI of the device</returns>
        public static Rectangle ScaleRectangle(Rectangle rect)
        {
            if (!hidpi)
                return rect;

            return new Rectangle(
                rect.Left * dpi / NORMAL_DPI,
                rect.Top * dpi / NORMAL_DPI,
                rect.Width * dpi / NORMAL_DPI,
                rect.Height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Get a Rectangle to account for the DPI of the device.
        /// </summary>
        /// <param name="left">X-coordinate of the Rectangle</param>
        /// <param name="top">Y-coordinate of the Rectangle</param>
        /// <param name="width">Width of the Rectangle</param>
        /// <param name="height">Height of the Rectangle</param>
        /// <returns>A Rectangle scaled to fit regarding the DPI of the device</returns>
        public static RectangleF ScaleRectangleF(float left, float top, float width, float height)
        {
            if (!hidpi)
                return new RectangleF(left, top, width, height);

            return new RectangleF(
                left * dpi / NORMAL_DPI,
                top * dpi / NORMAL_DPI,
                width * dpi / NORMAL_DPI,
                height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Get a Rectangle to amount for the DPI of the device
        /// </summary>
        /// <param name="rect">Source Rectangle</param>
        /// <returns>A Rectangle scaled to fit regarding the DPI of the device</returns>
        public static RectangleF ScaleRectangleF(RectangleF rect)
        {
            if (!hidpi)
                return rect;

            return new RectangleF(
                rect.Left * dpi / NORMAL_DPI,
                rect.Top * dpi / NORMAL_DPI,
                rect.Width * dpi / NORMAL_DPI,
                rect.Height * dpi / NORMAL_DPI);
        }

        /// <summary>
        /// Scale a coordinate to account for the dpi.
        /// </summary>
        /// <param name="x">The number of pixels at NORMAL_DPI dpi</param>
        /// <returns>Scaled coordinate</returns>
        public static int Scale(int x)
        {
            if (!hidpi)
                return x;

            return x * dpi / NORMAL_DPI;
        }

        /// <summary>
        /// Get a Size to amount for the DPI of the device
        /// </summary>
        /// <param name="size">Source Size</param>
        /// <returns>A Size scaled to fit regarding the DPI of the device</returns>
        public static Size ScaleSize(Size size)
        {
            if (!hidpi)
                return size;

            return new Size(
                size.Width * dpi / NORMAL_DPI,
                size.Height * dpi / NORMAL_DPI);

        }
    }
}