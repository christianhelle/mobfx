using System;

namespace ChristianHelle.Framework.WindowsMobile
{
    /// <summary>
    /// Thrown when an exception occurs within the mobile framework
    /// </summary>
    [Serializable]
    public class MobileApplicationException : Exception
    {
        /// <summary>
        /// Creates an instance of <see cref="MobileApplicationException"/>
        /// </summary>
        public MobileApplicationException()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="MobileApplicationException"/>
        /// </summary>
        /// <param name="message">Message</param>
        public MobileApplicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="MobileApplicationException"/>
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="inner">Inner exception</param>
        public MobileApplicationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
