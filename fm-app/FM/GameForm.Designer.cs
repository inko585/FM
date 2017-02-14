using AE.Graphics;

namespace FM.UI
{
    partial class GameForm
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.areaBar = new System.Windows.Forms.TrackBar();
            this.away = new AE.Graphics.AEListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.home = new AE.Graphics.AEListView();
            this.events = new AE.Graphics.AEListView();
            ((System.ComponentModel.ISupportInitialize)(this.areaBar)).BeginInit();
            this.SuspendLayout();
            // 
            // areaBar
            // 
            this.areaBar.Enabled = false;
            this.areaBar.Location = new System.Drawing.Point(12, 404);
            this.areaBar.Maximum = 8;
            this.areaBar.Name = "areaBar";
            this.areaBar.Size = new System.Drawing.Size(752, 45);
            this.areaBar.TabIndex = 0;
            this.areaBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // away
            // 
            this.away.CheckBoxes = true;
            this.away.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.away.FullRowSelect = true;
            this.away.GridLines = true;
            this.away.Location = new System.Drawing.Point(577, 55);
            this.away.Name = "away";
            this.away.OwnerDraw = true;
            this.away.ShowItemToolTips = true;
            this.away.Size = new System.Drawing.Size(187, 315);
            this.away.TabIndex = 5;
            this.away.UseCompatibleStateImageBehavior = false;
            this.away.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 0;
            this.columnHeader1.Text = "Player";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Level";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Rating";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(689, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // home
            // 
            this.home.CheckBoxes = true;
            this.home.FullRowSelect = true;
            this.home.GridLines = true;
            this.home.Location = new System.Drawing.Point(377, 55);
            this.home.Name = "home";
            this.home.OwnerDraw = true;
            this.home.ShowItemToolTips = true;
            this.home.Size = new System.Drawing.Size(194, 315);
            this.home.TabIndex = 7;
            this.home.UseCompatibleStateImageBehavior = false;
            this.home.View = System.Windows.Forms.View.Details;
            // 
            // events
            // 
            this.events.CheckBoxes = true;
            this.events.FullRowSelect = true;
            this.events.GridLines = true;
            this.events.Location = new System.Drawing.Point(12, 55);
            this.events.Name = "events";
            this.events.OwnerDraw = true;
            this.events.ShowItemToolTips = true;
            this.events.Size = new System.Drawing.Size(359, 315);
            this.events.TabIndex = 8;
            this.events.UseCompatibleStateImageBehavior = false;
            this.events.View = System.Windows.Forms.View.Details;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 506);
            this.Controls.Add(this.events);
            this.Controls.Add(this.home);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.away);
            this.Controls.Add(this.areaBar);
            this.Name = "GameForm";
            this.Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)(this.areaBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar areaBar;
        private AEListView away;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button1;
        private AEListView home;
        private AEListView events;
    }
}