using System;
using System.Drawing;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class SpecialMessageBox : AEForm
    {
        public SpecialMessageBox(string msg, string title, Icon icon)
        {
            InitializeComponent();
            label1.Text = msg;
            Text = title;
            pictureBox1.Image = icon.ToBitmap();
            this.TopMost = true;

        }

        private void AESpecialMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
