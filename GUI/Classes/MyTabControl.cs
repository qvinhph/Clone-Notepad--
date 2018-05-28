using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyntaxHighlightingTextbox;
using System.Drawing;

namespace GUI
{
    class MyTabControl
    {
        //the "x" image 
        private static Image closeImage;

        //A variable refer on current handling TabControl.
        private static TabControl tabControl;

        #region Properties

        public static TypingArea CurrentTextArea
        {
            get
            {
                if (tabControl.SelectedTab != null)
                    return (tabControl.SelectedTab.Controls[0] as TypingArea);
                return null;
            }
        }

        public static TabControl TabControl
        {
            get
            {
                return tabControl;
            }

            set
            {
                tabControl = value;
            }
        }

        #endregion

        public static void SetupTabControl(TabControl _tabControl)
        {
            tabControl = _tabControl;

            tabControl.AllowDrop = true;
            
            //Allow to custom the way tab control are drawn through DrawItem event raising
            //whenever the tab redraw
            tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;

            //Set size of each tab page.
            tabControl.Padding = new Point(15, 3);

            //get the "x" image 
            closeImage = Properties.Resources.CloseImage;

            //draw the "x" button
            tabControl.DrawItem += tabControl_DrawItem;

            //click "x" button to close the tab page.
            tabControl.MouseClick += tabControl_MouseDown;
        }

        #region TabControl event handlers

        private static void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Get the area of the tab being drawn.
            Rectangle tabRect = tabControl.GetTabRect(e.Index);

            if (e.Index == tabControl.SelectedIndex)
            {
                //If the tab being drawn is the selected tab.

                //Get the rectangle of the selected tab
                tabRect = tabControl.GetTabRect(tabControl.SelectedIndex);

                //Set background color for this tab
                e.Graphics.FillRectangle(Brushes.White, tabRect);
            }

            //Reduce the area of rectangle to centerize the text inside.
            tabRect.Inflate(-2, -2);

            //Show name of the tab page.
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                                  tabControl.Font, Brushes.Black, tabRect);

            //"x" image
            Rectangle imageRect = new Rectangle(tabRect.Right - closeImage.Width/3,
                                                tabRect.Top, closeImage.Width/3, closeImage.Height/3);

            //draw the string text 
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                                  tabControl.Font, Brushes.Black, tabRect);
            //draw the "x" image
            e.Graphics.DrawImage(closeImage, imageRect);
        }

        private static void tabControl_MouseDown(object sender, MouseEventArgs e)
        { 
            //Check all the tab to determind which tab that has mouse click fall into.
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                //get the area of tab
                Rectangle tabRect = tabControl.GetTabRect(tabControl.TabPages.IndexOf(tabPage));
                //get the area of the "x" image
                Rectangle imageRect = new Rectangle(tabRect.Right - closeImage.Width/3,
                                         tabRect.Top,
                                         closeImage.Width/3,
                                         closeImage.Height/3);
                
                //Remove
                if (imageRect.Contains(e.Location))
                {
                    //If the tab we are removing is not the selected tab.
                    if (tabPage != (TabPage)tabControl.Tag)
                    {
                        //When we click to this tag area, it will be selected tab.
                        //So we need reset to the right selected tab.
                        tabControl.SelectedTab = (TabPage)tabControl.Tag;

                        //remove tabpage status
                        RemoveTabPageInfo((TabPage)tabControl.Tag);

                        //remove tab page
                        tabControl.TabPages.Remove(tabPage);
                    }
                    else
                    {
                        //The selected tab is the first tab
                        if (tabControl.SelectedIndex == 0)
                        {
                            //The next selected tab after this be removed
                            tabControl.SelectedIndex = 1;
                        }
                        else
                        {
                            tabControl.SelectedIndex -= 1;
                        }

                        //remove tabpage status
                        RemoveTabPageInfo((TabPage)tabControl.Tag);

                        //remove tab page
                        tabControl.TabPages.Remove((TabPage)tabControl.Tag);
                    }
                }
            }
        }

        #endregion

        #region TabControl Add/Remove

        public static TabPage CreateNewTabPage(String tabName)
        {
            TabPage newTabPage = new TabPage(tabName);
            InitTabPageInfo(newTabPage);

            //Create the textbox inside
            TypingArea typingArea = new TypingArea();
            newTabPage.Controls.Add(typingArea);
            typingArea.Dock = DockStyle.Fill;

            tabControl.TabPages.Add(newTabPage);
            //Switch to the new tabpage
            tabControl.SelectedTab = newTabPage;

            return newTabPage;
        }

        #endregion

        #region Manipulate the TabPageInfo

        static List<TabPageInfo> listOfTabPageInfo = new List<TabPageInfo>();

        /// <summary>
        /// Get the selected tab page info.
        /// </summary>
        public static TabPageInfo CurrentTabPageInfo
        {
            get
            {
                foreach (TabPageInfo tabPageInfo in listOfTabPageInfo)
                {
                    if (tabPageInfo.TabPage == tabControl.SelectedTab)
                    {
                        return tabPageInfo;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Init the tab page info.
        /// </summary>
        /// <param name="tabPage"></param>
        public static void InitTabPageInfo(TabPage tabPage)
        {
            TabPageInfo tabPageInfo = new TabPageInfo();
            tabPageInfo.TabPage = tabPage;
            //tabPageInfo.Language = .DefaultLanguage;
            tabPageInfo.CanUndo = false;
            tabPageInfo.CanRedo = false;
            listOfTabPageInfo.Add(tabPageInfo);
        }

        /// <summary>
        /// Delete the tab page info.
        /// </summary>
        /// <param name="tabPage"></param>
        public static void RemoveTabPageInfo(TabPage tabPage)
        {
            //Locate the tab page info to remove.
            TabPageInfo tabStatusToDelete = null;
            foreach (TabPageInfo tabPageStatus in listOfTabPageInfo)
            {
                if (tabPageStatus.TabPage == tabPage)
                {
                    tabStatusToDelete = tabPageStatus;
                }
            }

            if (tabStatusToDelete != null)
            {
                listOfTabPageInfo.Remove(tabStatusToDelete);
            }
        }

        #endregion
    }
}
