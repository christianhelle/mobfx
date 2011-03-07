using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// Utility for manipulating images
    /// </summary>
    public static class ImageManipulator
    {
        /// <summary>
        /// Stretches an image to a specified <see cref="Size"/>
        /// </summary>
        /// <param name="image"><see cref="Image"/> to stretch</param>
        /// <param name="size">Desired size</param>
        /// <returns>Returns the stretched image</returns>
        /// <example>
        /// This is an example of how you might use this method:
        /// 
        /// <code>
        /// public Image BackgroundImage { get; set; }
        /// 
        /// protected override void OnPaint(PaintEventArgs e)
        /// {
        ///     using (Image stretchedImage = ImageManipulator.Stretch(BackgroundImage, ClientSize))
        ///     {
        ///         e.Graphics.DrawImage(stretchedImage, 0, 0);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Image Stretch(Image image, Size size)
        {
            if (image == null) 
                throw new ArgumentNullException("image");

            Image bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawImage(
                    image,
                    new Rectangle(0, 0, size.Width, size.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }
            return bmp;
        }

        /// <summary>
        /// Creates a grey scaled version of the specified <see cref="Image"/>
        /// </summary>
        /// <param name="image"><see cref="Image"/> to create into a grey scale image</param>
        /// <returns>Returns a grey scaled image of the one provided</returns>
        /// <example>
        /// This is an example of how you might use this method:
        /// 
        /// <code>
        /// public Image BackgroundImage { get; set; }
        /// 
        /// protected override void OnPaint(PaintEventArgs e)
        /// {
        ///     using (Image greyScaledImage = ImageManipulator.GreyScale(BackgroundImage))
        ///     {
        ///         e.Graphics.DrawImage(greyScaledImage, 0, 0);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Image GreyScale(Image image)
        {
            if (image == null) 
                throw new ArgumentNullException("image");

            var greyScaleImage = new Bitmap(image);
            var bmData = greyScaleImage.LockBits(
                new Rectangle(0, 0, greyScaleImage.Width, greyScaleImage.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            var stride = bmData.Stride;
            var scan0 = bmData.Scan0;

            unsafe
            {
                var p = (byte*)(void*)scan0;
                var nOffset = stride - greyScaleImage.Width * 3;
                byte red, green, blue;

                for (int y = 0; y < greyScaleImage.Height; ++y)
                {
                    for (int x = 0; x < greyScaleImage.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red
                            + .587 * green
                            + .114 * blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            greyScaleImage.UnlockBits(bmData);
            return greyScaleImage;
        }

        /// <summary>
        /// Brightens the specified <see cref="Image"/>
        /// </summary>
        /// <param name="image"><see cref="Image"/> to brighten</param>
        /// <param name="percentChange">Change percentage</param>
        /// <returns>Returns a manipulated image of the one provided</returns>
        /// <example>
        /// This is an example of how you might use this method:
        /// 
        /// <code>
        /// public Image BackgroundImage { get; set; }
        /// 
        /// protected override void OnPaint(PaintEventArgs e)
        /// {
        ///     using (Image image = ImageManipulator.Brighten(BackgroundImage, 20))
        ///     {
        ///         e.Graphics.DrawImage(image, 0, 0);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Image Brighten(Image image, decimal percentChange)
        {
            return AdjustBrightness(image, 100 + percentChange);
        }

        /// <summary>
        /// Darkens the specified <see cref="Image"/>
        /// </summary>
        /// <param name="image"><see cref="Image"/> to darken</param>
        /// <param name="percentChange">Change percentage</param>
        /// <returns>Returns a manipulated image of the one provided</returns>
        /// <example>
        /// This is an example of how you might use this method:
        /// 
        /// <code>
        /// public Image BackgroundImage { get; set; }
        /// 
        /// protected override void OnPaint(PaintEventArgs e)
        /// {
        ///     using (Image image = ImageManipulator.Darken(BackgroundImage, 20))
        ///     {
        ///         e.Graphics.DrawImage(image, 0, 0);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Image Darken(Image image, decimal percentChange)
        {
            return AdjustBrightness(image, 100 - percentChange);
        }

        private struct PixelData
        {
            public byte Blue;
            public byte Green;
            public byte Red;
        }

        private static unsafe Image AdjustBrightness(Image image, decimal percentChange)
        {
            if (image == null) 
                throw new ArgumentNullException("image");

            percentChange /= 100;
            var snapshot = new Bitmap(image);
            var rect = new Rectangle(0, 0, image.Width, image.Height);

            var bitmapBase = snapshot.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var bitmapBaseByte = (byte*)bitmapBase.Scan0.ToPointer();

            var rowByteWidth = rect.Width * 3;
            if (rowByteWidth % 4 != 0)
                rowByteWidth += (4 - (rowByteWidth % 4));

            for (var i = 0; i < rowByteWidth * rect.Height; i += 3)
            {
                var p = (PixelData*)(bitmapBaseByte + i);
                p->Red = (byte)Math.Round(Math.Min(p->Red * percentChange, 255));
                p->Green = (byte)Math.Round(Math.Min(p->Green * percentChange, 255));
                p->Blue = (byte)Math.Round(Math.Min(p->Blue * percentChange, 255));
            }

            snapshot.UnlockBits(bitmapBase);
            return snapshot;
        }
    }
}