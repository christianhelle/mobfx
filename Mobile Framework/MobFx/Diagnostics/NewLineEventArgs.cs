#region Imported Namespaces

using System;
using System.Text;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Diagnostics
{
    /// <summary>
    /// Event Argument class used to provide the last line written to a log
    /// </summary>
    public class NewLineEventArgs : EventArgs
    {
        /// <summary>
        /// Creates an instance of <see cref="NewLineEventArgs"/>
        /// </summary>
        /// <param name="message">The last line written to a log</param>
        public NewLineEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates an instance of <see cref="NewLineEventArgs"/>
        /// </summary>
        /// <param name="message">The last line written to a log</param>
        public NewLineEventArgs(StringBuilder message)
            : this(message.ToString())
        {
        }

        /// <summary>
        /// The last line written to a log
        /// </summary>
        public string Message { get; set; }
    }
}