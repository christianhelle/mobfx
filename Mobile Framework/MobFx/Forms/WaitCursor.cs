using System;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Helper class for Displaying/Hiding the Busy Cursor
    /// </summary>
    /// <remarks>
    /// Use this class to display a busy cursor during a long running or performance heavy operation
    /// </remarks>
    /// <example>
    /// This is an example of how you might use this class:
    /// 
    /// <code>
    /// using (new WaitCursor())
    /// {
    ///     // Some long running or performance heavy operation
    /// }
    /// </code>
    /// </example>
    public class WaitCursor : IDisposable
    {
        /// <summary>
        /// Creates an instance of <see cref="WaitCursor"/> and displays the Busy Cursor
        /// </summary>
        public WaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Creates an instance of <see cref="WaitCursor"/> and displays the Busy Cursor
        /// </summary>
        /// <param name="showWaitForm">Set to <c>true</c> to display a wait form</param>
        public WaitCursor(bool showWaitForm)
            : this()
        {
            if (!showWaitForm)
                return;

            var waitForm = Singleton<WaitForm>.Instance;
            if (!MobileApplication.DispoableResources.ContainsKey("WaitForm"))
                MobileApplication.DispoableResources.Add("WaitForm", waitForm);

            Header = string.Empty;
            Message = "Please wait...";

            if (Environment.OSVersion.Platform != PlatformID.WinCE)
                waitForm.WindowState = FormWindowState.Normal;

            waitForm.Show();
            waitForm.Update();
            waitForm.BringToFront();
        }

        /// <summary>
        /// Gets or sets the Header text displayed. 
        /// </summary>
        /// <remarks>Default is "Loading..."</remarks>
        public string Header
        {
            get
            {
                var waitForm = Singleton<WaitForm>.Instance;
                return waitForm != null ? waitForm.Header.Text : null;
            }
            set
            {
                var waitForm = Singleton<WaitForm>.Instance;
                if (waitForm == null) return;
                waitForm.Header.Text = value;
                waitForm.Header.Update();
            }
        }

        /// <summary>
        /// Gets or sets the Header text displayed. 
        /// </summary>
        /// <remarks>Default is NULL</remarks>
        public string Message
        {
            get
            {
                var waitForm = Singleton<WaitForm>.Instance;
                return waitForm != null ? waitForm.Message.Text : null;
            }
            set
            {
                var waitForm = Singleton<WaitForm>.Instance;
                if (waitForm == null) return;
                waitForm.Message.Text = value;
                waitForm.Message.Update();
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="WaitCursor"/> and displays the Waiting Form
        /// </summary>
        public static WaitCursor Create()
        {
            return new WaitCursor();
        }

        /// <summary>
        /// Creates an instance of <see cref="WaitCursor"/> and displays the Waiting Form
        /// </summary>
        /// <param name="message">Message</param>
        public static WaitCursor Create(string message)
        {
            return Create(null, message);
        }

        /// <summary>
        /// Creates an instance of <see cref="WaitCursor"/> and displays the Waiting Form
        /// </summary>
        /// <param name="header">Header text</param>
        /// <param name="message">Message</param>
        public static WaitCursor Create(string header, string message)
        {
            var waitCursor = new WaitCursor(true);
            waitCursor.Header = header;
            waitCursor.Message = message;
            return waitCursor;
        }

        /// <summary>
        /// Displays the wait cursor
        /// </summary>
        public static void Show()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Hides the wait cursor
        /// </summary>
        public static void Hide()
        {
            Cursor.Current = Cursors.Default;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Sets the cursor to the default cursor
        /// </summary>
        public void Dispose()
        {
            var waitForm = Singleton<WaitForm>.Instance;
            if (waitForm != null && waitForm.Visible)
                waitForm.Hide();

            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}