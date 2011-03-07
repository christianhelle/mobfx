using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing.Native;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// Provides extended drawing functionality not provided by the
    /// <see cref="System.Drawing.Graphics"/> class.
    /// </summary>
    /// <example>
    /// This is an example of how you might want to use this class:
    /// 
    /// <code>
    /// public Image BackgroundImage { get; set; }
    /// 
    /// protected override void OnPaint(PaintEventArgs e)
    /// {
    ///     base.OnPaint(e);
    ///     
    ///     GraphicsEx gfx = GraphicsEx.FromGraphics(e.Graphics);
    ///     gfx.DrawImage(BackgroundImage);
    /// }
    /// </code>
    /// </example>
    public class GraphicsEx : IDisposable
    {
        private const string NULL_ATTRIB = "ImageAttributes has not been initialized";
        private const string NULL_IMAGE = "Image to draw has not been initialized";
        private const string NULL_SURFACE = "Surface has not been initialized";

        private GraphicsEx(Graphics graphics)
        {
            Surface = graphics;
        }

        /// <summary>
        /// Creates a new instance of <see cref="GraphicsEx"/> from the specified <see cref="Graphics"/>
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public static GraphicsEx FromGraphics(Graphics graphics)
        {
            return new GraphicsEx(graphics);
        }

        /// <summary>
        /// Creates a new instance of <see cref="GraphicsEx"/> from the specified <see cref="Image"/>
        /// </summary>
        /// <param name="image">
        /// <see cref="Image"/> from which to create the new <see cref="GraphicsEx"/>
        /// </param>
        /// <returns>
        /// Returns a new <see cref="GraphicsEx"/> object forthe specified image
        /// </returns>
        public static GraphicsEx FromImage(Image image)
        {
            return new GraphicsEx(Graphics.FromImage(image));
        }

        /// <summary>
        /// Gets the drawing surface
        /// </summary>
        public Graphics Surface { get; private set; }

        #region IDisposable Members

        /// <summary>
        /// Releases all native drawing resources
        /// </summary>
        public void Dispose()
        {
            if (Surface != null)
            {
                Surface.Dispose();
                Surface = null;
            }
        }

        #endregion

        /// <summary>
        /// Draws the specified <see cref="Image"/>, using its original physical size on the
        /// top-left corner of the drawing surface
        /// </summary>
        /// <param name="image"><see cref="Image"/> to draw</param>
        public void DrawImage(Image image)
        {
            DrawImage(image, 0, 0);
        }

        /// <summary>
        /// Draws an <see cref="Image"/> to the drawing surface using the provided drawing parameters
        /// </summary>
        /// <param name="image"><see cref="Image"/> to draw</param>
        /// <param name="size">Size of the container</param>
        /// <param name="mode">Detemines how the image is drawn</param>
        public void DrawImage(Image image, Size size, ImageDrawMode mode)
        {
            switch (mode)
            {
                case ImageDrawMode.Normal:
                    DrawImage(image);
                    break;
                case ImageDrawMode.Center:
                    Point location = GetCenter(image.Size, size);
                    DrawImage(image, location.X, location.Y);
                    break;
                case ImageDrawMode.Stretch:
                    using (Image stretchedImage = ImageManipulator.Stretch(image, size))
                        DrawImage(stretchedImage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        /// <summary>
        /// Draws the specified image, using its original physical size, at the location 
        /// specified by a coordinate pair.
        /// </summary>
        /// <param name="image"><see cref="Image"/> to draw</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
        public void DrawImage(Image image, int x, int y)
        {
            if (Surface == null)
                throw new NullReferenceException(NULL_SURFACE);

            if (image == null)
                throw new ArgumentNullException(NULL_IMAGE);

            Surface.DrawImage(image, x, y);
        }

        /// <summary>
        /// Draws the specified image, using its original physical size, at the location 
        /// specified by a coordinate pair.
        /// </summary>
        /// <param name="image"><see cref="Image"/> to draw</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
        /// <param name="imageBounds">Bounds of the image</param>
        public void DrawImage(Image image, int x, int y, Rectangle imageBounds)
        {
            if (Surface == null)
                throw new NullReferenceException(NULL_SURFACE);

            if (image == null)
                throw new ArgumentNullException(NULL_IMAGE);

            Surface.DrawImage(image, x, y, imageBounds, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Draws the specified image, using its original physical size, at the location 
        /// specified by a coordinate pair.
        /// </summary>
        /// <param name="image"><see cref="Image"/> to draw</param>
        /// <param name="destBounds"></param>
        /// <param name="imageBounds">Bounds of the image</param>
        /// <param name="attributes"></param>
        public void DrawImage(Image image, Rectangle destBounds, Rectangle imageBounds,
                              ImageAttributes attributes)
        {
            if (Surface == null)
                throw new NullReferenceException(NULL_SURFACE);

            if (image == null)
                throw new ArgumentNullException(NULL_IMAGE);

            if (attributes == null)
                throw new ArgumentNullException(NULL_ATTRIB);

            Surface.DrawImage(
                image,
                destBounds,
                imageBounds.X,
                imageBounds.Y,
                imageBounds.Width,
                imageBounds.Height,
                GraphicsUnit.Pixel,
                attributes);
        }

        /// <summary>
        /// Retrieves the center of a specified <see cref="Rectangle"/>
        /// </summary>
        /// <param name="destination">Size of the destination (Surface)</param>
        /// <param name="source">Size of the source (Image)</param>
        /// <returns>
        /// Returns an instance of <see cref="Point"/> containing the center 
        /// coordinates of the specified <see cref="Size"/>s
        /// </returns>
        public static Point GetCenter(Size destination, Size source)
        {
            return new Point(
                (destination.Width - source.Width) / 2,
                (destination.Height - source.Height) / 2);
        }

        /// <summary>
        /// Fills <see cref="Rectangle"/> with Colors that smoothly fades from one side to the other.
        /// </summary>
        /// <param name="rectangle"><see cref="Rectangle"/> to fill</param>
        /// <param name="startColor">Start Color for the fill</param>
        /// <param name="endColor">End Color for the fill</param>
        /// <param name="direction">Direction of the fade from Start to End</param>
        /// <remarks>
        /// This method only supports linear gradient fills
        /// </remarks>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when this method is used in a device older than Pocket PC 2003
        /// </exception>
        public void GradientFill(Rectangle rectangle, Color startColor,
            Color endColor, GradientFillDirection direction)
        {
            var tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rectangle.Right, rectangle.Bottom, endColor);
            tva[1] = new TRIVERTEX(rectangle.X, rectangle.Y, startColor);
            var gra = new[] { new GRADIENT_RECT(0, 1) };

            var hdc = Surface.GetHdc();
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.WinCE)
                    NativeMethods.GradientFill(
                        hdc,
                        tva,
                        (uint)tva.Length,
                        gra,
                        (uint)gra.Length,
                        (uint)direction);
                else
                    NativeMethods.GdiGradientFill(
                        hdc,
                        tva,
                        (uint)tva.Length,
                        gra,
                        (uint)gra.Length,
                        (uint)direction);
            }
            catch (MissingMethodException)
            {
                throw new PlatformNotSupportedException("Gradient Fill is not supported in this platform");
            }
            finally
            {
                Surface.ReleaseHdc(hdc);
            }
        }

        /// <summary>
        /// Displays bitmaps that have transparent or semitransparent pixels.
        /// </summary>
        /// <param name="image">Image to blend the surface with</param>
        /// <param name="opacity">Opacity level (0-255 bytes)</param>
        /// <param name="destX">X-axis of the destination</param>
        /// <param name="destY">Y-axis of the destination</param>
        /// <exception cref="PlatformNotSupportedException">
        /// Thrown when this method is used in a device older than Pocket PC 2003
        /// </exception>
        public void AlphaBlend(Image image, byte opacity, int destX, int destY)
        {
            if (Environment.OSVersion.Version.Major < 5)
                return;

            using (var imageSurface = Graphics.FromImage(image))
            {
                var hdcDst = Surface.GetHdc();
                var hdcSrc = imageSurface.GetHdc();

                var blendFunction = new BLENDFUNCTION
                                        {
                                            BlendOp = ((byte)BlendOperation.AC_SRC_OVER),
                                            BlendFlags = ((byte)BlendFlags.Zero),
                                            SourceConstantAlpha = opacity,
                                            AlphaFormat = 0
                                        };

                try
                {
                    if (Environment.OSVersion.Platform == PlatformID.WinCE)
                        NativeMethods.AlphaBlend(
                            hdcDst,
                            destX == 0 ? 0 : -destX,
                            destY == 0 ? 0 : -destY,
                            image.Width,
                            image.Height,
                            hdcSrc,
                            0,
                            0,
                            image.Width,
                            image.Height,
                            blendFunction);
                    else
                        NativeMethods.GdiAlphaBlend(
                            hdcDst,
                            destX == 0 ? 0 : -destX,
                            destY == 0 ? 0 : -destY,
                            image.Width,
                            image.Height,
                            hdcSrc,
                            0,
                            0,
                            image.Width,
                            image.Height,
                            blendFunction);
                }
                catch (MissingMethodException)
                {
                    throw new PlatformNotSupportedException("Alpha Blend is not supported in this platform");
                }
                finally
                {
                    Surface.ReleaseHdc(hdcDst);
                    imageSurface.ReleaseHdc(hdcSrc);
                }
            }
        }

        /// <summary>
        /// Draws and fills a rectangle with rounded borders
        /// </summary>
        /// <param name="bounds">Bounds of the rectangle</param>
        /// <param name="fillColor">Color of the fill</param>
        public void DrawRoundedRectangle(Rectangle bounds, Color fillColor)
        {
            const int WIDTH = 24;
            const int HEIGHT = 24;

            var points = new Point[8];

            points[0].X = bounds.Left + WIDTH / 2;
            points[0].Y = bounds.Top + 1;

            points[1].X = bounds.Right - WIDTH / 2;
            points[1].Y = bounds.Top + 1;

            points[2].X = bounds.Right;
            points[2].Y = bounds.Top + HEIGHT / 2;

            points[3].X = bounds.Right;
            points[3].Y = bounds.Bottom - HEIGHT / 2;

            points[4].X = bounds.Right - WIDTH / 2;
            points[4].Y = bounds.Bottom;

            points[5].X = bounds.Left + WIDTH / 2;
            points[5].Y = bounds.Bottom;

            points[6].X = bounds.Left + 1;
            points[6].Y = bounds.Bottom - HEIGHT / 2;

            points[7].X = bounds.Left + 1;
            points[7].Y = bounds.Top + HEIGHT / 2;

            using (var brush = new SolidBrush(fillColor))
            using (var pen = new Pen(fillColor))
            {
                Surface.DrawLine(pen, bounds.Left + WIDTH / 2, bounds.Top, bounds.Right - WIDTH / 2, bounds.Top);
                Surface.FillEllipse(brush, bounds.Right - WIDTH, bounds.Top, WIDTH, HEIGHT);
                Surface.DrawEllipse(pen, bounds.Right - WIDTH, bounds.Top, WIDTH, HEIGHT);

                Surface.DrawLine(pen, bounds.Right, bounds.Top + HEIGHT / 2, bounds.Right, bounds.Bottom - HEIGHT / 2);
                Surface.FillEllipse(brush, bounds.Right - WIDTH, bounds.Bottom - HEIGHT, WIDTH, HEIGHT);
                Surface.DrawEllipse(pen, bounds.Right - WIDTH, bounds.Bottom - HEIGHT, WIDTH, HEIGHT);

                Surface.DrawLine(pen, bounds.Right - WIDTH / 2, bounds.Bottom, bounds.Left + WIDTH / 2, bounds.Bottom);
                Surface.FillEllipse(brush, bounds.Left, bounds.Bottom - HEIGHT, WIDTH, HEIGHT);
                Surface.DrawEllipse(pen, bounds.Left, bounds.Bottom - HEIGHT, WIDTH, HEIGHT);

                Surface.DrawLine(pen, bounds.Left, bounds.Bottom - HEIGHT / 2, bounds.Left, bounds.Top + HEIGHT / 2);
                Surface.FillEllipse(brush, bounds.Left, bounds.Top, WIDTH, HEIGHT);
                Surface.DrawEllipse(pen, bounds.Left, bounds.Top, WIDTH, HEIGHT);

                Surface.FillPolygon(brush, points);
            }
        }

        /// <summary>
        /// Gets a snapshot of the current screen display
        /// </summary>
        /// <returns>Returns an <see cref="Image"/> of the containing a snapshot current display</returns>
        public Image GetSnapshot()
        {
            var rect = Screen.PrimaryScreen.Bounds;
            var snapshot = new Bitmap(rect.Width, rect.Height);

            using (var snapshotGraphics = Graphics.FromImage(snapshot))
            {
                var snapshotHdc = snapshotGraphics.GetHdc();
                var surfaceHdc = Surface.GetHdc();

                try
                {
                    NativeMethods.BitBlt(
                            snapshotHdc,
                            0,
                            0,
                            rect.Width,
                            rect.Height,
                            surfaceHdc,
                            rect.Left,
                            rect.Top,
                            0xCC0020);
                }
                finally
                {
                    snapshotGraphics.ReleaseHdc(snapshotHdc);
                    Surface.ReleaseHdc(surfaceHdc);
                }
            }

            return snapshot;
        }
    }
}