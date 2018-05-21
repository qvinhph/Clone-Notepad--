using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.mMain = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openCointainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pythonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.bMenu = new System.Windows.Forms.Button();
            this.bNew = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bSaveas = new System.Windows.Forms.Button();
            this.bSaveall = new System.Windows.Forms.Button();
            this.ttMenu = new System.Windows.Forms.ToolTip(this.components);
            this.ttNew = new System.Windows.Forms.ToolTip(this.components);
            this.ttOpen = new System.Windows.Forms.ToolTip(this.components);
            this.ttSave = new System.Windows.Forms.ToolTip(this.components);
            this.ttSaveas = new System.Windows.Forms.ToolTip(this.components);
            this.ttSaveall = new System.Windows.Forms.ToolTip(this.components);
            this.bNewtab = new System.Windows.Forms.Button();
            this.mMain.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.White;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(1, 141);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(30, 270);
            this.materialDivider1.TabIndex = 1;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialDivider2
            // 
            this.materialDivider2.BackColor = System.Drawing.Color.White;
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(5, 68);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(775, 36);
            this.materialDivider2.TabIndex = 2;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // mMain
            // 
            this.mMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mMain.Depth = 0;
            this.mMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mMain.MouseState = MaterialSkin.MouseState.HOVER;
            this.mMain.Name = "materialContextMenuStrip1";
            this.mMain.Size = new System.Drawing.Size(127, 180);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.openCointainingFolderToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.openToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.newToolStripMenuItem1.Text = "New";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // openCointainingFolderToolStripMenuItem
            // 
            this.openCointainingFolderToolStripMenuItem.Name = "openCointainingFolderToolStripMenuItem";
            this.openCointainingFolderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openCointainingFolderToolStripMenuItem.Text = "Open Cointaining Folder";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.newToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.tabToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveToolStripMenuItem.Text = "View";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // tabToolStripMenuItem
            // 
            this.tabToolStripMenuItem.Name = "tabToolStripMenuItem";
            this.tabToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.tabToolStripMenuItem.Text = "Tab";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalTextToolStripMenuItem,
            this.cToolStripMenuItem,
            this.cToolStripMenuItem1,
            this.pythonToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // normalTextToolStripMenuItem
            // 
            this.normalTextToolStripMenuItem.Name = "normalTextToolStripMenuItem";
            this.normalTextToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.normalTextToolStripMenuItem.Text = "Normal text";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cToolStripMenuItem.Text = "C#";
            // 
            // cToolStripMenuItem1
            // 
            this.cToolStripMenuItem1.Name = "cToolStripMenuItem1";
            this.cToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.cToolStripMenuItem1.Text = "C";
            // 
            // pythonToolStripMenuItem
            // 
            this.pythonToolStripMenuItem.Name = "pythonToolStripMenuItem";
            this.pythonToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.pythonToolStripMenuItem.Text = "Python";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.materialTabSelector1.Location = new System.Drawing.Point(1, 110);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(779, 23);
            this.materialTabSelector1.TabIndex = 5;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(343, 276);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(743, 270);
            this.materialTabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(735, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(6, 6);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(725, 232);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // bMenu
            // 
            this.bMenu.BackColor = System.Drawing.Color.White;
            this.bMenu.ContextMenuStrip = this.mMain;
            this.bMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMenu.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bMenu.FlatAppearance.BorderSize = 0;
            this.bMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMenu.Location = new System.Drawing.Point(1, 68);
            this.bMenu.Name = "bMenu";
            this.bMenu.Size = new System.Drawing.Size(39, 36);
            this.bMenu.TabIndex = 7;
            this.bMenu.UseVisualStyleBackColor = false;
            this.bMenu.Click += new System.EventHandler(this.OpenMenu);
            // 
            // bNew
            // 
            this.bNew.BackColor = System.Drawing.Color.White;
            this.bNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bNew.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bNew.FlatAppearance.BorderSize = 0;
            this.bNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNew.Location = new System.Drawing.Point(41, 68);
            this.bNew.Name = "bNew";
            this.bNew.Size = new System.Drawing.Size(39, 36);
            this.bNew.TabIndex = 7;
            this.bNew.UseVisualStyleBackColor = false;
            this.bNew.Click += new System.EventHandler(this.New);
            // 
            // bOpen
            // 
            this.bOpen.BackColor = System.Drawing.Color.White;
            this.bOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bOpen.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bOpen.FlatAppearance.BorderSize = 0;
            this.bOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpen.Location = new System.Drawing.Point(86, 68);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(39, 36);
            this.bOpen.TabIndex = 7;
            this.bOpen.UseVisualStyleBackColor = false;
            this.bOpen.Click += new System.EventHandler(this.OpenItems);
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.Color.White;
            this.bSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bSave.FlatAppearance.BorderSize = 0;
            this.bSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Location = new System.Drawing.Point(131, 68);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(39, 36);
            this.bSave.TabIndex = 7;
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.Save);
            // 
            // bSaveas
            // 
            this.bSaveas.BackColor = System.Drawing.Color.White;
            this.bSaveas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSaveas.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bSaveas.FlatAppearance.BorderSize = 0;
            this.bSaveas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bSaveas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bSaveas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSaveas.Location = new System.Drawing.Point(176, 68);
            this.bSaveas.Name = "bSaveas";
            this.bSaveas.Size = new System.Drawing.Size(39, 36);
            this.bSaveas.TabIndex = 7;
            this.bSaveas.UseVisualStyleBackColor = false;
            this.bSaveas.Click += new System.EventHandler(this.Saveas);
            // 
            // bSaveall
            // 
            this.bSaveall.BackColor = System.Drawing.Color.White;
            this.bSaveall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSaveall.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bSaveall.FlatAppearance.BorderSize = 0;
            this.bSaveall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bSaveall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bSaveall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSaveall.Location = new System.Drawing.Point(221, 68);
            this.bSaveall.Name = "bSaveall";
            this.bSaveall.Size = new System.Drawing.Size(39, 36);
            this.bSaveall.TabIndex = 7;
            this.bSaveall.UseVisualStyleBackColor = false;
            this.bSaveall.Click += new System.EventHandler(this.Saveall);
            // 
            // ttMenu
            // 
            this.ttMenu.ToolTipTitle = "Menu";
            // 
            // ttNew
            // 
            this.ttNew.ToolTipTitle = "New";
            // 
            // ttOpen
            // 
            this.ttOpen.ToolTipTitle = "Open";
            // 
            // ttSave
            // 
            this.ttSave.ToolTipTitle = "Save";
            // 
            // ttSaveas
            // 
            this.ttSaveas.Tag = "";
            this.ttSaveas.ToolTipTitle = "Save as";
            // 
            // ttSaveall
            // 
            this.ttSaveall.ToolTipTitle = "Save all";
            // 
            // bNewtab
            // 
            this.bNewtab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.bNewtab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bNewtab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bNewtab.FlatAppearance.BorderSize = 0;
            this.bNewtab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bNewtab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bNewtab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNewtab.ForeColor = System.Drawing.Color.Navy;
            this.bNewtab.Location = new System.Drawing.Point(131, 110);
            this.bNewtab.Name = "bNewtab";
            this.bNewtab.Size = new System.Drawing.Size(24, 23);
            this.bNewtab.TabIndex = 8;
            this.bNewtab.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 560);
            this.Controls.Add(this.bNewtab);
            this.Controls.Add(this.bSaveall);
            this.Controls.Add(this.bSaveas);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.bNew);
            this.Controls.Add(this.bMenu);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.materialDivider1);
            this.Name = "Form1";
            this.Text = "Improved Notepad++";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mMain.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private void OpenMenu(object sender, EventArgs e)
        {
            Button btnsender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnsender.Height);
            ptLowerLeft = btnsender.PointToScreen(ptLowerLeft);
            mMain.Show(ptLowerLeft);

        }


        private void New(object sender, EventArgs e)
        {

        }
        private void OpenItems(object sender, EventArgs e)
        {

        }
        private void Save(object sender, EventArgs e)
        {

        }
        private void Saveas(object sender, EventArgs e)
        {

        }
        private void Saveall(object sender, EventArgs e)
        {

        }
        #endregion
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialContextMenuStrip mMain;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button bMenu;
        private Button bNew;
        private Button bOpen;
        private Button bSave;
        private Button bSaveas;
        private Button bSaveall;
        private ToolTip ttMenu;
        private ToolTip ttNew;
        private ToolTip ttOpen;
        private ToolTip ttSave;
        private ToolTip ttSaveas;
        private ToolTip ttSaveall;
        private Button bNewtab;
        private ToolStripMenuItem newToolStripMenuItem1;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem openCointainingFolderToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem saveAllToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingToolStripMenuItem;
        private ToolStripMenuItem toolToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem tabToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem normalTextToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem1;
        private ToolStripMenuItem pythonToolStripMenuItem;
    }
}

