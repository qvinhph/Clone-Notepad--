using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace SyntaxHighlightingTextbox
{
    public class FontDictionary
    {
        /// <summary>
        /// The collection with pair of values.
        /// TKey is <see cref="Font"/>. TValue is string which contains rtf tags.
        /// </summary>
        private Dictionary<Font, string> styles = new Dictionary<Font, string>();

        /// <summary>
        /// Clears the list of styles.
        /// </summary>
        public void Clear()
        {
            styles.Clear();
        }

        /// <summary>
        /// Save the font and rtf tags to the dictionary of styles.
        /// </summary>
        /// <param name="font">The font to add.</param>
        /// <returns></returns>
        public string SaveFontStyle(Font font)
        {
            StringBuilder fontStyle = new StringBuilder();

            if (font.Bold)
                fontStyle.Append(@"\b");

            if (font.Italic)
                fontStyle.Append(@"\i");

            if (font.Underline)
                fontStyle.Append(@"\ul");

            if (font.Strikeout)
                fontStyle.Append(@"\strike");

            string result = fontStyle.ToString();

            //Load to the dictionary of styles.
            if(!styles.ContainsKey(font))
            {
                styles.Add(font, result);
            }
            else
            {
                styles[font] = result;
            }

            return result;
        }

        /// <summary>
        /// Get the rtf tags of the font.
        /// </summary>
        public string GetFontStyle(Font font)
        {
            string result = null;
            //Try to get value of font in the dictionary of styles.
            //If TryGetValue returns false, save the new font to dictionary.
            if (!styles.TryGetValue(font, out result))
            {
                result = SaveFontStyle(font);
            }

            return result;
        }
    }
}
