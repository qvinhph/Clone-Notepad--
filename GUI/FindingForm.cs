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
    public partial class FindingForm : Form
    {
        int indexOfSearchText = -1;

        List<int> textsFound = new List<int>();
        Color AllFoundTextBackColor = Color.Aquamarine;
       
       // Color SelectedFoundTextBackColor = Color.Orange;
       
        string previousText = "";

        public FindingForm()
        {
            InitializeComponent();
        }


        private void FindingForm_Load(object sender, EventArgs e)
        {

        }

        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void replacementTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void findButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;

            previousText = currentTextArea.Text;

            //Remove the highlight backcolor of the privous search 
            currentTextArea.ClearBackColor(currentTextArea.BackColor);

            textsFound.Clear();
            textsFound = currentTextArea.FindAndColorAll(searchTextbox.Text, AllFoundTextBackColor);

            indexOfSearchText = -1;

            this.Focus();
        }

        private void FindForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0.3;
            }
            catch { }
        }

        private void FindForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        public void ShowFindingForm()
        {
            this.Text = "Find";
            replacementLabel.Visible = false;
            replacementTextbox.Visible = false;
            replaceButton.Visible = false;
            replaceAllButton.Visible = false;
            this.Width = 400;
            this.Show();
        }

        public void ShowFindAndReplaceForm()
        {
            this.Text = "Find And Replace";
            replacementLabel.Visible = true;
            replacementTextbox.Visible = true;
            replaceButton.Visible = true;
            replaceAllButton.Visible = true;
            this.AutoSize = true;
            this.Show();
        }

        private void findNextButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;

            //Check if the current string hasn't been search last turns
            if (previousText != currentTextArea.Text)
            {
                previousText = currentTextArea.Text;

                textsFound.Clear();
                textsFound = currentTextArea.FindAll(searchTextbox.Text);
            }

            if (textsFound.Count != 0)
            {
                if (searchTextbox.Text.Length == 0)
                    return;

                indexOfSearchText++;
                if (indexOfSearchText == textsFound.Count)
                {
                    indexOfSearchText = 0;
                }

                //Each every text found next will be highlighted  
                currentTextArea.Select(textsFound[indexOfSearchText], searchTextbox.Text.Length);
            }

            currentTextArea.Focus();
        }

        private void findPreviousButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;

            if (previousText != currentTextArea.Text)
            {
                previousText = currentTextArea.Text;

                textsFound.Clear();
                textsFound = currentTextArea.FindAll(searchTextbox.Text);
            }

            if (textsFound.Count != 0)
            {
                if (searchTextbox.Text.Length == 0)
                    return;
                
                indexOfSearchText--;
                if (indexOfSearchText <= -1)
                {
                    indexOfSearchText = textsFound.Count - 1;
                }

                //Each every text found privous will be highlighted      
                currentTextArea.Select(textsFound[indexOfSearchText], searchTextbox.Text.Length);
            }

            currentTextArea.Focus();
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;
            currentTextArea.Focus();

            //Check if this string has been replaced or this string has nothing
            if (indexOfSearchText == -1 || searchTextbox.Text.Equals(replacementTextbox.Text) || currentTextArea.SelectionLength == 0)
            {
                return;
            }

            //We might changed or do something with the text so we need to get this again
            textsFound.Clear();
            textsFound = currentTextArea.FindAll(searchTextbox.Text);

            currentTextArea.Select(textsFound[indexOfSearchText], searchTextbox.Text.Length);

            //Make it be replaced
            currentTextArea.SelectedText = replacementTextbox.Text;

            //just select for nothing much
            currentTextArea.Select(textsFound[indexOfSearchText], replacementTextbox.Text.Length);
        }


        private void FindingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage tabPage in TabControlMethods.TabControl.TabPages)
            {
                TypingArea textArea = (tabPage.Controls[0] as MyRichTextBox).TypingArea;
                textArea.ClearBackColor(textArea.BackColor);
            }

            this.Visible = false;

            e.Cancel = true;
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            //Check if this string has been replaced or this string has nothing
            if (searchTextbox.Text.Equals(replacementTextbox.Text))
            {
                return;
            }

            TypingArea currentTextArea = TabControlMethods.CurrentTextArea;

            //Replace all the selected found text by replacement text
            string textToReplace = currentTextArea.Text.Replace(searchTextbox.Text, replacementTextbox.Text);
            currentTextArea.Select(0, currentTextArea.TextLength);
            currentTextArea.Text = currentTextArea.Text.Replace(searchTextbox.Text, replacementTextbox.Text);
        }
    }
}
