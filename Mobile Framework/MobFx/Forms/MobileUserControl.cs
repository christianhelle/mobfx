#region Imported Namespaces

using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Core;
using ChristianHelle.Framework.WindowsMobile.Drawing;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Provides an empty control that can be used to create other controls.
    /// </summary>
    public partial class MobileUserControl : UserControl, IControlBackground
    {
        private bool autoScaleBackgroundImage;
        private ImageDrawMode backgroundDrawMode;
        private Image backgroundImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileUserControl"/> class.
        /// </summary>
        public MobileUserControl()
        {
            InitializeComponent();
        }

        #region IControlBackground Members

        /// <summary>
        /// Gets or sets the background image of the <see cref="MobileUserControl"/>
        /// </summary>
        public Image BackgroundImage
        {
            get { return backgroundImage; }
            set
            {
                backgroundImage = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets how the <see cref="BackgroundImage"/> 
        /// will be drawn on <see cref="MobileUserControl"/> 
        /// </summary>
        public ImageDrawMode BackgroundDrawMode
        {
            get { return backgroundDrawMode; }
            set
            {
                backgroundDrawMode = value;

                // We need to redraw the child controls as well to not break the transparency effect
                Refresh();
            }
        }

        /// <summary>
        /// Gets or Sets how the <see cref="BackgroundImage"/> will be drawn depending 
        /// on the devices screen resolution
        /// </summary>
        /// <remarks>
        /// Set this to <c>true</c> to automatically the background image on high DPI devices.
        /// This way the developer does not need to provide seperate images for each device
        /// screen resolution. The difference notificability depends entirely on the 
        /// quality of the background image.
        /// </remarks>
        public bool AutoScaleBackgroundImage
        {
            get { return autoScaleBackgroundImage; }
            set
            {
                autoScaleBackgroundImage = value;

                // We need to redraw the child controls as well to not break the transparency effect
                Refresh();
            }
        }

        #endregion

        #region Code Behind Generation

        private bool ShouldSerializeBackgroundImage()
        {
            return BackgroundImage != null;
        }

        private bool ShouldSerializeBackgroundDrawMode()
        {
            return BackgroundDrawMode != ImageDrawMode.Normal;
        }

        private bool ShouldSerializeAutoScaleBackgroundImage()
        {
            return AutoScaleBackgroundImage;
        }

        #endregion

        /// <summary>
        /// Brings the control to the front of the z-order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method hides the <see cref="Control.BringToFront"/> method and P/Invokes
        /// SetForegroundWindow(HWND) instead.
        /// </para><para>
        /// The SetForegroundWindow function puts the thread that created the specified window into the 
        /// foreground and activates the window. Keyboard input is directed to the window, and various 
        /// visual cues are changed for the user. The system assigns a slightly higher priority to the 
        /// thread that created the foreground window than it does to other threads. 
        /// </para>
        /// </remarks>
        public new void BringToFront()
        {
            SystemWindow.SetForegroundWindow(Handle);
        }

        /// <summary>
        /// Draws the <see cref="BackgroundImage"/> property onto <see cref="MobileUserControl"/>
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (BackgroundImage == null)
                return;

            var image = BackgroundImage;
            if (AutoScaleBackgroundImage && Dpi.IsHiDpi)
                image = ImageManipulator.Stretch(BackgroundImage, Dpi.ScaleSize(BackgroundImage.Size));

            var gfx = GraphicsEx.FromGraphics(e.Graphics);
            gfx.DrawImage(image, ClientSize, BackgroundDrawMode);
        }

        /// <summary>
        /// Forces a redraw of the <see cref="backgroundImage"/>
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        /// <summary>
        /// Forces a redraw of the <see cref="backgroundImage"/>
        /// </summary>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Invalidate();
        }
    }
}