using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Base class for all owner drawn controls
    /// </summary>
    public class OwnerDrawnBase : Control
    {
        private Bitmap memoryBitmap;

        /// <summary>
        /// Instantiating <see cref="OwnerDrawnBase"/> is not allowed
        /// </summary>
        protected OwnerDrawnBase() { }

        /// <summary>
        /// Memory bitmap used for off screen drawing
        /// </summary>
        protected Bitmap MemoryBitmap
        {
            get
            {
                if (memoryBitmap == null)
                    memoryBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
                return memoryBitmap;
            }
            private set { memoryBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <remarks>
        /// Returns <c>true</c> if the control can respond to user interaction; 
        /// otherwise, <c>false</c>. The default is <c>true</c>.
        /// </remarks>
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Forces the control to redraw when the Text property is changed
        /// </summary>
        /// <param name="e">Event data</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        ///  Forces the control to redraw when the Parent property is changed
        /// </summary>
        /// <param name="e">Event data</param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Forces the control to redraw when the control is resized
        /// </summary>
        /// <param name="e">Event data</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeControl();
        }

        /// <summary>
        /// Disposed the <see cref="MemoryBitmap"/> and redraws the control
        /// </summary>
        protected virtual void ResizeControl()
        {
            if (MemoryBitmap != null)
            {
                MemoryBitmap.Dispose();
                MemoryBitmap = null;
            }

            if (ClientSize.Width != 0 && ClientSize.Height != 0)
                MemoryBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

            Invalidate();
        }
    }
}