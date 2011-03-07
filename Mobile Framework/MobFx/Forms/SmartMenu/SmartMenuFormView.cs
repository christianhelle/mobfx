#region Imported Namespaces

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    internal partial class SmartMenuFormView : MobileForm, ISmartMenuView
    {
        internal SmartMenuFormView()
        {
            InitializeComponent();
        }

        #region Implementation of IView

        /// <summary>
        /// Fired when the view loads
        /// </summary>
        public event EventHandler ViewLoad;

        #endregion

        #region Implementation of IFormView

        /// <summary>
        /// Fired when the Close button is clicked
        /// </summary>
        public event EventHandler ViewClose;

        #endregion

        #region Implementation of ISmartMenuView

        /// <summary>
        /// fired when the view needs to paint/repaint itself
        /// </summary>
        public event PaintEventHandler ViewPaint;

        /// <summary>
        /// fired when the view is resized
        /// </summary>
        public event EventHandler ViewResize;

        /// <summary>
        /// Gets the rectangle that represents the client area of the form
        /// </summary>
        public Rectangle ViewRectangle
        {
            get { return ClientRectangle; }
        }

        #endregion

        private void SmartMenuForm_Load(object sender, EventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewLoad, sender);
        }

        private void SmartMenuFormView_Closing(object sender, CancelEventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewClose, sender);
        }

        private void SmartMenuFormView_Paint(object sender, PaintEventArgs e)
        {
            if (ViewPaint != null)
                ViewPaint.Invoke(sender, e);
        }

        private void SmartMenuFormView_Resize(object sender, EventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewResize, sender);
        }
    }
}