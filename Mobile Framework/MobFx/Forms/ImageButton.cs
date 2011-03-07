#region Imported Namespaces

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using System;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// An owner drawn image button control
    /// </summary>
    public class ImageButton : TransparentButton
    {
        private readonly Pen border;
        private readonly SolidBrush text;
        private ImageDrawMode drawMode;

        private Image image;

        /// <summary>
        /// Creates an instance of <see cref="ImageButton"/>
        /// </summary>
        public ImageButton()
        {
            border = new Pen(base.ForeColor);
            text = new SolidBrush(base.ForeColor);
            PressedColor = Color.PowderBlue;
        }

        /// <summary>
        /// Image to display
        /// </summary>
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the drawing mode for the image
        /// </summary>
        public ImageDrawMode DrawMode
        {
            get { return drawMode; }
            set
            {
                drawMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control
        /// </summary>
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                border.Color = value;
                text.Color = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the control when pressed down
        /// </summary>
        public Color PressedColor { get; set; }

        /// <summary>
        /// Draws the control
        /// </summary>
        /// <param name="e">Drawing surface data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var gxOff = GraphicsEx.FromImage(MemoryBitmap))
            {
                gxOff.Surface.Clear(Parent.BackColor);
                if (Environment.OSVersion.Platform != PlatformID.WinCE)
                    gxOff.Surface.DrawRectangle(
                        border,
                        0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                if (Pushed)
                    gxOff.DrawRoundedRectangle(ClientRectangle, PressedColor);

                DrawImage(gxOff);

                if (!string.IsNullOrEmpty(Text))
                {
                    var size = gxOff.Surface.MeasureString(Text, Font);
                    gxOff.Surface.DrawString(
                        Text,
                        Font,
                        text,
                        (ClientSize.Width - size.Width) / 2,
                        (ClientSize.Height - size.Height));
                }

                if (Transparent)
                {
                    try
                    {
                        var bgOwner = Parent as IControlBackground;
                        if (bgOwner != null && bgOwner.BackgroundImage != null)
                            gxOff.AlphaBlend(bgOwner.BackgroundImage, 70, Location.X, Location.Y);
                    }
                    catch (PlatformNotSupportedException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }

            e.Graphics.DrawImage(MemoryBitmap, 0, 0);
        }

        private void DrawImage(GraphicsEx gxOff)
        {
            if (image == null)
                return;

            var imageAttr = new ImageAttributes();
            using (var bmp = new Bitmap(image))
            {
                var transparentKey = bmp.GetPixel(1, 1);
                imageAttr.SetColorKey(transparentKey, transparentKey);
            }

            //var imgRect = Dpi.ScaleRectangle((Width - image.Width) / 2, 5, image.Width, image.Height);
            int width = Dpi.Scale(image.Width);
            int height = Dpi.Scale(image.Height);
            var imgRect = new Rectangle((Width - width) / 2, Dpi.Scale(5), width, height);
            gxOff.Surface.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
        }

        /// <summary>
        /// Releases the unmanaged resources used by this BatteryLife object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        /// <remarks>This method is called by the public Dispose() method and the Finalize method. <b>Dispose()</b> invokes the protected <b>Dispose(Boolean)</b> method with the disposing parameter set to <b>true</b>. <b>Finalize</b> invokes <b>Dispose</b> with <i>disposing</i> set to <b>false</b>.</remarks>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    border.Dispose();
                    text.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Code Behind Generation

        private bool ShouldSerializeForeColor()
        {
            return ForeColor != SystemColors.ControlText;
        }

        private bool ShouldSerializeDrawMode()
        {
            return DrawMode != ImageDrawMode.Normal;
        }

        private bool ShouldSerializePressedColor()
        {
            return PressedColor != Color.PowderBlue;
        }

        #endregion
    }
}