using AE.Graphics.Wpf.Basis;
using FM.Common.Generic;
using FM.Common.Season;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FM.ViewModels
{
    public class RoosterViewModel : BaseViewModel
    {

        public static RoosterViewModel Instance { get; set; }

        public RoosterViewModel()
        {
            Instance = this;
            Subscribe();
        }

        public void Subscribe()
        {
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            Season.OnSeasonChange += HandleSeasonSwitch;
        }

        public void HandleProgress(object o, EventArgs e)
        {
            Update();
        }

        public void HandleSeasonSwitch(object o, EventArgs e)
        {
            Update();
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
        }

        public void Update()
        {
            NotifyPropertyChanged(nameof(PlayerClub));
        }
        public Club PlayerClub
        {
            get
            {
                return Game.Instance.PlayerClub;
            }
        }
    }
}
