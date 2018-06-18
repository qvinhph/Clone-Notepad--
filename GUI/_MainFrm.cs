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
        private AboutEasyType aboutEasyType = null;


        public string Language
        {
            get { return language; }
            set { language = value; }
        }


        public _MainFrm()
        {
            InitializeComponent();
            TabControlMethods.SetupTabControl(tabControl);
            this.FormClosing += _MainFrm_FormClosing;
        }


        #region Event handler methods


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
                    if (TabControlMethods.CurrentTextArea.requiresSaving==1 && result == "Cancel") return;

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
            TabControlMethods.CurrentTextArea.requiresSaving = 0;
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialog.ShowSaveAsDialog(tabControl.SelectedTab);
            TabControlMethods.CurrentTextArea.requiresSaving = 0;
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if (tabControl.TabPages.Count > 0)
            {
                //Show SaveDialog
                if (TabControlMethods.CurrentTextArea.requiresSaving==1)
                { string result = Dialog.ShowSafeCloseTabDialog(tabControl.SelectedTab);
                if (result == "Cancel") return;
                }


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


        #region Click Language Menu handlers 


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
            TabControlMethods.CurrentTextArea.CaseSensitive = true;
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

            SetHighlightRule(Language);
        }


        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Language = "C";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            cToolStripMenuItem1.Checked = true;

            if (TabControlMethods.IsEmpty()) return;

            SetHighlightRule(Language);
            TabControlMethods.CurrentTextArea.CaseSensitive = true;
        }


        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "SQL";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            sQLToolStripMenuItem.Checked = true;

            if (TabControlMethods.IsEmpty()) return;

            SetHighlightRule(Language);
            TabControlMethods.CurrentTextArea.CaseSensitive = false;
        }


        private void cToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Language = "C++";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            cToolStripMenuItem2.Checked = true;

            if (TabControlMethods.IsEmpty()) return;

            SetHighlightRule(Language);
            TabControlMethods.CurrentTextArea.CaseSensitive = true;
        }


        private void javascriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "Javascript";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            javascriptToolStripMenuItem.Checked = true;

            if (TabControlMethods.IsEmpty()) return;

            SetHighlightRule(Language);
            TabControlMethods.CurrentTextArea.CaseSensitive = true;
        }


        private void vBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Language = "Javascript";

            //Remove the previous check and set the new one
            foreach (var item in languageToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }
            vBToolStripMenuItem.Checked = true;

            if (TabControlMethods.IsEmpty()) return;

            SetHighlightRule(Language);
            TabControlMethods.CurrentTextArea.CaseSensitive = true;
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
                            "elif"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


                        //Keyword highlight another color
                        var keywords2 = new List<string>()
                        {
                            "define", "error", "import", "undef", "include", "using",
                            "ifdef", "line", "endif", "ifndef", "pragma"
                        };
                        currentTextArea.AddHighlightKeywords(keywords2, Color.LightSeaGreen, typingFont);


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
                        currentTextArea.AddHighlightBoundaries(listPair, Color.Red, typingFont);


                        //Highlight string start with '#'
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "#", HighlightType.ToEOW,
                                                        Color.LightSeaGreen, typingFont, UsedForAutoComplete.No);
                    }
                    break;

                case "C++":
                    {
                        //Number highlight
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, typingFont, UsedForAutoComplete.No);


                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//", "///", "////"
                        };
                        currentTextArea.AddListOfHighlightDescriptors(commentSymbols, DescriptorRecognition.StartsWith,
                                                HighlightType.ToEOL, Color.Green, typingFont, UsedForAutoComplete.No);

                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "/*",
                                                            HighlightType.ToCloseToken, "*/", Color.Green, typingFont, UsedForAutoComplete.No);


                        //Keyword highlight
                        var keywords = new List<string>()
                        {
                           "asm", "auto", "bool", "break", "case", "catch", "char","class", "const_cast", "continue", "default", "delete", "double", "else",
                           "enum", "dynamic_cast", "extern", "false", "float", "for", "union", "unsigned", "using", "friend", "goto", "if", "inline", "int",
                           "long", "mutable", "virtual", "namespace", "new", "operator", "private", "protected", "public", "register", "void", "reinterpret_cast",
                           "return", "short", "signed", "sizeof", "static", "static_cast", "volatile", "struct", "switch", "template", "this", "throw", "true",
                           "try", "typedef", "typeid", "unsigned", "wchar_t", "while"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);
                       

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

                case "C":
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
                        

                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//"
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

                case "VB":
                    {
                        //Number highlight
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, typingFont, UsedForAutoComplete.No);


                        //Keyword highlight
                        var keywords = new List<string>()
                        {
                            "addhandler", "addressof", "alias", "and", "andalso", "as", "boolean", "byref", "byte", "byval",
                            "call", "case ", "catch", "cbool", "cbyte", "cchar", "cdate", "cdbl", "cdec", "char", "cint", "class",
                            "clng", "cobj", "const", "continue", "csbyte", "cshort", "csng", "cstr", "ctype", "cuint", "culng", "cushort",
                            "date", "decimal", "declare", "default", "delegate", "dim", "directcast", "do", "double", "each", "else",
                            "elseif", "end", "endif", "enum", "erase", "error", "event", "exit", "false", "finally", "for", "friend",
                            "function", "get", "gettype", "getxmlnamespace", "global", "gosub", "goto", "handles", "if", "implements",
                            "imports", "in", "inherits", "integer", "interface", "is", "isnot", "let", "lib", "like", "long", "loop", "me",
                            "mod", "module", "mustinherit", "mustoverride", "mybase", "myclass", "namespace", "narrowing", "new", "not", "nothing",
                            "notinheritable", "notoverridable", "object", "of", "on", "operator", "option", "optional", "or", "orelse", "out",
                            "overloads", "overridable", "overrides", "paramarray", "partial", "private", "property", "protected", "public",
                            "raiseevent", "readonly", "redim", "rem", "removehandler", "resume", "return", "sbyte", "select", "set", "shadows",
                            "shared", "short", "single", "static", "step", "stop", "string", "structure", "sub", "synclock", "then", "throw",
                            "to", "true", "try", "trycast", "typeof", "uinteger", "ulong", "ushort", "using", "variant", "wend", "when", "while",
                            "widening", "with", "withevents", "writeonly", "xor"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


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

                case "Javascript":
                    {
                        //Number highlight
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, typingFont, UsedForAutoComplete.No);


                        //Keyword highlight
                        var keywords = new List<string>()
                        {
                            "abstract", "arguments", "await", "boolean", "break", "byte", "case", "catch", "char", "class", "const", "continue",
                            "debugger", "default", "delete", "do", "double", "elseenum", "eval", "export", "extends", "false", "final", "finally",
                            "float", "for", "function", "goto", "if", "implements", "import", "in", "instanceof", "int", "interface", "let",
                            "long", "native", "newnull", "package", "private", "protected", "public", "return", "short", "static", "super",
                            "switch", "synchronized", "this", "throw", "throws", "transient", "truetry", "typeof", "var", "void", "volatile",
                            "while", "with", "yield", "abstract", "boolean", "byte", "char", "double", "final", "float", "goto", "int",
                            "long", "native", "short", "synchronized", "throws", "transient", "volatile"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


                        //Keyword highlight another color
                        var keywords2 = new List<string>()
                        {
                            "Array", "Date,  eval", "function", "hasOwnProperty", "Infinity", "isFinite", "isNaN", "isPrototypeOf",
                            "length", "Math", "NaN", "name", "Number", "Object", "prototype", "String", "toString", "undefined", "valueOf"
                        };
                        currentTextArea.AddHighlightKeywords(keywords2, Color.LightSeaGreen, typingFont);


                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//"
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
                        currentTextArea.AddHighlightBoundaries(listPair, Color.Red, typingFont);
                    }
                    break;

                case "SQL":
                    {
                        //Number highlight
                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.IsNumber, "",
                                                    HighlightType.ToEOW, Color.IndianRed, typingFont, UsedForAutoComplete.No);


                        //Keyword highlight
                        var keywords = new List<string>()
                        {
                            "add", "except", "percent", "all", "exec", "plan", "alter", "execute", "precision",
                            "and", "exists", "primary", "any", "exit", "print", "as", "fetch", "proc", "asc",
                            "file", "procedure", "authorizationfillfactor", "public", "backup", "for", "raiserror",
                            "begin", "foreign", "read", "between", "freetext", "readtext", "break", "freetexttable",
                            "reconfigure", "browse", "from", "references", "bulk", "full", "replication", "by",
                            "function", "restore", "cascade", "goto", "restrict", "case", "grant", "return", "check",
                            "group", "revoke", "checkpoint", "having", "right", "close", "holdlock", "rollback",
                            "clustered", "identity", "rowcount", "coalesce", "identity_insert", "rowguidcol", "collate",
                            "identitycol", "rule", "column", "if", "save", "commit", "in", "schema", "compute", "index",
                            "select", "constraint", "inner", "session_user", "contains", "insert", "set", "containstable",
                            "intersect", "setuser", "continue", "into", "shutdown", "convert", "is", "some", "create",
                            "join", "statistics", "cross", "key", "system_user", "current", "kill", "table", "current_date",
                            "left", "textsize", "current_time", "like", "then", "current_timestamp", "lineno", "to",
                            "current_user", "load", "top", "cursor", "national", "tran", "database", "nocheck", "transaction",
                            "dbcc", "nonclustered", "trigger", "deallocate", "not", "truncate", "declare", "null",
                            "tsequal", "default", "nullif", "union", "delete", "of", "unique", "deny", "off", "update",
                            "desc", "offsets", "updatetext", "disk", "on", "use", "distinct", "open", "user", "distributed",
                            "opendatasource", "values", "double", "openquery", "varying", "drop", "openrowset", "view", "dummy",
                            "openxml", "waitfor", "dump", "option", "when", "else", "or", "where", "end", "order", "while",
                            "errlvl", "outer", "with", "escape", "over", "writetext"
                        };
                        fontToSet = new Font(typingFont, FontStyle.Bold);
                        currentTextArea.AddHighlightKeywords(keywords, Color.CornflowerBlue, fontToSet);


                        //Comment highlight
                        var commentSymbols = new List<string>()
                        {
                            "//"
                        };
                        currentTextArea.AddListOfHighlightDescriptors(commentSymbols, DescriptorRecognition.StartsWith,
                                                HighlightType.ToEOL, Color.Green, typingFont, UsedForAutoComplete.No);

                        currentTextArea.AddHighlightDescriptor(DescriptorRecognition.StartsWith, "/*",
                                                            HighlightType.ToCloseToken, "*/", Color.Green, typingFont, UsedForAutoComplete.No);


                        //Highlight text between begin and end token
                        var listPair = new List<string>()
                        {
                            "\'", "\'",
                        };
                        currentTextArea.AddHighlightBoundaries(listPair, Color.Red, typingFont);
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
               

        private void _MainFrm_Load(object sender, EventArgs e)
        {

        }


        private void btSaveAs_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.PerformClick();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (aboutEasyType == null)
            {
                aboutEasyType = new AboutEasyType();
            }
            //aboutEasyType.ShowAboutEasyType();
            aboutEasyType.ShowDialog();
        }




        private void _MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TabControlMethods.IsEmpty())
            { return; }
            if ((TabControlMethods.CurrentTextArea.requiresSaving == 1) && Dialog.ShowSafeCloseFormDialog(tabControl) == "Cancel")
            {
                e.Cancel = true;
            }
        }
    }
}

