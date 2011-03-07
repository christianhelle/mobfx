using System.Drawing;
using System.Xml.Serialization;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    /// <summary>
    /// Holds configuration data about the SmartMenu
    /// </summary>
    [XmlRoot("SmartMenu")]
    public class SmartMenuConfiguration
    {
        /// <summary>
        /// A collection of <see cref="SmartMenuItem"/>
        /// </summary>
        [XmlElement("MenuItem")]
        public SmartMenuItem[] SmartMenuItem;

        ///// <summary>
        ///// Initializes a new instance of the SmartMenuConfiguration class with default values.
        ///// </summary>
        //public SmartMenuConfiguration()
        //{
        //    BackgroundColor = "#ffffff";
        //    ButtonBorderColor = "#000000";
        //    ButtonFillColor = "#eeeeee";
        //    PressedBorderColor = "#000000";
        //    BorderWidth = 1;
        //    PressedBorderWidth = 2;
        //}

        /// <summary>
        /// Gets or sets how the smart menu items are laid out
        /// Supported: 
        ///     Hex7 - 7 hexagon buttons rendered as 2-3-2
        ///     Rect4 - 4 rectangular buttons rendered to take up the entire screen
        /// Default (or no matching layout): 
        ///     8 square buttons
        /// </summary>
        public string Layout { get; set; }

        ///// <summary>
        ///// Gets or sets the background color
        ///// </summary>
        //public string BackgroundColor { get; set; }

        ///// <summary>
        ///// Gets or sets the border color of all the <see cref="SmartMenuButton"/>
        ///// </summary>
        //public string ButtonBorderColor { get; set; }

        ///// <summary>
        ///// Gets or sets the fill color of all the <see cref="SmartMenuButton"/>
        ///// </summary>
        //public string ButtonFillColor { get; set; }

        ///// <summary>
        ///// Gets or sets the border width of all the <see cref="SmartMenuButton"/>
        ///// </summary>
        //public int BorderWidth { get; set; }

        ///// <summary>
        ///// Gets or sets the border width of all the <see cref="SmartMenuButton"/> when pressed
        ///// </summary>
        //public int PressedBorderWidth { get; set; }

        ///// <summary>
        ///// Gets or sets the border color of all the <see cref="SmartMenuButton"/> when pressed
        ///// </summary>
        //public string PressedBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the background image of the <see cref="SmartMenuFormView"/>
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the forward button image
        /// </summary>
        public string ForwardButtonImage { get; set; }

        /// <summary>
        /// Gets or sets the button style
        /// </summary>
        public string ButtonStyle { get; set; }
    }
}
