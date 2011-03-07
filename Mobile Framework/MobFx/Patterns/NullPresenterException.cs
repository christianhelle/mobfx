#region Imported Namespaces

using System;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Throw when the an attempt to retrieve a presenter from the 
    /// <see cref="Presenter.PresenterBag"/> is a NULL value
    /// </summary>
    [Serializable]
    public class NullPresenterException : Exception
    {
        /// <summary>
        /// Creates an instance of <see cref="NullPresenterException"/>
        /// </summary>
        public NullPresenterException()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="NullPresenterException"/>
        /// </summary>
        /// <param name="message">The error message</param>
        public NullPresenterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="NullPresenterException"/>
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified
        /// </param>
        public NullPresenterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}