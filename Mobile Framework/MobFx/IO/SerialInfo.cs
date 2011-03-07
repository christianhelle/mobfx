namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Retrieves serial port information
    /// </summary>
    public class SerialInfo
    {
        /// <summary>
        /// Reads the Registry for available serial ports
        /// </summary>
        /// <returns>A collection containing a list of available serial ports</returns>
        public static SerialPortList GetSerialPorts()
        {
            var mSerialPorts = new SerialPortList();
            if (!mSerialPorts.IsInitialized)
                mSerialPorts.ScanRegistry();
            return mSerialPorts;
        }
    }
}