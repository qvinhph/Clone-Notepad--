using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyntaxHighlightingTextbox;

namespace Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typingArea1.Focus();
            this.typingArea1.Location = new Point(0, 0);
            typingArea1.Separators.Add(' ');
            typingArea1.Separators.Add('\r');
            typingArea1.Separators.Add('\n');
            typingArea1.Separators.Add(',');
            typingArea1.Separators.Add('.');
            typingArea1.Separators.Add('-');
            typingArea1.Separators.Add('+');
            //shtb.Seperators.Add('*');
            //shtb.Seperators.Add('/');
            Controls.Add(typingArea1);
            typingArea1.WordWrap = false;
            typingArea1.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            typingArea1.Descriptors.Add(new HighlightDescriptor("Hello", Color.Red, this.Font, HighlightType.ToEOW, DescriptorRecognition.WholeWord, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("", Color.Green, null, HighlightType.ToEOW, DescriptorRecognition.IsNumber, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("/*", "*/", HighlightType.ToCloseToken, DescriptorRecognition.StartsWith, Color.Green, 
                                                                null, false));
        }
    }
}
