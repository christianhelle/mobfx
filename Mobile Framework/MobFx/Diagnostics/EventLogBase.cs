#region Imported Namespaces

using System;
using System.Text;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Base class for event loggers
    /// </summary>
    public abstract class EventLogBase : IEventLog
    {
        #region IEventLog Members

        /// <summary>
        /// Fired when a new line is written to the log file
        /// </summary>
        public event EventHandler<NewLineEventArgs> NewLine;

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        public virtual void WriteLine(string message)
        {
            InvokeNewLine(this, new NewLineEventArgs(message));
        }

        /// <summary>
        /// Writes a line to the log file
        /// </summary>
        /// <param name="message">Text to log</param>
        public virtual void WriteLine(StringBuilder message)
        {
            InvokeNewLine(this, new NewLineEventArgs(message));
        }

        #endregion

        /// <summary>
        /// Invokes the <see cref="NewLine"/> event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">
        /// Instance of <see cref="NewLineEventArgs"/> containing the data written to the log file
        /// </param>
        protected void InvokeNewLine(object sender, NewLineEventArgs e)
        {
            var newLineHandler = NewLine;
            if (newLineHandler != null)
                newLineHandler(sender ?? this, e);
        }
    }
}