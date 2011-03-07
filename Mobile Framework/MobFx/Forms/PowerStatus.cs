using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Power line status.	
    /// </summary>
    /// <remarks>Used by the <see cref="PowerStatus"/> class.</remarks>
    public enum PowerLineStatus : byte
    {
        /// <summary>
        /// AC power is offline.
        /// </summary>
        Offline = 0x00,
        /// <summary>
        /// AC power is online.
        /// </summary>
        Online = 0x01,
        /// <summary>
        /// Unit is on backup power.
        /// </summary>
        BackupPower = 0x02,
        /// <summary>
        /// AC line status is unknown.
        /// </summary>
        Unknown = 0xFF,
    }

    /// <summary>
    /// Indicates current system power status information.
    /// </summary>
    internal class PowerStatus
    {
        private byte mACLineStatus = Byte.MinValue;
        private byte mBatteryFlag = Byte.MinValue;
        private byte mBatteryLifePercent = Byte.MinValue;
        //private byte mReserved1 = Byte.MinValue;
        private int mBatteryLifeTime = 0;
        private int mBatteryFullLifeTime = 0;
        //private byte mReserved2 = Byte.MinValue;
        private byte mBackupBatteryFlag = Byte.MinValue;
        private byte mBackupBatteryLifePercent = Byte.MinValue;
        //private byte mReserved3 = Byte.MinValue;
        private int mBackupBatteryLifeTime = 0;
        private int mBackupBatteryFullLifeTime = 0;

        internal PowerStatus()
        {
        }

        /// <summary>
        /// AC power status.
        /// </summary>
        public PowerLineStatus PowerLineStatus
        {
            get
            {
                Update();
                return (PowerLineStatus)mACLineStatus;
            }
        }

        /// <summary>
        /// Gets the current battery charge status.
        /// </summary>
        public BatteryChargeStatus BatteryChargeStatus
        {
            get
            {
                Update();
                return (BatteryChargeStatus)mBatteryFlag;
            }
        }

        /// <summary>
        /// Gets the approximate percentage of full battery time remaining.
        /// </summary>
        /// <remarks>TThe approximate percentage, from 0 to 100, of full battery time remaining, or 255 if the percentage is unknown.</remarks>
        public byte BatteryLifePercent
        {
            get
            {
                Update();
                return mBatteryLifePercent;
            }
        }

        /// <summary>
        /// Gets the approximate number of seconds of battery time remaining.
        /// </summary>
        /// <value>The approximate number of seconds of battery life remaining, or -1 if the approximate remaining battery life is unknown.</value>
        public int BatteryLifeRemaining
        {
            get
            {
                Update();
                return mBatteryLifeTime;
            }
        }

        /// <summary>
        /// Gets the reported full charge lifetime of the primary battery power source in seconds.
        /// </summary>
        /// <value>The reported number of seconds of battery life available when the battery is fullly charged, or -1 if the battery life is unknown.</value>
        public int BatteryFullLifeTime
        {
            get
            {
                Update();
                return mBatteryFullLifeTime;
            }
        }

        /// <summary>
        /// Gets the backup battery charge status.
        /// </summary>
        public BatteryChargeStatus BackupBatteryChargeStatus
        {
            get
            {
                Update();
                return (BatteryChargeStatus)mBackupBatteryFlag;
            }
        }

        /// <summary>
        /// Percentage of full backup battery charge remaining. Must be in the range 0 to 100.
        /// </summary>
        public byte BackupBatteryLifePercent
        {
            get
            {
                Update();
                return mBackupBatteryLifePercent;
            }
        }

        /// <summary>
        /// Number of seconds of backup battery life remaining.
        /// </summary>
        public int BackupBatteryLifeRemaining
        {
            get
            {
                Update();
                return mBackupBatteryLifeTime;
            }
        }

        /// <summary>
        /// Number of seconds of backup battery life when at full charge. Or -1 If unknown.
        /// </summary>
        public int BackupBatteryFullLifeTime
        {
            get
            {
                Update();
                return mBackupBatteryFullLifeTime;
            }
        }

        private void Update()
        {
            if (!GetSystemPowerStatusEx(this, true))
                Debug.WriteLine("P/Invoke call to GetSystemPowerStatusEx failed");
        }

        [DllImport("coredll.dll", EntryPoint = "GetSystemPowerStatusEx", SetLastError = true)]
        private static extern bool GetSystemPowerStatusEx(PowerStatus pStatus, bool fUpdate);
    }
}