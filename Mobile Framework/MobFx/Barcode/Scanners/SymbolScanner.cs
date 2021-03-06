﻿using System;
using System.Collections.Generic;
using Symbol;
using Symbol.Barcode;
using System.Threading;

namespace ChristianHelle.Framework.WindowsMobile.Barcode.Scanners
{
    /// <summary>
    /// Symbol Barcode scanner device
    /// </summary>
    public class SymbolScanner : IBarcodeScanner
    {
        private const int TimeoutMilliseconds = 10000;
        private readonly EventHandler scanEvent;

        private Reader reader;
        private ReaderData readerData;
        private ScannerStatus scannerStatus;
        private Timer timeout;

        /// <summary>
        /// Event raised when the scanned data changes
        /// </summary>
        public event EventHandler<ScannedDataEventArgs> Scanned;

        /// <summary>
        /// Create an instance of the Symbol Barcode Scanner device
        /// </summary>
        public SymbolScanner()
        {
            scanEvent = new EventHandler(ScannedDataEvent);
            scannerStatus = ScannerStatus.Closed;                        
        }

        /// <summary>
        /// Get the scannerStatus of the scanner device
        /// </summary>
        public ScannerStatus Status
        {
            get
            {
                if (reader != null)
                {
                    try
                    {
                        if (reader.Info.SoftTrigger)
                            return ScannerStatus.Scanning;
                        return scannerStatus;
                    }
                    catch
                    {
                        return scannerStatus;
                    }
                }
                return scannerStatus;
            }
        }

        /// <summary>
        /// Open the scanner device
        /// </summary>
        public void Open()
        {
            if (scannerStatus == ScannerStatus.Opened)
                return;

            try
            {
                reader = new Reader();
                readerData = new ReaderData(ReaderDataTypes.Text, ReaderDataLengths.MaximumLabel);
                reader.Actions.Enable();
                
                StartRead();

                scannerStatus = ScannerStatus.Opened;
            }
            catch
            {
                Close();
                throw;
            }
        }

        /// <summary>
        /// Close the scanner device
        /// </summary>
        public void Close()
        {
            if (reader == null)
            {
                scannerStatus = ScannerStatus.Closed;
                return;
            }

            StopRead();

            reader.Actions.Disable();
            reader.Dispose();
            reader = null;
            readerData.Dispose();
            readerData = null;

            scannerStatus = ScannerStatus.Closed;
        }

        /// <summary>
        /// Start Scanning
        /// </summary>
        public void Scan()
        {
            if (reader == null || Status != ScannerStatus.Opened)
                return;
            
            scannerStatus = ScannerStatus.Scanning;
            Thread.Sleep(10);                                                                                          

            reader.Actions.ToggleSoftTrigger();
            timeout = new Timer(TimeoutCallback, this, TimeoutMilliseconds, Timeout.Infinite);
        }        

        /// <summary>
        /// Clean up resources used by the scanner device
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called to free resources.
        /// </summary>
        /// <param name="disposing">Should be true when calling from Dispose().</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timeout != null)
                {
                    timeout.Dispose();
                    timeout = null;
                }
                Close();
            }
        }

        private void ScannedDataEvent(object sender, EventArgs e)
        {
            ReaderData data = reader.GetNextReaderData();
            if (data != null)
            {
                if (data.Result == Results.SUCCESS)
                {
                    HandleData(data);
                    StartRead();
                }
            }
        }

        private void StartRead()
        {
            reader.ReadNotify += scanEvent;
            reader.Actions.Read(readerData);
        }

        private void StopRead()
        {
            reader.Actions.Flush();
            reader.ReadNotify -= scanEvent;
        }

        private void HandleData(ReaderData reader)
        {
            List<string> list = new List<string>();
            list.Add(reader.Text);

            if (Scanned != null)
                Scanned.Invoke(this, new ScannedDataEventArgs(list.ToArray()));

            scannerStatus = ScannerStatus.Opened;
        }

        private void TimeoutCallback(object state)
        {
            var instance = (SymbolScanner)state;
            instance.reader.Info.SoftTrigger = false;
            instance.scannerStatus = ScannerStatus.Opened;
            timeout.Dispose();
        }
    }
}