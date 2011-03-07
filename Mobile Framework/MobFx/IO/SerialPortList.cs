using System.Collections.Generic;
using Microsoft.Win32;

namespace ChristianHelle.Framework.WindowsMobile.IO
{
    /// <summary>
    /// Contains a list of available serial ports
    /// </summary>
    public class SerialPortList : List<SerialPortInfo>
    {
        /// <summary>
        /// Checks if the registry has been scanned
        /// </summary>
        internal bool IsInitialized { get; set; }

        /// <summary>
        /// Scans the Registry for available serial ports
        /// </summary>
        internal void ScanRegistry()
        {
            RegistryKey mDeviceEntry;
            RegistryKey mDeviceDetail;
            RegistryKey mActiveDrivers = Registry.LocalMachine.OpenSubKey("Drivers\\Active", false);
            
            if (mActiveDrivers != null)
            {
                string[] mSubKeys = mActiveDrivers.GetSubKeyNames();
                string mDeviceName = string.Empty;
                string mDeviceInfo = string.Empty;
                string mFriendlyName = string.Empty;

                for (int i = 1; i < mSubKeys.Length; i++)
                {
                    // read device info from Drivers\Active branch of local machine's settings
                    mDeviceEntry = Registry.LocalMachine.OpenSubKey("Drivers\\Active\\" + mSubKeys[i], false);
                    if (mDeviceEntry != null)
                    {
                        mDeviceName = mDeviceEntry.GetValue("Name", string.Empty).ToString();
                        mDeviceInfo = mDeviceEntry.GetValue("Key", string.Empty).ToString();
                        mDeviceEntry.Close();
                    }

                    // check if we are looking at a COM device entry
                    if (string.Compare(mDeviceName, 0, "COM", 0, 3, true) == 0)
                    {
                        // now read friendly name for COM Port from registry
                        mDeviceDetail = Registry.LocalMachine.OpenSubKey(mDeviceInfo, false);
                        if (mDeviceDetail != null)
                        {
                            mFriendlyName = mDeviceDetail.GetValue("FriendlyName", string.Empty).ToString();
                            mDeviceDetail.Close();
                        }
                        Add(new SerialPortInfo(mFriendlyName, mDeviceName));
                    }
                }
            }
            IsInitialized = true;
        }
    }
}