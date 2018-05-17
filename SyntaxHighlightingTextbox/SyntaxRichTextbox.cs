using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;

namespace SyntaxHighlightingTextbox
{
    public partial class SyntaxRichTextbox : RichTextBox
    {
        #region Fields
        //Members exposed via properties
        private SeparatorCollection separators = new SeparatorCollection();
        private HighlightDescriptorCollection highlightDescriptors = new HighlightDescriptorCollection();
        private bool caseSensitive = false;
        private bool filterAutoComplete = false;

        //Members not exposed 
        private bool autoCompleteShown = false;
        private bool parsing = false;
        private bool ignoreLostFocus = false;

        //private AutoCompleteForm autoCompleteForm = new AutoCompleteForm();
        //Do later

        //Undo/Redo members
        //private List<UndoRedoInfo> undoList = new List<UndoRedoInfo>();
        //private Stack<UndoRedoInfo> redoStack = new Stack<UndoRedoInfo>();
        private bool isUndo = false;
        //private UndoRedoInfo lastInfo = new UndoRedoInfo("", new Win32.POINT(), 0);
        //private int maxUndoRedoSteps = 50;



        #endregion

        #region Properties
        /// <summary>
        /// Determine if token recognition is case sensitive
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
        /// Show the AutoComplete windows or not
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
        /// A list of separators.
        /// </summary>
        public SeparatorCollection Separators
        {
            get
            {
                return separators;
            }
        }

        /// <summary>
        /// A list of highlight descriptors.
        /// </summary>
        public HighlightDescriptorCollection HighlightDescriptors
        {
            get
            {
                return highlightDescriptors;
            }
        }

        //Properties MaxUndoRedoSteps

        #endregion

        #region Override Methods
        protected override void OnTextChanged(EventArgs e)
        {
            //Ngăn cho nhiều hàm OnTextChanged thực hiện cùng lúc
            if (parsing) return;
            parsing = true;

            //Ngăn việc update RichTextBox
            Win32.LockWindowUpdate(Handle);
            base.OnTextChanged(e);

            //Undo things




            //Create with an estimate of how big the stringbuilder has to be ..
            var sb = new StringBuilder((int)(Text.Length * 1.5 + 150));

            //Adding RTF header
            sb.Append(@"{\rtf1\ansi\deff0 {\fonttbl");

            //Creating font table
            int fontCounter = 0;
            var fonts = new Dictionary<string, int>();
            AddFontToTable(sb, Font, ref fontCounter, fonts);
            foreach (var item in highlightDescriptors)
            {
                if ((item.font != null) && !fonts.ContainsKey(item.font.Name))
                {
                    AddFontToTable(sb, Font, ref fontCounter, fonts);
                }
            }
            sb.Append(@"}\n");

            //Creating color table
            sb.Append(@"{\colortbl ;");
            int colorCounter = 1;
            var colors = new Dictionary<Color, int>();
            AddColorToTable(sb, ForeColor, ref colorCounter, colors);
            AddColorToTable(sb, BackColor, ref colorCounter, colors);

            foreach (var item in highlightDescriptors)
            {
                if (!colors.ContainsKey(item.color))
                {
                    AddColorToTable(sb, item.color, ref colorCounter, colors);
                }
            }
            sb.Append("}\n");

            //Parsing text
            sb.Append(@"\viewkind4\uc1");
            SetDefaultSettings(sb, colors, fonts);

            //Convert List of separator to separator array
            var separatorArray = separators.ToArray();

            //Replacing "\" to "\\" for RTF...
            string[] lines = Text.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}").Split('\n');

            for (int lineCounter = 0; lineCounter < lines.Length; lineCounter++)
            {
                if (lineCounter != 0)
                {
                    AddNewLine(sb);
                }

                string line = lines[lineCounter];

                string[] tokens = caseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);

