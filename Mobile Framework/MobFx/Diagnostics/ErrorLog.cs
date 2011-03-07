#region Imported Namespaces

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Utility for writing to a log file.
    /// </summary>
    /// <remarks>
    /// Uses [Working Directory]\Error.txt as the default log file
    /// </remarks>
    /// <example>
    /// This is an example of how you might use this class:
    /// 
    /// <code>
    /// try
    /// {
    ///     // Do something that can possibly throw an exception
    /// }
    /// catch (Exception e)
    /// {
    ///     ErrorLog.WriteLine("Some Method and Description", e.Message, e.StackTrace);
    /// }
    /// </code>
    /// </example>
    [Obsolete("Use ErrorLogFile instead", false)]
    public sealed class ErrorLog
    {
        private static readonly object syncRoot = new object();
        private static string logFile;

        /// <summary>
        /// Represents of the Log File
        /// </summary>
        public static string LogFile
        {
            get
            {
                if (string.IsNullOrEmpty(logFile))
                {
                    var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    logFile = string.Format("{0}\\Errors.txt", path);
                }
                return logFile;
            }
            set { logFile = value; }
        }

        private static StreamWriter PrepareLogFile()
        {
            if (File.Exists(LogFile))
            {
                var fi = new FileInfo(LogFile);
                if (fi.Length > 100 * 1024)
                {
                    fi.Delete();
                }
            }

            var sw = new StreamWriter(LogFile, true, Encoding.UTF8);
            return sw;
        }

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="value">Text to log</param>
        public static void WriteLine(string value)
        {
            lock (syncRoot)
            {
                using (var sw = PrepareLogFile())
                {
                    sw.WriteLine(value);
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="source">Source of the Exception</param>
        /// <param name="message">Exception Message</param>
        /// <param name="stacktrace">Exception or the Environments Stack Trace</param>
        public static void AppendError(string source, string message, string stacktrace)
        {
            try
            {
                WriteLine(
                    string.Format(
                        "-=-=-=-=-=--=-=-\n{0}:\n{1}\n\nSTACK TRACE:\n{2}\n",
                        DateTime.Now,
                        message,
                        stacktrace));
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Error saving error information to Errors.txt", e.Message);
            }
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="source">Source of the Exception</param>
        /// <param name="message">Exception Message</param>
        /// <param name="innerexception">InnerExceptions Message</param>
        /// <param name="stacktrace">Exception or the Environments Stack Trace</param>
        public static void AppendError(string source, string message, string innerexception, string stacktrace)
        {
            try
            {
                if (string.IsNullOrEmpty(innerexception))
                {
                    WriteLine(
                        string.Format(
                            "-=-=-=-=-=--=-=-\n{0}:\n{1}\n\nSTACK TRACE:\n{2}\n",
                            DateTime.Now,
                            message,
                            stacktrace));
                }
                else
                {
                    WriteLine(
                        string.Format(
                            "-=-=-=-=-=--=-=-\n{0}:\n{1}\n\nINNER EXCEPTION:\n{2}\n\nSTACK TRACE:\n{3}\n",
                            DateTime.Now,
                            message,
                            innerexception,
                            stacktrace));
                }
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Error saving error information to Errors.txt", e.Message);
            }
        }

        /// <summary>
        /// Writes a web exception to the log file
        /// </summary>
        /// <param name="source">Source of the Exception</param>
        /// <param name="exception">Instance of the <see cref="WebException"/></param>
        public static void AppendWebException(string source, WebException exception)
        {
            try
            {
                if (exception.InnerException == null || string.IsNullOrEmpty(exception.InnerException.Message))
                {
                    WriteLine(
                        string.Format(
                            "-=-=-=-=-=--=-=-\n{0}:\n{1}\n\nWEB EXCEPTION MESSAGE:\n{2}\n\nWEB EXCEPTION STATUS:\n{3}\n\nSTACK TRACE:\n{4}\n",
                            DateTime.Now,
                            source,
                            exception.Message,
                            GetWebExceptionStatus(exception),
                            exception.StackTrace));
                }
                else
                {
                    WriteLine(
                        string.Format(
                            "-=-=-=-=-=--=-=-\n{0}:\n{1}\n\nWEB EXCEPTION MESSAGE:\n{2}\n\nWEB EXCEPTION STATUS:\n{3}\n\nINNER EXCEPTION\n{4}:\n\nSTACK TRACE:\n{5}\n",
                            DateTime.Now,
                            source,
                            exception.Message,
                            GetWebExceptionStatus(exception),
                            exception.InnerException.Message,
                            exception.StackTrace));
                }
            }
            catch (Exception e)
            {
                Debug.Assert(false, "Error saving error information to Errors.txt", e.Message);
            }
        }

        private static string GetWebExceptionStatus(WebException e)
        {
            switch (e.Status)
            {
                case WebExceptionStatus.ConnectFailure:
                    return "The remote service point could not be contacted at the transport level.";
                case WebExceptionStatus.ConnectionClosed:
                    return "The connection was prematurely closed.";
                case WebExceptionStatus.KeepAliveFailure:
                    return "The connection for a request that specifies the Keep-alive header was closed unexpectedly.";
                case WebExceptionStatus.NameResolutionFailure:
                    return "The remote service point could not be contacted at the transport level.";
                case WebExceptionStatus.Pending:
                    return "An internal asynchronous request is pending.";
                case WebExceptionStatus.PipelineFailure:
                    return
                        "The request was a piplined request and the connection was closed before the response was received.";
                case WebExceptionStatus.ProtocolError:
                    return
                        "The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.";
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    return "The name resolver service could not resolve the proxy host name.";
                case WebExceptionStatus.ReceiveFailure:
                    return "A complete response was not received from the remote server.";
                case WebExceptionStatus.RequestCanceled:
                    return
                        "The request was canceled, the System.Net.WebRequest.Abort() method was called, or an unclassifiable error occurred. This is the default value for System.Net.WebException.Status.";
                case WebExceptionStatus.SecureChannelFailure:
                    return "An error occurred while establishing a connection using SSL.";
                case WebExceptionStatus.SendFailure:
                    return "A complete request could not be sent to the remote server.";
                case WebExceptionStatus.ServerProtocolViolation:
                    return "The server response was not a valid HTTP response.";
                case WebExceptionStatus.Timeout:
                    return "No response was received during the time-out period for a request.";
                case WebExceptionStatus.TrustFailure:
                    return "A server certificate could not be validated.";
                case WebExceptionStatus.Success:
                    return "No error was encountered.";
                default:
                    return "No error was encountered.";
            }
        }
    }
}