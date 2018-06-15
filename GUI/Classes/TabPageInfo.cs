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
    class TabPageInfo
    {
        private TabPage tabPage;
        private string language;
        private bool canUndo;
        private bool canRedo;

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

        public bool CanUndo
        {
            get
            {
                return canUndo;
            }

            set
            {
                canUndo = value;
            }
        }

        public bool CanRedo
        {
            get
            {
                return canRedo;
            }

            set
            {
                canRedo = value;
            }
        }

    }
}
