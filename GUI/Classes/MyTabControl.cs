using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyntaxHighlightingTextbox;

namespace GUI
{
    class MyTabControl
    {
        private static TabControl _tabControl;

        #region Properties

        public static TypingArea CurrentTextArea
        {
            get
            {
                if (tabControl.SelectedTab != null)

                    return (tabControl.SelectedTab.Controls[0] as TypingArea);
                return null;
            }
        }

        public static TabControl TabControl
        {
            get
            {
                return tabControl;
            }

            set
            {
                tabControl = value;
            }
        }

        #endregion

        public static void SetupTabControl(TabControl tabControl)
        {
            //tabControl = _tabControl;

            tabControl.AllowDrop = true;
            
            //Allow you to custom the way tab control are drawn through DrawItem event raising
            //whenever redraw
            tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
        }
    }
}
