using AE.Graphics;
using FM.Entities.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FM.UI
{
    public partial class GameForm : Form
    {
        private Club HomeClub;
        private Club AwayClub;
        public GameForm(Club home, Club away, int areaCount)
        {
            InitializeComponent();
            HomeClub = home;
            AwayClub = away;
            areaBar.Maximum = areaCount;
            this.home.CheckBoxes = false;
            this.home.Columns.Add("Player", 100);
            this.home.Columns.Add("Level", -2);
            this.home.Columns.Add("Rating", -2);
            this.away.CheckBoxes = false;
            this.events.CheckBoxes = false;
            this.events.Columns.Add("Minute", -2);
            this.events.Columns.Add("Event", -2);

        }

        public void UpdateHomeLineup(LineUp lineup)
        {
            UpdateLineUp(lineup, home);
            Refresh();
        }

        public void UpdateAwayLineUp(LineUp lineup)
        {
            UpdateLineUp(lineup, away);
            Refresh();
        }

        public void SetArea(int area, Club club)
        {
            if (club == HomeClub)
            {
                areaBar.Value = area;
            }
            else
            {
                areaBar.Value = areaBar.Maximum - area;
            }
        }

        public void Log(string s, int minute)
        {
            events.Items.Add(new ListViewItem(new string[] { "", minute.ToString(), s }));
            events.Columns[1].Width = -2;
            Refresh();
        }

        private void UpdateLineUp(LineUp lineup, AEListView lw)
        {
            lw.Fill(lineup.Players.Select(p => new object[] {"", p.LastName, p.CurrentSkill, p.Rating }));
            //for (int i = 0; i < lineup.Players.Count; i++)
            //{
            //    if (lw.Items.Count <= i)
            //    {
            //        lw.Items.Add(new ListViewItem(new string[] { "", lineup.Players[i].LastName, lineup.Players[i].CurrentSkill.ToString(), lineup.Players[i].Rating.ToString() }));
            //        lw.Columns[0].Width = -2;
            //    }
            //    else
            //    {
            //        lw.Items[i].SubItems[0].Text = lineup.Players[i].LastName;
            //        lw.Items[i].SubItems[1].Text = lineup.Players[i].BaseSkill.ToString();
            //        lw.Items[i].SubItems[2].Text = lineup.Players[i].Rating.ToString();
            //    }
            //}

        }
    }
}
