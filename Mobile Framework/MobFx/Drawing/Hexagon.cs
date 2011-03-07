using System;
using System.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// A six-sided shape.
    /// </summary>
    public class Hexagon : IShape
    {
        private const int DrawingSpillOver = 5;
        private Point location;
        private Rectangle drawingRegion;
        private Rectangle interactiveRegion;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hexagon"/> class based on a given height and width.
        /// </summary>
        /// <param name="height">Height of the hexagon, from its topmost tip to the lowest tip.</param>
        /// <param name="width">Width of the hexagon.</param>
        public Hexagon( int height, int width )
        {
            int middle = width / 2;
            int upperThird = height / 3;
            int lowerThird = ( height * 2 ) / 3;

            Points = new Point[ 6 ];
            Points[ 0 ] = new Point( 0, upperThird );
            Points[ 1 ] = new Point( middle, 0 );
            Points[ 2 ] = new Point( width, upperThird );
            Points[ 3 ] = new Point( width, lowerThird );
            Points[ 4 ] = new Point( middle, height );
            Points[ 5 ] = new Point( 0, lowerThird );

            interactiveRegion = new Rectangle( ( middle / 2 ) + 1, ( upperThird / 2 ) + 1, middle - 1, lowerThird - 1 );
            drawingRegion = new Rectangle(
                -DrawingSpillOver,
                -DrawingSpillOver,
                width + ( DrawingSpillOver * 2 ),
                height + ( DrawingSpillOver * 2 ) );

            location = new Point( 0, 0 );
        }

        private Hexagon()
        {
        }

        /// <summary>
        /// Gets or sets the location of the shape
        /// </summary>
        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                drawingRegion.Offset( value.X, value.Y );
                interactiveRegion.Offset( value.X, value.Y );
                for ( int i = 0; i < Points.Length; i++ )
                {
                    Points[ i ].Offset( value.X, value.Y );
                }
            }
        }

        /// <summary>
        /// Gets the six points that bound the shape
        /// </summary>
        public Point[] Points { get; private set; }

        /// <summary>
        /// Gets or sets the color of the lines/edges of the polygon
        /// </summary>
        public Pen LinePen { get; set; }

        /// <summary>
        /// Gets the region that interacts with user input (like mouse clicks).
        /// </summary>
        public Rectangle InteractiveRegion 
        {
            get
            {
                return interactiveRegion;
            }
        }

        /// <summary>
        /// Gets the entire region that the shape occupies in its drawing.
        /// </summary>
        public Rectangle DrawingRegion
        {
            get
            {
                return drawingRegion;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Brush"/> used to fill the main parts of the shape.
        /// </summary>
        public Brush FillBrush { get; set; }

        /// <summary>
        /// Draw the <see cref="Hexagon"/> object
        /// </summary>
        public void Draw( Graphics g )
        {
            if ( FillBrush != null )
            {
                g.FillPolygon( FillBrush, Points );
            }

            if ( LinePen != null )
            {
                g.DrawPolygon( LinePen, Points );
            }
        }
    }
}
