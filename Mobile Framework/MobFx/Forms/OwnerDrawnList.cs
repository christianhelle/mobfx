using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    public class OwnerDrawnList : Control
    {
        private readonly ArrayList items;
        private int itemHeight = -1;
        protected Bitmap OffScreen;
        protected int ScrollWidth;
        private int selectedIndex = -1;
        protected VScrollBar VScrollBar;

        public OwnerDrawnList()
        {
            VScrollBar = new VScrollBar();
            ScrollWidth = VScrollBar.Width;

            VScrollBar.Parent = this;
            VScrollBar.Visible = false;
            VScrollBar.SmallChange = 1;
            VScrollBar.ValueChanged += ScrollValueChanged;

            items = new ArrayList();
        }

        public ArrayList Items
        {
            get { return items; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, EventArgs.Empty);
                Refresh();
            }
        }

        public object SelectedItem
        {
            get
            {
                if (selectedIndex >= 0 && selectedIndex < items.Count)
                    return items[selectedIndex];
                return null;
            }
        }

        protected virtual int ItemHeight
        {
            get { return itemHeight; }
            set { itemHeight = value; }
        }

        protected int DrawCount
        {
            get
            {
                if (VScrollBar.Value + VScrollBar.LargeChange > VScrollBar.Maximum)
                {
                    return VScrollBar.Maximum - VScrollBar.Value + 1;
                }
                else
                {
                    return VScrollBar.LargeChange;
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(this, e);
            }
        }

        protected void ScrollValueChanged(object o, EventArgs e)
        {
            Refresh();
        }

        public void EnsureVisible(int index)
        {
            if (index < VScrollBar.Value)
            {
                VScrollBar.Value = index;
                Refresh();
            }
            else if (index >= VScrollBar.Value + DrawCount)
            {
                VScrollBar.Value = index - DrawCount + 1;
                Refresh();
            }
        }

        public void MoveUp()
        {
            if (SelectedIndex > VScrollBar.Minimum)
            {
                EnsureVisible(--SelectedIndex);
                Refresh();
            }
        }

        public void MoveDown()
        {
            if (SelectedIndex < VScrollBar.Maximum)
            {
                EnsureVisible(++SelectedIndex);
                Refresh();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    MoveDown();
                    break;
                case Keys.Up:
                    MoveUp();
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            SelectedIndex = VScrollBar.Value + (e.Y/itemHeight);
            if (SelectedIndex > items.Count - 1)
            {
                SelectedIndex = -1;
            }

            if (!Focused)
            {
                Focus();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            Debug.WriteLine("OwnerDrawnList::OnResize is called");

            var viewableItemCount = ClientSize.Height/ItemHeight;

            VScrollBar.Bounds = new Rectangle(
                ClientSize.Width - ScrollWidth,
                0,
                ScrollWidth,
                ClientSize.Height);

            if (items.Count > viewableItemCount)
            {
                VScrollBar.Visible = true;
                VScrollBar.LargeChange = viewableItemCount;

                OffScreen = new Bitmap(
                    ClientSize.Width - ScrollWidth, ClientSize.Height);
            }
            else
            {
                VScrollBar.Visible = false;
                VScrollBar.LargeChange = items.Count;

                OffScreen = new Bitmap(
                    ClientSize.Width, ClientSize.Height);
            }

            VScrollBar.Maximum = items.Count - 1;
        }

        public void Redraw()
        {
            try
            {
                OnResize(null);
            }
            catch (OutOfMemoryException)
            {
                Debug.WriteLine("OutOfMemoryException caught at OwnerDrawnList::Redraw()");

                GC.Collect();
                OnResize(null);
            }

            Refresh();
        }
    }
}