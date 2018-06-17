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
    public partial class AboutEasyType : Form
    {
        public AboutEasyType()
        {
            InitializeComponent();
        }

        private void AboutEasyType_Load(object sender, EventArgs e)
        {

        }

        public void ShowAboutEasyType()
        {
            this.Text = "About EasyType";
            abtEasyType.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            this.Width = 344;
            this.Height = 164;
            this.Show();
        }

        private void AboutEasyType_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0.3;
            }
            catch { }
        }

        private void AboutEasyType_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void abtEasyType_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AboutEasyType_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
