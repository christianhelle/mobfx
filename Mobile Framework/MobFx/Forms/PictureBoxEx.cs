using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Extends the functionality of <see cref="PictureBox"/>
    /// </summary>
    public class PictureBoxEx : TransparentControl
    {
        private bool pushed;

        /// <summary>
        /// Creates an instance of <see cref="PictureBoxEx"/>
        /// </summary>
        public PictureBoxEx()
        {
            Transparent = true;
            Pushable = true;
        }

        /// <summary>
        /// Gets or sets the image that is displayed by <see cref="PictureBoxEx"/>
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Gets or sets the value that determines whether the picturebox will react to mouse clicks.
        /// </summary>
        public bool Pushable { get; set; }

        /// <summary>
        /// Indicates how the image is displayed.
        /// </summary>
        /// <remarks>The default is <see cref="PictureBoxSizeMode.Normal"/></remarks>
        /// <exception cref="ArgumentException"> 
        /// The value assigned is not one of the <see cref="PictureBoxSizeMode"/> values
        /// </exception>
        public PictureBoxSizeMode SizeMode { get; set; }

        /// <summary>
        /// Sets the pushed flag to TRUE
        /// </summary>
        /// <param name="e">Mouse event data</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if ( Pushable )
            {
                pushed = true;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets the pushed flag to FALSE
        /// </summary>
        /// <param name="e">Mouse event data</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if ( Pushable )
            {
                pushed = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Draws the Image
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent == null && Image == null)
            {
                base.OnPaint(e);
                return;
            }

            using (var g = GraphicsEx.FromImage(MemoryBitmap))
            {
                var form = Parent as IControlBackground;
                if (!pushed)
                {
                    if (form != null && Transparent && form.BackgroundImage != null)
                        DrawBackground(g.Surface, form, Bounds, Parent);
                    else if (Parent != null)
                        g.Surface.Clear(Parent.BackColor);
                }

                if ( Image != null )
                {
                    switch ( SizeMode )
                    {
                        case PictureBoxSizeMode.Normal:
                            DrawNormal( g );
                            break;
                        case PictureBoxSizeMode.CenterImage:
                            DrawCentered( g );
                            break;
                        case PictureBoxSizeMode.StretchImage:
                            DrawStretched( g );
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                if (pushed && form != null && form.BackgroundImage != null)
                    g.AlphaBlend(form.BackgroundImage, 170, Location.X, Location.Y);
            }

            e.Graphics.DrawImage(MemoryBitmap, 0, 0);
        }

        private static ImageAttributes GetImageAttributes(Image image)
        {
            var attributes = new ImageAttributes();
            using (var bmp = new Bitmap(image))
            {
                Color key = bmp.GetPixel(0, 0);
                attributes.SetColorKey(key, key);
            }
            return attributes;
        }

        private void DrawNormal(GraphicsEx graphics)
        {
            ImageAttributes attributes = GetImageAttributes(Image);
            graphics.DrawImage(Image,
                               new Rectangle(0, 0, Image.Width, Image.Height),
                               new Rectangle(0, 0, Image.Width, Image.Height),
                               attributes);
        }

        private void DrawCentered(GraphicsEx graphics)
        {
            Point location = GraphicsEx.GetCenter( ClientSize, Image.Size );
            ImageAttributes attributes = GetImageAttributes(Image);
            graphics.DrawImage(Image,
                               new Rectangle(location.X, location.Y, Image.Width, Image.Height),
                               new Rectangle(0, 0, Image.Width, Image.Height),
                               attributes);
        }

        private void DrawStretched(GraphicsEx graphics)
        {
            using (Image image = ImageManipulator.Stretch(Image, ClientSize))
                DrawStretched(image, graphics);
        }

        private static void DrawStretched(Image image, GraphicsEx graphics)
        {
            ImageAttributes attributes = GetImageAttributes(image);
            graphics.DrawImage(image,
                               new Rectangle(0, 0, image.Width, image.Height),
                               new Rectangle(0, 0, image.Width, image.Height),
                               attributes);
        }
    }
}