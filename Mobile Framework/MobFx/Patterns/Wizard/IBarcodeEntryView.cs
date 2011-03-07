using System;
using ChristianHelle.Framework.WindowsMobile.Patterns;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Represents a generic passive view for barcode entry
    /// </summary>
    public interface IBarcodeEntryView : IUserControlView
    {
        /// <summary>
        /// Fired when the back button is clicked
        /// </summary>
        event EventHandler BackClicked;
        /// <summary>
        /// Fired when the barcode text changes
        /// </summary>
        event EventHandler<BarcodeEventArgs> BarcodeChanged;
        /// <summary>
        /// Gets or sets the value of the barcode field
        /// </summary>
        string Barcode { get; set; }

        /// <summary>
        /// Gets the input control
        /// </summary>
        Control BarcodeInputControl { get; }
    }
}