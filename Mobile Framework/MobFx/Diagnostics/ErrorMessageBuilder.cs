#region Imported Namespaces

using System;
using System.Globalization;
using System.Text;
using System.Net;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Helper class for building Error Messages
    /// </summary>
    public static class ErrorMessageBuilder
    {
        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildExceptionMessage(Exception exception)
        {
            //if (exception is SqlException)
            //    return BuildSqlExceptionMessage((SqlException) exception);

            if (exception is WebException)
                return BuildWebExceptionMessage((WebException)exception);

            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "DATE:\n{0}", DateTime.Now);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);

            if (exception.InnerException != null)
            {
                body.Append("\n\n");
                body.Append("INNER EXCEPTION\n\n");
                body.Append(BuildExceptionMessage(exception.InnerException));
            }

            return body.ToString();
        }

        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildSimpleExceptionMessage(Exception exception, bool showStackTrace, bool showInnerException)
        {
            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);

            if (showStackTrace)
            {
                body.Append("\n\n");
                body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);
            }

            if (showInnerException)
                if (exception.InnerException != null)
                {
                    body.Append("\n\n");
                    body.Append("INNER EXCEPTION\n\n");
                    body.Append(BuildExceptionMessage(exception.InnerException));
                }

            return body.ToString();
        }

        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <param name="message">Error description</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildExceptionMessage(Exception exception, string message)
        {
            if (exception is WebException)
                return BuildWebExceptionMessage((WebException)exception, message);

            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "ERROR MESSAGE:\n{0}", message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "DATE:\n{0}", DateTime.Now);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);

            if (exception.InnerException != null)
            {
                body.Append("\n\n");
                body.Append("INNER EXCEPTION\n\n");
                body.Append(BuildExceptionMessage(exception.InnerException));
            }

            return body.ToString();
        }

        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <param name="message">Error description</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildSimpleExceptionMessage(Exception exception, string message, bool showStackTrace, bool showInnerException)
        {
            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "ERROR MESSAGE:\n{0}", message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);

            if (showStackTrace)
            {
                body.Append("\n\n");
                body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);
            }

            if (showInnerException)
                if (exception.InnerException != null)
                {
                    body.Append("\n\n");
                    body.Append("INNER EXCEPTION\n\n");
                    body.Append(BuildSimpleExceptionMessage(exception.InnerException, showStackTrace, showInnerException));
                }

            return body.ToString();
        }

        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildWebExceptionMessage(WebException exception)
        {
            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "DATE:\n{0}", DateTime.Now);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nWebException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "WEB EXCEPTION STATUS:\n{0}", GetWebExceptionStatus(exception));
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);

            if (exception.InnerException != null)
            {
                body.Append("\n\n");
                body.Append("INNER EXCEPTION\n\n");
                body.Append(BuildExceptionMessage(exception.InnerException));
            }

            return body.ToString();
        }


        /// <summary>
        /// Builds an error message
        /// </summary>
        /// <param name="exception">Exception containg the details of the error</param>
        /// <param name="message">Error description</param>
        /// <returns>Returns a string describing the error</returns>
        public static string BuildWebExceptionMessage(WebException exception, string message)
        {
            var body = new StringBuilder();
            body.AppendFormat(CultureInfo.CurrentCulture, "ERROR MESSAGE:\n{0}", message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "DATE:\n{0}", DateTime.Now);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION TYPE:\nWebException");
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "EXCEPTION MESSAGE:\n{0}", exception.Message);
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "WEB EXCEPTION STATUS:\n{0}", GetWebExceptionStatus(exception));
            body.Append("\n\n");
            body.AppendFormat(CultureInfo.CurrentCulture, "STACK TRACE:\n{0}", exception.StackTrace);

            if (exception.InnerException != null)
            {
                body.Append("\n\n");
                body.Append("INNER EXCEPTION\n\n");
                body.Append(BuildExceptionMessage(exception.InnerException));
            }

            return body.ToString();
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
                    return "The request was a piplined request and the connection was closed before the response was received.";
                case WebExceptionStatus.ProtocolError:
                    return "The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.";
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    return "The name resolver service could not resolve the proxy host name.";
                case WebExceptionStatus.ReceiveFailure:
                    return "A complete response was not received from the remote server.";
                case WebExceptionStatus.RequestCanceled:
                    return "The request was canceled, the System.Net.WebRequest.Abort() method was called, or an unclassifiable error occurred. This is the default value for System.Net.WebException.Status.";
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