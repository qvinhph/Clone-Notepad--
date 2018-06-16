using System;
using System.Collections.Generic;
///using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SyntaxHighlightingTextbox
{
    public partial class TypingArea : RichTextBox
    {
        #region Fields

        //Members exposed via properties.
        private SeparatorCollection separators;
        /// <summary>
        /// List of highlight descriptors describing how to highlight.
        /// </summary>
        private HighlightDescriptorCollection descriptors;
        private bool caseSensitive;
        private bool enableHighlight;

        //The control to focus on the highligting
        private Control controlToFocus = null;

        //Internal use members.
        private bool parsing; /*Help us prevent another action while parsing*/
        private char[] separatorArray;


        //Members used for Highlight() function.
        private StringBuilder rtfHeader;
        private FontDictionary fontStyles = new FontDictionary();


        //Members uses for Auto Complete Word.
        private ListBox autoCompleteListBox;
        private bool enableAutoComplete;
        public DocumentMap documentMap;


        //Undo/Redo members.
        private Stack<UndoRedoInfo> undoStack;
        private Stack<UndoRedoInfo> redoStack;
        private bool isUndoRedo; //Use to avoid recording undo info.
        private UndoRedoInfo lastInfo;


        public struct UndoRedoInfo
        {
            /// <summary>
            /// Create a new instance of UndoRedoInfo.
            /// </summary>
            /// <param name="text">The current text in textbox.</param>
            /// <param name="caretPos">the current</param>
            /// <param name="scrollPos"></param>
            public UndoRedoInfo(string text, int caretPos, Win32.POINT scrollPos)
            {
                this.caretPosition = caretPos;
                this.scrollPosition = scrollPos;
                this.text = text;
            }

            public readonly int caretPosition;
            public readonly Win32.POINT scrollPosition;
            public readonly string text;
        }


        #endregion


        #region Properties

        public DocumentMap DocumentMap
        {
            get
            {
                return documentMap;
            }
            set
            {
                documentMap = value;
            }
        }

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
        /// A collection of separators. A token is every string between two seperators.
        /// </summary>
        public SeparatorCollection Separators
        {
            get
            {
                if (separators == null)
                    separators = new SeparatorCollection();

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
                if (descriptors == null)
                    descriptors = new HighlightDescriptorCollection();

                return descriptors;
            }
        }


        public bool EnableHighlight
        {
            get
            {
                return enableHighlight;
            }

            set
            {
                enableHighlight = value;
            }
        }


        public bool EnableAutoComplete
        {
            get
            {
                return enableAutoComplete;
            }

            set
            {
                enableAutoComplete = value;
            }
        }



        #endregion


        #region Constructor

        public TypingArea()
        {
            InitializeComponent();

            //Some default setup
            caseSensitive = true;
            enableHighlight = true;
            parsing = false;
            isUndoRedo = false;
            EnableAutoComplete = true;

            this.Font = new Font(FontFamily.GenericMonospace, 12);

            rtfHeader = new StringBuilder();
            undoStack = new Stack<UndoRedoInfo>();
            redoStack = new Stack<UndoRedoInfo>();
            lastInfo = new UndoRedoInfo("", 0, new Win32.POINT());

            //Add ListBox to this TypingArea
            autoCompleteListBox = new ListBox();
            autoCompleteListBox.Visible = false;
            autoCompleteListBox.Font = new Font(FontFamily.GenericMonospace, 14);
            this.Controls.Add(autoCompleteListBox);

            //Inherited properties
            this.AcceptsTab = true;

            //Load separators
            var separators = new List<char>()
                        {
                            '?', ',', '.', ';', '(', ')', '[', ']', '{', '}', '+', '-',
                            '%', '^', '=', '~', '!', '|', ' ', '\r', '\n', '\t'
                        };
            AddListOfSeparators(separators);

        }
        

        #endregion


        #region Methods

        /// <summary>
        /// Clear the highlight descriptors of the current typing area.
        /// </summary>
        public new void Clear()
        {
            Descriptors.Clear();
        }


        public void ZoomIn()
        {
            //if (this.ZoomFactor > 10f) return;
            //this.ZoomFactor = this.ZoomFactor + 0.4f;
            this.Font = new Font(FontFamily.GenericMonospace,this.Font.Size + 1);
            
            this.Refresh();
            autoCompleteListBox.Visible = false;
        }
            

        public void ZoomOut()
        {
            //if (this.ZoomFactor < 1f) return;
            //this.ZoomFactor = this.ZoomFactor - 0.4f;
            this.Font = new Font(FontFamily.GenericMonospace, this.Font.Size - 1);

            this.Refresh();
            autoCompleteListBox.Visible = false;
        }


        /// <summary>
        /// Reload the typing area
        /// </summary>
        public new void Refresh()
        {
            BeginUpdate();

            //Save the current information
            string currentText = this.Text;
            int currentCaretPos = this.SelectionStart;
            Win32.POINT scrollPos = GetScrollPos();


            this.Rtf = "";

            //Return the information
            this.Text = currentText;
            this.SelectionStart = currentCaretPos;
            SetScrollPos(scrollPos);

            FinishUpdate();
        }

        #endregion


        #region Override Methods


        protected override void OnTextChanged(EventArgs e)
        {
            if (parsing) return;


            if (!isUndoRedo)
            {
                redoStack.Clear();
                undoStack.Push(lastInfo);
                lastInfo = new UndoRedoInfo(this.Text, this.SelectionStart, GetScrollPos());
            }


            var temp = this.ZoomFactor;
            if (EnableHighlight)
            {
                Highlight();
            }
            this.ZoomFactor = 1;
            this.ZoomFactor = temp;
            

            if (enableAutoComplete)
            {
                AutoShowListBox();
            }

            
            base.OnTextChanged(e);
        }
        

        /// <summary>
        /// Catch the keyboard input events for auto completing, undo, redo
        /// </summary>
        /// <param name="m"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            //Complete matched word at ListBox
            if (autoCompleteListBox.Visible)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        {
                            if (autoCompleteListBox.SelectedIndex != 0)
                                autoCompleteListBox.SelectedIndex -= 1;
                            return true;
                        }

                    case Keys.Down:
                        {
                            if (autoCompleteListBox.SelectedIndex != autoCompleteListBox.Items.Count - 1)
                                autoCompleteListBox.SelectedIndex += 1;
                            return true;
                        }

                    case Keys.Enter:
                    case Keys.Tab:
                        {
                            CompleteMatchedWord();

                            //Re-focus to the typing area to continue typing.
                            this.Focus();

                            autoCompleteListBox.Visible = false;

                            return true;
                        }

                    default:
                        break;
                }
            }

            
            //Process shortcut and some specified keys
            switch (keyData)
            {
                //Process Undo/Redo shortcut
                case (Keys.Control | Keys.Z):
                    {
                        Undo();
                        return true;
                    }

                case (Keys.Control | Keys.Y):
                    {
                        Redo();
                        return true;
                    }


                //Set tab stop
                case (Keys.Tab):
                    {
                        //string previousTabText = this.Text.Substring(0, SelectionStart);
                        //string afterTabText = this.Text.Substring(SelectionStart, this.Text.Length - SelectionStart);
                        //int caretPosition = this.SelectionStart + 4;

                        ////Insert 4 space-character to describe the tab size.
                        //this.Text = previousTabText + "    " + afterTabText;

                        ////Because when set the new this.Text, the caret will defaultly set to the 0 index
                        ////May cause the ListBox auto show, which we don't want
                        ////So we add this code to prevent it and set the caret to the right place
                        //if (autoCompleteListBox.Visible == true)
                        //    autoCompleteListBox.Visible = false;

                        //this.SelectionStart = caretPosition;
                        //return true;
                        this.SelectedText = "    ";
                        return true;
                    }


                //Use for brace matching
                case (Keys.OemOpenBrackets):
                    BraceMatching('[');
                    return true;

                case (Keys.OemOpenBrackets | Keys.Shift):
                    BraceMatching('{');
                    return true;

                case (Keys.OemQuotes | Keys.Shift):
                    BraceMatching('\"');
                    return true;

                case (Keys.Shift | Keys.D9):
                    BraceMatching('(');
                    return true;

                //Use for auto indenting
                case (Keys.Enter):
                    if (AutoIndenting())
                        return true;
                    else
                        break;                
                default:
                    break;
            }

            return base.ProcessCmdKey(ref m, keyData);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Hide ListBox on mouse down
            autoCompleteListBox.Visible = false;
            base.OnMouseDown(e);
        }


        protected override void OnLostFocus(EventArgs e)
        {
            //Hide ListBox if lose focus
            autoCompleteListBox.Visible = false;
            base.OnLostFocus(e);
        }


        protected override void OnVScroll(EventArgs e)
        {
            //Hide ListBox while scrolling
            if (parsing) return;
            autoCompleteListBox.Visible = false;
            base.OnVScroll(e);
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


        #region Undo/Redo code

        public new bool CanUndo
        {
            get
            {
                return undoStack.Count > 0;
            }
        }


        public new bool CanRedo
        {
            get
            {
                return redoStack.Count > 0;
            }
        }


        public new void Undo()
        {
            if (!CanUndo)
                return;

            isUndoRedo = true;
            redoStack.Push(new UndoRedoInfo(Text, SelectionStart, GetScrollPos()));
            UndoRedoInfo info = undoStack.Pop();

            this.Text = info.text;
            this.SelectionStart = info.caretPosition;
            SetScrollPos(info.scrollPosition);

            //In case the first index of this.Text is the character matching auto complete word table
            autoCompleteListBox.Visible = false;

            lastInfo = info;
            isUndoRedo = false;
        }


        public new void Redo()
        {
            if (!CanRedo)
                return;

            isUndoRedo = true;
            undoStack.Push(new UndoRedoInfo(Text, SelectionStart, GetScrollPos()));
            UndoRedoInfo info = redoStack.Pop();

            Text = info.text;
            SelectionStart = info.caretPosition;
            SetScrollPos(info.scrollPosition);
            //In case the first index of this.Text is the character matching auto complete word table
            autoCompleteListBox.Visible = false;

            lastInfo = info;
            isUndoRedo = false;
        }
        #endregion


        #region Highlight Syntax code


        public void Highlight()
        {
            parsing = true;
            rtfHeader.Length = 0;
            fontStyles.Clear();

            BeginUpdate();

            //Save caret and scrollbars position
            Win32.POINT scrollPosition = GetScrollPos();
            int caretPosition = SelectionStart;

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
            fontStyles.SaveFontStyle(Font);

            //Parsing text in the rtf body
            rtfBody.Append(@"\viewkind4\uc1\pard\ltrpar").Append("\n");
            //Set default color and font for the text
            SetDefaultSetting(rtfBody, colors, fonts);

            separatorArray = separators.ToArray();

            //Replace some specified symbols that has meaning in RTF text
            string[] lines = Text.Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}").Split('\n');

            //Scan every line of input text.
            for (int lineCounter = 0; lineCounter < lines.Length; lineCounter++)
            {
                if (lineCounter != 0)
                {
                    AddNewLine(rtfBody);
                }

                string line = lines[lineCounter];

                //Put every word of line to an array.
                string[] tokens = CaseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);

                if (tokens.Length == 0)
                {
                    AddTextToRTFBody(rtfBody, line);
                    AddNewLine(rtfBody);
                    continue;
                }

                int tokenCounter = 0;
                for (int i = 0; i < line.Length;)
                {
                    char currentChar = line[i];
                    if (Separators.Contains(currentChar))
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
                            string stringToCompare = CaseSensitive ? item.token : item.token.ToUpper();
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
                                //case DescriptorRecognition.Contains:
                                //    if (currentToken.Contains(stringToCompare))
                                //        match = true;
                                //    break;
                                case DescriptorRecognition.IsNumber:
                                    double number = 0;
                                    //To check if it is a real number.
                                    if (currentToken.Trim('/') != currentToken)
                                    {
                                        match = false;
                                        break;
                                    }
                                    string tokenRemovedSlash = currentToken.Replace("/", "");
                                    if (double.TryParse(currentToken, out number) || double.TryParse(tokenRemovedSlash, out number))
                                        match = true;
                                    break;
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
                            switch (item.highlightType)
                            {
                                //We will find which text to format.
                                case HighlightType.ToEOW:
                                    textToFormat = line.Substring(i, currentToken.Length);
                                    i += currentToken.Length;
                                    break;
                                case HighlightType.ToEOL:
                                    //textToFormat = line.Substring(i, line.Length);
                                    textToFormat = line.Remove(0, i);
                                    i = line.Length;
                                    break;
                                case HighlightType.ToCloseToken:
                                    {
                                        StringBuilder sbOfTextToFormat = new StringBuilder();

                                        //Find the close token
                                        int highlightStartFrom = i + item.token.Length;
                                        while ((line.IndexOf(item.closeToken, highlightStartFrom) < 0) && (lineCounter < lines.Length))
                                        {
                                            //Add text between the boundary of two token.
                                            sbOfTextToFormat.Append(line.Remove(0, i)); /*Use for the line containing open token.*/
                                            lineCounter++;

                                            if (lineCounter < lines.Length)
                                            {
                                                //Not the last line.
                                                AddNewLine(sbOfTextToFormat);
                                                line = lines[lineCounter];
                                                i = highlightStartFrom = 0;
                                            }
                                            else
                                                i = highlightStartFrom = line.Length;
                                        }


                                        bool hasCloseToken = (line.IndexOf(item.closeToken, highlightStartFrom) < 0) ? false : true;
                                        if (hasCloseToken)
                                        {
                                            int closeTokenIndex = line.IndexOf(item.closeToken, highlightStartFrom);
                                            sbOfTextToFormat.Append(line.Substring(i, closeTokenIndex + item.closeToken.Length - i));

                                            //Because we might skip some token since add it to text to format.
                                            //--> We need to generate the list of tokens in line again. 
                                            line = line.Remove(0, closeTokenIndex + item.closeToken.Length);
                                            tokenCounter = 0;
                                            tokens = CaseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);
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

            //Restore caret and scrollbars location.
            SelectionStart = caretPosition;
            SelectionLength = 0;
            SetScrollPos(scrollPosition);
            this.Focus();

            FinishUpdate();

            parsing = false;
        }      


        /// <summary>
        /// Disable the ability to repaint of RichTextBox to avoid flicker.
        /// </summary>
        public void BeginUpdate()
        {
            Win32.SendMessage(this.Handle, Win32.WM_SETREDRAW, 0, 0);
        }


        /// <summary>
        /// Enable the ability to repaint of RichTextBox.
        /// </summary>
        public void FinishUpdate()
        {
            Win32.SendMessage(this.Handle, Win32.WM_SETREDRAW, 1, 0);
            this.Invalidate();
        }


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


        /// <summary>
        /// Insert style tags to RTF according to the specified Descriptor.
        /// </summary>
        /// <param name="rtfBody"></param>
        /// <param name="descriptor"></param>
        /// <param name="colors"></param>
        /// <param name="fonts"></param>
        private void SetDescriptorSetting(StringBuilder rtfBody, HighlightDescriptor descriptor,
            Dictionary<Color, int> colors, Dictionary<string, int> fonts)
        {
            if (descriptor.color != null)
                SetColor(rtfBody, descriptor.color, colors);

            if (descriptor.font != null)
            {
                SetFont(rtfBody, descriptor.font, fonts);
                //SetFontSize(rtfBody, descriptor.font.Size);
                SetFontSize(rtfBody, this.Font.Size);
            }

            EndTags(rtfBody);
        }


        /// <summary>
        /// End a rtf tags to begin insert text.
        /// </summary>
        /// <param name="rtfBody"></param>
        private void EndTags(StringBuilder rtfBody)
        {
            rtfBody.Append(' ');
        }


        /// <summary>
        /// Add new line to the RTF.
        /// </summary>
        /// <param name="rtfBody"></param>
        private void AddNewLine(StringBuilder rtfBody)
        {
            rtfBody.Append(@"\par").Append("\n");
        }


        /// <summary>
        /// Insert the default style tags to RTF.
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
        /// Add style tags corresponding to the specified font.
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
            rtfBody.Append(fontStyles.GetFontStyle(fontToSet));
        }


        /// <summary>
        /// Add the color table to the rtf header.
        /// </summary>
        /// <param name="rtfHeader">The rtf to add.</param>
        /// <returns>A HashSet of colors contains in rtf's color table.</returns>
        private Dictionary<Color, int> AddColorTable(StringBuilder rtfHeader)
        {
            //Color table
            rtfHeader.Append(@"{\colortbl ;");
            Dictionary<Color, int> colors = new Dictionary<Color, int>();
            int colorCounter = 1;
            AddColorToTable(rtfHeader, ForeColor, ref colorCounter, colors);
            AddColorToTable(rtfHeader, BackColor, ref colorCounter, colors);
            foreach (var item in Descriptors)
            {
                if (!colors.ContainsKey(item.color))
                {
                    AddColorToTable(rtfHeader, item.color, ref colorCounter, colors);
                }
            }
            //Close the block of color table.
            rtfHeader.Append("}\n");
            return colors;
        }
        #endregion

        #endregion        


        #region Add Descriptors And Separators Functions
        /// <summary>
        /// Use to add a pair of token which will set a boundary highlight.
        /// </summary>
        /// <param name="descriptorRecognition">Enum of DescriptorRecognition</param>
        /// <param name="openToken">The open token.</param>
        /// <param name="highlightType">Enum of HighlightType</param>
        /// <param name="closeToken">The close token.</param>
        /// <param name="color">The color to highlight.</param>
        /// <param name="font">The font to highlight.</param>
        public void AddHighlightDescriptor(DescriptorRecognition descriptorRecognition, string openToken, HighlightType highlightType,
            string closeToken, Color color, Font font, UsedForAutoComplete used)
        {
            Descriptors.Add(new HighlightDescriptor(openToken, closeToken, highlightType, descriptorRecognition, color, font, used));
        }

        /// <summary>
        /// Use to add a single token expressing syntax highlighting.
        /// </summary>
        /// <param name="descriptorRecognition">Enum of DescriptorRecognition</param>
        /// <param name="token">The open token.</param>
        /// <param name="highlightType">Enum of HighlightType</param>s
        /// <param name="color">The color to highlight.</param>
        /// <param name="font">The font to highlight.</param>
        public void AddHighlightDescriptor(DescriptorRecognition descriptorRecognition, string token, HighlightType highlightType,
            Color color, Font font, UsedForAutoComplete used)
        {
            Descriptors.Add(new HighlightDescriptor(token, color, font, highlightType, descriptorRecognition, used));
        }


        /// <summary>
        /// Add a number of keyword that need to be highlight.
        /// </summary>
        /// <param name="listOfKeyword">The list of keywords.</param>
        /// <param name="color">The color setting to the keywords.</param>
        /// <param name="font">The font setting to the keywords.</param>
        public void AddHighlightKeywords(List<string> listOfKeyword, Color color, Font font)
        {
            foreach (string word in listOfKeyword)
            {
                AddHighlightDescriptor(DescriptorRecognition.WholeWord, word, HighlightType.ToEOW, color, font, UsedForAutoComplete.Yes);
            }
        }


        /// <summary>
        /// Add a list of open and close tokens that set boundary for highlighting.
        /// </summary>
        /// <param name="listOfPairToken">The list of tokens of which even index is open token, odd index is close token.</param>
        /// <param name="color">The color of text inside the boundary.</param>
        /// <param name="font">The font of text inside the boundary.</param>
        public void AddHighlightBoundaries(List<string> listOfPairToken, Color color, Font font)
        {
            if (listOfPairToken.Count % 2 != 0)
            {
                throw new ArgumentException("A pair of tokens is required.");
            }

            for (int i = 0; i < listOfPairToken.Count; i = i + 2)
            {
                AddHighlightDescriptor(DescriptorRecognition.StartsWith, listOfPairToken[i],
                    HighlightType.ToCloseToken, listOfPairToken[i + 1], color, font, UsedForAutoComplete.No);
            }
        }


        /// <summary>
        /// Add a list of character having the same way to highlight.
        /// </summary>
        /// <param name="listThings">A list of characters.</param>
        /// <param name="recognition"></param>
        /// <param name="highlightType"></param>
        /// <param name="color"></param>
        /// <param name="font"></param>
        /// <param name="used"></param>
        public void AddListOfHighlightDescriptors(List<string> listDescriptor, DescriptorRecognition recognition,
                                        HighlightType highlightType, Color color, Font font, UsedForAutoComplete used)
        {
            foreach (string item in listDescriptor)
            {
                AddHighlightDescriptor(recognition, item, highlightType, color, font, used);
            }
        }


        /// <summary>
        /// Add a list of separators.
        /// </summary>
        /// <param name="list"></param>
        public void AddListOfSeparators(List<char> listSeparator)
        {
            foreach (char item in listSeparator)
            {
                Separators.Add(item);
            }
        }

        #endregion


        #region Auto Complete Word

        /// <summary>
        /// Add keywords to ListBox.
        /// </summary>
        /// <param name="arrayOfKeyword">A array of keywords.</param>
        private void AddKeywordsToListBox(string[] arrayOfKeyword)
        {
            if (autoCompleteListBox == null) return;

            //Clear the revious ListBox
            autoCompleteListBox.Items.Clear();

            //Add keywords to ListBox
            autoCompleteListBox.Items.AddRange(arrayOfKeyword);
        }


        /// <summary>
        /// Load the keywords which contains the specified characters.
        /// </summary>
        /// <param name="characters">The char.</param>
        private void LoadKeywordsToListBox(string characters)
        {
            var listOfKeywords = Descriptors.Where(d => d.token.Contains(characters) && d.isUsedForAutoComplete == UsedForAutoComplete.Yes)
                                            .Select(d => d.token);

            AddKeywordsToListBox(listOfKeywords.ToArray());
        }


        /// <summary>
        /// Auto show ListBox containing keywords that may use for auto complete.
        /// </summary>
        private void AutoShowListBox()
        {
            if (!enableAutoComplete)
                return;

            string currentWord = GetCurrentWord();

            if (currentWord != "")
            {
                //if ListBox has't shown
                if (autoCompleteListBox.Visible == false)
                {
                    //Determind where to show ListBox
                    int wordStartLoc = GetWordStartIndex();
                    Point textPos = GetPositionFromCharIndex(wordStartLoc);

                    //Set the position
                    autoCompleteListBox.Location = new Point(textPos.X,
                                                            textPos.Y + (int)(this.FontHeight * this.ZoomFactor));

                    int bottomListBoxLoc = textPos.Y + (int)(this.FontHeight * this.ZoomFactor) + autoCompleteListBox.Height;

                    if (bottomListBoxLoc > this.Height)
                    {
                        //Not enough space to show ListBox below the text -> Show above the text
                        autoCompleteListBox.Location = new Point(textPos.X,
                                                            textPos.Y - autoCompleteListBox.Height);
                    }
                    else
                    {
                        //Enough space to show ListBox below the text
                        autoCompleteListBox.Location = new Point(textPos.X,
                                                            textPos.Y + (int)(this.FontHeight * this.ZoomFactor));
                    }

                    //Load list of keywords to ListBox
                    LoadKeywordsToListBox(currentWord);
                }

                //Set selected index
                autoCompleteListBox.SelectedIndex = autoCompleteListBox.FindString(currentWord);


                //Show ListBox
                if (autoCompleteListBox.SelectedIndex >= 0)
                {
                    autoCompleteListBox.Visible = true;
                }
                else
                {
                    //No keyword matchs.
                    //-> Hide ListBox.
                    autoCompleteListBox.Visible = false;
                }
            }
            else
            {
                autoCompleteListBox.Visible = false;
            }
        }


        private void CompleteMatchedWord()
        {
            if (autoCompleteListBox.Visible)
            {
                this.SelectionStart = GetWordStartIndex();
                this.SelectionLength = GetCurrentWord().Length;
                this.SelectedText = autoCompleteListBox.SelectedItem.ToString();
            }
        }
        


        #region Get Word Position

        /// <summary>
        /// Get the word being with the caret.
        /// </summary>
        /// <returns></returns>
        private string GetCurrentWord()
        {
            int wordStartLoc = GetWordStartIndex();
            int wordEndLoc = GetWordEndIndex();
            
            return this.Text.Substring(wordStartLoc, wordEndLoc - wordStartLoc + 1);
        }


        /// <summary>
        /// Get the start index of the word being with the caret.
        /// </summary>
        /// <returns></returns>
        private int GetWordStartIndex()
        {
            int charIndex = SelectionStart;
            while (charIndex > 0)
            {
                if (!Separators.Contains(Text[charIndex - 1]))
                    charIndex--;
                else
                    break;
            }

            return charIndex;
        }


        /// <summary>
        /// Get the end index of the word being with the caret.
        /// </summary>
        /// <returns></returns>
        private int GetWordEndIndex()
        {
            int charIndex = SelectionStart;
            while (charIndex < Text.Length)
            {
                if (!Separators.Contains(Text[charIndex]))
                {
                    charIndex++;
                }
                else
                    break;
            }

            charIndex--;
            return charIndex;
        }

        #endregion

        #endregion


        #region Find and replace code


        /// Find all the positions of a string 
        /// <param name="textToSearch"></param>
        public List<int> FindAll(string textToSearch)
        {
            if (controlToFocus != null)
            {
                controlToFocus.Focus();
            }
            //List to hold all the position
            List<int> listOfPositions = new List<int>();
            int position = -1;
            int searchStart = 0;
            int searchEnd = this.TextLength;

            while (searchStart < this.Text.Length)
            {
                // Find the position of search string in SyntaxHighlightingTextbox
                position = this.Find(textToSearch, searchStart, searchEnd, RichTextBoxFinds.None);

                // Determine whether the text was found in the textbox
                if (position != -1)
                {
                    listOfPositions.Add(position);
                    searchStart = position + textToSearch.Length;
                }
                else
                {
                    break;
                }
            }

            this.Focus();

            return listOfPositions;
        }


        /// <summary>       
        /// Clear background color of selected text
        /// </summary>
        public void ClearBackColor(Color clearColor)
        { 
            //Select the text, clear background color and unselect that text
            this.Select(0, this.TextLength);
            this.SelectionBackColor = clearColor;
            this.SelectionLength = 0;
        }


        /// <summary>
        /// Find all and color background
        /// </summary>
        /// <param name="textToSearch"></param>
        /// <param name="backColor"></param>
        public List<int> FindAndColorAll(string textToSearch, Color backColor)
        {
            if (controlToFocus != null)
            {
                controlToFocus.Focus();
            }

            //List of position of the found texts
            List<int> listOfPositions = new List<int>();

            int position = -1;
            int searchStart = 0;
            int searchEnd = this.TextLength;

            while (searchStart < this.Text.Length)
            {
                // Find the position of search text in SyntaxHighlightingTextbox
                position = this.Find(textToSearch, searchStart, searchEnd, RichTextBoxFinds.None);

                // Determine whether the text was found in the textbox
                if (position != -1)
                {
                    //Add this pos to the list, remember this pos and the text
                    listOfPositions.Add(position);
                    this.Select(position, textToSearch.Length);
                    this.SelectionBackColor = backColor;
                    searchStart = position + textToSearch.Length;
                }
                else
                {
                    break;
                }
            }

            this.Focus();

            return listOfPositions;
        }


        #endregion


        #region Brace matching and Auto indenting code


        /// <summary>
        /// Auto add close braces
        /// This will be use in ProcessWnd as a helper
        /// </summary>
        /// <param name="input"></param>
        private void BraceMatching(char input)
        {
            List<char> braceList = new List<char> { '{', '(', '[', '\"' };

            if (braceList.Contains(input))
            {
                switch (input)
                {
                    case '{':
                        {
                            this.SelectedText = "{ }";
                            break;
                        }
                    case '(':
                        {
                            this.SelectedText = "()";
                            break;
                        }
                    case '[':
                        {
                            this.SelectedText = "[]";
                            break;
                        }
                    case '"':
                        {
                            this.SelectedText = "\"\"";
                            break;
                        }
                    default:
                        break;
                }
                
                this.SelectionStart -= 1;
            }
        }


        /// <summary>
        /// Use for auto indent when press Enter.
        /// This will be used in ProcessWndKey as a helper.
        /// </summary>
        /// <returns>Whether the auto indent has done or not</returns>
        private bool AutoIndenting()
        {
            //Because I will use this method in ProcessWndKey, it will process before KeyDown event
            //So the caret will remain the same position
            int currentLineNumber = this.GetLineFromCharIndex(this.SelectionStart);


            if (currentLineNumber >= this.Lines.Count())
                return false;


            string currentLine = this.Lines[currentLineNumber];
            int amountOfIndent = 0;


            //Calculate the amount of indenting ( by space character)
            foreach(char c in currentLine)
            {
                if (c == ' ')
                {
                    amountOfIndent++;
                }
                else
                    break;
            }


            //We will only indent by Tab Size ( 1 Tab = 4 space character)
            //So we need to calculate the number of Tab 
            int numberOfTab = amountOfIndent / 4;
            string indent = "";
            

            //Get the amount of indent according to Tab size
            for (int i = 0; i < numberOfTab; i++)
            {
                indent += "    ";
            }


            //After the caret is the '}' 
            if (this.SelectionStart < this.Text.Length && this.Text[this.SelectionStart] == '}')
            {
                this.SelectedText = "\n" + indent + "    ";
                int caretPos = this.SelectionStart;
                this.SelectedText = "\n" + indent;

                //Convenient caret position for inputing
                this.SelectionStart = caretPos;
                return true;
            }
            
            
            if (numberOfTab > 0)
            {
                this.SelectedText = "\n" + indent;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }

}
