using System;

using System.Collections.Generic;
using System.Text;

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base class for implementing the Decorator pattern
    /// </summary>
    /// <typeparam name="T">Object to decorate</typeparam>
    public abstract class Decorator<T> : IDisposable
    {
        /// <summary>
        /// The decorated object
        /// </summary>
        protected T Target;

        /// <summary>
        /// Creates an instance of <see cref="Decorator"/>
        /// </summary>
        /// <param name="target">Object to decorate</param>
        protected Decorator(T target)
        {
            this.Target = target;
        }

        #region IDisposable Members

        /// <summary>
        /// Checks if the decorated object implements IDiposable, if so then Dispose() is called
        /// </summary>
        public void Dispose()
        {
            var disposable = Target as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        #endregion
    }
}
