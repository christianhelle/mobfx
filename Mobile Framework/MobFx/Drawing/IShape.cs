using System;
using System.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    internal interface IShape
    {
        Point Location { get; set; }
        
        Point[] Points { get; }
        
        Pen LinePen { get; set; }
        
        Brush FillBrush { get; set; }
        
        Rectangle InteractiveRegion { get; }
        
        Rectangle DrawingRegion { get; }
        
        void Draw( Graphics g );        
    }
}
