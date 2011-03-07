using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    internal class SmartMenuButton : IDisposable
    {
        private ISmartMenuView view;
        private IShape shape;
        private SmartMenuButtonHotspot hotspot;
        ////private LabelEx labelControl;
        
        private int borderWidth;
        private int pressedBorderWidth;
        private Color borderColor;
        private Color pressedBorderColor;
        private Color fillColor;
        private Pen pressedLinePen;
        private Pen defaultLinePen;

        public SmartMenuButton( ISmartMenuView view, IShape shape )
        {
            borderWidth = 1;
            pressedBorderWidth = 2;
            this.view = view;
            this.shape = shape;
            
            hotspot = new SmartMenuButtonHotspot();
            hotspot.Location = shape.InteractiveRegion.Location;
            hotspot.Size = shape.InteractiveRegion.Size;
            hotspot.MouseDown += new MouseEventHandler( Hotspot_MouseDown );
            hotspot.MouseUp += new MouseEventHandler( Hotspot_MouseUp );
            ////hotspot.Click += new EventHandler( Hotspot_Click );
            view.Controls.Add( hotspot );

            ////labelControl = new LabelEx();
            ////labelControl.Font = new Font( labelControl.Font.Name, 7, FontStyle.Regular );
            ////labelControl.Transparent = true;
            ////this.Controls.Add( labelControl );

            this.BorderColor = Color.WhiteSmoke;
            this.PressedBorderColor = Color.WhiteSmoke;
            this.FillColor = Color.SlateGray;
        }

        protected SmartMenuButton()
        {
        }

        public event EventHandler Click;

        public IShape Shape
        {
            get
            {
                return shape;
            }
        }

        ////public override string Text 
        ////{
        ////    get
        ////    {
        ////        return labelControl.Text;
        ////    }

        ////    set
        ////    {
        ////        labelControl.Text = value;
        ////    }
        ////}

        ////public override Font Font
        ////{
        ////    get
        ////    {
        ////        return labelControl.Font;
        ////    }

        ////    set
        ////    {
        ////        labelControl.Font = value;
        ////    }
        ////}

        public Image Image
        {
            get
            {
                return hotspot.Image;
            }

            set
            {
                hotspot.Image = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }

            set
            {
                borderColor = value;
                DisposeObject( defaultLinePen );
                defaultLinePen = new Pen( borderColor, borderWidth );
                shape.LinePen = defaultLinePen;
            }
        }

        public Color PressedBorderColor
        {
            get
            {
                return pressedBorderColor;
            }

            set
            {
                pressedBorderColor = value;
                DisposeObject( pressedLinePen );
                pressedLinePen = new Pen( pressedBorderColor, pressedBorderWidth );
            }
        }

        public Color FillColor
        {
            get
            {
                return fillColor;
            }

            set
            {
                fillColor = value;
                hotspot.BackColor = value;
                shape.FillBrush = new SolidBrush( value );
            }
        }

        public Point Location
        {
            get
            {
                return shape.Location;
            }

            set
            {
                shape.Location = value;
                hotspot.Location = shape.InteractiveRegion.Location;
            }
        }

        public int BorderWidth
        {
            get
            {
                return borderWidth;
            }

            set
            {
                borderWidth = value;
                DisposeObject( defaultLinePen );
                defaultLinePen = new Pen( borderColor, borderWidth );
                shape.LinePen = defaultLinePen;
            }
        }

        public int PressedBorderWidth
        {
            get
            {
                return pressedBorderWidth;
            }

            set
            {
                pressedBorderWidth = value;
                DisposeObject( pressedLinePen );
                pressedLinePen = new Pen( pressedBorderColor, pressedBorderWidth );
            }
        }

        public string Text { get; set; }

        public IAction Action { get; set; }

        public void Draw( Graphics g )
        {
            shape.Draw( g );
        }

        #region IDisposable Members

        /// <summary>
        /// Releases all the resources used by this object.
        /// </summary>
        public void Dispose()
        {
            DisposeObject( pressedLinePen );
            DisposeObject( defaultLinePen );
        }

        #endregion

        private void DisposeObject( IDisposable disposable )
        {
            if ( disposable != null )
            {
                disposable.Dispose();
                disposable = null;
            }
        }

        private void Hotspot_Click( object sender, EventArgs e )
        {
            if ( Click != null )
            {
                Click.Invoke( sender, e );
            }
        }

        private void Hotspot_MouseUp( object sender, MouseEventArgs e )
        {
            shape.LinePen = defaultLinePen;
            view.Invalidate( shape.DrawingRegion );
        }

        private void Hotspot_MouseDown( object sender, MouseEventArgs e )
        {
            shape.LinePen = pressedLinePen;
            view.Invalidate( shape.DrawingRegion );
        }
    }
}
