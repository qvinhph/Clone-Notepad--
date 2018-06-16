using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxHighlightingTextbox
{
    public partial class DocumentMap : RichTextBox
    {

        const int EM_LINESCROLL = 0x00B6;
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
