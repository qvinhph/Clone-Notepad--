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

        #region Event handler methods


        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "C#";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            cToolStripMenuItem.Checked = true;

            if (TabControlMethods.IsEmpty()) return;
            SetHighlightRule(Language);
        }


        private void normalTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "Normal Text";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            normalTextToolStripMenuItem.Checked = true;

            if (TabControlMethods.IsEmpty()) return;
            TabControlMethods.CurrentTextArea.Refresh();

            SetHighlightRule(Language);
        }       


        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Language = "C";
        }


        private void btNew_Click(object sender, EventArgs e)
        {
            int newTabIndex = 1;
            string newTabName = "new 1";
            for (int i = 0; i < tabControl.TabPages.Count; i++)
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

            //Set the current highlight language for new tab
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
            undoToolStripMenuItem.PerformClick();
        }


        private void btRedo_Click(object sender, EventArgs e)
        {
            redoToolStripMenuItem.PerformClick();
        }
        

        private void btZoomIn_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;
            
            TabControlMethods.CurrentTextArea.ZoomIn();
        }


        private void btZoomOut_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;
            
            TabControlMethods.CurrentTextArea.ZoomOut();
        }


        private void findToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            if (findingForm == null)
            {
                findingForm = new FindingForm();
            }
            findingForm.ShowFindingForm();
        }


        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

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
            if (tabControl.TabPages.Count > 0)
            {
                //Show SaveDialog
                string result = Dialog.ShowSafeCloseTabDialog(tabControl.SelectedTab);


                if (result == "Cancel") return;


                //Choose the selected tab to remove
                TabPage tabToRemove = tabControl.SelectedTab;


                if (tabControl.SelectedIndex != 0)
                {
                    //set the selectedtab to the first tab page
                    tabControl.SelectedIndex = tabControl.SelectedIndex - 1;
                }
                //if the tab we are about to remove is the first tab, just simply set selectedtab to 1
                else
                {
                    tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                }


                //remove app info
                TabControlMethods.RemoveTabPageInfo(tabToRemove);


                //remove tab 
                tabControl.TabPages.Remove(tabToRemove);


                if (tabControl.TabPages.Count > 0)
                {
                    TabControlMethods.CurrentTextArea.Focus();
                }
            }
        }


        #region TabControl Event Handlers


        /// <summary>
        /// Event handler helping update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }
        

        /// <summary>
        /// Raise when a new tab page added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            //We cannot use TabControlMethods.CurrentArea 
            //Because this event raise before the SelectedTab is set.
            MyRichTextBox currentRTB = tabControl.TabPages[tabControl.TabPages.Count - 1].Controls[0] as MyRichTextBox;
            TypingArea currentArea = currentRTB.TypingArea;

            //Set the event handler helping update status bar
            currentArea.TextChanged += TextArea_TextChanged;
        }


        /// <summary>
        /// Event handler helping update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }


        #endregion

        

        #endregion


        #region Other methods


        /// <summary>
        /// Set syntax highlight to the specified language.
        /// </summary>
        /// <param name="language"></param>
        private void SetHighlightRule(string language)
        {
            if (TabControlMethods.IsEmpty()) return;
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;

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
                            "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
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

            UpdateStatusBar();
            TabControlMethods.CurrentTextArea.Refresh();
            TabControlMethods.CurrentTabPageInfo.Language = language;
        }


        /// <summary>
        /// Update the status bar with current tab page infomation
        /// </summary>
        public void UpdateStatusBar()
        {
            //Get the current caret location of the current typing area
            if (TabControlMethods.IsEmpty()) return;

            int caretLocation = TabControlMethods.CurrentTextArea.SelectionStart;

            slbLanguage.Text = "Language: " + TabControlMethods.CurrentTabPageInfo.Language;
            slbTextLength.Text = "Length: " + TabControlMethods.CurrentTextArea.TextLength;
            slbLine.Text = "Line: " + (TabControlMethods.CurrentTextArea.GetLineFromCharIndex(caretLocation) + 1);
        }


        #endregion

        private void closeCurrentFile_Click(object sender, EventArgs e)
        {
            TabControlMethods.CloseCurrentTabPage();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove all the tab except the selected Tab
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage != tabControl.SelectedTab)
                {
                    string result = Dialog.ShowSafeCloseTabDialog(tabPage);
                    if (result == "Cancel") return;

                    ////Delete tab status
                    //TabControlMethods.RemoveTabPageStatus(tabPage);

                    //Remove tab Page
                    tabControl.TabPages.Remove(tabPage);
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            if (TabControlMethods.CurrentTextArea.CanUndo)
            {
                TabControlMethods.CurrentTextArea.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            if (TabControlMethods.CurrentTextArea.CanRedo)
            {
                TabControlMethods.CurrentTextArea.Redo();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.CurrentTextArea != null)
                TabControlMethods.CurrentTextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check to make sure no exception occur
            if (TabControlMethods.CurrentTextArea != null)
                TabControlMethods.CurrentTextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.CurrentTextArea != null)
                TabControlMethods.CurrentTextArea.Paste();
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem.PerformClick();
        }

        private void btCut_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem.PerformClick();
        }

        private void btPaste_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem.PerformClick();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.CurrentTextArea != null) TabControlMethods.CurrentTextArea.SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.CurrentTextArea != null)
                TabControlMethods.CurrentTextArea.Rtf = "";
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            TabControlMethods.CurrentTextArea.ZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            TabControlMethods.CurrentTextArea.ZoomOut();
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            if (TabControlMethods.IsEmpty()) return;

            findToolStripMenuItem.PerformClick();
        }

        private void btFindAndReplace_Click(object sender, EventArgs e)
        { 
            if (TabControlMethods.IsEmpty()) return;

            findAndReplaceToolStripMenuItem.PerformClick();
        }

        private void btCloseCurFile_Click(object sender, EventArgs e)
        {
            TabControlMethods.CloseCurrentTabPage();
        }
    }
}

