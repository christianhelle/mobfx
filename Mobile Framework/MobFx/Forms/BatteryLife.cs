using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Creates a simple custom progress bar to display the percentage of battery 
    /// life left on a device.
    /// </summary>
    /// <remarks>This control will not work as expected on the emulator.</remarks>
    public class BatteryLife : OwnerDrawnBase
    {
        private static readonly bool onDesktop = !(Environment.OSVersion.Platform == PlatformID.WinCE);
        private readonly PowerStatus powerStatus;
        private const byte borderSize = 1;
        private Bitmap bitmap;
        private SolidBrush brushPercentage;
        private SolidBrush brushStatusBarColor;
        private bool disposed;
        private Graphics graphics;
        private Pen penBorderColor;
        private Timer pollTimer;

        /// <summary>
        /// Initializes a new instance of the BatteryLife class.
        /// </summary>
        public BatteryLife()
        {
            brushPercentage = new SolidBrush(Color.LightSlateGray);
            penBorderColor = new Pen(Color.Black);
            brushStatusBarColor = new SolidBrush(Color.Lime);

            if (onDesktop)
                return;

            powerStatus = new PowerStatus();

            pollTimer = new Timer();
            pollTimer.Tick += PollTimerTick;
            pollTimer.Interval = 30000;
            pollTimer.Enabled = true;            
        }

        void PollTimerTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// The color used to display the battery life percentage.
        /// </summary>
        public override Color ForeColor
        {
            get
            {
                if (brushPercentage != null)
                    return brushPercentage.Color;
                return Color.Empty;
            }
            set
            {
                if (brushPercentage != null)
                    brushPercentage.Color = value;
                else
                    brushPercentage = new SolidBrush(value);
                base.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The color used to display the 1 pixel border around the control.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                if (penBorderColor != null)
                    return penBorderColor.Color;
                return Color.Empty;
            }
            set
            {
                if (penBorderColor != null)
                    penBorderColor.Color = value;
                else
                    penBorderColor = new Pen(value);
                Invalidate();
            }
        }

        /// <summary>
        /// The color used to display the status bar representing the percentage of battery life.
        /// </summary>
        public Color StatusBarColor
        {
            get
            {
                if (brushStatusBarColor != null)
                    return brushStatusBarColor.Color;
                return Color.Empty;
            }
            set
            {
                if (brushStatusBarColor != null)
                    brushStatusBarColor.Color = value;
                else
                    brushStatusBarColor = new SolidBrush(value);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets a value that represents the status of AC power to the device.
        /// </summary>
        public PowerLineStatus ACPowerStatus
        {
            get
            {
                if (onDesktop)
                    return PowerLineStatus.Unknown;

                return powerStatus.PowerLineStatus;
            }
        }

        /// <summary>
        /// Gets a value that represents the status of the battery charge.
        /// </summary>
        public BatteryChargeStatus BatteryChargeStatus
        {
            get
            {
                if (onDesktop)
                    return BatteryChargeStatus.Unknown;

                return powerStatus.BatteryChargeStatus;
            }
        }

        /// <summary>
        /// Gets a value that represents the percentage of battery life left in the device.
        /// </summary>
        public byte BatteryLifePercent
        {
            get
            {
                if (onDesktop)
                    return 0xFF;

                return powerStatus.BatteryLifePercent;
            }
        }

        /// <summary>
        /// Gets a value that represents the number of seconds of battery life remaining, or -1 if remaining seconds are unknown.
        /// </summary>
        public int BatteryLifeTime
        {
            get
            {
                if (onDesktop)
                    return -1;

                return powerStatus.BackupBatteryLifeRemaining;
            }
        }

        /// <summary>
        /// Gets a value that represents the number of seconds of battery life when at full charge, or -1 if full lifetime is unknown.
        /// </summary>
        public int BatteryFullLifeTime
        {
            get
            {
                if (onDesktop)
                    return -1;

                return powerStatus.BatteryFullLifeTime;
            }
        }

        /// <summary>
        /// Destroys this BatteryLife object.
        /// </summary>
        ~BatteryLife()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="BatteryLife"/> object.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by this BatteryLife object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        /// <remarks>This method is called by the public Dispose() method and the Finalize method. <b>Dispose()</b> invokes the protected <b>Dispose(Boolean)</b> method with the disposing parameter set to <b>true</b>. <b>Finalize</b> invokes <b>Dispose</b> with <i>disposing</i> set to <b>false</b>.</remarks>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    if (disposing)
                    {
                        // Dispose managed resources.
                        if (pollTimer != null) pollTimer.Dispose();
                        if (bitmap != null) bitmap.Dispose();
                        if (graphics != null) graphics.Dispose();
                        if (penBorderColor != null) penBorderColor.Dispose();
                        if (brushStatusBarColor != null) brushStatusBarColor.Dispose();
                        if (brushPercentage != null) brushPercentage.Dispose();
                    }
                    disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //hard code value when shown on desktop
            int bat;

            if (onDesktop)
                bat = 75;
            else
                bat = powerStatus.BatteryLifePercent;

            string text = bat + "%";

            SizeF size = e.Graphics.MeasureString(text, Font);

            // Paint the background.
            var backColorBrush = new SolidBrush(BackColor);
            graphics.FillRectangle(backColorBrush, 0, 0, Width, Height);
            backColorBrush.Dispose();

            graphics.FillRectangle(brushStatusBarColor, ClientRectangle.X + (borderSize + 1),
                                   ClientRectangle.Y + (borderSize + 1),
                                   Convert.ToInt32((ClientRectangle.Width - ((borderSize + 1) * 2)) *
                                                   (Convert.ToSingle(bat) / 100)),
                                   (ClientRectangle.Height - (borderSize + 1) * 2));
            graphics.DrawRectangle(penBorderColor, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1,
                                   ClientRectangle.Height - 1);
            graphics.DrawString(text, Font, brushPercentage, ((ClientRectangle.Width / 2) - (size.Width / 2)),
                                ((ClientRectangle.Height / 2) - (size.Height / 2)));

            e.Graphics.DrawImage(bitmap, 0, 0);

            base.OnPaint(e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (bitmap != null)
                bitmap.Dispose();
            if (graphics != null)
                graphics.Dispose();

            bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            graphics = Graphics.FromImage(bitmap);

            Invalidate();

            base.OnResize(e);
        }

        /// <summary>
        /// Forces an update of the progress bar that represents the battery life percentage. 
        /// </summary>
        /// <returns><b>true</b> if successful, otherwise <b>false</b></returns>
        public void UpdateBatteryLife()
        {
            Invalidate();
        }

        private bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(base.Text);
        }
    }
}