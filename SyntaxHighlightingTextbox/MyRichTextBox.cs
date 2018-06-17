using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SyntaxHighlightingTextbox
{
    public partial class MyRichTextBox : UserControl
    {

        #region Fields
        private LineNumbering lineNumberTextBox;
        private TypingArea typingArea;
        private DocumentMap documentMap;
        #endregion


        #region Properties
        public LineNumbering LineNumberTextBox
        {
            get { return lineNumberTextBox; }
            set { lineNumberTextBox = value; }
        }
        public TypingArea TypingArea
        {
            get { return typingArea; }
            set { typingArea = value; }
        }
        public DocumentMap DocumentMap
        {
            get { return documentMap; }
            set { documentMap = value; }
        }

        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }

        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        #endregion


        #region Constructor
        public MyRichTextBox()
        {
            InitializeComponent();

            //Default Font
            this.Font = new Font(FontFamily.GenericMonospace, 12);

            //Avoid Flickering
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Handle some comment event
            HandleEvent();
        }
        #endregion


        #region Event
        private void HandleEvent()
        {
            typingArea.VScroll += OnTypingArea_VScroll;
            typingArea.TextChanged += OnTypingArea_TextChanged;
            this.Resize += OnMyRichTextBox_Resize;
            typingArea.SelectionChanged += OnTypingArea_SelectionChanged;
            typingArea.FontChanged += OnTypingArea_FontChanged;
            typingArea.MouseDown += OnTypingArea_MouseDown;
            typingArea.SizeChanged += OnTypingArea_SizeChanged;
            //documentMap.SelectionChanged += OnDocumentMap_SelectionChanged;
        }
        #endregion


        #region Methods for Line Numbering

        #region MyRichTextBox Event
        private void MyRichTextBox_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = TypingArea.Font;
            TypingArea.Select();
            AddLineNumbers();
            DocumentMap.Text = TypingArea.Text;
        }

        private void OnMyRichTextBox_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }
        #endregion


        #region Typing Area Event
        //Update number margin when text in typing area changed

        private void OnTypingArea_TextChanged(object sender, EventArgs e)
        {
            AddLineNumbers();
            DocumentMap.Text = TypingArea.Text;
            DocumentMap.Rtf = TypingArea.Rtf;
            DocumentMap.Font = new Font(FontFamily.GenericMonospace, 5);
        }


        //Update number margin when change selection in typing area
        private void OnTypingArea_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = typingArea.GetPositionFromCharIndex(typingArea.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }
        private void OnTypingArea_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            //AddLineNumbers();
            AddLineNumbers();
            LineNumberTextBox.Refresh();
            DocumentMap.Refresh();

            //Method to synchronize scroll between Typing Area and Document Map
            //Reference: https://stackoverflow.com/questions/1827323/c-synchronize-scroll-position-of-two-richtextboxes
            int nPos = GetScrollPos(TypingArea.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(DocumentMap.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        //Update font of number margin when font of typing area changed
        private void OnTypingArea_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = TypingArea.Font;
            TypingArea.Select();
            AddLineNumbers();
        }

        private void OnTypingArea_MouseDown(object sender, MouseEventArgs e)
        {
            typingArea.Select();
            LineNumberTextBox.DeselectAll();
        }
        

        //Update the number margin when zoom in or zoom out
        private void OnTypingArea_SizeChanged(object sender, EventArgs e)
        {
            //Font fnt = new Font(FontFamily.GenericMonospace, typingArea.Font.Size);
            LineNumberTextBox.Font = typingArea.Font;
            AddLineNumbers();
            LineNumberTextBox.Refresh();
            LineNumberTextBox.Invalidate();
            //LineNumberTextBox.ZoomFactor = typingArea.ZoomFactor;
            //Font fnt = new Font(FontFamily.GenericMonospace, typingArea.Font.Size * LineNumberTextBox.ZoomFactor);
            //LineNumberTextBox.Font=fnt;
        }
        //private void OnDocumentMap_SelectionChanged(object sender, EventArgs e)
        //{
        //    DocumentMap.SelectionStart = DocumentMap.TextLength;
        //}
        #endregion

        #region Main methods to display Line Numbering TextBox
        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = typingArea.Lines.Length;

            if (line <= 10)
            {
                w = 20 + (int)typingArea.Font.Size;
            }
            else if (line <= 50)
            {
                w = 30 + (int)typingArea.Font.Size;
            }
            else
            {
                w = 50 + (int)typingArea.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = typingArea.GetCharIndexFromPosition(pt);
            int First_Line = typingArea.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = typingArea.GetCharIndexFromPosition(pt);
            int Last_Line = typingArea.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            lineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            lineNumberTextBox.Text = "";
            lineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }
        #endregion

        //Automatically added by VS
        private void lineNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
