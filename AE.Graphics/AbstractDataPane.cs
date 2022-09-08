using System;
using System.Windows.Forms;

namespace AE.Graphics
{
    public  partial class AbstractDataPane : UserControl
    {
        public AbstractDataPane()
        {
            InitializeComponent();


        }

        

        
        public Action<AbstractDataPane> OnDelete { get; set; }


        private void button1_Click(object sender, EventArgs e)
        {

            OnDelete(this);
        }

        public virtual void ClearInput()
        {

        }

        public virtual void AddInput(object[] content)
        {
            
        }
    }




}
