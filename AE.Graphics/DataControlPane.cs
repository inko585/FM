using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AE.Graphics
{
    public class DataControlPane <A>: UserControl  where A : AbstractDataPane, new()
    {
        

        public class Sorter<C>  where C : Control
        {
            private Func<A, C> getControl;
            private Func<C, IComparable> sortFunc;

            public Sorter(Func<A, C> getControl, Func<C, IComparable> sortCriteria)
            {
                this.getControl = getControl;
                this.sortFunc = sortCriteria;
            }

            public List<A> Sort(List<A> input, bool ascending)
            {
                if (ascending)
                {
                    return input.OrderBy(x => sortFunc(getControl(x))).ToList();
                }
                else
                {
                    return input.OrderByDescending(x => sortFunc(getControl(x))).ToList();
                }
            }

            public Button SortButton (string text)
            {
                var b = new Button();
                var a = new A();
                b.Size = new Size(getControl(a).Width, 23);
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.TextAlign = ContentAlignment.MiddleLeft;
                b.UseVisualStyleBackColor = true;
                b.Text = text;
                return b;
            }
        }

        public  void AddTextBoxSorter(Func<A, TextBox> getControl, string text)
        {
            AddSorter<TextBox>(new Sorter<TextBox>(getControl, x => x.Text), text);
        }

        public  void AddTextComboBoxSorter (Func<A, ComboBox> getControl, string text)
        {
            AddSorter<ComboBox>(new Sorter<ComboBox>(getControl, x => x.SelectedItem.ToString()), text);
        }

        public DataControlPane()
        {
            InitializeComponent();
            AddDataPane();
            
            //foreach (var c in dataPanes.First().Fields)
            //{
            //    var b = new Button();

            //    flowLayoutPanel1.Controls.Add(b);
               
            //}
            
        }

        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private List<AbstractDataPane> dataPanes = new List<AbstractDataPane>();

        public void AddSorter<T>(Sorter <T> s, string text) where T : Control
        {

            this.flowLayoutPanel1.Controls.Add(s.SortButton(text));

        }

        public void AddDataPane()
        {
            var x = new A();
            x.OnDelete = RemoveDataPane;
            x.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.Controls.Add(x);
            dataPanes.Add(x);
      
        }

        public void AddDataPane(params object [] content)
        {
            var x = new A();
            x.AddInput(content);
            x.OnDelete = RemoveDataPane;
            x.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.Controls.Add(x);
            dataPanes.Add(x);
        }

        public void RemoveDataPane(AbstractDataPane p)
        {
            if (dataPanes.Contains(p))
            {
                if (dataPanes.Count > 1)
                {
                    this.tableLayoutPanel1.Controls.Remove(p);
                    this.dataPanes.Remove(p);
                } else
                {
                    this.dataPanes[0].ClearInput();
                }

            } 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 33);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(446, 151);
            this.panel2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 27);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 143);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::AE.Graphics.Properties.Resources.onebit_31;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataControlPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DataControlPane";
            this.Size = new System.Drawing.Size(446, 220);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;

        private void button1_Click(object sender, EventArgs e)
        {
            AddDataPane();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
