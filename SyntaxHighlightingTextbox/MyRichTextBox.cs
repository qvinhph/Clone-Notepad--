using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxHighlightingTextbox
{
    public partial class MyRichTextBox : UserControl
    {
        public MyRichTextBox()
        {
            
            InitializeComponent();

            //Default Font
            this.Font = new Font(FontFamily.GenericMonospace, 12);

            //Avoid Flickering
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Catch Some Event
            typingArea.VScroll += TypingArea_VScroll;
            typingArea.TextChanged += TypingArea_TextChanged;
            this.Resize += MyRichTextBox_Resize;
            typingArea.SelectionChanged += TypingArea_SelectionChanged;
            typingArea.FontChanged += TypingArea_FontChanged;
            typingArea.MouseDown += TypingArea_MouseDown;
            typingArea.SizeChanged += TypingArea_SizeChanged;

        }
        private void MyRichTextBox_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = TypingArea.Font;
            TypingArea.Select();
            AddLineNumbers();
        }

        private void MyRichTextBox_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        //Update number margin when text in typing area changed
        private void TypingArea_TextChanged(object sender, EventArgs e)
        {
            AddLineNumbers();
        }


        //Update number margin when change selection in typing area
        private void TypingArea_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = typingArea.GetPositionFromCharIndex(typingArea.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }
        private void TypingArea_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            //AddLineNumbers();
            AddLineNumbers();
            LineNumberTextBox.Refresh();
        }

        //Update font of number margin when font of typing area changed
        private void TypingArea_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = TypingArea.Font;
            TypingArea.Select();
            AddLineNumbers();
        }

        private void TypingArea_MouseDown(object sender, MouseEventArgs e)
        {
            typingArea.Select();
            LineNumberTextBox.DeselectAll();
        }
        


        private void TypingArea_SizeChanged(object sender, EventArgs e)
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

        private void lineNumberTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
