using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    /// <summary>
    /// Represents the IView of the Smart Menu Form
    /// </summary>
    public interface ISmartMenuView : IFormView
    {
        /// <summary>
        /// fired when the view needs to paint/repaint itself
        /// </summary>
        event PaintEventHandler ViewPaint;

        /// <summary>
        /// fired when the view is resized
        /// </summary>
        event EventHandler ViewResize;

        /// <summary>
        /// Gets the rectangle that represents the client area of the form
        /// </summary>
        Rectangle ViewRectangle { get; }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// Gets the collection of controls contained within the control
        /// </summary>
        new Control.ControlCollection Controls { get; }

        /// <summary>
        /// Invalidates the specified region of the SmartMenuView
        /// </summary>
        /// <param name="rc">A Rectangle that represents the region to invalidate.</param>
        void Invalidate(Rectangle rc);
    }
}