using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AE.Graphics
{
    public partial class CheckBoxSelectionForm<T> : AEForm
    {

        private Dictionary<CheckBox, T> boxes = new Dictionary<CheckBox, T>();

        public CheckBoxSelectionForm(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItmes)
        {
            init(text, frameTitle, selectionTitle, selectionItmes, x => x.ToString());
        }

        public CheckBoxSelectionForm(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItems, Func<T, string> itemDescription)
        {
            init(text, frameTitle, selectionTitle, selectionItems, itemDescription);
        }


        private void init(string text, string frameTitle, string selectionTitle, IEnumerable<T> selectionItems, Func<T, string> itemDescription)
        {
            if (selectionItems.Count() == 0)
            {
                throw new ArgumentException("AECheckBoxSelectionForm needs at least one selectable Item");
            }
            InitializeComponent();
            foreach (var ent in selectionItems)
            {
                var cb = new CheckBox();
                cb.Text = itemDescription(ent);
                boxes.Add(cb, ent);
                
                cb.MaximumSize = new Size(280, 0);
                cb.AutoSize = true;
                cb.Size = new System.Drawing.Size(29, 13);
                flowLayoutPanel1.Controls.Add(cb);
            }
            Text = frameTitle;
            label1.Text = text;
            groupBox1.Text = selectionTitle;
            
        }
        public List<T> Selection
        {
            get
            {
                return boxes.Where(x => x.Key.Checked).Select(x => x.Value).ToList();
            }

        }

    }
}
