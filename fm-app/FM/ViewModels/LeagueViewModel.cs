
using FM.Models.Generic;
using FM.Models.Season;
using FM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class LeagueViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public LeagueViewModel()
        {
            LeagueAssociations = new ObservableCollection<LeagueAssociation>(Game.Instance.FootballUniverse.LeagueAssociations);
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            Season.OnSeasonChange += HandleSeasonSwitch;
            SelectedLeagueAssociation = Game.Instance.PlayerLeagueAssociation;
            SelectedLeague = Game.Instance.PlayerLeague;
            //NotifyPropertyChanged("LeagueAssociations");
        }

        public void HandleProgress(object o, EventArgs e)
        {
            var s = o as Season;
            SelectedLeague = SelectedLeague;
            NotifyPropertyChanged("SelectedLeague");
        }

        public void HandleSeasonSwitch(object o, EventArgs e)
        {
            SelectedLeague = SelectedLeague;
            NotifyPropertyChanged("SelectedLeague");
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
        }

        public ObservableCollection<LeagueAssociation> LeagueAssociations { get; set; }

        private LeagueAssociation selectedLeagueAssociation;

        public LeagueAssociation SelectedLeagueAssociation
        {
            get
            {
                return selectedLeagueAssociation;
            }
            set
            {
                selectedLeagueAssociation = value;
                NotifyPropertyChanged("SelectedLeagueAssociation");
            }
        }

        private League league;

        public League SelectedLeague
        {
            get { return league; }
            set
            {
                league = value;
                NotifyPropertyChanged("SelectedLeague");
            }
        }

    }
}
