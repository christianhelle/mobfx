#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base class for all Presenters/Controllers
    /// </summary>
    public abstract class Presenter : IDisposable
    {
        /// <summary>
        /// Instantiates the <see cref="PresenterBag"/>
        /// </summary>
        protected Presenter()
        {
            PresenterBag = new Dictionary<Type, Presenter>();
        }

        /// <summary>
        /// A state bag for presenters used by this presenter
        /// </summary>
        public Dictionary<Type, Presenter> PresenterBag { get; private set; }

        #region IDisposable Members

        /// <summary>
        /// Calls <see cref="DeattachView"/>        
        /// </summary>
        public virtual void Dispose()
        {
            DeattachView();

            foreach (var presenter in PresenterBag.Values)
                if (presenter != null)
                    presenter.Dispose();

            PresenterBag.Clear();
        }

        #endregion

        /// <summary>
        /// Subscribes this presenter to the events of its corresponding View
        /// </summary>
        protected abstract void AttachView();

        /// <summary>
        /// Unsubscribes this presenter to the events of its corresponding View
        /// </summary>
        protected virtual void DeattachView()
        {
        }

        /// <summary>
        /// Gets the presenter from the <see cref="PresenterBag"/>
        /// </summary>
        /// <typeparam name="T">Type of the presenter</typeparam>
        /// <returns>Returns an instance of <see cref="Presenter"/> from the <see cref="PresenterBag"/></returns>
        public T GetPresenter<T>() where T : Presenter
        {
            if (!PresenterBag.ContainsKey(typeof(T)))
                throw new ArgumentException("The presenter bag does not contain the specified type");

            var returnType = PresenterBag[typeof(T)] as T;
            if (returnType == null)
                throw new NullPresenterException("The presenter retrieved is a NULL refernce");

            return returnType;
        }

        /// <summary>
        /// Gets the View in form of a <see cref="UserControl"/> out of the <see cref="PresenterBag"/>
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Presenter"/></typeparam>
        /// <returns>Returns a UserControl for the View</returns>
        public UserControl GetUserControlView<T>() where T : UserControlPresenter
        {
            return (UserControl)GetPresenter<T>().BaseView;
        }
    }
}