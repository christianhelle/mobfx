
using System;
using System.Runtime.InteropServices;

namespace ChristianHelle.Framework.WindowsMobile.Core 
{
    /// <summary>
    /// Managed class in charge of handling unamanaged calls for system-time information.
    /// </summary>
    public static class SystemTime 
    {

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Sets the local time as the current system time, i.e. the local time
        /// is converted to UTC time prior to setting it as the system time.
        /// </summary>
        /// <param name="localTime">The local time of the system.</param>
        public static void SetLocalTime(DateTime localTime)
        {
            CommitSystemTime(localTime.ToUniversalTime());
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Sets an UTC time as the current system time.
        /// </summary>
        /// <param name="utcTime"><see cref="DateTime"/> in Coordinated Universal Time (UTC)</param>
        public static void SetUTCTime(DateTime utcTime)
        {
            CommitSystemTime(utcTime);
        }

        ////////////////////////////////////////////////////////////////////////

        private static void CommitSystemTime(DateTime dt)
        {
            var st = DateTimeToSystemTime(dt);

            if (Environment.OSVersion.Platform == PlatformID.WinCE)
                SetSystemTimeCE(ref st);
            else
                SetSystemTimeNT(ref st);
        }

        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This structure represents a date and time using individual members for the month
        /// , day, year, weekday, hour, minute, second, and millisecond.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME {
            /// <summary>
            /// Specifies the current year.
            /// </summary>
            public short wYear;
            /// <summary>
            /// Specifies the current month; January = 1, February = 2, and so on
            /// </summary>
            public short wMonth;
            /// <summary>
            /// Specifies the current day of the week; Sunday = 0, Monday = 1, and so on. 
            /// </summary>
            public short wDayOfWeek;
            /// <summary>
            /// Specifies the current day of the month.
            /// </summary>
            public short wDay;
            /// <summary>
            /// Specifies the current hour.
            /// </summary>
            public short wHour;
            /// <summary>
            /// Specifies the current minute.
            /// </summary>
            public short wMinute;
            /// <summary>
            /// Specifies the current second. 
            /// </summary>
            public short wSecond;
            /// <summary>
            /// Specifies the current millisecond. 
            /// </summary>
            public short wMilliseconds;
        }

        [DllImport("coredll.dll", EntryPoint="SetSystemTime", SetLastError = true)]
        private static extern bool SetSystemTimeCE(ref SYSTEMTIME sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        private static extern bool SetSystemTimeNT(ref SYSTEMTIME sysTime);

        ////////////////////////////////////////////////////////////////////////

        private static SYSTEMTIME DateTimeToSystemTime(DateTime dt)
        {
            var st = new SYSTEMTIME
                {
                    wYear = ((short)dt.Year),
                    wMonth = ((short)dt.Month),
                    wDay = ((short)dt.Day),
                    wHour = ((short)dt.Hour),
                    wMinute = ((short)dt.Minute),
                    wSecond = ((short)dt.Second),
                    wMilliseconds = ((short)dt.Millisecond)
                };
            return st;
        }

        ////////////////////////////////////////////////////////////////////////

    }
}