using System.Drawing;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Interface for enalbing transparency effect
    /// </summary>
    /// <remarks>
    /// Transparency can achieved by having a container control implement <see cref="IControlBackground"/>
    /// and drawing the backgrond image to the form
    /// </remarks>
    /// <example>
    /// This is an example of how to create a container for transparent controls:
    /// 
    /// <code>
    /// public class FormBase : Form, IControlBackground
    /// {
    ///     public Image BackgroundImage { get; set; }
    ///     public ImageDrawMode BackgroundDrawMode { get; set; }
    ///     
    ///     protected override void OnPaint(PaintEventArgs e)
    ///     {
    ///         base.OnPaint(e);
    ///         
    ///         if (BackgroundImage != null)
    ///         {
    ///             e.Graphics.DrawImage(BackgroundImage, 0, 0);
    ///         }
    ///     }
    /// }
    /// </code>
    /// 
    /// 
    /// This is an example of how to create a transparent control from scratch:
    /// 
    /// <code>
    /// public class TransparentLabel : Control
    /// {    
    ///     protected override void OnPaint(PaintEventArgs e)
    ///     {
    ///         using (var brush = new SolidBrush(ForeColor))
    ///         {
    ///             e.Graphics.DrawString(Text, Font, brush, 0, 0);
    ///         }
    ///     }
    /// 
    ///     protected override void OnPaintBackground(PaintEventArgs e) 
    ///     { 
    ///         IControlBackground parent = Parent as IControlBackground;
    ///         if (parent != null)
    ///         {
    ///             if (parent.BackgroundImage != null)
    ///             {
    ///                 e.Graphics.DrawImage(parent.BackgroundImage, 0, 0);
    ///             }
    ///         }
    ///     }
    /// }
    /// </code>
    /// 
    /// 
    /// This is an example of how to create a transparent control by inheriting 
    /// from <see cref="TransparentControl"/>:
    /// 
    /// <code>
    /// public class TransparentLabel : TransparentControl
    /// {
    ///     protected override void OnPaint(PaintEventArgs e)
    ///     {
    ///         using (var brush = new SolidBrush(ForeColor))
    ///         {
    ///             e.Graphics.DrawString(Text, Font, brush, 0, 0);
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IControlBackground
    {
        /// <summary>
        /// Gets or Sets the background image of the container control
        /// </summary>
        Image BackgroundImage { get; set; }

        /// <summary>
        /// Gets or Sets how the <see cref="BackgroundImage"/> will be drawn on control
        /// </summary>
        ImageDrawMode BackgroundDrawMode { get; set; }

        /// <summary>
        /// Gets or Sets how the <see cref="BackgroundImage"/> will be drawn depending 
        /// on the devices screen resolution
        /// </summary>
        /// <remarks>
        /// Set this to <c>true</c> to automatically the background image on high DPI devices.
        /// This way the developer does not need to provide seperate images for each device
        /// screen resolution. The difference notificability depends entirely on the 
        /// quality of the background image.
        /// </remarks>
        bool AutoScaleBackgroundImage { get; set; }
    }
}