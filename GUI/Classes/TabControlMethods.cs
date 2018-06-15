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
    static partial class TabControlMethods 
    {
        //the "x" image 
        private static Image closeImage;

        //A variable refer on current handling TabControl.
        private static TabControl tabControl;

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
            //Store the reference of _tabControl
            TabControl = _tabControl;

            //Allow drop
            TabControl.AllowDrop = true;
            
            //Allow to custom the way tab control are drawn through DrawItem event raising
            //whenever the tab redraw
            TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;

            //Set size of each tab page.
            TabControl.Padding = new Point(15, 4);

            //Get the "x" image 
            closeImage = Properties.Resources.CloseImage;

            //Draw the "x" button
            TabControl.DrawItem += TabControl_OnDrawItem;

            //Click "x" button to close the tab page.
            TabControl.MouseDown += TabControl_OnMouseDown;

            //To track the previous TabPage
            TabControl.Deselecting += TabControl_OnDeselecting;

            //Allow to do drag and drop when mouse move around
            TabControl.MouseMove += TabControl_DragDrop;

            //Dragging event
            TabControl.DragOver += TabControl_DragTab;
            
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


        private static void TabControl_OnMouseDown(object sender, MouseEventArgs e)
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

            if (TabControl.TabPages.Count > 0)
            {
                CurrentTextArea.Focus();
            }

          }


        private static void TabControl_OnDeselecting(object sender, TabControlCancelEventArgs e)
        {
            //Keep trach of the previous selected tabpage
            if (e.TabPage != null)
                PreviousSelectedTabpage = TabControl.SelectedTab;
        }


        private static void TabControl_DragDrop(object sender, MouseEventArgs e)
        {
            //Just do when the left-mouse is clicked
            if (e.Button != MouseButtons.Left || TabControl.TabPages.Count == 0) return;
            tabControl.DoDragDrop(tabControl.SelectedTab, DragDropEffects.All);
        }


        private static void TabControl_DragTab(object sender, DragEventArgs e)
        {
            //If the thing dragged over isn't a Tab
            if (!e.Data.GetDataPresent(typeof(TabPage))) return;

            //Get the tab being dragged
            TabPage draggedTab = (TabPage)e.Data.GetData(typeof(TabPage));

            //Get the index of the tab being dragged
            int draggedTabIndex = TabControl.TabPages.IndexOf(draggedTab);

            //Get the index of the tab having the mouse on it
            int hoveredTabIndex = GetHoverTabIndex();

            //If the mouse is not on any Tab
            if (hoveredTabIndex < 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            //Change the type of cursor showing while dragging
            e.Effect = DragDropEffects.Move;

            TabPage hoveredTab = TabControl.TabPages[hoveredTabIndex];

            if (draggedTab == hoveredTab)
                return;

            SwapTabPages(draggedTab, hoveredTab);

            //Set the selected tab to the dragged tab
            TabControl.SelectedTab = draggedTab;
            CurrentTextArea.Focus();
        }
               

        #endregion


        #region TabControl Add/Remove

        public static TabPage CreateNewTabPage(String tabName)
        {
            TabPage newTabPage = new TabPage(tabName);
            InitTabPageInfo(newTabPage);
            
            //Add the MyRichTextBox inside
            MyRichTextBox myRichText = new MyRichTextBox();
            newTabPage.Controls.Add(myRichText);
            myRichText.Dock = DockStyle.Fill;

            TabControl.TabPages.Add(newTabPage);

            //Switch to the new tabpage
            TabControl.SelectedTab = newTabPage;

            return newTabPage;
        }

        #endregion


        #region Some other methods

        /// <summary>
        /// Swap two Tab pages
        /// </summary>
        /// <param name="srcTab">The first tab</param>
        /// <param name="dstTab">The second tab</param>
        private static void SwapTabPages(TabPage srcTab, TabPage dstTab)
        {
            int indexSrc = TabControl.TabPages.IndexOf(srcTab);
            int indexDst = TabControl.TabPages.IndexOf(dstTab);
            TabControl.TabPages[indexDst] = srcTab;
            TabControl.TabPages[indexSrc] = dstTab;
        }


        /// <summary>
        /// Get the index of the tab having the mouse on it
        /// </summary>
        /// <returns></returns>
        private static int GetHoverTabIndex()
        {
            for (int i = 0; i < TabControl.TabPages.Count; i++)
            {
                Rectangle tabArea = TabControl.GetTabRect(i);
                if (tabArea.Contains(TabControl.PointToClient(Cursor.Position)))
                    return i;
            }

            //The mouse is not on the Tab
            return -1;
        }

        #endregion


        #region Manipulate the TabPageInfo

        static TabPageInfoCollection listOfTabPageInfo = new TabPageInfoCollection();

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
            listOfTabPageInfo.Add(tabPageInfo);
        }

        /// <summary>
        /// Delete the tab page info.
        /// </summary>
        /// <param name="tabPage"></param>
        public static bool RemoveTabPageInfo(TabPage tabPage)
        {
            //Locate the tab page info to remove.
            TabPageInfo tabInfoToDelelte = null;
            foreach (TabPageInfo tabPageInfo in listOfTabPageInfo)
            {
                if (tabPageInfo.TabPage == tabPage)
                {
                    tabInfoToDelelte = tabPageInfo;
                    break;
                }
            }

            return listOfTabPageInfo.Remove(tabInfoToDelelte);            
        }

        #endregion
        
    }
}
