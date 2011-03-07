using System;
using ChristianHelle.Framework.WindowsMobile.Barcode;
using ChristianHelle.Framework.WindowsMobile.Diagnostics;
using ChristianHelle.Framework.WindowsMobile.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Base class for barcode scanner enabled wizard presenters
    /// </summary>
    /// <remarks>This class automatically handles the openning and closing of the barcode scanner device</remarks>
    public abstract class BarcodeEntryWizardPresenter : WizardPresenter
    {
        private bool barcodeProcessing;

        /// <summary>
        /// Creates an instance of<see cref="BarcodeEntryWizardPresenter"/>
        /// </summary>
        /// <param name="view">passive view</param>
        protected BarcodeEntryWizardPresenter(IWizardView view)
            : base(view)
        {
        }

        /// <summary>
        /// Subscribes this presenter to the events of its corresponding View
        /// </summary>
        protected override void AttachView()
        {
            SubscribeScannerEvents();
        }

        /// <summary>
        /// Unsubscribes this presenter to the events of its corresponding View
        /// </summary>
        protected override void DeattachView()
        {
            UnsubscribeScannerEvents();
            base.DeattachView();
        }

        /// <summary>
        /// Enables handling of scanner events
        /// </summary>
        protected void SubscribeScannerEvents()
        {
            BarcodeScannerFacade.Instance.Scanned += OnBarcodeScanned;
        }

        /// <summary>
        /// Disables handling of scanner events
        /// </summary>
        protected void UnsubscribeScannerEvents()
        {
            BarcodeScannerFacade.Instance.Scanned -= OnBarcodeScanned;
        }

        private void OnBarcodeScanned(object sender, ScannedDataEventArgs e)
        {
            HandleScannedBarcode(e.Data);
        }

        /// <summary>
        /// Sends the barcode string to the barcode input control of the visible wizard step
        /// </summary>
        /// <param name="barcodes">Barcodes collected by the scanner</param>
        public void HandleScannedBarcode(string[] barcodes)
        {
            if (barcodeProcessing)
                return;

            try
            {
                barcodeProcessing = true;

                if (CurrentPresenter.BaseView == null || !CurrentPresenter.BaseView.Visible) return;
                if (barcodes == null || barcodes.Length <= 0) return;
                if (string.IsNullOrEmpty(barcodes[0])) return;

                var barcodeEntryPresenter = CurrentPresenter as BarcodeEntryPresenter;
                if (barcodeEntryPresenter == null) return;

                var barcode = IgnoreBarcodeCharacters(barcodes);
                barcodeEntryPresenter.BarcodeEntryView.Barcode = barcode.Trim();
                barcodeEntryPresenter.ValidateBarcode(barcodeEntryPresenter.BarcodeEntryView.Barcode, true);
            }
            finally
            {
                barcodeProcessing = false;
            }
        }

        private static string IgnoreBarcodeCharacters(string[] barcodes)
        {
            var barcode = barcodes[0].Replace("]C1", string.Empty);
            if (barcode.StartsWith("00")) barcode = barcode.Remove(0, 2);
            return barcode;
        }

        /// <summary>
        /// Closes the wizard
        /// </summary>
        public override void CancelWizard()
        {
            try
            {
                ViewState.Clear();
                Position = 0;
                CloseView();
            }
            catch (Exception e)
            {
                var handler = new DeviceExceptionHandler(e);
                handler.HandleException(e.Message, false);
                MessagePrompt.ShowErrorDialog(e, "Unable to cancel the wizard", "Operation Failed");
            }
        }
    }
}
