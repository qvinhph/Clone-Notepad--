using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Classes;
using SyntaxHighlightingTextbox;

namespace GUI
{
    public partial class _MainFrm : Form
    {
        private string language = "C#";

        public string Language
        {
            get { return language; }
            set { language = value; }
        }


        public _MainFrm()
        {
            InitializeComponent();
        }

        private FindingForm findingForm = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            MyTabControl.SetupTabControl(tabControl);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            cToolStripMenuItem.Checked = true;
            Language = "C#";
            MyTabControl.CurrentTextArea.EnableHighlight = true;
            SetHighlightRule(Language);
        }

        private void normalTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "Normal Text";
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            normalTextToolStripMenuItem.Checked = true;
            SetHighlightRule(Language);
        }

        private void SetHighlightRule(string language)
        {
            var currentTypingArea = MyTabControl.CurrentTextArea;
            Font fontToSet; //Use for the specified keyword highlighting.
            var typingFont = currentTypingArea.Font;
            MyTabControl.CurrentTextArea.EnableHighlight = true;
            MyTabControl.CurrentTextArea.Clear();
            switch (language)
            {
                case "C#":
                    {
                        //Number highlight
                        currentTypingArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, null);


                        //Keyword highlight
                        var keywords = new List<string>()
                        {
                            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
                            "class", "const", "continue" , "decimal", "default", "delegate", "do", "double", "else",
                            "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach",
                            "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace",
                            "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
                            "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string",
                            "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTypingArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


                        //Keyword highlight another color
                        var keywords2 = new List<string>()
                        {
                            "define", "error", "import", "undef", "elif", "if", "include", "using", "else",
                            "ifdef", "line", "endif", "ifndef", "pragma"
                        };
                        currentTypingArea.AddHighlightKeywords(keywords2, Color.SlateGray, typingFont);


                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//", "///", "////"
                        };
                        currentTypingArea.AddListOfHighlightDescriptors(commentSymbols, DescriptorRecognition.StartsWith,
                                                HighlightType.ToEOL, Color.Green, null);
                        currentTypingArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "/*",
                                                            HighlightType.ToCloseToken, "*/", Color.Green, typingFont);


                        //Highlight text between begin and end token
                        var listPair = new List<string>()
                        {
                            "\"", "\"", "\'", "\'",
                        };
                        currentTypingArea.AddHighlightBoundaries(listPair, Color.Red, typingFont);


                        //Highlight string start with '#'
                        currentTypingArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "#", HighlightType.ToEOW,
                                                        Color.SlateGray, typingFont);
                        
                    }
                    break;
                case "Normal Text":
                    {
                        MyTabControl.CurrentTextArea.EnableHighlight = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Language = "C";
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            int newTabIndex = 1;
            string newTabName = "new 1";
            for (int i = 0; i <tabControl.TabPages.Count; i++)
            {
                string tabName = tabControl.TabPages[i].Text;
                
                if (tabName == newTabName)
                {
                    newTabIndex++;
                    newTabName = "new " + newTabIndex.ToString();

                    //Start over the loop
                    i = -1;
                }
            }

            MyTabControl.CreateNewTabPage(newTabName);
            MyTabControl.CurrentTextArea.Focus();
        }

        private void btUndo_Click(object sender, EventArgs e)
        {
            MyTabControl.CurrentTextArea.Undo();
        }

        private void btRedo_Click(object sender, EventArgs e)
        {
            MyTabControl.CurrentTextArea.Redo();
        }

        private void btZoomIn_Click(object sender, EventArgs e)
        {
            MyTabControl.CurrentTextArea.ZoomIn();
        }

        private void btZoomOut_Click(object sender, EventArgs e)
        {
            MyTabControl.CurrentTextArea.ZoomOut();
        }

        private void findToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (findingForm == null)
            {
                findingForm = new FindingForm();
            }
            findingForm.ShowFindingForm();
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (findingForm == null)
            {
                findingForm = new FindingForm();
            }
            findingForm.ShowFindAndReplaceForm();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btNew.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ShowOpenDialog(tabControl);
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem.PerformClick();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ShowSaveDialog(tabControl.SelectedTab);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ShowSaveAsDialog(tabControl.SelectedTab);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 1) //we don't remove tab page when tabControl has only one tab page
            {
                //we can use this one line of code below to easily remove selected tab page but it causes flinking  
                // tabControl.TabPages.Remove(tabControl.SelectedTab);

                //Show SaveDialog
                string result = Dialog.ShowSafeCloseTabDialog(tabControl.SelectedTab);

                if (result == "Cancel") return;

                ////Delete the selected tab page status
                //MyTabControl.RemoveTabPageStatus(tabControl.SelectedTab);

                //that's the reason why use the number lines of code below 
                //somehow if we set the tab page we are about to remove to another one (in this case we are about to remove selected tab page),
                //it doesn't cause the flinking
                TabPage tabToRemove = tabControl.SelectedTab;
                if (tabControl.SelectedIndex != 0)
                {
                    //set the selectedtab to the first tab page
                    tabControl.SelectedIndex = tabControl.SelectedIndex - 1;
                }
                else //if the tab we are about to remove is the first tab, just simply set selectedtab to 1
                {
                    tabControl.SelectedTab = tabControl.TabPages[1];
                }
                //remove tab 
                tabControl.TabPages.Remove(tabToRemove);
            }
        }
    }
}
