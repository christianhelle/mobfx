#region Imported Namespaces

using System;
using System.Runtime.InteropServices;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Drawing.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
// ReSharper disable InconsistentNaming
    internal struct LOGFONT
// ReSharper restore InconsistentNaming
    {
        public int Height;
        public int Width;
        public int Escapement;
        public int Orientation;
        public LogFontWeight Weight;
        [MarshalAs(UnmanagedType.U1)] public bool Italic;
        [MarshalAs(UnmanagedType.U1)] public bool Underline;
        [MarshalAs(UnmanagedType.U1)] public bool StrikeOut;
        public LogFontCharSet CharSet;
        public LogFontPrecision OutPrecision;
        public LogFontClipPrecision ClipPrecision;
        public LogFontQuality Quality;
        public LogFontPitchAndFamily PitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string FaceName;
    }

    internal enum LogFontWeight
    {
        Black = 900,
        Bold = 700,
        DemiBold = 600,
        DontCare = 0,
        ExtraBold = 800,
        ExtraLight = 200,
        Heavy = 900,
        Light = 300,
        Medium = 500,
        Normal = 400,
        Regular = 400,
        SemiBold = 600,
        Thin = 100,
        UltraBold = 800,
        UltraLight = 200
    }

    internal enum LogFontQuality : byte
    {
        AntiAliased = 4,
        ClearType = 5,
        ClearTypeCompat = 6,
        Default = 0,
        Draft = 1,
        NonAntiAliased = 3
    }

    [Flags]
    internal enum LogFontPitchAndFamily : byte
    {
        Decorative = 80,
        Default = 0,
        DontCare = 0,
        Fixed = 1,
        Modern = 0x30,
        Roman = 0x10,
        Script = 0x40,
        Swiss = 0x20,
        Variable = 2
    }

    internal enum LogFontPrecision : byte
    {
        Default = 0,
        Raster = 6,
        String = 1
    }

    internal enum LogFontClipPrecision : byte
    {
        Character = 1,
        Default = 0,
        Stroke = 2
    }

    internal enum LogFontCharSet : byte
    {
        ANSI = 0,
        Arabic = 0xb2,
        Baltic = 0xba,
        ChineseBig5 = 0x88,
        Default = 1,
        EastEurope = 0xee,
        GB2312 = 0x86,
        Greek = 0xa1,
        Hangeul = 0x81,
        Hebrew = 0xb1,
        Johab = 130,
        Mac = 0x4d,
        OEM = 0xff,
        Russian = 0xcc,
        ShiftJIS = 0x80,
        Symbol = 2,
        Thai = 0xde,
        Turkish = 0xa2
    }
}