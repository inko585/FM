
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
    public class TransferViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public TransferViewModel()
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
            NotifyPropertyChanged("TransferHistory");
            NotifyPropertyChanged("SelectedLeague");
        }

        public void HandleSeasonSwitch(object o, EventArgs e)
        {
            NotifyPropertyChanged("SelectedLeague");
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
        }

        public ObservableCollection<LeagueAssociation> LeagueAssociations { get; set; }

        public ObservableCollection<Transfer> TransferHistory 
        {
            get
            {
                return new ObservableCollection<Transfer>(Game.Instance.FootballUniverse.TransferList);
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

    }
}
