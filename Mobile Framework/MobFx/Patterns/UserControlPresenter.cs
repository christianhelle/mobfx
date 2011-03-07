#region Imported Namespaces

using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base class for all Presenters/Controllers using a <see cref="UserControl"/> based UI
    /// </summary>
    public abstract class UserControlPresenter : Presenter
    {
        /// <summary>
        /// Sets the current <see cref="IUserControlView"/>
        /// </summary>
        /// <param name="view">Instance of <see cref="IUserControlView"/></param>
        protected UserControlPresenter(IUserControlView view)
        {
            BaseView = view;
            AttachView();
        }

        /// <summary>
        /// Instance of the User Control
        /// </summary>
        public IUserControlView BaseView { get; set; }

        /// <summary>
        /// Gets an instance of the passive view
        /// </summary>
        /// <typeparam name="T">Type of the passive view</typeparam>
        /// <returns>Returns an instance of the passive view as <paramref name="{T}"/></returns>
        public T GetView<T>() where T : class, IUserControlView
        {
            return BaseView as T;
        }
    }
}