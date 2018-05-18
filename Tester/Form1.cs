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
            typingArea.Focus();
            this.typingArea.Location = new Point(0, 0);
            typingArea.Separators.Add(' ');
            typingArea.Separators.Add('\r');
            typingArea.Separators.Add('\n');
            typingArea.Separators.Add(',');
            typingArea.Separators.Add('.');
            typingArea.Separators.Add('-');
            typingArea.Separators.Add('+');
            //shtb.Seperators.Add('*');
            //shtb.Seperators.Add('/');
            Controls.Add(typingArea);
            typingArea.WordWrap = false;
            typingArea.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            typingArea.Descriptors.Add(new HighlightDescriptor("Hello", Color.Red, this.Font, DescriptorType.Word, DescriptorRecognition.WholeWord, false));
            typingArea.Descriptors.Add(new HighlightDescriptor("", Color.Green, null, DescriptorType.Word, DescriptorRecognition.IsNumber, false));
            typingArea.Descriptors.Add(new HighlightDescriptor("/*", "*/", DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, Color.Green, 
                                                                null, false));
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void typingArea_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