                int tokenCounter = 0;
                for (int i = 0; i < line.Length;)
                {
                    char currentChar = line[i];
                    if (separators.Contains(currentChar))
                    {
                        sb.Append(currentChar);
                        i++;
                    }
                    else
                    {
                        string currentToken = tokens[tokenCounter];
                        bool addToken = true;
                        tokenCounter++;
                        foreach (var item in highlightDescriptors)
                        {
                            string keyWord = caseSensitive ? item.token : item.token.ToUpper();
                            bool match = false;

                            //Check if the current token matches the highlight descriptor according to the DescriptorRecognition property
                            switch (item.descriptorRecognition)
                            {
                                case DescriptorRecognition.WholeWord:
                                    if (currentToken == keyWord)
                                    {
                                        match = true;
                                    }
                                    break;
                                case DescriptorRecognition.StartsWith:
                                    if (currentToken.StartsWith(keyWord))
                                    {
                                        match = true;
                                    }
                                    break;
                                case DescriptorRecognition.Contains:
                                    if (currentToken.Contains(keyWord))
                                    {
                                        match = true;
                                    }
                                    break;
                            }

                            if (!match)
                            {
                                //If this token doesn't match check the next one
                                continue;
                            }

                            //Printing the matched token with the inner code - dont use the default setting
                            addToken = false;

                            //Set colors and fonts to the current highlight descriptor.
                            SetDescriptorSettings(sb, item, colors, fonts);

                            //Print the text affected by the descriptor
                            switch (item.descriptorType)
                            {
                                case DescriptorType.Word:
                                    sb.Append(line.Substring(i, currentToken.Length));
                                    SetDefaultSettings(sb, colors, fonts);
                                    i += currentToken.Length;
                                    break;
                                case DescriptorType.ToEOL:
                                    sb.Append(line.Remove(0, i));
                                    i = line.Length;
                                    SetDefaultSettings(sb, colors, fonts);
                                    break;
                                case DescriptorType.ToCloseToken:
                                    while ((!line.Contains(item.closeToken)) && (lineCounter < lines.Length))
                                    {
                                        sb.Append(line.Remove(0, i));
                                        lineCounter++;
                                        if (lineCounter < lines.Length)
                                        {
                                            AddNewLine(sb);
                                            line = lines[lineCounter];
                                            i = 0;
                                        }
                                        else
                                        {
                                            i = line.Length;
                                        }
                                    }

                                    if (line.Contains(item.closeToken))
                                    {
                                        sb.Append(line.Substring(i,
                                            line.IndexOf(item.closeToken, i) + item.closeToken.Length - i));
                                        line = line.Remove(0, line.IndexOf(item.closeToken) + item.closeToken.Length);

                                        //Vì một sô token có thể đã bị bỏ qua khi tìm closeToken.
                                        //Nên ta tạo lại tokens và set tokenCounter = 0
                                        tokenCounter = 0;
                                        tokens = caseSensitive ? line.Split(separatorArray) : line.ToUpper().Split(separatorArray);
                                        SetDefaultSettings(sb, colors, fonts);
                                        i = 0;
                                    }
                                    break;
                            }
                        }

                        if (addToken)
                        {
                            sb.Append(line.Substring(i, currentToken.Length));
                            i += currentToken.Length;
                        }
                    }
                }
            }

            //Mọi thứ đều được thể hiện ở trong RTF trước khi hiển thị kèm theo các tags của RTF để định dạng màu
            Rtf = sb.ToString();

            //Cho phép update richtextbox
            Win32.LockWindowUpdate(IntPtr.Zero);
            Invalidate();

