namespace GUI
{
    partial class Form2
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
            this.rightMargin1 = new SyntaxHighlightingTextbox.RightMargin(this.components);
            this.typingArea1 = new SyntaxHighlightingTextbox.TypingArea();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // typingArea1
            // 
            this.typingArea1.AcceptsTab = true;
            this.typingArea1.CaseSensitive = false;
            this.typingArea1.EnableAutoComplete = true;
            this.typingArea1.EnableHighlight = true;
            this.typingArea1.Location = new System.Drawing.Point(75, 43);
            this.typingArea1.Name = "typingArea1";
            this.typingArea1.Size = new System.Drawing.Size(229, 347);
            this.typingArea1.TabIndex = 0;
            this.typingArea1.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(425, 82);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(277, 343);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.typingArea1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private SyntaxHighlightingTextbox.RightMargin rightMargin1;
        private SyntaxHighlightingTextbox.TypingArea typingArea1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}