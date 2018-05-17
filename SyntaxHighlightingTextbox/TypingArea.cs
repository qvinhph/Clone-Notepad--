using System;
using System.Collections.Generic;
///using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SyntaxHighlightingTextbox
{
    public partial class TypingArea : RichTextBox
    {
        #region Fields
        //Members exposed via properties
        private SeparatorCollection separators = new SeparatorCollection();
        private HighlightDescriptorCollection descriptors = new HighlightDescriptorCollection();
        private bool caseSensitive = false;
        private bool filterAutoComplete = false;
        private bool enabledAutoCompleteForm = false;
        private int maxUndoRedoSteps = 1000;
        private char[] separatorArray;

        //Internal use members
        private bool autoCompleteShown = false;
        private bool parsing = false;
        private bool ignoreLostFocus = false;

        //Members used for Highlight() function.
        private StringBuilder rtfHeader = new StringBuilder();
        private FontDictionary fontStyles = new FontDictionary();

        //private AutoCompleteForm mAutoCompleteForm = new AutoCompleteForm();

        //Undo/Redo members
        //private ArrayList mUndoList = new ArrayList();
        //private Stack mRedoStack = new Stack();
        //private bool mIsUndo = false;
        //private UndoRedoInfo mLastInfo = new UndoRedoInfo("", new Win32.POINT(), 0);

        #endregion

        #region Properties

        /// <summary>
        /// Determines if token recognition is case sensitive.
        /// </summary>
        public bool CaseSensitive
        {
            get
            {
                return caseSensitive;
            }
            set
            {
                caseSensitive = value;
            }
        }


        /// <summary>
        /// Sets whether or not to remove items from the Autocomplete window as the user types...
        /// </summary>
        public bool FilterAutoComplete
        {
            get
            {
                return filterAutoComplete;
            }
            set
            {
                filterAutoComplete = value;
            }
        }

        /// <summary>
        /// Set the maximum amount of Undo/Redo steps.
        /// </summary>
        public int MaxUndoRedoSteps
        {
            get
            {
                return maxUndoRedoSteps;
            }
            set
            {
                maxUndoRedoSteps = value;
            }
        }

        /// <summary>
        /// A collection of separators. A token is every string between two seperators.
        /// </summary>
        public SeparatorCollection Separators
        {
            get
            {
                return separators;
            }
        }

        /// <summary>
        /// The collection of highlight descriptors.
        /// </summary>
        /// 
        public HighlightDescriptorCollection Descriptors
        {
            get
            {
                return descriptors;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable auto complete form].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable auto complete form]; otherwise, <c>false</c>.
        /// </value>
        public bool enableAutoCompleteForm
        {
            get
            {
                return enableAutoCompleteForm;
            }
            set
            {
                enableAutoCompleteForm = value;
            }
        }

        #endregion

        #region Override Methods

        protected override void OnTextChanged(EventArgs e)
        {
            if (parsing) return;

            //if (!mIsUndo)
            //{
            //    mRedoStack.Clear();
            //    mUndoList.Insert(0, mLastInfo);
            //    this.LimitUndo();
            //    mLastInfo = new UndoRedoInfo(Text, GetScrollPos(), SelectionStart);
            //}
            
            Highlight();
          

            if (autoCompleteShown)
            {
                //if (mFilterAutoComplete)
                //{
                //    SetAutoCompleteItems();
                //    SetAutoCompleteSize();
                //    SetAutoCompleteLocation(false);
                //}
                //SetBestSelectedAutoCompleteItem();
            }

            base.OnTextChanged(e);
        }

        public void Highlight()
        {
            parsing = true;
            rtfHeader.Length = 0;
            fontStyles.Clear();

            //Save cursor and scrollbars position
            Win32.LockWindowUpdate(Handle);
            Win32.POINT scrollPosition = GetScrollPos();
            int cursorPosition = SelectionStart;

            StringBuilder rtfBody = new StringBuilder();

            //Dictionary that saves the index of font/color in font/color table.
            Dictionary<string, int> fonts = new Dictionary<string, int>();
            Dictionary<Color, int> colors = new Dictionary<Color, int>();

            //Add RTF header
            rtfHeader.Append(@"{\rtf1\ansi\deff0");

            //Create color table, loaded from color's descriptors.
            colors = AddColorTable(rtfHeader);

            //Create font table
            rtfHeader.Append(@"{\fonttbl ");

            //Add default font of the textbox.
            AddFontToTable(rtfHeader, Font, fonts);

            //Add the defaults font tags to 
            fontStyles.GetFontStyle(Font);

            //Parsing text in the rtf body
            rtfBody.Append(@"\viewkind4\uc1\pard\ltrpar").Append("\n");
            //Set default color and font for the text
            SetDefaultSetting(rtfBody, colors, fonts);

            separatorArray = separators.ToArray();

            //Replace some specified symbols that has meaning in RTF file.
            string inputText = Text;
            string[] lines = inputText.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}").Split('\n');

            //Scan every line of input text.
            for (int lineCounter = 0; lineCounter < lines.Length; lineCounter++)
            {
                if (lineCounter != 0)
                {
                    AddNewLine(rtfBody);
                }

                string line = lines[lineCounter];

                //Put every word of line to an array.
                string[] tokens = caseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);

                int tokenCounter = 0;
                for (int i = 0; i < line.Length;)
                {
                    char currentChar = line[i];
                    if (separators.Contains(currentChar))
                    {
                        rtfBody.Append(currentChar);
                        i++;
                    }
                    else
                    {
                        if (tokenCounter >= tokens.Length) break;
                        string currentToken = tokens[tokenCounter];
                        tokenCounter++;
                        bool isPlainTextToken = true;

                        foreach (var item in descriptors)
                        {
                            string stringToCompare = caseSensitive ? item.token : item.token.ToUpper();
                            bool match = false;

                            //Check if the current token matches any of descriptors according to the DescriptorRecognition property.
                            switch (item.descriptorRecognition)
                            {
                                case DescriptorRecognition.WholeWord:
                                    if (stringToCompare == currentToken)
                                        match = true;
                                    break;
                                case DescriptorRecognition.StartsWith:
                                    if (currentToken.StartsWith(stringToCompare))
                                        match = true;
                                    break;
                                case DescriptorRecognition.Contains:
                                    if (currentToken.Contains(stringToCompare))
                                        match = true;
                                    break;
                                case DescriptorRecognition.IsNumber:
                                    double number = 0;
                                    if (double.TryParse(currentToken, out number))
                                        match = true;
                                    continue;
                                default:
                                    break;
                            }
                            if (!match)
                            {
                                //If doesn't match, continue to check another item in descriptors.
                                continue;
                            }

                            //Found the item of descriptors that matches.
                            isPlainTextToken = false;
                            //Open a "block" that contains the text we are going to add
                            //and its style tags
                            rtfBody.Append("{");
                            //Apply the font and color for the text.
                            SetDescriptorSetting(rtfBody, item, colors, fonts);

                            string textToFormat = "";
                            switch (item.descriptorType)
                            {
                                case DescriptorType.Word:
                                    textToFormat = line.Substring(i, currentToken.Length);
                                    i += currentToken.Length;
                                    break;
                                //case DescriptorType.ToEOW:
                                //    textToFormat = line.Substring(i, currentToken.Length);
                                //    i += currentToken.Length;
                                //    break;
                                case DescriptorType.ToEOL:
                                    textToFormat = line.Substring(i, line.Length);
                                    i = line.Length;
                                    break;
                                case DescriptorType.ToCloseToken:
                                    {
                                        StringBuilder sbOfTextToFormat = new StringBuilder();
                                        //int closeStart = i + item.token.Length;
                                        while ((!line.Contains(item.closeToken)) && (lineCounter < lines.Length))
                                        {
                                            sbOfTextToFormat.Append(line.Remove(0, i));
                                            lineCounter++;
                                            if (lineCounter < lines.Length) //Not the last line.
                                            {
                                                AddNewLine(sbOfTextToFormat);
                                                line = lines[lineCounter];
                                                i = 0;
                                            }
                                            else
                                                i = line.Length;
                                        }

                                        bool hasCloseToken = line.Contains(item.closeToken);
                                        if (hasCloseToken)
                                        {
                                            int closeTokenIndex = line.IndexOf(item.closeToken);
                                            sbOfTextToFormat.Append(line.Substring(i, closeTokenIndex + item.closeToken.Length - i));

                                            //Because we might skip some token since add it to text to format.
                                            //--> We need to generate the list of tokens in line again. 
                                            line = line.Remove(0, closeTokenIndex + item.closeToken.Length);
                                            tokenCounter = 0;
                                            tokens = caseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);
                                            i = 0;
                                        }

                                        textToFormat = sbOfTextToFormat.ToString();
                                    }
                                    break;
                                default:
                                    break;
                            }

                            AddTextToRTFBody(rtfBody, textToFormat);
                            //Close the block contains the text formated.
                            rtfBody.Append("}");
                            break;
                        } //End of foreach(var item in descriptors).

                        if (isPlainTextToken)
                        {
                            AddTextToRTFBody(rtfBody, line.Substring(i, currentToken.Length));
                            i += currentToken.Length;
                        }
                    }
                } //End for(int i = 0; i < line.Length;).
            } //End of for(int lineCounter = 0; lineCounter < lines.Length; lineCounter++)

            //Close rtf header.
            rtfHeader.Append("\n}\n");

            //Join the rtf header with the rtf body.
            //Then show it to the text box.
            string rtfString = rtfHeader.ToString() + rtfBody.ToString();
            this.Rtf = rtfString;

            //Restore cursor and scrollbars location.
            SelectionStart = cursorPosition;
            SelectionLength = 0;
            SetScrollPos(scrollPosition);
            Win32.LockWindowUpdate((IntPtr)0);
            Invalidate();
            parsing = false;
        }

        #endregion

        #region Methods helping build RTF

        /// <summary>
        /// Adds a color to the RTF's color table and to the HashSet of Colors.
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="color">The color to add</param>
        /// <param name="counter">a counter, containing the amount of colors in the table</param>
        /// <param name="colors">an hashtable. the key is the color. the value is it's index in the table</param>
        private void AddColorToTable(StringBuilder sb, Color color, ref int counter, Dictionary<Color, int> colors)
        {
            sb.Append(@"\red").Append(color.R).Append(@"\green").Append(color.G).Append(@"\blue").Append(color.B).Append(";");
            colors.Add(color, counter);
            counter++;
        }

        /// <summary>
        /// Adds a font to the rtf's font table and to the fonts hashtable.
        /// </summary>
        /// <param name="rtfHeader">The RTF to add.</param>
        /// <param name="font">The font to add.</param>
        /// <param name="fonts">The Dictionary of Fonts whose key is font's name and value is its index.<param>
        private void AddFontToTable(StringBuilder rtfHeader, Font font, Dictionary<string, int> fonts)
        {
            int counter = fonts.Count;

            rtfHeader.Append("\n    ");
            rtfHeader.Append(@"{\f").Append(counter).Append(@"\fnil");
            rtfHeader.Append(@"\fcharset0 ").Append(font.Name).Append(";}");
            fonts.Add(font.Name, counter);
        }

        /// <summary>
        /// Kết thúc việc thêm các tags định dạng cho chữ.
        /// </summary>
        /// <param name="rtfBody"></param>
        private void EngTag(StringBuilder rtfBody)
        {
            rtfBody.Append(" ");
        }

        /// <summary>
        /// Thêm tag tương ứng với font size cần chữ.
        /// </summary>
        /// <param name="rtfBody"></param>
        /// <param name="size"></param>
        private void SetFontSize(StringBuilder rtfBody, float size)
        {
            rtfBody.Append(@"\fs").Append((int)(size * 2));
        }

        /// <summary>
        /// Thêm tag tương ứng với màu cần set cho chữ.
        /// </summary>
        /// <param name="rtfBody"></param>
        /// <param name="color"></param>
        /// <param name="colors"></param>
        private void SetColor(StringBuilder rtfBody, Color color, Dictionary<Color, int> colors)
        {
            rtfBody.Append(@"\cf").Append(colors[color]);
        }

        /// <summary>
        /// Add text to RTF body with unicode characters.
        /// </summary>
        /// <param name="rtfBody">The RTF body to add.</param>
        /// <param name="textToAdd">The text to add.</param>
        private void AddTextToRTFBody(StringBuilder rtfBody, string textToAdd)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in textToAdd)
            {
                if (c < 128) sb.Append(c);
                else
                {
                    //Add unicode character to rtfBody.
                    sb.Append(@"\u" + ((int)c).ToString() + "?");
                }
            }

            rtfBody.Append(sb.ToString());
        }

        //private string GetWordPosition(string text, int charIndex, 
        //    out int currentTokenStartIndex, out int currentTokenEndIndex)
        //{
        //    currentTokenStartIndex = text.LastIndexOfAny(separatorArray, )
        //}

        private void SetDescriptorSetting(StringBuilder rtfBody, HighlightDescriptor descriptor,
            Dictionary<Color, int> colors, Dictionary<string, int> fonts)
        {
            if (descriptor.color != null)
                SetColor(rtfBody, descriptor.color, colors);

            if (descriptor.font != null)
            {
                SetFont(rtfBody, descriptor.font, fonts);
                SetFontSize(rtfBody, descriptor.font.Size);
            }
        }

        private void AddNewLine(StringBuilder rtfBody)
        {
            rtfBody.Append(@"\par").Append("\n");
        }

        /// <summary>
        /// S
        /// </summary>
        /// <param name="rtfBody"></param>
        /// <param name="colors"></param>
        /// <param name="fonts"></param>
        private void SetDefaultSetting(StringBuilder rtfBody, Dictionary<Color, int> colors, Dictionary<string, int> fonts)
        {
            SetFont(rtfBody, Font, fonts);
            SetColor(rtfBody, ForeColor, colors);
            SetFontSize(rtfBody, (int)Font.Size);
            EngTag(rtfBody);
        }

        /// <summary>
        /// Add style tags corresponding to the font.
        /// </summary>
        /// <param name="rtfBody">The rtf body to add in.</param>
        /// <param name="fontToSet">The font to set.</param>
        /// <param name="fonts">The dictionary of fonts.</param>
        private void SetFont(StringBuilder rtfBody, Font fontToSet, Dictionary<string, int> fonts)
        {
            if (fontToSet == null) return;

            int fontIndex = fonts.Count;

            //If fontToSet doesn't exist in Dictionary of fonts
            if (!fonts.TryGetValue(fontToSet.Name, out fontIndex))
            {
                AddFontToTable(rtfHeader, fontToSet, fonts);
            }

            rtfBody.Append(@"\f").Append(fontIndex);
            //rtfBody.Append(fontStyles.GetFontStyle(fontToSet));
        }

        /// <summary>
        /// Add the color table to the rtf.
        /// </summary>
        /// <param name="rtfBody">The rtf to add.</param>
        /// <returns>A HashSet of colors contains in rtf's color table.</returns>
        private Dictionary<Color, int> AddColorTable(StringBuilder rtfBody)
        {
            //Color table
            rtfBody.Append(@"{\colortbl ;");
            Dictionary<Color, int> colors = new Dictionary<Color, int>();
            int colorCounter = 1;
            AddColorToTable(rtfBody, ForeColor, ref colorCounter, colors);
            AddColorToTable(rtfBody, BackColor, ref colorCounter, colors);
            foreach (var item in descriptors)
            {
                if (!colors.ContainsKey(item.color))
                {
                    AddColorToTable(rtfBody, item.color, ref colorCounter, colors);
                }
            }
            rtfBody.Append("}\n");
            return colors;
        }
        #endregion

        #region Scrollbar position methods

        /// <summary>
        /// Sends a win32 message to get the scrollbars' position.
        /// </summary>
        /// <returns>a POINT structure contains horizontal and vertical scrollbar position.</returns>
        private Win32.POINT GetScrollPos()
        {
            Win32.POINT scrollPos = new Win32.POINT();
            Win32.SendMessage(Handle, Win32.EM_GETSCROLLPOS, 0, ref scrollPos);
            return scrollPos;
        }

        /// <summary>
        /// Sends a win32 message to set scrollbars position.
        /// </summary>
        /// <param name="point">a POINT contains horizaoltal and vertical position to set to the scrollbar.</param>
        private void SetScrollPos(Win32.POINT point)
        {
            Win32.SendMessage(Handle, Win32.EM_SETSCROLLPOS, 0, ref point);
        }


        
        #endregion

        //public TypingText()
        //{
        //    InitializeComponent();
        //}
    }
}
