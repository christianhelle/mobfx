using System;

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Event arguments that contains the barcode scanned
    /// </summary>
    public class BarcodeEventArgs : EventArgs
    {
        /// <summary>
        /// Barcode
        /// </summary>
        public string Barcode { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="BarcodeEventArgs"/>
        /// </summary>
        /// <param name="barcode">barcode scanned</param>
        public BarcodeEventArgs(string barcode)
        {
            Barcode = barcode;
        }
    }
}
