using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using ChristianHelle.Framework.WindowsMobile.Drawing;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    internal class SmartMenuLayout : IDisposable
    {
        private ISmartMenuView view;
        private List<SmartMenuButton> buttons;

        public SmartMenuLayout( ISmartMenuView view, string layoutName )
        {
            this.view = view;
            Layout currentLayout;
            try
            {
                currentLayout = ( Layout )Enum.Parse( typeof( Layout ), layoutName, true );
            }
            catch ( ArgumentException )
            {
                currentLayout = Layout.rect8;
            }

            switch ( currentLayout )
            {
                case Layout.hex7:
                    Create7HexButtons();
                    break;
                case Layout.rect8:
                    break;
                default:
                    break;
            }            
        }

        private SmartMenuLayout()
        {
        }

        public enum Layout
        {
            /// <summary>
            /// Default layout of 8 rectangular(or square) buttons
            /// </summary>
            rect8,

            /// <summary>
            /// A set of hexagonal buttons displayed in a 2-3-2 layout
            /// </summary>
            hex7
        }

        public ReadOnlyCollection<SmartMenuButton> SmartMenuButtons
        {
            get
            {
                return buttons.AsReadOnly();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            int controlCount = view.Controls.Count;
            for ( int i = controlCount - 1; i >= 0; --i )
            {
                var c = view.Controls[ i ] as SmartMenuButtonHotspot;
                if ( c != null )
                {
                    view.Controls.Remove( c );
                    DisposeObject( c );
                }
            }
        }

        private void DisposeObject( IDisposable obj )
        {
            if ( obj != null )
            {
                obj.Dispose();
                obj = null;
            }
        }

        #endregion

        private void Create7HexButtons()
        {
            buttons = new List<SmartMenuButton>( 7 );
            int hexagonHeight = ( int )( ( double )view.ViewRectangle.Height / 3 ); // 105;
            int hexagonWidth = ( int )( ( double )view.ViewRectangle.Width * 0.3 ); // 70;
            int left = ( view.ViewRectangle.Width / 2 ) - ( ( hexagonWidth * 3 ) / 2 );
            int top = ( view.ViewRectangle.Height / 2 ) - ( ( ( hexagonHeight * 2 ) + ( hexagonHeight / 3 ) ) / 2 );
            Point topLeftAnchor = new Point( left, top );
            left += ( hexagonWidth / 2 );
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 0 ].Location = new Point( left, top );            

            left += hexagonWidth;
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 1 ].Location = new Point( left, top );
            
            left = topLeftAnchor.X;
            top += ( int )( hexagonHeight * 2 / 3 );
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 2 ].Location = new Point( left, top );
            
            left += hexagonWidth;
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 3 ].Location = new Point( left, top );
            
            left += hexagonWidth;
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 4 ].Location = new Point( left, top );
            
            left = topLeftAnchor.X;
            left += ( hexagonWidth / 2 );
            top += ( int )( hexagonHeight * 2 / 3 );
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 5 ].Location = new Point( left, top );
            
            left += hexagonWidth;
            buttons.Add( new SmartMenuButton( view, new Hexagon( hexagonHeight, hexagonWidth ) ) );
            buttons[ 6 ].Location = new Point( left, top );            
        }
    }
}
