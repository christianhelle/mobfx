using System.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Drawing.Native
{
    internal struct TRIVERTEX
    {
        private int x;
        private int y;
        private ushort Red;
        private ushort Green;
        private ushort Blue;
        private ushort Alpha;

        public TRIVERTEX(int x, int y, Color color)
            : this(x, y, color.R, color.G, color.B, color.A)
        {
        }

        public TRIVERTEX(
            int x, int y,
            ushort red, ushort green, ushort blue,
            ushort alpha)
        {
            this.x = x;
            this.y = y;
            Red = (ushort)(red << 8);
            Green = (ushort)(green << 8);
            Blue = (ushort)(blue << 8);
            Alpha = (ushort)(alpha << 8);
        }
    }
}