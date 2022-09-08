using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE.Graphics
{
    public class AEGridView : DataGridView
    {
        public AEGridView()
        {
            CurrentCellDirtyStateChanged += new EventHandler(currentCellDirtyStateChanged);
            
        }

        public bool IsEmpty()
        {
            var tmp = AllowUserToAddRows;
            AllowUserToAddRows = false;
            var ret = Rows.Count == 0;
            AllowUserToAddRows = tmp;
            return ret;            
        }


        private void currentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (CurrentCell != null)
            {
                DataGridViewColumn col = Columns[CurrentCell.ColumnIndex];
                if (col is DataGridViewComboBoxColumn)
                {
                    CommitEdit(DataGridViewDataErrorContexts.Commit);
                    //CurrentCell.Value = ((DataGridViewComboBoxEditingControl)sender).SelectedValue;
                    EndEdit();
                }
            }
        }


    }
}
