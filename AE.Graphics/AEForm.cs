using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AE.Graphics
{

    public class AEForm : Form
    {
        public AEForm()
        {
            Application.EnableVisualStyles();

            this.Icon = AE.Graphics.Properties.Resources.nexans_logo;
            this.Load +=  new System.EventHandler(LoadForm);
            this.FormClosing += new FormClosingEventHandler(Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;         
        }

        private void SaveSize()
        {
            var att = this.GetType().GetCustomAttributes(false).FirstOrDefault(x => x.GetType().Equals(typeof(PersistentSize)));
            if (att != null)
            {
                var perSize = (PersistentSize)att;
                //Registry.SetValue(perSize.RegPath, this.GetType().Name + "X", this.Location.X);
                //Registry.SetValue(perSize.RegPath, this.GetType().Name + "Y", this.Location.Y);
                Registry.SetValue(perSize.RegPath, this.GetType().Name + "Width", this.Size.Width);
                Registry.SetValue(perSize.RegPath, this.GetType().Name + "Height", this.Size.Height);
            }
        }

        private void LoadSize()
        {
            var att = this.GetType().GetCustomAttributes(false).FirstOrDefault(x => x.GetType().Equals(typeof(PersistentSize)));
            if (att != null)
            {
                var perSize = (PersistentSize)att;
                //var X = Registry.GetValue(perSize.RegPath, this.GetType().Name + "X", null);
                //var Y = Registry.GetValue(perSize.RegPath, this.GetType().Name + "Y", null);
                var W = Registry.GetValue(perSize.RegPath, this.GetType().Name + "Width", null);
                var H = Registry.GetValue(perSize.RegPath, this.GetType().Name + "Height", null);
                if ( W != null && H != null)
                {
                    //this.Location = new Point(int.Parse(X.ToString()), int.Parse(Y.ToString()));
                    this.Size = new Size((int) W, (int) H);
                }
            }
        }

        public virtual new void OnLoad(EventArgs e)
        {
            Activate();
            BringToFront();
            LoadSize();
        }

        public virtual void OnClose(EventArgs e)
        {
            SaveSize();
        }

        private void LoadForm (object sender, EventArgs e)
        {
            OnLoad(e);
        }

        private void Close(object sender, EventArgs e)
        {
            OnClose(e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AEForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "AEForm";
            this.ResumeLayout(false);

        }

    }

    public class PersistentSize : Attribute
    {
        public PersistentSize(string regPath)
        {
            RegPath = regPath;
        }
        public string RegPath { get; set; }
    }

    
}
