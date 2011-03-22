using System;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base class for all Presenters/Controllers using a 
    /// <see cref="System.Windows.Forms"/> based UI
    /// </summary>
    public abstract class FormPresenter : Presenter
    {
        /// <summary>
        /// Sets the current <see cref="IFormView"/>
        /// </summary>
        /// <param name="view">Instance of <see cref="IFormView"/></param>
        protected FormPresenter(IFormView view)
        {
            FormView = view;
            AttachView();
        }

        /// <summary>
        /// Instance of the Form
        /// </summary>
        public IFormView FormView { get; set; }

        /// <summary>
        /// Gets an instance of the passive view
        /// </summary>
        /// <typeparam name="T">Type of the passive view</typeparam>
        /// <returns>Returns an instance of the passive view as <paramref name="{T}"/></returns>
        public T GetView<T>() where T : class, IFormView
        {
            return FormView as T;
        }

        /// <summary>
        /// Disposes the instance of <see cref="FormView"/>
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (FormView == null) return;
            FormView.Dispose();
            FormView = null;
        }

        /// <summary>
        /// Opens a UI
        /// </summary>
        /// <typeparam name="T">UI Presenter</typeparam>
        /// <param name="modalDialog">Set to <c>true</c> to display the UI as a modal dialog</param>
        public DialogResult? OpenForm<T>(bool modalDialog) where T : FormPresenter, new()
        {
            return OpenForm<T>(modalDialog, true);
        }

        /// <summary>
        /// Opens a UI
        /// </summary>
        /// <typeparam name="T">UI Presenter</typeparam>
        /// <param name="modalDialog">Set to <c>true</c> to display the UI as a modal dialog</param>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        public DialogResult? OpenForm<T>(bool modalDialog, bool displayWaitCursor) where T : FormPresenter, new()
        {
            using (var presenter = ViewHelper.CreatePresenter<T>(displayWaitCursor))
                return OpenForm(presenter, modalDialog);
        }

        /// <summary>
        /// Opens a UI
        /// </summary>
        /// <typeparam name="T">UI Presenter</typeparam>
        /// <param name="loadMethod">Method to execute when the UI loads</param>
        /// <param name="modalDialog">Set to <c>true</c> to display the UI as a modal dialog</param>
        public DialogResult? OpenForm<T>(bool modalDialog, EventHandler loadMethod) where T : FormPresenter, new()
        {
            return OpenForm<T>(modalDialog, loadMethod, true);
        }

        /// <summary>
        /// Opens a UI
        /// </summary>
        /// <typeparam name="T">UI Presenter</typeparam>
        /// <param name="loadMethod">Method to execute when the UI loads</param>
        /// <param name="modalDialog">Set to <c>true</c> to display the UI as a modal dialog</param>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        public DialogResult? OpenForm<T>(bool modalDialog, EventHandler loadMethod, bool displayWaitCursor) where T : FormPresenter, new()
        {
            using (var presenter = ViewHelper.CreatePresenter<T>(displayWaitCursor))
            {
                presenter.FormView.ViewLoad += loadMethod;
                return OpenForm(presenter, modalDialog);
            }
        }

        /// <summary>
        /// Opens a UI
        /// </summary>
        /// <param name="presenter">UI presenter</param>
        /// <param name="modalDialog">Set to <c>true</c> to display the UI as a modal dialog</param>
        public DialogResult? OpenForm(FormPresenter presenter, bool modalDialog)
        {
            try
            {
                var form = presenter.FormView as Form;
                if (form == null)
                    return null;

                form.Owner = (Form)FormView;
                form.Activated += delegate { FormView.Visible = false; };

                if (modalDialog)
                {
                    var dialogResult = form.ShowDialog();
                    FormView.Visible = true;
                    return dialogResult;
                }

                form.Closed += delegate { FormView.Visible = true; };
                form.Show();
            }
            catch (Exception)
            {
                FormView.Visible = true;
                throw;
            }
            return null;
        }

        /// <summary>
        /// Closes the view
        /// </summary>
        public void CloseView()
        {
            if (FormView == null) return;
            FormView.DialogResult = DialogResult.OK;
            FormView.Close();
        }
    }
}