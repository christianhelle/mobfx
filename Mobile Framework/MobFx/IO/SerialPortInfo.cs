namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Contains information about the Serial Port
    /// </summary>
    public class SerialPortInfo
    {
        private string mName = string.Empty;

        /// <summary>
        /// Get the Friendly name
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        private string mPort = string.Empty;

        /// <summary>
        /// Get the Port
        /// </summary>
        public string Port
        {
            get { return mPort; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aName">Friendly name</param>
        /// <param name="aPort">Port</param>
        public SerialPortInfo(string aName, string aPort)
        {
            mName = aName;
            mPort = aPort;
        }

        /// <summary>
        /// Get the Friendly name + the port
        /// </summary>
        /// <returns>Friendly name + (Port)</returns>
        public override string ToString()
        {
            return mName + " (" + mPort + ")";
        }
    }
}