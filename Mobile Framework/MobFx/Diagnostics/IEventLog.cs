#region Imported Namespaces

using System;
using System.Text;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Represents an event logging system
    /// </summary>
    public interface IEventLog
    {
        /// <summary>
        /// Fired when a new line is written to the log file
        /// </summary>
        event EventHandler<NewLineEventArgs> NewLine;

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        void WriteLine(string message);

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        void WriteLine(StringBuilder message);
    }
}