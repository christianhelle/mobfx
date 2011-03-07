using System.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// Enumeration determining how an <see cref="Image"/> is drawn onto a surface
    /// </summary>
    /// <remarks>
    /// This is to be used as a parameter to methods of the <see cref="ImageManipulator"/> and 
    /// <see cref="GraphicsEx"/> class
    /// </remarks>
    public enum ImageDrawMode
    {
        /// <summary>
        /// Draws the <see cref="Image"/> normally, starting from the Top-Left corner of the drawing surface
        /// </summary>
        Normal,
        /// <summary>
        /// Draws the <see cref="Image"/> in the center of the drawing surface
        /// </summary>
        Center,
        /// <summary>
        /// Draws the <see cref="Image"/> stretched to the drawing surface
        /// </summary>
        Stretch
    }
}