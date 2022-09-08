using System;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class QuestionBox : AEForm 
    {

        public enum MessageSymbol {
         Error, Warning
        }

        public QuestionBox(string text, string title, string yesText, string noText)
        {
            this.TopMost = true;
            InitializeComponent();
            label1.Text = text;           
            label1.AutoSize = true;
            Text = title;
            button2.Text = yesText;
            button1.Text = noText;
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
