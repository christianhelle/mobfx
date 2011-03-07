using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using Microsoft.WindowsCE.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// A sidebar to display a company logo or text to be attached to a form
    /// </summary>
    public class Sidebar : Control
    {
        /// <summary>
        /// Creates an instance of <see cref="Sidebar"/>
        /// </summary>
        public Sidebar()
        {
            base.Location = new Point(0, 0);
            base.Size = new Size(Dpi.Scale(25), Screen.PrimaryScreen.WorkingArea.Height);

            base.Dock = DockStyle.Left;
            base.ForeColor = Color.White;
        }

        /// <summary>
        /// Gets or sets the size and location of the control including its non-client elements,
        /// in pixels, relative to the parent control
        /// </summary>
        public new Rectangle Bounds
        {
            get { return base.Bounds; }
            set { }
        }

        /// <summary>
        /// Gets or sets the height and width of the control
        /// </summary>
        public new Size Size
        {
            get { return base.Size; }
            set { }
        }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the 
        /// control relative to the upper-left corner of its container.
        /// </summary>
        public new Point Location
        {
            get { return base.Location; }
            set { }
        }

        /// <summary>
        /// Raised when the Text property is changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raised when the Parent property is changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(System.EventArgs e)
        {
            base.OnParentChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raised when the form is painted
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var buffer = new Bitmap(Bounds.Width, Bounds.Height))
            {
                using (var g = GraphicsEx.FromImage(buffer))
                {
                    g.GradientFill(
                        Bounds,
                        Color.DarkBlue,
                        Color.LightBlue,
                        GradientFillDirection.Vertical);

                    using (var font = FontFactory.CreateRotatedFont(Font.Name, 50))
                    {
                        var size = g.Surface.MeasureString(Text, font);
                        var x = (Bounds.Width - size.Width) / 2f;

                        using (var brush = new SolidBrush(ForeColor))
                            g.Surface.DrawString(Text, font, brush, x, Dpi.Scale(5));
                    }
                }

                e.Graphics.DrawImage(buffer, 0, 0);
            }
        }

        /// <summary>
        /// Nothing is done here, all painting is done in OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        #region Code Behind Generation

        private bool ShouldSerializeBounds()
        {
            return false;
        }

        private bool ShouldSerializeSize()
        {
            return false;
        }

        private bool ShouldSerializeLocation()
        {
            return false;
        }

        private bool ShouldSerializeDock()
        {
            return false;
        }

        private bool ShouldSerializeAnchor()
        {
            return false;
        }

        #endregion
    }
}
