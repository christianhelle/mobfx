using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    internal sealed class SmartMenuButtonHotspot : TransparentControl
    {
        private PictureBoxEx imageControl;

        public SmartMenuButtonHotspot()
        {
            this.Transparent = true;

            imageControl = new PictureBoxEx();
            imageControl.Pushable = false;
            imageControl.Location = new Point( 0, 0 );
            imageControl.SizeMode = PictureBoxSizeMode.CenterImage;
            imageControl.Transparent = true;
            imageControl.MouseDown += ( sender, e ) => OnMouseDown( e );
            imageControl.MouseUp += ( sender, e ) => OnMouseUp( e );
            this.Controls.Add( imageControl );
        }

        public Image Image
        {
            get
            {
                return imageControl.Image;
            }

            set
            {
                imageControl.Image = value;
                Invalidate();
            }
        }

        protected override void OnResize( EventArgs e )
        {
            base.OnResize( e );

            imageControl.Size = this.Size;
        }        

        /// <summary>
        /// Overridden with an emtpy definition to avoid flickering (All drawing must be done in Paint)
        /// </summary>
        protected override void OnPaintBackground( PaintEventArgs e )
        {
        }
    }
}
