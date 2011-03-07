using System;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns;

namespace ChristianHelle.Framework.WindowsMobile.Forms.Login
{
    internal partial class LoginView : MobileForm, IFormView
    {
        internal LoginView()
        {
            InitializeComponent();
        }

        #region Implementation of IFormView

        public event EventHandler ViewLoad;
        public event EventHandler ViewClose;

        #endregion

        private void LoginView_Load(object sender, EventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewLoad, sender);
        }

        private void LoginView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewHelper.ForwardEventToPresenter(ViewClose, sender);
        }
    }
}