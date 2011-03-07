#region Imported Namespaces

using System;
using System.ComponentModel;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Represents the wizard view
    /// </summary>
    public partial class WizardView : MobileForm, IWizardView
    {
        /// <summary>
        /// Creates an instance of <see cref="WizardView"/>
        /// </summary>
        public WizardView()
        {
            InitializeComponent();
        }

        #region Event Forwarding

        private void LoginWizardView_Load(object sender, EventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewLoad, sender);
        }

        private void LoginWizardView_Closing(object sender, CancelEventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewClose, sender);
        }

        #endregion

        #region IWizardView Members

        /// <summary>
        /// Fired when the view loads
        /// </summary>
        public event EventHandler ViewLoad;

        /// <summary>
        /// Fired when the Close button is clicked
        /// </summary>
        public event EventHandler ViewClose;

        /// <summary>
        /// Gets the container where the wizard steps are to be docked to
        /// </summary>
        public Control Container
        {
            get { return this; }
        }

        /// <summary>
        /// Fired to navigate to the next step in the wizard
        /// </summary>
        public event EventHandler Next;

        /// <summary>
        /// Fired to navigate to the previous step in the wizard
        /// </summary>
        public event EventHandler Previous;

        /// <summary>
        /// Fired to complete the wizard
        /// </summary>
        public event EventHandler Finish;

        /// <summary>
        /// Fired when the wizard is cancelled
        /// </summary>
        public event EventHandler Cancel;

        #endregion
    }
}