using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ChristianHelle.Framework.WindowsMobile.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Represents the barcode entry presenter
    /// </summary>
    /// <remarks>this class is to automatically handle and validate barcodes scanned or entered</remarks>
    public abstract class BarcodeEntryPresenter : WizardStepPresenter
    {
        protected string errorMessage;

        /// <summary>
        /// Creates an instance of <see cref="BarcodeEntryPresenter"/>
        /// </summary>
        /// <param name="view">passive view implementation</param>
        /// <param name="stateKey">state key to use for the <see cref="WizardPresenter.ViewState"/></param>
        protected BarcodeEntryPresenter(IBarcodeEntryView view, string stateKey)
            : base(view, stateKey)
        {
        }

        /// <summary>
        /// Gest or sets the passive view implementation
        /// </summary>
        public IBarcodeEntryView BarcodeEntryView
        {
            get { return (IBarcodeEntryView)BaseView; }
        }

        /// <summary>
        /// Subscribes this presenter to the events of its corresponding View
        /// </summary>
        protected override void AttachView()
        {
            BarcodeEntryView.BackClicked += OnBarcodeEntryViewOnBackClicked;
            BarcodeEntryView.BarcodeChanged += OnBarcodeEntryViewOnBarcodeChanged;
        }

        /// <summary>
        /// Unsubscribes this presenter to the events of its corresponding View
        /// </summary>
        protected override void DeattachView()
        {
            BarcodeEntryView.BackClicked -= OnBarcodeEntryViewOnBackClicked;
            BarcodeEntryView.BarcodeChanged -= OnBarcodeEntryViewOnBarcodeChanged;

            base.DeattachView();
        }

        private void OnBarcodeEntryViewOnBarcodeChanged(object sender, BarcodeEventArgs e)
        {
            ValidateBarcode(e.Barcode, true);
        }

        private void OnBarcodeEntryViewOnBackClicked(object sender, EventArgs e)
        {
            InvokePrevious(new WizardStepEventArgs(WizardStateKey, null));
        }

        /// <summary>
        /// Validate the barcode
        /// </summary>
        /// <param name="barcode">barcode to validate</param>
        /// <param name="navigateForward">
        /// Set to <c>true</c> to automatically navigate to the next wizard step if the barcode is valid, 
        /// otherwise <c>false</c>
        /// </param>
        /// <returns>Returns <c>true</c> if the barcode is valid, otherwise <c>false</c></returns>
        public virtual bool ValidateBarcode(string barcode, bool navigateForward)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                MessagePrompt.ShowWarning("Missing Field", "Error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Builds a the <see cref="WizardStepEventArgs"/> state information from the first row in a <see cref="DataTable"/>
        /// </summary>
        /// <param name="dataTable">data table source</param>
        /// <param name="barcode">barcode value for the <see cref="WizardStepPresenter.WizardStateKey"/> as the key</param>
        /// <returns>
        /// Returns a <see cref="WizardStepEventArgs"/> using the <see cref="WizardStepPresenter.WizardStateKey"/> 
        /// as the key and a key/value (string/object) pair collection as the state
        /// </returns>
        protected WizardStepEventArgs BuildViewState(DataTable dataTable, string barcode)
        {
            return BuildViewState(dataTable, barcode, null);
        }

        /// <summary>
        /// Builds a the <see cref="WizardStepEventArgs"/> state information from the first row in a <see cref="DataTable"/>
        /// </summary>
        /// <param name="dataTable">data table source</param>
        /// <param name="barcode">barcode value for the <see cref="WizardStepPresenter.WizardStateKey"/> as the key</param>
        /// <returns>
        /// Returns a <see cref="WizardStepEventArgs"/> using the <see cref="WizardStepPresenter.WizardStateKey"/> 
        /// as the key and a key/value (string/object) pair collection as the state
        /// </returns>
        protected WizardStepEventArgs BuildViewState(DataTable dataTable, string barcode, int? wizardIndex)
        {
            Debug.WriteLine("Building ViewState for: " + WizardStateKey);

            var state = new Dictionary<string, object> { { WizardStateKey, barcode } };
            foreach (DataColumn column in dataTable.Columns)
            {
                var value = dataTable.Rows[0][column.ColumnName];
                state.Add(column.ColumnName, value);

                Debug.WriteLine(column.ColumnName + ": " + value);
            }
            return new WizardStepEventArgs(WizardStateKey, state, wizardIndex);
        }

        /// <summary>
        /// Builds a the <see cref="WizardStepEventArgs"/> state information from the first row in a <see cref="DataSet"/>
        /// </summary>
        /// <param name="dataSet">data source</param>
        /// <param name="barcode">barcode value for the <see cref="WizardStepPresenter.WizardStateKey"/> as the key</param>
        /// <returns>
        /// Returns a <see cref="WizardStepEventArgs"/> using the <see cref="WizardStepPresenter.WizardStateKey"/> 
        /// as the key and a key/value (string/object) pair collection as the state
        /// </returns>
        protected WizardStepEventArgs BuildViewState(DataSet dataSet, string barcode)
        {
            Debug.WriteLine("Building ViewState for: " + WizardStateKey);

            var state = new Dictionary<string, object> { { WizardStateKey, barcode } };
            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    var value = dataTable.Rows[0][column.ColumnName];

                    if (state.ContainsKey(column.ColumnName))
                    {
                        if (state[column.ColumnName] == value) 
                            continue;
                        state[column.ColumnName] = value;
                    }
                    else
                        state.Add(column.ColumnName, value);

                    Debug.WriteLine(column.ColumnName + ": " + value);
                }
            }

            return new WizardStepEventArgs(WizardStateKey, state);
        }
    }
}