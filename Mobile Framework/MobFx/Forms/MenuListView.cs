using System.Drawing;
using System.Windows.Forms;
using System;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    public class MenuListView : OwnerDrawnList
    {
        private const int TOPLEFT = 10;
        private readonly Pen selectedPen;
        private readonly SolidBrush backBrush;
        private readonly Pen backcolorPen;
        private readonly Pen pen;
        private readonly SolidBrush textBrush;

        public MenuListView()
        {
            selectedPen = new Pen(SystemColors.Highlight);
            pen = new Pen(base.ForeColor);
            textBrush = new SolidBrush(base.ForeColor);
            backcolorPen = new Pen(base.BackColor);
            backBrush = new SolidBrush(base.BackColor);

            ScrollWidth = 0;
            VisibleItems = 3;
        }

        public int VisibleItems { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (selectedPen != null) selectedPen.Dispose();
            if (pen != null) pen.Dispose();
            if (textBrush != null) textBrush.Dispose();
            if (backcolorPen != null) backcolorPen.Dispose();
            if (backBrush != null) backBrush.Dispose();

            base.Dispose(disposing);
        }

        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                pen.Color = value;
                textBrush.Color = value;
                base.ForeColor = value;
            }
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                backcolorPen.Color = value;
                backBrush.Color = value;
                base.BackColor = value;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            ItemHeight = Height / VisibleItems;
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Items.Count == 0)
                base.OnPaint(e);

            using (var gxOff = Graphics.FromImage(OffScreen))
            {
                gxOff.FillRectangle(backBrush, ClientRectangle);

                var top = 0;
                for (var position = VScrollBar.Value; position < VScrollBar.Value + DrawCount; position++)
                {
                    var rect = new Rectangle(0, top, ClientSize.Width - 3, ItemHeight);
                    if (position == SelectedIndex)
                        GraphicsExtensions.DrawThemedGradientRectangle(gxOff, selectedPen, rect, new Size(8, 8));

                    var text = string.Format("({0}) {1}", position + 1, Items[position]);
                    var stringBounds = new RectangleF(TOPLEFT, top + 3, ClientSize.Width, ItemHeight - 3);
                    gxOff.DrawString(text, Font, textBrush, stringBounds);

                    top += ItemHeight;
                }

                e.Graphics.DrawImage(OffScreen, 1, 1);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (backcolorPen != null)
                e.Graphics.DrawRectangle(
                    backcolorPen,
                    0,
                    0,
                    ClientSize.Width - 1,
                    ClientSize.Height - 1);
        }
    }
}