using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using SyntaxHighlightingTextbox;

namespace MaterialNotepad
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            this.bMenu.MouseHover += Menu_Mouseover;
            this.bNew.MouseHover += New_Mouseover;
            this.bOpen.MouseHover += Open_Mouseover;
            this.bSave.MouseHover += Save_Mouseover;
            this.bSaveas.MouseHover += Saveas_Mouseover;
            this.bSaveall.MouseHover += Saveall_Mouseover;

            bMenu.MouseEnter += OnMouseEnter;
            bMenu.MouseLeave += OnMouseLeave;
            bNew.MouseEnter += OnMouseEnter;
            bNew.MouseLeave += OnMouseLeave;
            bOpen.MouseEnter += OnMouseEnter;
            bOpen.MouseLeave += OnMouseLeave;
            bSave.MouseEnter += OnMouseEnter;
            bSave.MouseLeave += OnMouseLeave;
            bSaveas.MouseEnter += OnMouseEnter;
            bSaveas.MouseLeave += OnMouseLeave;
            bSaveall.MouseEnter += OnMouseEnter;
            bSaveall.MouseLeave += OnMouseLeave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typingArea1.Focus();
            typingArea1.Separators.Add(' ');
            typingArea1.Separators.Add('\r');
            typingArea1.Separators.Add('\n');
            typingArea1.Separators.Add(',');
            typingArea1.Separators.Add('.');
            typingArea1.Separators.Add('-');
            typingArea1.Separators.Add('+');
            //shtb.Seperators.Add('*');
            //shtb.Seperators.Add('/');
            typingArea1.WordWrap = false;
            typingArea1.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            typingArea1.Descriptors.Add(new HighlightDescriptor("Hello", Color.Red, this.Font, HighlightType.ToEOW, DescriptorRecognition.WholeWord, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("", Color.Green, null, HighlightType.ToEOW, DescriptorRecognition.IsNumber, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("/*", "*/", HighlightType.ToCloseToken, DescriptorRecognition.StartsWith, Color.Green,
                                                                null, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("Nghiangu", Color.Red, this.Font, HighlightType.ToEOW, DescriptorRecognition.WholeWord, false));
            typingArea1.Descriptors.Add(new HighlightDescriptor("Quangdepzai", Color.Red, this.Font, HighlightType.ToEOW, DescriptorRecognition.WholeWord, false));

        }
        private void Menu_Mouseover(object sender, EventArgs e)
        {
            ttMenu.Show("Show the menu", bMenu);
        }
        private void New_Mouseover(object sender, EventArgs e)
        {
            ttNew.Show("Create a new item", bNew);
        }
        private void Open_Mouseover(object sender, EventArgs e)
        {
            ttOpen.Show("Open another file", bOpen);
        }
        private void Save_Mouseover(object sender, EventArgs e)
        {
            ttSave.Show("Save current file", bSave);
        }
        private void Saveas_Mouseover(object sender, EventArgs e)
        {
            ttSaveas.Show("Save current file as another file", bSaveas);
        }
        private void Saveall_Mouseover(object sender, EventArgs e)
        {
            ttSaveall.Show("Save all opening file", bSaveall);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ButtonHighlight;
        }
        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ButtonFace;
        }
    }
}
