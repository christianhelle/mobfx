using System;
using System.Drawing;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using ChristianHelle.Framework.WindowsMobile.Forms;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChristianHelle.WMS.BoConcept.Controls
{
    /// <summary>
    /// Owner drawn button with a gradient background
    /// </summary>
    public class GradientButton : TransparentButton
    {
        private readonly Pen border;
        private readonly SolidBrush text;

        /// <summary>
        /// Creates an instance of <see cref="GradientButton"/>
        /// </summary>
        public GradientButton()
        {
            border = new Pen(SystemColors.ControlDark);
            text = new SolidBrush(SystemColors.ControlText);
            GradientStart = Color.SteelBlue;
            GradientEnd = Color.LightCyan;
        }

        /// <summary>
        /// Gets or Sets the foreground color of the control
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
        /// Gradient start color
        /// </summary>
        public Color GradientStart { get; set; }

        /// <summary>
        ///  Gradient end color
        /// </summary>
        public Color GradientEnd { get; set; }

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

        /// <summary>
        /// Draws the control
        /// </summary>
        /// <param name="e">Drawing surface data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var gxOff = GraphicsEx.FromImage(MemoryBitmap))
            {
                if (!Pushed)
                    gxOff.GradientFill(ClientRectangle, GradientStart, GradientEnd, GradientFillDirection.Vertical);
                else
                    gxOff.GradientFill(ClientRectangle, GradientEnd, GradientStart, GradientFillDirection.Vertical);

                gxOff.Surface.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                if (!string.IsNullOrEmpty(Text))
                {
                    var size = gxOff.Surface.MeasureString(Text, Font);
                    gxOff.Surface.DrawString(
                        Text,
                        Font,
                        text,
                        (ClientSize.Width - size.Width) / 2,
                        (ClientSize.Height - size.Height) / 2);
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

        /// <summary>
        /// Overridden with an emtpy definition to avoid flickering (All drawing must be done in Paint)
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        #region Code Behind Generation
        private bool ShouldSerializeForeColor()
        {
            return ForeColor != Color.White;
        }
        #endregion
    }
}
