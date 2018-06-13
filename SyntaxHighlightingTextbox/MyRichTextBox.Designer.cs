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
            this.lineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.typingArea = new SyntaxHighlightingTextbox.TypingArea();
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
            this.lineNumberTextBox.Size = new System.Drawing.Size(113, 532);
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
            this.typingArea.EnableAutoComplete = true;
            this.typingArea.EnableHighlight = true;
            this.typingArea.Font = new System.Drawing.Font("Courier New", 12F);
            this.typingArea.Location = new System.Drawing.Point(113, 0);
            this.typingArea.Name = "typingArea";
            this.typingArea.Size = new System.Drawing.Size(488, 532);
            this.typingArea.TabIndex = 1;
            this.typingArea.Text = "";
            this.typingArea.TextChanged += new System.EventHandler(this.TypingArea_TextChanged);
            // 
            // MyRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.typingArea);
            this.Controls.Add(this.lineNumberTextBox);
            this.Name = "MyRichTextBox";
            this.Size = new System.Drawing.Size(601, 532);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox lineNumberTextBox;
        private SyntaxHighlightingTextbox.TypingArea typingArea;

        public System.Windows.Forms.RichTextBox LineNumberTextBox
        {
            get { return lineNumberTextBox; }
            set { lineNumberTextBox = value; }
        }
        public SyntaxHighlightingTextbox.TypingArea TypingArea
        {
            get { return typingArea; }
            set { typingArea = value; }
        }
    }
}
