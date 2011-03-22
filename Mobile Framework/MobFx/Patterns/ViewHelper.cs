#region Imported Namespaces

using System;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Helper class for the View in a Model-View-Presenter based UI
    /// </summary>
    public static class ViewHelper
    {
        /// <summary>
        /// Forwards an event to the presenter
        /// </summary>
        /// <param name="eventHandler"><see cref="EventHandler"/> of the IView interface to invoke</param>
        /// <param name="sender">The sender of the event</param>
        public static void ForwardEventToPresenter(EventHandler eventHandler, object sender)
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Forwards an event to the presenter
        /// </summary>
        /// <param name="eventHandler"><see cref="EventHandler"/> of the IView interface to invoke</param>
        /// <param name="sender">The sender of the event</param>
        /// <param name="eventArgs"></param>
        public static void ForwardEventToPresenter<T>(EventHandler<T> eventHandler,
                                                      object sender,
                                                      T eventArgs) where T : EventArgs
        {
            if (eventHandler != null)
                eventHandler.Invoke(sender, eventArgs);
        }

        /// <summary>
        /// Opens a Form
        /// </summary>
        /// <param name="owner">The owner of the dialog</param>
        /// <param name="presenter">The presenter of the form to display</param>
        /// <param name="modalDialog">
        /// Set to <c>true</c> to display the about box as a modal dialog, otherwise <c>false</c>,
        /// </param>
        /// <returns>Returns the <see cref="DialogResult"/> returned by the displayed form</returns>
        public static DialogResult OpenDialog(IFormView owner, FormPresenter presenter, bool modalDialog)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter");

            if (!(presenter.FormView is Form))
                throw new ArgumentException("the FormView of the presenter parameter is not of type Form");

            var form = GetForm(presenter);
            SetFormOwner(owner, form);

            if (modalDialog)
                return form.ShowDialog();

            form.Show();
            return DialogResult.None;
        }

        /// <summary>
        /// Opens a form as a modal dialog
        /// </summary>
        /// <typeparam name="T">Presenter of the UI</typeparam>
        /// <param name="owner">The owner of the form as <see cref="IFormView"/></param>
        /// <returns>Returns the <see cref="DialogResult"/> returned by the displayed form</returns>
        public static DialogResult OpenModalDialog<T>(IFormView owner) where T : FormPresenter, new()
        {
            return OpenModalDialog<T>(owner, false);
        }

        /// <summary>
        /// Opens a form as a modal dialog
        /// </summary>
        /// <typeparam name="T">Presenter of the UI</typeparam>
        /// <param name="owner">The owner of the form as <see cref="IFormView"/></param>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        /// <returns>Returns the <see cref="DialogResult"/> returned by the displayed form</returns>
        public static DialogResult OpenModalDialog<T>(IFormView owner, bool displayWaitCursor) where T : FormPresenter, new()
        {
            using (var presenter = CreatePresenter<T>(displayWaitCursor))
            {
                if (!(presenter.FormView is Form))
                    throw new ArgumentException("the FormView of the presenter parameter is not of type Form");

                var form = GetForm(presenter);
                SetFormOwner(owner, form);

                return form.ShowDialog();
            }
        }

        /// <summary>
        /// Creates an instance of the specified <see cref="FormPresenter"/>
        /// </summary>
        /// <typeparam name="T">Presenter of the UI</typeparam>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        /// <returns>Returns a new instance of <see cref="T"/></returns>
        public static T CreatePresenter<T>(bool displayWaitCursor) where T : FormPresenter, new()
        {
            try
            {
                if (displayWaitCursor)
                    WaitCursor.Show();
                return Activator.CreateInstance<T>();
            }
            finally
            {
                WaitCursor.Hide();
            }
        }

        private static Form GetForm(FormPresenter presenter)
        {
            var form = presenter.FormView as Form;
            if (form == null)
                throw new ArgumentException("form");
            return form;
        }

        private static void SetFormOwner(IView owner, Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            if (owner == null)
                return;

            if (!(owner is Form))
                throw new ArgumentException("owner must be of type Form");

            form.Owner = owner as Form;
            form.Activated += (sender, e) => owner.Visible = false;
            form.Closed += (sender, e) =>
            {
                owner.Visible = true;
                owner.BringToFront();
                owner.Refresh();
            };
        }

        /// <summary>
        /// Creates an instance of a View. This method automatically sets the Parent and Owner properties of the Form
        /// </summary>
        /// <typeparam name="T">The view to instantiate</typeparam>
        /// <param name="view">The owner and parent of this view</param>
        /// <returns>Returns an instance of <see cref="T"/></returns>
        public static T CreateView<T>(IFormView view) where T : Form, new()
        {
            return CreateView<T>(view as Form, false);
        }

        /// <summary>
        /// Creates an instance of a View. This method automatically sets the Parent and Owner properties of the Form
        /// </summary>
        /// <typeparam name="T">The view to instantiate</typeparam>
        /// <param name="view">The owner and parent of this view</param>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        /// <returns>Returns an instance of <see cref="T"/></returns>
        public static T CreateView<T>(IFormView view, bool displayWaitCursor) where T : Form, new()
        {
            return CreateView<T>(view as Form, displayWaitCursor);
        }

        /// <summary>
        /// Creates an instance of a View. This method automatically sets the Parent and Owner properties of the Form
        /// </summary>
        /// <typeparam name="T">The view to instantiate</typeparam>
        /// <param name="form">The owner and parent of this view</param>
        /// <returns>Returns an instance of <see cref="T"/></returns>
        public static T CreateView<T>(Form form) where T : Form, new()
        {
            return CreateView<T>(form, false);
        }

        /// <summary>
        /// Creates an instance of a View. This method automatically sets the Parent and Owner properties of the Form
        /// </summary>
        /// <typeparam name="T">The view to instantiate</typeparam>
        /// <param name="form">The owner and parent of this view</param>
        /// <param name="displayWaitCursor">Set to <c>true</c> to display a busy cursor during instantiation</param>
        /// <returns>Returns an instance of <see cref="T"/></returns>
        public static T CreateView<T>(Form form, bool displayWaitCursor) where T : Form, new()
        {
            try
            {
                if (displayWaitCursor)
                    WaitCursor.Show();
                return new T { Owner = form };
            }
            finally
            {
                WaitCursor.Hide();
            }
        }
    }
}