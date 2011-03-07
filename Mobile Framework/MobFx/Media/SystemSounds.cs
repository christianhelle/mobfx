namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Retrieves sounds associated with a set of Windows operating system sound-event types. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class SystemSounds
    {
        /// <summary>
        /// Gets the sound associated with the Asterisk program 
        /// event in the current Windows sound scheme.
        /// </summary>
        public static SystemSound Asterisk
        {
            get { return new SystemSound(0x40); }
        }

        /// <summary>
        /// Gets the sound associated with the Beep program 
        /// event in the current Windows sound scheme.
        /// </summary>
        public static SystemSound Beep
        {
            get { return new SystemSound(0); }
        }

        /// <summary>
        /// Gets the sound associated with the Exclamation program 
        /// event in the current Windows sound scheme.
        /// </summary>
        public static SystemSound Exclamation
        {
            get { return new SystemSound(0x30); }
        }

        /// <summary>
        /// Gets the sound associated with the Hand program 
        /// event in the current Windows sound scheme.
        /// </summary>
        public static SystemSound Hand
        {
            get { return new SystemSound(0x10); }
        }

        /// <summary>
        /// Gets the sound associated with the Question program 
        /// event in the current Windows sound scheme.
        /// </summary>
        public static SystemSound Question
        {
            get { return new SystemSound(0x20); }
        }
    }
}