using System;
using System.Drawing;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms
{
    /// <summary>
    /// Base class for all transparent controls
    /// </summary>
    public class TransparentControl : OwnerDrawnBase
    {
        private bool transparent;

        /// <summary>
        /// Instantiating <see cref="TransparentControl"/> is not allowed
        /// </summary>
        protected TransparentControl()
        {
            Transparent = true;
        }

        /// <summary>
        /// Is set to true if the container of this control implements <see cref="IControlBackground"/>
        /// </summary>
        protected bool HasBackground { get; private set; }

        /// <summary>
        /// Enables and Disables the Transparency effect for the control
        /// </summary>
        /// <remarks>
        /// To enable transparency, this control must be placed on a container control that implements
        /// <see cref="IControlBackground"/>
        /// </remarks>
        public bool Transparent
        {
            get { return transparent; }
            set
            {
                transparent = value;
                Invalidate();
            }
        }

        #region Code Behind Generation
        private bool ShouldSerializeTransparent()
        {
            return !Transparent;
        } 
        #endregion

        /// <summary>
        /// Draws part of the parents background into the control
        /// </summary>
        /// <param name="e">Paint event data</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var form = Parent as IControlBackground;
            if (form == null || !Transparent || form.BackgroundImage == null)
            {
                base.OnPaintBackground(e);
                return;
            }
            HasBackground = true;

            DrawBackground(e.Graphics, form, Bounds, Parent);
        }

        /// <summary>
        /// Draws the background of the parent control giving a transparent effect
        /// </summary>
        /// <param name="graphics">Drawing surface</param>
        /// <param name="form">Parent Container that implements <see cref="IControlBackground"/></param>
        /// <param name="bounds">Bounds of the Control</param>
        /// <param name="parent">Instance of the Parent Container</param>
        public void DrawBackground(Graphics graphics, IControlBackground form, Rectangle bounds, Control parent)
        {
            if (form.BackgroundImage == null)
                return;

            var g = GraphicsEx.FromGraphics(graphics);
            switch (form.BackgroundDrawMode)
            {
                case ImageDrawMode.Normal:
                    g.DrawImage(form.BackgroundImage, 0, 0, bounds);
                    break;

                case ImageDrawMode.Center:
                    Point location = GraphicsEx.GetCenter(form.BackgroundImage.Size, parent.ClientSize);
                    g.DrawImage(form.BackgroundImage,
                                location.X,
                                location.Y,
                                new Rectangle(bounds.X,
                                              bounds.Y,
                                              form.BackgroundImage.Width,
                                              form.BackgroundImage.Height));
                    break;

                case ImageDrawMode.Stretch:
                    using (Image image = ImageManipulator.Stretch(form.BackgroundImage, parent.ClientSize))
                        g.DrawImage(image, 0, 0, bounds);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}