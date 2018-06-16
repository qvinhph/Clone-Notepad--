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
        protected override void WndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case 0x0201://WM_LBUTTONDOWN

                    {

                        return;

                    }

                case 0x0202://WM_LBUTTONUP

                    {

                        return;

                    }

                case 0x0203://WM_LBUTTONDBLCLK

                    {

                        return;

                    }

                case 0x0204://WM_RBUTTONDOWN

                    {

                        return;

                    }

                case 0x0205://WM_RBUTTONUP

                    {

                        return;

                    }

                case 0x0206://WM_RBUTTONDBLCLK

                    {

                        return;

                    }

            }



            base.WndProc(ref m);

        }


    }
}
