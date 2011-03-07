#region Imported Namespaces

using System;
using System.Net;
using ChristianHelle.Framework.WindowsMobile.Configuration;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Handles exceptions by logging to a file and writting to the database event log
    /// </summary>
    public class DeviceExceptionHandler
    {
        /// <summary>
        /// Exception object
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="DeviceExceptionHandler"/>
        /// </summary>
        /// <param name="exception">Exception object</param>
        public DeviceExceptionHandler(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Handles the exception by logging the details to a file 
        /// and writting to the database event log
        /// </summary>
        /// <param name="message">Message describing the source of the exception</param>
        /// <param name="rethrow">Set to <c>true</c> to re-throw the exception</param>
        public void HandleException(string message, bool rethrow)
        {
            if (message == null)
                message = string.Empty;

            var errorMessage = string.Empty;

            if (Exception != null)
                errorMessage = ErrorMessageBuilder.BuildExceptionMessage(Exception, message);

            ErrorLogFile.Instance.WriteLine(errorMessage);

            if (!rethrow)
                return;

            if (Exception != null)
                throw Exception;
        }
    }
}