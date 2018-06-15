using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// A class to store tab info
    /// </summary>
    public class TabPageInfo
    {
        private TabPage tabPage;
        private string language;

        public TabPage TabPage
        {
            get
            {
                return tabPage;
            }

            set
            {
                tabPage = value;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }

            set
            {
                language = value;
            }
        }
        
    }
}
