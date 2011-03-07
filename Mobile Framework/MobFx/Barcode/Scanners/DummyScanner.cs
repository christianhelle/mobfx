using System;
using System.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Barcode.Scanners
{
    internal class DummyScanner : IBarcodeScanner
    {
        public DummyScanner()
        {
            Debug.WriteLine("Dummy Scanner Loaded");
        }

        #region IBarcodeScanner Members

        public ScannerStatus Status
        {
            get { return ScannerStatus.NotAvailable; }
        }

        public event EventHandler<ScannedDataEventArgs> Scanned;

        public void Open()
        {
        }

        public void Close()
        {
        }

        public void Scan()
        {
            if (Scanned != null)
                Scanned.Invoke(this, new ScannedDataEventArgs(new[] { "NO_BARCODE_SCANNER" }));
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
