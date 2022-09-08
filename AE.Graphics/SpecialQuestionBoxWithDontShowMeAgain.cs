using System;
using System.Drawing;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class SpecialQuestionBoxWithDontShowMeAgain : AEForm
    {
        public SpecialQuestionBoxWithDontShowMeAgain(string msg, string title, string yesText, string noText, Icon icon)
        {
            this.TopMost = true;
            InitializeComponent();
            label1.Text = msg;
            Text = title;
            pictureBox1.Image = icon.ToBitmap();
            button1.Text = yesText;
            button2.Text = noText;
            checkBox1.Text = "Dont ask me again";

        }

        public bool ShowAgain
        {
            get
            {
                return checkBox1.Checked;
            }
            
        }
        private void AESpecialMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
