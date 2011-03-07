using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    internal static class Theme
    {
        internal static Color AlphaBlend(Color value1, Color value2, int alpha)
        {
            int ialpha = 256 - alpha; //inverse alpha
            return Color.FromArgb((value1.R * alpha) + (value2.R * ialpha) >> 8,
                                  (value1.G * alpha) + (value2.G * ialpha) >> 8,
                                  (value1.B * alpha) + (value2.B * ialpha) >> 8);
        }

        internal static Color ThemeBase { get { return Color.LightBlue; } }

        internal static Color GradientLight
        {
            get
            {
                var color = AlphaBlend(ThemeBase, Color.White, 100);
                return AlphaBlend(Color.White, color, 50); ;
            }
        }

        internal static Color GradientDark
        {
            get
            {
                var color = AlphaBlend(ThemeBase, Color.Black, 256);
                return AlphaBlend(Color.White, color, 50); ;
            }
        }
    }

    internal struct TRIVERTEX
    {
        internal int x;
        internal int y;
        internal ushort Red;
        internal ushort Green;
        internal ushort Blue;
        internal ushort Alpha;

        internal TRIVERTEX(int x, int y, Color color)
            : this(x, y, color.R, color.G, color.B, color.A)
        {
        }

        internal TRIVERTEX(
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

    internal struct GRADIENT_RECT
    {
        internal uint UpperLeft;
        internal uint LowerRight;

        internal GRADIENT_RECT(uint ul, uint lr)
        {
            UpperLeft = ul;
            LowerRight = lr;
        }
    }

    internal static class GraphicsExtensions
    {
        [DllImport("coredll.dll")]
        static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        [DllImport("coredll.dll")]
        static extern int SetBrushOrgEx(IntPtr hdc, int nXOrg, int nYOrg, ref Point lppt);

        [DllImport("coredll.dll")]
        static extern IntPtr CreateSolidBrush(uint color);

        [DllImport("coredll.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobject);

        [DllImport("coredll.dll")]
        static extern bool DeleteObject(IntPtr hgdiobject);

        [DllImport("coredll.dll")]
        static extern IntPtr CreatePatternBrush(IntPtr HBITMAP);

        [DllImport("coredll.dll")]
        static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, int dwNumVertex, GRADIENT_RECT[] pMesh, int dwNumMesh, int dwMode);

        [DllImport("coredll.dll")]
        static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidth, int nHeight);

        static uint GetColorRef(Color value)
        {
            return 0x00000000 | ((uint)value.B << 16) | ((uint)value.G << 8) | (uint)value.R;
        }

        const int PS_SOLID = 0;
        const int PS_DASH = 1;

        static IntPtr CreateGdiPen(Pen pen)
        {
            var style = pen.DashStyle == DashStyle.Solid ? PS_SOLID : PS_DASH;
            return CreatePen(style, (int)pen.Width, GetColorRef(pen.Color));
        }

        const int GRADIENT_FILL_RECT_V = 0x00000001;

        internal static void GradientFill(
            Graphics graphics,
            Rectangle rect,
            Color startColor,
            Color endColor)
        {
            var tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rect.X, rect.Y, startColor);
            tva[1] = new TRIVERTEX(rect.Right, rect.Bottom, endColor);
            var gra = new GRADIENT_RECT[] { new GRADIENT_RECT(0, 1) };

            var hdc = graphics.GetHdc();
            GradientFill(hdc, tva, tva.Length, gra, gra.Length, GRADIENT_FILL_RECT_V);
            graphics.ReleaseHdc(hdc);
        }

        internal static void DrawThemedGradientRectangle(
            Graphics graphics,
            Pen border,
            Rectangle area,
            Size ellipseSize)
        {
            using (var texture = new Bitmap(area.Right, area.Bottom))
            {
                using (var g = Graphics.FromImage(texture))
                    GradientFill(g,
                                 new Rectangle(0, 0, texture.Width, texture.Height),
                                 Theme.GradientLight,
                                 Theme.GradientDark);

                FillRoundedTexturedRectangle(graphics, border, texture, area, ellipseSize);
            }
        }

        internal static void FillRoundedTexturedRectangle(
            Graphics graphics,
            Pen border,
            Bitmap texture,
            Rectangle rect,
            Size ellipseSize)
        {
            Point old = new Point();

            var hdc = graphics.GetHdc();
            var hpen = CreateGdiPen(border);
            var hbitmap = texture.GetHbitmap();
            var hbrush = CreatePatternBrush(hbitmap);

            SetBrushOrgEx(hdc, rect.Left, rect.Top, ref old);
            SelectObject(hdc, hpen);
            SelectObject(hdc, hbrush);

            RoundRect(hdc, rect.Left, rect.Top, rect.Right, rect.Bottom, ellipseSize.Width, ellipseSize.Height);

            SetBrushOrgEx(hdc, old.Y, old.X, ref old);
            DeleteObject(hpen);
            DeleteObject(hbrush);

            graphics.ReleaseHdc(hdc);
        }
    }
}