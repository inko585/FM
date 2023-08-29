
using FM.Common.Generic;
using FM.Common.Season;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static MatchDayViewModel Instance { get; private set; }
        public MatchDayViewModel()
        {
            SelectedLeagueAssociation = Game.Instance.PlayerLeagueAssociation;
            SelectedLeague = Game.Instance.PlayerLeague;
            SelectedMatchDay = SelectedLeague.LatestMatchDay;
            Subscribe();

            //NotifyPropertyChanged("LeagueAssociations");
            Instance = this;
        }

        public void Subscribe()
        {
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            Season.OnSeasonChange += HandleSeasonSwitch;
        }

        public void HandleSeasonSwitch(object o, EventArgs e)
        {
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            SelectedMatchDay = SelectedLeague.LatestMatchDay;
            //NotifyPropertyChanged("SelectedMatchDay.ObservableMatches");
        }

        public void HandleProgress(object o, EventArgs e)
        {
            var s = o as Season;
            SelectedMatchDay = SelectedLeague.LatestMatchDay;
            //NotifyPropertyChanged("SelectedMatchDay.ObservableMatches");
        }

        public ObservableCollection<LeagueAssociation> LeagueAssociations
        {
            get
            {
                return new ObservableCollection<LeagueAssociation>(Game.Instance.FootballUniverse.LeagueAssociations);
            }
        }

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
