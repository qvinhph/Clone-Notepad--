using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTextbox
{
    public partial class MyTextBox : Component
    {
        public MyTextBox()
        {
            InitializeComponent();
        }

        public MyTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