            parsing = false;

        }
        #endregion

        #region Undo/Redo Code
        //new modifier: hide the member that is inherited from the base class
        //The base class has "CanUndo" method.
        //new public bool CanUndo
        //{
        //    get
        //    {
        //        return undoList.Count > 0;
        //    }
        //}

        //The base class has "CanRedo" method.
        //new public bool CanRedo
        //{
        //    get
        //    {
        //        return redoStack.Count > 0;
        //    }
        //}

        //public new void Undo()
        //{
        //    if (!CanUndo)
        //        return;
        //    isUndo = true;
        //    //redoStack.Push(new UndoRedoInfo(Text, GetScrollPos(), SelectionStart));
        //    UndoRedoInfo info = (UndoRedoInfo)undoList[0];
        //    undoList.RemoveAt(0);
        //    Text = info.Text;
        //    SelectionStart = info.CursorLocation;
        //    //SetScrollPos(info.ScrollPos);
        //    lastInfo = info;
        //    isUndo = false;
        //}
        #endregion

        #region Methods for building RTF
        /// <summary>
        /// Add a font to the RTF's font table and to the font HashSet
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="font">The font to add</param>
        /// <param name="counter">The number of fonts in the table</param>
        /// <param name="fonts">The collection of font with the key is name of font and the value is a number </param>
        private void AddFontToTable(StringBuilder sb, Font font, ref int counter, Dictionary<string, int> fonts)
        {
            sb.Append(@"{\f").Append(counter).Append(@" ").Append(font.Name).Append(";}");
            fonts.Add(font.Name, counter);
            counter++;
        }

        /// <summary>
        /// Add a color to the RTF's color table and to the color HashSet
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="color">The color to add</param>
        /// <param name="counter">The number of colors in the table</param>
        /// <param name="colors">The collection of color with the key is name of color and the value is a number</param>
        private void AddColorToTable(StringBuilder sb, Color color, ref int counter, Dictionary<Color, int> colors)
        {
            sb.Append(@"\red").Append(color.R).Append(@"\green").Append(color.G).Append(@"\blue")
                .Append(color.B).Append(";");
            colors.Add(color, counter);
            counter++;
        }

        /// <summary>
        /// Set color and font to the default control setting
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="colors">The collection of color with the key is name of color and the value is a number</param>
        /// <param name="fonts">The collection of color with the key is name of font and the value is a number</param>
        private void SetDefaultSettings(StringBuilder sb, Dictionary<Color, int> colors, Dictionary<String, int> fonts)
        {
            SetColor(sb, ForeColor, colors);
            SetFont(sb, Font, fonts);
            SetFontSize(sb, (int)Font.Size);
            EndTags(sb);
        }

        /// <summary>
        /// Ends a RTF tags section
        /// </summary>
        private void EndTags(StringBuilder sb)
        {
            sb.Append(" ");
        }

        /// <summary>
        /// Set the font size to the specified font size
        /// </summary>
        private void SetFontSize(StringBuilder sb, int size)
        {
            sb.Append(@"\fs").Append(size * 2);
        }

        /// <summary>
        /// Set the color to the specified color
        /// </summary>
        private void SetColor(StringBuilder sb, Color color, Dictionary<Color, int> colors)
        {
            sb.Append(@"\cf").Append(colors[color]);
        }

        /// <summary>
        /// Set the font to the specified font
        /// </summary>
        private void SetFont(StringBuilder sb, Font font, Dictionary<string, int> fonts)
        {
            if (font == null) return;
            sb.Append(@"\f").Append(fonts[font.Name]);
        }

        /// <summary>
        /// Add a new line mark to the RTF
        /// </summary>
        private void AddNewLine(StringBuilder sb)
        {
            sb.Append("\\par\n");
        }

        /// <summary>
        /// Set color and font to the token with the specified highlight descriptor.
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="highlightDescriptor">The highlight descriptor to use</param>
        /// <param name="colors">The collection of color</param>
        /// <param name="fonts">The collection of font</param>
        private void SetDescriptorSettings(StringBuilder sb, HighlightDescriptor highlightDescriptor,
            Dictionary<Color, int> colors, Dictionary<string, int> fonts)
        {
            SetColor(sb, highlightDescriptor.color, colors);
            if (highlightDescriptor.font != null)
            {
                SetFont(sb, highlightDescriptor.font, fonts);
                SetFontSize(sb, (int)highlightDescriptor.font.Size);
            }
            EndTags(sb);
        }
        #endregion

        #region Scrollbar position methods
        //private unsafe Win32.POINT GetScrollPos()
        //{
        //    Win32.POINT res = new Win32.POINT();
        //    IntPtr ptr = new IntPtr(&res);
        //    Win32.SendMessage(Handle, Win32.EM_GETSCROLLPOS, 0, ptr);
        //    return res;
        //}
        #endregion

        //#region unknown things
        ///// <summary>
        ///// Required designer variable.
        ///// </summary>
        //private System.ComponentModel.IContainer components = null;

        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //#region Component Designer generated code

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //    components = new System.ComponentModel.Container();
        //}

        //#endregion
        //#endregion
    }
}