using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SyntaxHighlightingTextbox;

namespace GUI
{
    class Dialog
    {
        public static void ShowOpenDialog(TabControl tabControl)
        {
            //create a new file dialog and choose file to open
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "txt Files (*txt)|*txt|All Files (*.*)|*.*";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                //check to see if there is already this tab page
                TabPage currentTabPage = null;
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    if (tabPage.Name == openDialog.FileName)
                    {
                        currentTabPage = tabPage;
                        break;
                    }
                }

                //Create a new tab page
                TabPage newTabPage = TabControlMethods.CreateNewTabPage(openDialog.SafeFileName);

                //a variable to hold text box contained in tab page
                TypingArea newTypingArea = TabControlMethods.CurrentTextArea;

                //Get the path of the File
                string filePath = openDialog.FileName;

                //Get the text of the file
                string fileText = File.ReadAllText(filePath);

                //Set the text of current text box by file Text    
                newTypingArea.Text = fileText;

                //In the next time, if this tab page already has a name, just open it 
                tabControl.SelectedTab.Name = openDialog.FileName;
            }
            //dispose for sure
            openDialog.Dispose();
        }

        public static void ShowSaveDialog(TabPage tabPage)
        {
            //Choose the current typing area
            TypingArea currentTextArea = (tabPage.Controls[0] as MyRichTextBox).TypingArea;

            //Create a save file Dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "txt Files (*txt)|*txt";
            saveDialog.DefaultExt = "txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //If the tab opening typing area already has a name, just save it
                if (tabPage.Name != "")
                {
                    using (Stream s = File.Open(tabPage.Name, FileMode.Create))
                    {
                        //get the streamwriter of the new file 
                        using (StreamWriter sw = new StreamWriter(s))
                        {
                            //Get the text of the current typing area and write it to streamwriter
                            sw.Write(currentTextArea.Text);
                            tabPage.Text = Path.GetFileName(tabPage.Name);
                            return;
                        }
                    }
                }
                //Else open a dialog to set the name and choose path to save
                else
                {
                    using (Stream s = File.Open(saveDialog.FileName, FileMode.Create))
                    {
                        //get the streamwriter of the new file 
                        using (StreamWriter sw = new StreamWriter(s))
                        {
                            sw.Write(currentTextArea.Text);

                            //change the text title of the tab by file name
                            tabPage.Text = Path.GetFileName(saveDialog.FileName);

                            //In the next time, if this tab page already has a name, just save it 
                            tabPage.Name = saveDialog.FileName;
                        }
                    }
                }
            }

            //dispose for sure
            saveDialog.Dispose();
        }

        public static void ShowSaveAsDialog(TabPage tabPage)
        {
            TypingArea currentTextArea = (tabPage.Controls[0] as MyRichTextBox).TypingArea;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "txt Files (*txt)|*txt";
            saveDialog.DefaultExt = "txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveDialog.FileName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(currentTextArea.Text);
                        tabPage.Text = Path.GetFileName(saveDialog.FileName);
                        tabPage.Name = saveDialog.FileName;
                    }
                }
            }

            //dispose for sure
            saveDialog.Dispose();
        }

        /// <summary>
        /// This string make sure that you want to close all or close when there is only one tab 
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns></returns>
        public static string ShowSafeCloseTabDialog(TabPage tabPage)
        {
            if (tabPage.Text.Contains("*"))
            {
                //Get the real name of the tab page
                string tabName = tabPage.Text;
                tabName = tabName.Replace("*", "");

                string text = "You haven't save " + tabName + "\n Do you want to save it before closing?";
                DialogResult result = MessageBox.Show(text, "Yes or No?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowSaveDialog(tabPage);
                    return "Yes";
                }
                else
                {
                    if (result == DialogResult.No)
                    {
                        return "No";

                    }
                    else
                    {
                        return "Cancel";
                    }
                }
            }
            return "Nothing";
        }

        /// <summary>
        /// Safe Close Form Dialog
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns></returns>
        public static string ShowSafeCloseFormDialog(TabControl tabControl)
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                string result = ShowSafeCloseTabDialog(tabPage);
                if (result == "Cancel") return "Cancel";
            }
            return "Nothing";
        }
    }
}
