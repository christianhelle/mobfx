#region Imported Namespaces

using System;
using ChristianHelle.Framework.WindowsMobile.Patterns;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Utility for writing to the error log file.
    /// </summary>
    /// <remarks>
    /// Default log file is [Working Directory]\Log\Error-[yyyy-MM-dd_HH.mm.ss].txt
    /// </remarks>
    /// <example>
    /// This is an example of how you might use this class for logging an exception:
    /// 
    /// <code>
    /// try
    /// {
    ///     // Do something that can possibly throw an exception
    /// }
    /// catch (Exception e)
    /// {
    ///     ErrorLogFile.Instance.AppendException(e);
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// This is an example of how you might use this class for logging an exception
    /// and describing the source of the exception:
    /// 
    /// <code>
    /// void MethodThatFails()
    /// try
    /// {
    ///     // Do something that can possibly throw an exception
    /// }
    /// catch (Exception e)
    /// {
    ///     ErrorLogFile.Instance.AppendException("MyClass::MethodThatFails() failed", e);
    /// }
    /// </code>
    /// </example>
    public sealed class ErrorLogFile : LogFile
    {
        /// <summary>
        /// Creates an instance of <see cref="ErrorLogFile"/>
        /// </summary>
        public ErrorLogFile()
        {
            Filename = GetDefaultFile();
        }

        /// <summary>
        /// Singleton implementation of <see cref="ErrorLogFile"/>
        /// </summary>
        public static ErrorLogFile Instance
        {
            get { return Singleton<ErrorLogFile>.GetInstance(); }
        }

        /// <summary>
        /// Retrieves the default log file to use
        /// </summary>
        /// <returns></returns>
        protected override string GetDefaultFile()
        {
            return LogFileRepository.GetLogFile("Error");
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="source">Source of the Exception</param>
        /// <param name="message">Exception Message</param>
        /// <param name="stacktrace">Exception or the Environments Stack Trace</param>
        public void AppendError(string source, string message, string stacktrace)
        {
            try
            {
                WriteLine(
                    string.Format(
                        @"-=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=-{3}{0}:{3}{1}{3}{3}STACK TRACE:{3}{2}{3}{3}{3}",
                        DateTime.Now,
                        message,
                        stacktrace,
                        "\n"));
            }
            catch (Exception e)
            {
                TraceException(e);
            }
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="source">Source of the Exception</param>
        /// <param name="message">Exception Message</param>
        /// <param name="innerexception">InnerExceptions Message</param>
        /// <param name="stacktrace">Exception or the Environments Stack Trace</param>
        public void AppendError(string source, string message, string innerexception,
                                string stacktrace)
        {
            try
            {
                if (string.IsNullOrEmpty(innerexception))
                {
                    AppendError(source, message, stacktrace);
                }
                else
                {
                    WriteLine(
                        string.Format(
                            "-=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=-{4}{0}:\t{1}{4}INNER EXCEPTION:\t{2}{4}STACK TRACE:{4}{3}{4}{4}{4}",
                            DateTime.Now,
                            message,
                            innerexception,
                            stacktrace,
                            "\n"));
                }
            }
            catch (Exception e)
            {
                TraceException(e);
            }
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="exception">Instance of the exception object</param>
        public void AppendException(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            try
            {
                WriteLine(
                    string.Format(
                        "-=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=--=-=-=-=-=--=-=-{2}{0}{1}{0}{2}{2}{2}",
                        DateTime.Now,
                        exception,
                        "\n"));
            }
            catch (Exception e)
            {
                TraceException(e);
            }
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="source">Source of the exception</param>
        /// <param name="exception">Instance of the exception object</param>
        public void AppendException(string source, Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            if (!string.IsNullOrEmpty(source))
                source = exception.Message;

            if (exception.InnerException != null)
                AppendError(source, exception.Message, exception.InnerException.Message, exception.StackTrace);
            else
                AppendError(source, exception.Message, exception.StackTrace);
        }
    }
}