using System;
using System.Windows.Forms;

namespace AE.Graphics
{
    public class AETreeView : TreeView
    {
        public AETreeView() : base()
        {
            AfterCheck += afterCheck;
            BeforeCheck += beforeCheck;
        }

        protected override void WndProc(ref Message m)
        {
            // Suppress WM_LBUTTONDBLCLK
            if (m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }

        private void beforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node is AETreeNode && !(e.Node as AETreeNode).Enabled)
            {
                e.Cancel = true;
            }
        }

        private void afterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (!e.Node.Checked && e.Node.Parent != null)
                {
                    e.Node.Parent.Checked = false;
                }


                adjustChildren(e.Node);

            }
        }

        private void adjustChildren(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
            }
        }

    }

    public class AETreeNode : TreeNode
    {
        public AETreeNode ()
        {

        }
        private bool enabled = true;
        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                if (value == false)
                {
                    ForeColor = System.Drawing.SystemColors.GrayText;

                }
                else
                {
                    ForeColor = System.Drawing.Color.Black;
                }
                enabled = value;
            }
        }

        public AETreeNode(string txt) : base(txt)
        {

        }
    }
}
