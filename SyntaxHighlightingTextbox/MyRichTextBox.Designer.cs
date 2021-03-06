﻿namespace SyntaxHighlightingTextbox
{
    partial class MyRichTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lineNumberTextBox = new LineNumbering();
            this.typingArea = new TypingArea();
            this.documentMap = new DocumentMap();
            this.foldfinder = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lineNumberTextBox
            // 
            this.lineNumberTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.lineNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lineNumberTextBox.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.lineNumberTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.lineNumberTextBox.Font = new System.Drawing.Font("Courier New", 12F);
            this.lineNumberTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lineNumberTextBox.Location = new System.Drawing.Point(0, 0);
            this.lineNumberTextBox.Name = "lineNumberTextBox";
            this.lineNumberTextBox.ReadOnly = true;
            this.lineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lineNumberTextBox.Size = new System.Drawing.Size(64, 532);
            this.lineNumberTextBox.TabIndex = 0;
            this.lineNumberTextBox.Text = "";
            this.lineNumberTextBox.ZoomFactor = 1.4F;
            this.lineNumberTextBox.TextChanged += new System.EventHandler(this.lineNumberTextBox_TextChanged);
            // 
            // typingArea
            // 
            this.typingArea.AcceptsTab = true;
            this.typingArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.typingArea.CaseSensitive = true;
            this.typingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typingArea.DocumentMap = null;
            this.typingArea.EnableAutoComplete = true;
            this.typingArea.EnableHighlight = true;
            this.typingArea.Font = new System.Drawing.Font("Courier New", 12F);
            this.typingArea.Location = new System.Drawing.Point(81, 0);
            this.typingArea.Name = "typingArea";
            this.typingArea.Size = new System.Drawing.Size(423, 532);
            this.typingArea.TabIndex = 1;
            this.typingArea.Text = "";
            this.typingArea.TextChanged += new System.EventHandler(this.OnTypingArea_TextChanged);
            // 
            // documentMap
            // 
            this.documentMap.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.documentMap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.documentMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.documentMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentMap.Location = new System.Drawing.Point(504, 0);
            this.documentMap.Name = "documentMap";
            this.documentMap.ReadOnly = true;
            this.documentMap.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.documentMap.Size = new System.Drawing.Size(97, 532);
            this.documentMap.TabIndex = 2;
            this.documentMap.Text = "";
            this.documentMap.TypingArea = this.typingArea;
            // 
            // foldfinder
            // 
            this.foldfinder.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.foldfinder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.foldfinder.Dock = System.Windows.Forms.DockStyle.Left;
            this.foldfinder.Location = new System.Drawing.Point(64, 0);
            this.foldfinder.Name = "foldfinder";
            this.foldfinder.ReadOnly = true;
            this.foldfinder.Size = new System.Drawing.Size(17, 532);
            this.foldfinder.TabIndex = 3;
            this.foldfinder.Text = "";
            // 
            // MyRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.typingArea);
            this.Controls.Add(this.foldfinder);
            this.Controls.Add(this.lineNumberTextBox);
            this.Controls.Add(this.documentMap);
            this.Name = "MyRichTextBox";
            this.Size = new System.Drawing.Size(601, 532);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.RichTextBox foldfinder;


        #endregion


        //At first i put everything below here, after some refactoring work i put them in MyRichTextBox.cs

        //private System.Windows.Forms.RichTextBox lineNumberTextBox;
        //private SyntaxHighlightingTextbox.LineNumbering lineNumberTextBox;
        //private SyntaxHighlightingTextbox.TypingArea typingArea;

        //public SyntaxHighlightingTextbox.LineNumbering LineNumberTextBox
        //{
        //    get { return lineNumberTextBox; }
        //    set { lineNumberTextBox = value; }
        //}
        //public SyntaxHighlightingTextbox.TypingArea TypingArea
        //{
        //    get { return typingArea; }
        //    set { typingArea = value; }
        //}
    }
}
