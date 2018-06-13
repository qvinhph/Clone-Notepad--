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
    public partial class LineNumbering : RichTextBox
    {
        #region Constructor
        public LineNumbering()
        {
            InitializeComponent();

        }

        public LineNumbering(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region Fields


        #endregion
    }
}
