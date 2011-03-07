using System;

namespace ChristianHelle.Framework.WindowsMobile
{
    /// <summary>
    /// Event Arguments for Unhandled Framework exceptions
    /// </summary>
    public class UnhandledFrameworkExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// The unhandled exception
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="UnhandledFrameworkExceptionEventArgs"/>
        /// </summary>
        /// <param name="exception">Instance of the unhandled <see cref="Exception"/></param>
        internal UnhandledFrameworkExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}