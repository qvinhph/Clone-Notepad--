using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SyntaxHighlightingTextbox
{
    public class HighlightDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighlightDescriptor"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="highlightType">Type of the descriptor.</param>
        /// <param name="dr">The dr.</param>
        /// <param name="useForAutoComplete">if set to <c>true</c> [use for auto complete].</param>
        public HighlightDescriptor(string token, Color color, Font font, HighlightType highlightType, 
            DescriptorRecognition descriptorRecognition, bool isUsedForAutoComplete = true)
        {
            if (highlightType == HighlightType.ToCloseToken)
            {
                throw new ArgumentException("You may not choose ToCloseToken HighlightType without specifing an end token.");
            }
            this.color = color;
            this.font = font;
            this.token = token;
            this.highlightType = highlightType;
            this.descriptorRecognition = descriptorRecognition;
            this.closeToken = null;
            this.isUsedForAutoComplete = isUsedForAutoComplete;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighlightDescriptor"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="closeToken">The close token.</param>
        /// <param name="highlightType">Type of the descriptor.</param>
        /// <param name="dr">The dr.</param>
        /// <param name="color">The color.</param>
        /// <param name="font">The font.</param>
        /// <param name="useForAutoComplete">if set to <c>true</c> [use for auto complete].</param>
        public HighlightDescriptor(string token, string closeToken, HighlightType highlightType,
            DescriptorRecognition descriptorRecognition, Color color, Font font, bool isUsedForAutoComplete = true)
        {
            this.color = color;
            this.font = font;
            this.token = token;
            this.closeToken = closeToken;
            this.highlightType = highlightType;
            this.descriptorRecognition = descriptorRecognition;
            this.isUsedForAutoComplete = isUsedForAutoComplete;
        }

        public readonly Color color;
        public readonly Font font;
        public readonly string token;
        public readonly string closeToken;
        public readonly HighlightType highlightType;
        public readonly DescriptorRecognition descriptorRecognition;
        public readonly bool isUsedForAutoComplete;
    }

    public enum HighlightType
    {
        /// <summary>
        /// Causes the entire line from this point on the be highlighted, regardless of other tokens.
        /// </summary>
        ToEOL,
        /// <summary>
        /// Highlights all text until the end token.
        /// </summary>
        ToCloseToken,
        /// <summary>
        /// To end of word.
        /// </summary>
        ToEOW
    }

    public enum DescriptorRecognition
    {
        /// <summary>
        /// Only if the whole token is equal to the word.
        /// </summary>
        WholeWord,
        /// <summary>
        /// If the word starts with the token.
        /// </summary>
        StartsWith,
        /// <summary>
        /// If the word contains the token.
        /// </summary>
        //Contains,
        /// <summary>
        /// If the word is the number.
        /// </summary>
        IsNumber
    }
}
