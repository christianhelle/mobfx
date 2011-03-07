using System.Xml.Serialization;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    /// <summary>
    /// An item in the SmartMenu
    /// </summary>
    public struct SmartMenuItem
    {
        /// <summary>
        /// The index value which describes the ordering of the items in the SmartMenu.
        /// </summary>
        public int Idx;

        /// <summary>
        /// The text displayed on the label of the <see cref="SmartMenuItem"/>
        /// </summary>
        public string Text;

        /// <summary>
        /// The filename of the image to be displayed.
        /// </summary>
        public string Image;

        /// <summary>
        /// The <see cref="IAction"/> implementation to associate with the item
        /// </summary>
        public string Action { get; set; }
    }
}