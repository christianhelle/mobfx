using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using System.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// A Vista Aero inspired owner drawn button control
    /// </summary>
    /// <remarks>
    /// This is a gradient filled button that supports transparency. The transparency effect is done
    /// by alpha blending with the parent controls background image.
    /// 
    /// To enable transparency, this control must be placed on a container control that implements 
    /// <see cref="IControlBackground"/>
    /// </remarks>
    public class ButtonEx : TransparentButton
    {
        private readonly Pen border;
        private readonly SolidBrush text;
        private ButtonMode mode;
        private ButtonTheme colorTheme;

        /// <summary>
        /// Creates an instance of <see cref="ButtonEx"/>
        /// </summary>
        public ButtonEx()
        {
            border = new Pen(Color.White);
            text = new SolidBrush(Color.White);
            ColorTheme = ButtonTheme.Black;
            mode = ButtonMode.TwoDimensional;
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
        /// Gets or Sets which <see cref="ButtonMode"/> to draw the control in
        /// </summary>
        /// <remarks>
        /// The default <see cref="ButtonMode"/> is <see cref="ButtonMode.TwoDimensional"/>
        /// </remarks>
        public ButtonMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or Sets which <see cref="ButtonTheme"/> the button should draw in
        /// </summary>
        /// <remarks>
        /// The default <see cref="ButtonTheme"/> is <see cref="ButtonTheme.Black"/>
        /// </remarks>
        public ButtonTheme ColorTheme
        {
            get { return colorTheme; }
            set
            {
                colorTheme = value;
                Invalidate();
            }
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

        /// <summary>
        /// Draws the control
        /// </summary>
        /// <param name="e">Drawing surface data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var gxOff = GraphicsEx.FromImage(MemoryBitmap))
            {
                var topRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height / 2);
                var bottomRect = new Rectangle(0, topRect.Height, ClientSize.Width, ClientSize.Height / 2);

                if (!Pushed)
                {
                    switch (ColorTheme)
                    {
                        case ButtonTheme.Black:
                            gxOff.GradientFill(bottomRect, Color.FromArgb(0, 0, 11), Color.FromArgb(32, 32, 32), GradientFillDirection.Vertical);
                            gxOff.GradientFill(topRect, Color.FromArgb(176, 176, 176), Color.FromArgb(32, 32, 32), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Red:
                            gxOff.GradientFill(bottomRect, Color.FromArgb(11, 0, 0), Color.FromArgb(32, 0, 0), GradientFillDirection.Vertical);
                            gxOff.GradientFill(topRect, Color.FromArgb(176, 0, 0), Color.FromArgb(32, 0, 0), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Green:
                            gxOff.GradientFill(bottomRect, Color.FromArgb(0, 11, 0), Color.FromArgb(0, 32, 0), GradientFillDirection.Vertical);
                            gxOff.GradientFill(topRect, Color.FromArgb(0, 176, 0), Color.FromArgb(0, 32, 0), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Blue:
                            gxOff.GradientFill( bottomRect, Color.FromArgb( 0, 0, 11 ), Color.FromArgb( 0, 0, 11 ), GradientFillDirection.Vertical );
                            gxOff.GradientFill( topRect, Color.FromArgb( 0, 0, 176 ), Color.FromArgb( 0, 0, 32 ), GradientFillDirection.Vertical );
                            break;
                        case ButtonTheme.Default:
                            throw new NotImplementedException("This feature is yet to be implemented");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    switch (ColorTheme)
                    {
                        case ButtonTheme.Black:
                            gxOff.GradientFill(topRect, Color.FromArgb(0, 0, 11), Color.FromArgb(32, 32, 32), GradientFillDirection.Vertical);
                            gxOff.GradientFill(bottomRect, Color.FromArgb(176, 176, 176), Color.FromArgb(32, 32, 32), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Red:
                            gxOff.GradientFill(topRect, Color.FromArgb(11, 0, 0), Color.FromArgb(32, 0, 0), GradientFillDirection.Vertical);
                            gxOff.GradientFill(bottomRect, Color.FromArgb(176, 0, 0), Color.FromArgb(32, 0, 0), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Green:
                            gxOff.GradientFill(topRect, Color.FromArgb(0, 11, 0), Color.FromArgb(0, 32, 0), GradientFillDirection.Vertical);
                            gxOff.GradientFill(bottomRect, Color.FromArgb(0, 176, 0), Color.FromArgb(0, 32, 0), GradientFillDirection.Vertical);
                            break;
                        case ButtonTheme.Blue:
                            gxOff.GradientFill( topRect, Color.FromArgb( 0, 0, 11 ), Color.FromArgb( 0, 0, 11 ), GradientFillDirection.Vertical );
                            gxOff.GradientFill( bottomRect, Color.FromArgb( 0, 0, 176 ), Color.FromArgb( 0, 0, 32 ), GradientFillDirection.Vertical );
                            break;
                        case ButtonTheme.Default:
                            throw new NotImplementedException("This feature is yet to be implemented");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

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
                    catch (PlatformNotSupportedException)
                    {
                        Debug.WriteLine("AlphaBlend is not a supported GDI feature on this device");
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

        private bool ShouldSerializeMode()
        {
            return Mode != ButtonMode.TwoDimensional;
        }

        private bool ShouldSerializeColorTheme()
        {
            return ColorTheme != ButtonTheme.Black;
        } 
        #endregion
    }
}