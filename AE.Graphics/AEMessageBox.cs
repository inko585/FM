using System;
using System.Drawing;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class AEMessageBox : AEForm 
    {

        public enum MessageSymbol {
         Error, Warning
        }

        public AEMessageBox(string text, string title)
        {
            this.TopMost = true;
            InitializeComponent();
            label1.Text = text;            
            label1.AutoSize = true;
            Text = title;
        }

        private static void DialogSettings(Form ins)
        {
            ins.FormBorderStyle = FormBorderStyle.FixedSingle;
            ins.StartPosition = AEGraphics.DialogStartPosition;
        }


        public static void Show(string text)
        {
            var ins = new AEMessageBox(text, "autoelectric");
            DialogSettings(ins);
            ins.Show();
        }

        public static void ShowDialog(string text)
        {
            var ins = new AEMessageBox(text, "");
            ins.FormBorderStyle = FormBorderStyle.FixedSingle;
            ins.ShowDialog();
        }


        public static void Show(string msg, string title)
        {
            var ins = new AEMessageBox(msg, title);
            DialogSettings(ins);
            ins.Show();
        }

        public static void ShowDialog(string msg, string title)
        {
            var ins = new AEMessageBox(msg, title);
            DialogSettings(ins);
            ins.ShowDialog();
        }


        public static void ShowDialog(string msg, string title, MessageSymbol symbol) {

            var ins = getSpecialMessageBox(msg, title, symbol);
            DialogSettings(ins);
            ins.Refresh();
            ins.ShowDialog();

            
        }

        public static bool Ask(string msg, string title, string yesText, string noText, MessageSymbol symbol, out bool showAgain)
        {
            SpecialQuestionBoxWithDontShowMeAgain ins = getSpecialQuestionBoxWithDontShowMeAgain(msg, title, yesText, noText, symbol);
            DialogSettings(ins);
            if (ins.ShowDialog() == DialogResult.Yes)
            {
                showAgain = ins.ShowAgain;
                return true;
            }
            else
            {
                showAgain = ins.ShowAgain;
                return false;
            }
        }

        public static bool Ask (string msg, string title, string yesText, string noText, MessageSymbol symbol)
        {
            SpecialQuestionBox ins = getSpecialQuestionBox(msg, title, yesText, noText, symbol);
            DialogSettings(ins);
            if (ins.ShowDialog() == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Ask(string msg, string title, string yesText, string noText)
        {
            var ins = new QuestionBox(msg, title, yesText, noText);
            DialogSettings(ins);
            if (ins.ShowDialog() == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Show(string msg, string title, MessageSymbol symbol)
        {
            SpecialMessageBox ins = getSpecialMessageBox(msg, title, symbol);
            DialogSettings(ins);
            ins.Show();
        }

        private static SpecialMessageBox getSpecialMessageBox(string msg, string title, MessageSymbol symbol)
        {
            SpecialMessageBox ins = null;
            switch (symbol)
            {
                case MessageSymbol.Warning: ins = new SpecialMessageBox(msg, title, SystemIcons.Warning);
                    break;
                case MessageSymbol.Error: ins = new SpecialMessageBox(msg, title, SystemIcons.Error);
                    break;
                default: throw new ArgumentException("Unknown Symbol: " + symbol);
            }
            return ins;
        }

        private static SpecialQuestionBox getSpecialQuestionBox(string msg, string title, string yesText, string noText, MessageSymbol symbol)
        {
            SpecialQuestionBox ins = null;
            switch (symbol)
            {
                case MessageSymbol.Warning: ins = new SpecialQuestionBox(msg, title, yesText, noText, SystemIcons.Warning);
                    break;
                case MessageSymbol.Error: ins = new SpecialQuestionBox(msg, title, yesText, noText, SystemIcons.Error);
                    break;
                default: throw new ArgumentException("Unknown Symbol: " + symbol);
            }
            return ins;
        }

        private static SpecialQuestionBoxWithDontShowMeAgain getSpecialQuestionBoxWithDontShowMeAgain(string msg, string title, string yesText, string noText, MessageSymbol symbol)
        {
            SpecialQuestionBoxWithDontShowMeAgain ins = null;
            switch (symbol)
            {
                case MessageSymbol.Warning:
                    ins = new SpecialQuestionBoxWithDontShowMeAgain(msg, title, yesText, noText, SystemIcons.Warning);
                    break;
                case MessageSymbol.Error:
                    ins = new SpecialQuestionBoxWithDontShowMeAgain(msg, title, yesText, noText, SystemIcons.Error);
                    break;
                default: throw new ArgumentException("Unknown Symbol: " + symbol);
            }
            return ins;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
