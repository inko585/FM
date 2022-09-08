using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class RadioSelectionForm<T> : AEForm
    {

        private Dictionary<RadioButton, T> buttons = new Dictionary<RadioButton, T>();

        public RadioSelectionForm(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItmes)
        {
            init(text, frameTitle, selectionTitle, selectionItmes, x => x.ToString());
        }

        public RadioSelectionForm(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItems, Func<T, string> itemDescription)
        {
            init(text, frameTitle, selectionTitle, selectionItems, itemDescription);
        }


        private void init(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItems, Func<T, string> itemDescription)
        {
            if (selectionItems.Count() == 0)
            {
                throw new ArgumentException("AERadioSelectionForm needs at least one selectable Item");
            }
            InitializeComponent();
            foreach (var ent in selectionItems)
            {
                var rb = new RadioButton();
                rb.Text = itemDescription(ent);
                buttons.Add(rb, ent);
                
                rb.MaximumSize = new Size(280, 0);
                rb.AutoSize = true;
                rb.Size = new System.Drawing.Size(29, 13);
                flowLayoutPanel1.Controls.Add(rb);
            }
            buttons.First().Key.Checked = true;
            Text = frameTitle;
            label1.Text = text;
            groupBox1.Text = selectionTitle;
            
        }
        public T Selection
        {
            get
            {
                return buttons.Where(x => x.Key.Checked).First().Value;
            }

        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AERadioSelectionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
