using System;
using System.Runtime.InteropServices;

namespace ChristianHelle.Framework.WindowsMobile.Drawing.Native
{
    internal static class NativeMethods
    {
        [DllImport("coredll.dll")]
        internal static extern bool GradientFill(
            IntPtr hdc,
            TRIVERTEX[] pVertex,
            uint dwNumVertex,
            GRADIENT_RECT[] pMesh,
            uint dwNumMesh,
            uint dwMode);

        [DllImport("coredll.dll")]
        internal static extern bool BitBlt(
            IntPtr hdc, 
            int nXDest, 
            int nYDest, 
            int nWidth, 
            int nHeight,
            IntPtr hdcSrc, 
            int nXSrc, 
            int nYSrc, 
            int dwRop);

        [DllImport("gdi32.dll")]
        internal static extern bool GdiGradientFill(
            IntPtr hdc,
            TRIVERTEX[] pVertex,
            uint dwNumVertex,
            GRADIENT_RECT[] pMesh,
            uint dwNumMesh,
            uint dwMode);

        [DllImport("coredll.dll")]
        internal static extern bool AlphaBlend(
            IntPtr hdcDest,
            Int32 xDest,
            Int32 yDest,
            Int32 cxDest,
            Int32 cyDest,
            IntPtr hdcSrc,
            Int32 xSrc,
            Int32 ySrc,
            Int32 cxSrc,
            Int32 cySrc,
            BLENDFUNCTION blendfunction);

        [DllImport("gdi32.dll")]
        internal static extern bool GdiAlphaBlend(
            IntPtr hdcDest,
            Int32 xDest,
            Int32 yDest,
            Int32 cxDest,
            Int32 cyDest,
            IntPtr hdcSrc,
            Int32 xSrc,
            Int32 ySrc,
            Int32 cxSrc,
            Int32 cySrc,
            BLENDFUNCTION blendfunction);
    }    
}