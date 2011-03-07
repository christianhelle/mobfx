using System;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Base class for transparent buttons
    /// </summary>
    public class TransparentButton : TransparentControl
    {
        /// <summary>
        /// Instantiating <see cref="TransparentControl"/> is not allowed
        /// </summary>
        protected TransparentButton()
        {
        }

        /// <summary>
        /// Gets or sets the pushed state of the button
        /// </summary>
        protected bool Pushed { get; private set; }

        /// <summary>
        /// Sets the pushed flag to TRUE
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Pushed = true;
            Invalidate();
        }

        /// <summary>
        /// Sets the pushed flag to FALSE
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            Pushed = false;
            Invalidate();
        }

        /// <summary>
        /// Sets the <see cref="Control.BackColor"/> property to the value of the 
        /// <see cref="Control.Parent"/>'s <see cref="Control.BackColor"/> property
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent != null)
                BackColor = Parent.BackColor;
        }
    }
}