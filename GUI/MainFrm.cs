﻿using System;
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
    public partial class MainFrm : Form
    {
        private string language = "C#";

        public string Language
        {
            get { return language; }
            set { language = value; }
        }


        public MainFrm()
        {
            InitializeComponent();
        }

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
                        var fontToSet = new Font(typingFont, FontStyle.Bold);
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
    }
}