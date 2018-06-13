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
    static partial class MyTabControl 
    {
        //the "x" image 
        private static Image closeImage;

        //A variable refer on current handling TabControl.
        private static TabControl tabControl;

        public static int PreviousSelectedIndex { get; private set; }
        public static TabPage PreviousSelectedTabpage { get; private set; }


        #region Properties

        /// <summary>
        /// Get the current typing area
        /// </summary>
        public static TypingArea CurrentTextArea
        {
            get
            {
                if (TabControl.SelectedTab != null)
                    return (TabControl.SelectedTab.Controls[0] as MyRichTextBox).TypingArea;
                return null;
            }
        }


        /// <summary>
        /// Get the current TabControl
        /// </summary>
        public static TabControl TabControl
        {
            get
            {
                return tabControl;
            }

            private set
            {
                tabControl = value;
            }
        }

        #endregion


        public static void SetupTabControl(TabControl _tabControl)
        {
            TabControl = _tabControl;

            TabControl.AllowDrop = true;
            
            //Allow to custom the way tab control are drawn through DrawItem event raising
            //whenever the tab redraw
            TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;

            //Set size of each tab page.
            TabControl.Padding = new Point(15, 4);

            //get the "x" image 
            closeImage = Properties.Resources.CloseImage;

            //draw the "x" button
            TabControl.DrawItem += TabControl_OnDrawItem;

            //click "x" button to close the tab page.
            TabControl.MouseClick += TabControl_OnMouseClick;

            //
            TabControl.Deselecting += TabControl_OnDeselecting;

            //
            TabControl.Selecting += TabControl_OnSelecting; 

            //TabControl.SelectedIndexChanged += 
        }

        private static void TabControl_OnSelecting(object sender, TabControlCancelEventArgs e)
        {
            
        }


        #region TabControl event handlers

        private static void TabControl_OnDrawItem(object sender, DrawItemEventArgs e)
        {
            //Get the area of the tab being drawn.
            Rectangle tabRect = TabControl.GetTabRect(e.Index);

            if (e.Index == TabControl.SelectedIndex)
            {
                //If the tab being drawn is the selected tab.

                //Set background color for this tab
                e.Graphics.FillRectangle(Brushes.White, tabRect);
            }

            //Reduce the area of rectangle to centerize the text inside.
            tabRect.Inflate(-2, -2);

            //Show name of the tab page.
            e.Graphics.DrawString(TabControl.TabPages[e.Index].Text,
                                  TabControl.Font, Brushes.Black, tabRect);

            //"x" image
            Rectangle imageXRect = new Rectangle(tabRect.Right - tabRect.Height,
                                          tabRect.Top, tabRect.Height, tabRect.Height);
            
            //draw the "x" image
            e.Graphics.DrawImage(closeImage, imageXRect);           
        }


        private static void TabControl_OnMouseClick(object sender, MouseEventArgs e)
        {
            //Check all the tab to determind which tab that has mouse click fall into.
            foreach (TabPage tabPage in TabControl.TabPages)
            {
                //get the area of tab
                Rectangle tabRect = TabControl.GetTabRect(TabControl.TabPages.IndexOf(tabPage));
                //get the area of the "x" image
                Rectangle imageXRect = new Rectangle(tabRect.Right - tabRect.Height,
                                          tabRect.Top, tabRect.Height, tabRect.Height);

                //Remove
                if (imageXRect.Contains(e.Location))
                {
                    //When we close the unselected tab, it will be automatically selected
                    //So we need reset to the previous selected tab.
                    //Check whether the previous selected tab is existing
                    if (TabControl.TabPages.Contains(PreviousSelectedTabpage))
                    {
                        TabControl.SelectedTab = PreviousSelectedTabpage;
                    }
                    else
                    {
                        //if not, set the next selected tab to the nearest tab
                        if (TabControl.SelectedIndex == 0)
                            TabControl.SelectedIndex = 1;
                        else
                            TabControl.SelectedIndex -= 1;
                    }

                    //remove tabpage status
                    RemoveTabPageInfo(PreviousSelectedTabpage);

                    //remove tab page
                    TabControl.TabPages.Remove(tabPage);

                    break;
                }
            }
        }


        private static void TabControl_OnDeselecting(object sender, TabControlCancelEventArgs e)
        {
            //Keep trach of the previous selected tabpage
            if (e.TabPage != null)
                PreviousSelectedTabpage = TabControl.SelectedTab;
        }

        #endregion


        #region TabControl Add/Remove

        public static TabPage CreateNewTabPage(String tabName)
        {
            TabPage newTabPage = new TabPage(tabName);
            InitTabPageInfo(newTabPage);

            //Create the textbox inside
            //TypingArea typingArea = new TypingArea();
            //newTabPage.Controls.Add(typingArea);
            //typingArea.Dock = DockStyle.Fill;
            MyRichTextBox myRichText = new MyRichTextBox();
            newTabPage.Controls.Add(myRichText);
            myRichText.Dock = DockStyle.Fill;
            TabControl.TabPages.Add(newTabPage);
            //Switch to the new tabpage
            TabControl.SelectedTab = newTabPage;

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
                    if (tabPageInfo.TabPage == TabControl.SelectedTab)
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
