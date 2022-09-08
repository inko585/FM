
using FM.Models.Generic;
using FM.Models.Season;
using FM.Views;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class MatchDayViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MatchDayViewModel()
        {
            LeagueAssociations = new ObservableCollection<LeagueAssociation>(Game.Instance.FootballUniverse.LeagueAssociations);
            SelectedLeagueAssociation = Game.Instance.PlayerLeagueAssociation;
            SelectedLeague = Game.Instance.PlayerLeague;
            SelectedMatchDay = SelectedLeague.LatestMatchDay;
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            //NotifyPropertyChanged("LeagueAssociations");
        }

        public void HandleProgress(object o, EventArgs e)
        {
            var s = o as Season;
            SelectedMatchDay = SelectedLeague.LatestMatchDay;
            //NotifyPropertyChanged("SelectedMatchDay.ObservableMatches");
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



        private MatchDay matchDay;

        public MatchDay SelectedMatchDay
        {
            get { return matchDay; }
            set
            {
                matchDay = value;
                NotifyPropertyChanged("SelectedMatchDay");
            }
        }


        //private LeagueCompetitor selectedClub;
        public Match SelectedPairing
        {
            get
            {
                return null;
            }
            set
            {
                //selectedClub = value;
                if (value != null)
                {
                    //var cView = new ClubWindow(value.Club);
                    //cView.ShowDialog();
                }
            }
        }
    }
}
