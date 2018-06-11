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
        //int preSearchText_Length = 0;
        int indexOfSearchText = -1;
        List<int> textsFound = new List<int>();
        Color AllFoundTextBackColor = Color.Aquamarine;
        //Color SelectedFoundTextBackColor = Color.Orange;
        //Color MyDefaultBackColor = Color.White;
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
            TypingArea currentTextArea = MyTabControl.CurrentTextArea;

            previousText = currentTextArea.Text;

            ////Remove highlighted text of previous search        
            currentTextArea.ClearBackColor(currentTextArea.BackColor);

            // Highlighted backcolor of all found text
            //TextFound = currentTextArea.FindAll(searchTermTextBox.Text);
            //currentTextArea.ColorBackGround(TextFound, searchTermTextBox.Text.Length, AllFoundTextBackColor);
            textsFound.Clear();
            textsFound = currentTextArea.FindAndColorAll(searchTextbox.Text, AllFoundTextBackColor);
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
            matchCaseCheckBox.Location = replacementLabel.Location;
            this.Width = 380;
            this.Show();
        }

        public void ShowFindAndReplaceForm()
        {
            this.Text = "Find And Replace";
            replacementLabel.Visible = true;
            replacementTextbox.Visible = true;
            replaceButton.Visible = true;
            replaceAllButton.Visible = true;
            matchCaseCheckBox.Location = new Point(19, 110);
            this.AutoSize = true;
            this.Show();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.H | Keys.Control))
            {
                ShowFindAndReplaceForm();
                return true;
            }

            if (keyData == (Keys.F | Keys.Control))
            {
                ShowFindingForm();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void matchCaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void findNextButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = MyTabControl.CurrentTextArea;

            if (previousText != currentTextArea.Text)
            {
                previousText = currentTextArea.Text;

                //get this again because we might have changed the text in text area and it made some of the found text position changed
                textsFound.Clear();
                textsFound = currentTextArea.FindAll(searchTextbox.Text);
            }

            //set this to prevent some disturb things
            currentTextArea.BlockAllAction = true;

            if (textsFound.Count != 0)
            {
                if (searchTextbox.Text.Length == 0)
                    return;
                ////Change backcolor of current found text  
                //if (indexOfSearchText != -1)
                //{
                //    currentTextArea.Select(TextFound[indexOfSearchText], searchTermTextBox.Text.Length);
                //    currentTextArea.SelectionBackColor = AllFoundTextBackColor;
                //}
                //Reset the index
                indexOfSearchText++;
                if (indexOfSearchText == textsFound.Count)
                {
                    indexOfSearchText = 0;
                }

                //Chose the highlight backcolor for select text        
                currentTextArea.Select(textsFound[indexOfSearchText], searchTextbox.Text.Length);
                //currentTextArea.SelectionBackColor = SelectedFoundTextBackColor;
            }

            currentTextArea.BlockAllAction = false;

            currentTextArea.Focus();
        }

        private void findPreviousButton_Click(object sender, EventArgs e)
        {
            TypingArea currentTextArea = MyTabControl.CurrentTextArea;

            if (previousText != currentTextArea.Text)
            {
                previousText = currentTextArea.Text;

                //get this again because we might have changed the text in text area and it made some of the found text position changed
                textsFound.Clear();
                textsFound = currentTextArea.FindAll(searchTextbox.Text);
            }

            currentTextArea.BlockAllAction = true;

            if (textsFound.Count != 0)
            {
                if (searchTextbox.Text.Length == 0)
                    return;
                ////Change backcolor of current found text
                //if (indexOfSearchText != -1)
                //{
                //    currentTextArea.Select(TextFound[indexOfSearchText], searchTermTextBox.Text.Length);
                //    currentTextArea.SelectionBackColor = AllFoundTextBackColor;
                //}

                //Reset the index

                indexOfSearchText--;
                if (indexOfSearchText <= -1)
                {
                    indexOfSearchText = textsFound.Count - 1;
                }

                //Chose the highlight backcolor for select text        
                currentTextArea.Select(textsFound[indexOfSearchText], searchTextbox.Text.Length);
                //currentTextArea.SelectionBackColor = SelectedFoundTextBackColor;
            }

            currentTextArea.BlockAllAction = false;

            currentTextArea.Focus();
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
