namespace ChristianHelle.Framework.WindowsMobile.Drawing.Native
{
    internal struct BLENDFUNCTION
    {
        internal byte BlendOp;
        internal byte BlendFlags;
        internal byte SourceConstantAlpha;
        internal byte AlphaFormat;
    }

    internal enum BlendOperation : byte
    {
        AC_SRC_OVER = 0x00
    }

    internal enum BlendFlags : byte
    {
        Zero = 0x00
    }

    internal enum SourceConstantAlpha : byte
    {
        Transparent = 0x00,
        Opaque = 0xFF
    }

    internal enum AlphaFormat : byte
    {
        AC_SRC_ALPHA = 0x01
    }
}