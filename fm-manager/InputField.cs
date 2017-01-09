using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fm_manager
{
    public partial class InputField : Form
    {
        public InputField(string text)
        {
            InitializeComponent();
            Text = text;
        }
    }
}
