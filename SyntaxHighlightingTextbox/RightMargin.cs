using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SyntaxHighlightingTextbox
{
    /// <summary>
    /// Show the line number of textbox.
    /// </summary>
    public partial class RightMargin : Component
    {
        #region Constructors

        public RightMargin()
        {
            InitializeComponent();
        }

        public RightMargin(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Fields



        #endregion



    }
}
