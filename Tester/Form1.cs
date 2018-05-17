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
            typingText.Location = new Point(0, 0);
            typingText.Separators.Add(' ');
            typingText.Separators.Add('\r');
            typingText.Separators.Add('\n');
            typingText.Separators.Add(',');
            typingText.Separators.Add('.');
            typingText.Separators.Add('-');
            typingText.Separators.Add('+');
            //shtb.Seperators.Add('*');
            //shtb.Seperators.Add('/');
            Controls.Add(typingText);
            typingText.WordWrap = false;
            typingText.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            typingText.Descriptors.Add(new HighlightDescriptor("Hello", Color.Red, this.Font, DescriptorType.Word, DescriptorRecognition.WholeWord, false));

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
