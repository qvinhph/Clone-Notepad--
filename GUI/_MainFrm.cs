using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyntaxHighlightingTextbox;

namespace GUI
{
    public partial class _MainFrm : Form
    {
        private string language = "C#";
        private FindingForm findingForm = null;

        public string Language
        {
            get { return language; }
            set { language = value; }
        }


        public _MainFrm()
        {
            InitializeComponent();
            TabControlMethods.SetupTabControl(tabControl);
        }
          

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "C#";
            
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            cToolStripMenuItem.Checked = true;
            
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
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;
            if (currentTextArea == null) return;

            Font fontToSet; /*Use for the specified keyword highlighting.*/
            var typingFont = currentTextArea.Font;

            TabControlMethods.CurrentTextArea.EnableHighlight = true;
            TabControlMethods.CurrentTextArea.Clear();

            switch (language)
            {
                case "C#":
                    {
                        //Number highlight
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, typingFont, UsedForAutoComplete.No);


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
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


                        //Keyword highlight another color
                        var keywords2 = new List<string>()
                        {
                            "define", "error", "import", "undef", "elif", "if", "include", "using", "else",
                            "ifdef", "line", "endif", "ifndef", "pragma"
                        };
                        currentTextArea.AddHighlightKeywords(keywords2, Color.SlateGray, typingFont);


                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//", "///", "////"
                        };
                        currentTextArea.AddListOfHighlightDescriptors(commentSymbols, DescriptorRecognition.StartsWith,
                                                HighlightType.ToEOL, Color.Green, typingFont, UsedForAutoComplete.No);

                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "/*",
                                                            HighlightType.ToCloseToken, "*/", Color.Green, typingFont, UsedForAutoComplete.No);


                        //Highlight text between begin and end token
                        var listPair = new List<string>()
                        {
                            "\"", "\"", "\'", "\'",
                        };
                        //currentTextArea.AddHighlightBoundaries(listPair, Color.Red, typingFont);
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "\"",
                                                            HighlightType.ToCloseToken, "\"", Color.Red, typingFont, UsedForAutoComplete.No);


                        //Highlight string start with '#'
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "#", HighlightType.ToEOW,
                                                        Color.SlateGray, typingFont, UsedForAutoComplete.No);
                        
                    }
                    break;
                case "Normal Text":
                    {
                        TabControlMethods.CurrentTextArea.EnableHighlight = false;
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

            TabControlMethods.CreateNewTabPage(newTabName);

            //Set the highlight language for new tab
            string currentLanguage = "";
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                if (((ToolStripMenuItem)item).Checked == true)
                    currentLanguage = ((ToolStripMenuItem)item).Text;
            }
            SetHighlightRule(currentLanguage);

            TabControlMethods.CurrentTextArea.Focus();
        }


        private void btUndo_Click(object sender, EventArgs e)
        {
            TabControlMethods.CurrentTextArea.Undo();
        }


        private void btRedo_Click(object sender, EventArgs e)
        {
            TabControlMethods.CurrentTextArea.Redo();
        }


        private void btZoomIn_Click(object sender, EventArgs e)
        {
            TabControlMethods.CurrentTextArea.ZoomIn();
        }


        private void btZoomOut_Click(object sender, EventArgs e)
        {
            TabControlMethods.CurrentTextArea.ZoomOut();
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

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
