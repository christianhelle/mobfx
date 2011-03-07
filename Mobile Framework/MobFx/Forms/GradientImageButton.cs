using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using ChristianHelle.Framework.WindowsMobile.Forms;

namespace ChristianHelle.WMS.BoConcept.Controls
{
    /// <summary>
    /// An aero style image button
    /// </summary>
    public class GradientImageButton : TransparentButton
    {
        private Bitmap image;
        private bool stretchImage;

        public GradientImageButton()
        {
            StretchImage = true;
        }

        /// <summary>
        /// Gets or sets the Image to display
        /// </summary>
        public Bitmap Image
        {
            get { return image; }
            set
            {
                image = value;
                Invalidate();
            }
        }
        
        /// <summary>
        /// Enables or disables stretching the image
        /// </summary>
        public bool StretchImage
        {
            get { return stretchImage; }
            set
            {
                stretchImage = value;
                Invalidate();
            }
        }

        private bool ShouldSerializeStretchImage()
        {
            return !stretchImage;
        }

        /// <summary>
        /// Draws the control in normal or pushed states
        /// </summary>
        /// <param name="e">Paitn event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var attributes = new ImageAttributes();
            using (var g = Graphics.FromImage(MemoryBitmap))
            {
                if (Pushed)
                {
                    using (var pen = new Pen(Theme.ThemeBase))
                        GraphicsExtensions.DrawThemedGradientRectangle(g, pen, ClientRectangle, Dpi.ScaleSize(new Size(8, 8)));
                }
                else
                    g.Clear(Parent.BackColor);

                var textSize = g.MeasureString(Text, Font);
                var textArea = new RectangleF((ClientSize.Width - textSize.Width) / 2,
                                              (ClientSize.Height - textSize.Height),
                                              textSize.Width,
                                              textSize.Height);

                if (Image != null)
                {
                    var imageWidth = Dpi.Scale(Image.Width);
                    var imageHeight = Dpi.Scale(Image.Height);
                    var imageArea = new Rectangle((ClientSize.Width - imageWidth) / 2,
                                                  (ClientSize.Height - imageHeight) / 2,
                                                  imageWidth,
                                                  imageHeight);

                    var key = Image.GetPixel(0, 0);
                    attributes.SetColorKey(key, key);

                    g.DrawImage(Image,
                                StretchImage ? ClientRectangle : imageArea,
                                0, 0, Image.Width, Image.Height,
                                GraphicsUnit.Pixel,
                                attributes);
                }

                using (var brush = new SolidBrush(ForeColor))
                    g.DrawString(Text, Font, brush, textArea);

                if (Pushed)
                {
                    var key = MemoryBitmap.GetPixel(0, 0);
                    attributes.SetColorKey(key, key);
                }
                else
                    attributes.ClearColorKey();

                e.Graphics.DrawImage(MemoryBitmap,
                                     ClientRectangle,
                                     0, 0, MemoryBitmap.Width, MemoryBitmap.Height,
                                     GraphicsUnit.Pixel,
                                     attributes);
            }
        }

        /// <summary>
        /// Overridden with an emtpy definition to avoid flickering (All drawing must be done in Paint)
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}