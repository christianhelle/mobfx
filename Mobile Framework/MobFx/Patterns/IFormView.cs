#region Imported Namespaces

using System;
using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// View interface for any Form
    /// </summary>
    public interface IFormView : IView
    {
        /// <summary>
        /// Gets or sets the dialog result for the form
        /// </summary>
        DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets or sets the form that owns this View
        /// </summary>
        Form Owner { get; set; }

        /// <summary>
        /// Shows the form as a modal dialog box with the currently active window set as its owner.
        /// </summary>
        DialogResult ShowDialog();

        /// <summary>
        /// Closes the form
        /// </summary>
        void Close();

        /// <summary>
        /// Fired when the Close button is clicked
        /// </summary>
        event EventHandler ViewClose;

        /// <summary>
        /// Gets or sets a value indicating whether the form should be displayed as a topmost form
        /// </summary>
        /// <remarks>
        /// true to display the form as a topmost form; otherwise, false. The default is false
        /// </remarks>
        bool TopMost { get; set; }
    }
}