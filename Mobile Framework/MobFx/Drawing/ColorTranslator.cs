using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace ChristianHelle.Framework.WindowsMobile.Drawing
{
    /// <summary>
    /// Provides an easy way of encoding and decoding a Color structure.
    /// </summary>
    public class ColorTranslator
    {
        /// <summary>
        /// Decode a Color structure from a formatted string, similar to the one used on HTML.
        /// </summary>
        /// <param name="htmlColor">A string of hexadecimal numbers with the ff format: #RRGGBB</param>
        /// <returns>A Color structure equivalent to the provided string.</returns>
        public static Color FromHtml( string htmlColor )
        {
            if ( string.IsNullOrEmpty( htmlColor ) )
                return Color.Empty;

            if ( htmlColor.StartsWith( "#" ) && htmlColor.Length == 7 )
            {
                return Color.FromArgb( 
                    byte.Parse( htmlColor.Substring( 1, 2 ), NumberStyles.AllowHexSpecifier ),
                    byte.Parse( htmlColor.Substring( 3, 2 ), NumberStyles.AllowHexSpecifier ),
                    byte.Parse( htmlColor.Substring( 5, 2 ), NumberStyles.AllowHexSpecifier ) );
            }

            return Color.Empty;
        }


        ///<summary>
        /// Converts a color structure to a html string representation.
        ///</summary>
        ///<param name="color">Color to be converted.</param>
        ///<returns>Html string corresponding to the color.</returns>
        public static string ToHtml(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        static readonly Dictionary<Color, string> _colorNames = new Dictionary<Color, string>();
        static readonly Dictionary<string, Color> _colorValues = new Dictionary<string, Color>();

        /// <summary>
        /// Converts a string to its corresponding Color structure.
        /// </summary>
        /// <param name="colorName">Name of color.</param>
        /// <returns>Returns the color structure corresponding to the name specified.
        /// Returns Color.Empty if the .NET framework doesn't define a Color corresponding to the name.</returns>
        public static Color FromName(string colorName)
        {
            BuildColorTables();
            if (_colorValues.ContainsKey(colorName)) {
                return _colorValues[colorName];
            }
            return Color.Empty;
        }

        /// <summary>
        /// Converts a System.Drawing.Color structure to its corresponding name.
        /// </summary>
        /// <param name="color">Color structure</param>
        /// <returns>Name of color. Returns HTML format color string if no corresponding color name exists.</returns>
        public static string ToName(Color color)
        {
            BuildColorTables();
            if (_colorNames.ContainsKey(color)) {
                return _colorNames[color];
            }
            return ToHtml(color);
        }


        private static void BuildColorTables()
        {
            if (_colorValues.Count == 0) {
                _colorValues.Add("Empty", Color.Empty);
                _colorNames.Add(Color.Empty, "Empty");
                var p = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public);
                for (var i = 0; i < p.Length; i++) {
                    if ((p[i].PropertyType) == typeof(Color)) {
                        var name = p[i].Name;
                        var color = (Color)p[i].GetValue(null, null);
                        _colorValues.Add(name, color);
                        if (!_colorNames.ContainsKey(color)) {
                            _colorNames.Add(color, name);
                        }
                    }
                }
                p = typeof(SystemColors).GetProperties(BindingFlags.Static | BindingFlags.Public);
                for (var i = 0; i < p.Length; i++) {
                    if ((p[i].PropertyType) == typeof(Color)) {
                        var name = p[i].Name;
                        var color = (Color)p[i].GetValue(null, null);
                        _colorValues.Add(name, color);
                        if (!_colorNames.ContainsKey(color)) {
                            _colorNames.Add(color, name);
                        }
                    }
                }
            }
        }
    }
}
