namespace GUI
{
    partial class FindingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextbox = new System.Windows.Forms.TextBox();
            this.replacementLabel = new System.Windows.Forms.Label();
            this.replacementTextbox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.findNextButton = new System.Windows.Forms.Button();
            this.replaceAllButton = new System.Windows.Forms.Button();
            this.findPreviousButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search..";
            // 
            // searchTextbox
            // 
            this.searchTextbox.Location = new System.Drawing.Point(16, 30);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(251, 20);
            this.searchTextbox.TabIndex = 1;
            this.searchTextbox.TextChanged += new System.EventHandler(this.searchTextbox_TextChanged);
            // 
            // replacementLabel
            // 
            this.replacementLabel.AutoSize = true;
            this.replacementLabel.Location = new System.Drawing.Point(13, 66);
            this.replacementLabel.Name = "replacementLabel";
            this.replacementLabel.Size = new System.Drawing.Size(94, 13);
            this.replacementLabel.TabIndex = 2;
            this.replacementLabel.Text = "Replacement Text";
            // 
            // replacementTextbox
            // 
            this.replacementTextbox.Location = new System.Drawing.Point(16, 83);
            this.replacementTextbox.Name = "replacementTextbox";
            this.replacementTextbox.Size = new System.Drawing.Size(251, 20);
            this.replacementTextbox.TabIndex = 3;
            this.replacementTextbox.TextChanged += new System.EventHandler(this.replacementTextbox_TextChanged);
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(273, 28);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(93, 23);
            this.findButton.TabIndex = 4;
            this.findButton.Text = "&Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.Location = new System.Drawing.Point(372, 27);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(93, 23);
            this.replaceButton.TabIndex = 5;
            this.replaceButton.Text = "&Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // findNextButton
            // 
            this.findNextButton.Location = new System.Drawing.Point(273, 56);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(93, 23);
            this.findNextButton.TabIndex = 6;
            this.findNextButton.Text = "Find &Next";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.findNextButton_Click);
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Location = new System.Drawing.Point(372, 56);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(93, 23);
            this.replaceAllButton.TabIndex = 7;
            this.replaceAllButton.Text = "Replace &All";
            this.replaceAllButton.UseVisualStyleBackColor = true;
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // findPreviousButton
            // 
            this.findPreviousButton.Location = new System.Drawing.Point(273, 85);
            this.findPreviousButton.Name = "findPreviousButton";
            this.findPreviousButton.Size = new System.Drawing.Size(93, 23);
            this.findPreviousButton.TabIndex = 8;
            this.findPreviousButton.Text = "Find &Previous";
            this.findPreviousButton.UseVisualStyleBackColor = true;
            this.findPreviousButton.Click += new System.EventHandler(this.findPreviousButton_Click);
            // 
            // FindingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 131);
            this.Controls.Add(this.findPreviousButton);
            this.Controls.Add(this.replaceAllButton);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.replacementTextbox);
            this.Controls.Add(this.replacementLabel);
            this.Controls.Add(this.searchTextbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindingForm";
            this.Text = "Find And Replace";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindingForm_FormClosing);
            this.Load += new System.EventHandler(this.FindingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox searchTextbox;
        private System.Windows.Forms.Label replacementLabel;
        private System.Windows.Forms.TextBox replacementTextbox;
        internal System.Windows.Forms.Button findButton;
        internal System.Windows.Forms.Button replaceButton;
        internal System.Windows.Forms.Button findNextButton;
        internal System.Windows.Forms.Button replaceAllButton;
        internal System.Windows.Forms.Button findPreviousButton;
    }
}