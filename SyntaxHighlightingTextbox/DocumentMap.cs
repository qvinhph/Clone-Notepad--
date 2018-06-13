using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxHighlightingTextbox
{
    public partial class DocumentMap : RichTextBox
    {

        public DocumentMap()
        {
            InitializeComponent();

        }
        private TypingArea typingArea;
        public TypingArea TypingArea
        {
            get
            {
                return typingArea;
            }
            set
            {
                typingArea = value;
            }
        }
        
        
    }
}
