using System.Drawing;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Extends the functionality of the <see cref="Label"/> control by adding Transparency features
    /// </summary>
    /// <remarks>
    /// To enable transparency, this control must be placed on a container control that implements
    /// <see cref="IControlBackground"/>
    /// </remarks>
    public class LabelEx : TransparentControl
    {
        private readonly StringFormat format;
        private ContentAlignment alignment = ContentAlignment.TopLeft;
        private bool autosize = true;

        /// <summary>
        /// Creates an instance of the <see cref="LabelEx"/> control
        /// </summary>
        public LabelEx()
        {
            format = new StringFormat();
        }

        /// <summary>
        /// Gets or sets whether to autofit the control according to the text
        /// </summary>
        public bool AutoSize
        { get { return this.autosize; } set { this.autosize = value; } }

        /// <summary>
        /// Gets or sets the alignment of text in the label.
        /// </summary>
        public ContentAlignment TextAlign
        {
            get { return alignment; }
            set
            {
                alignment = value;
                switch (alignment)
                {
                    case ContentAlignment.TopCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Far;
                        break;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated to this control
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.ResizeControl();
            }
        }

        /// <summary>
        /// Resizes the control to fit the <see cref="Text"/>
        /// </summary>
        protected override void ResizeControl()
        {
            base.ResizeControl();

            using (Graphics g = CreateGraphics())
            {
                if ( this.autosize )
                {
                    var size = g.MeasureString( base.Text, Font );
                    Size = new Size( ( int )size.Width, ( int )size.Height );
                }
            }
        }

        /// <summary>
        /// Draws the control
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(ForeColor))
                e.Graphics.DrawString(
                    Text,
                    Font,
                    brush,
                    new Rectangle(0, 0, Width, Height),
                    format);
        }

        #region Code Behind Generation
        private bool ShouldSerializeForeColor()
        {
            return ForeColor != SystemColors.ControlText;
        }

        private bool ShouldSerializeBackColor()
        {
            return BackColor != Color.White;
        }

        private bool ShouldSerializeTextAlign()
        {
            return TextAlign != ContentAlignment.TopLeft;
        }

        private bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(Text);
        }
        #endregion
    }
}