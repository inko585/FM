
using FM.Common.Generic;
using FM.Common.Season;
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

        public static TransferViewModel Instance { get; private set; }
        public TransferViewModel()
        {
            Filter = "";
            Instance = this;
            Subscribe();
            SelectedSeason = Season.CurrentSeason;
            SelectedLeagueAssociation = Game.Instance.PlayerLeagueAssociation;
            SelectedLeague = Game.Instance.PlayerLeague;
            //NotifyPropertyChanged("LeagueAssociations");
        }

        public void Subscribe()
        {
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
            Season.OnSeasonChange += HandleSeasonSwitch;
        }

        public void HandleProgress(object o, EventArgs e)
        {
            NotifyPropertyChanged("TransferHistory");
            NotifyPropertyChanged("SelectedLeague");
        }

        public void HandleSeasonSwitch(object o, EventArgs e)
        {
            NotifyPropertyChanged("SelectedLeague");
            NotifyPropertyChanged("Seasons");
            SelectedSeason = Season.CurrentSeason;
            Season.CurrentSeason.OnSeasonProgress += HandleProgress;
        }

        private string filter;

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                NotifyPropertyChanged("Filter");
                NotifyPropertyChanged("TransferHistory");
            }
        }


        public ObservableCollection<LeagueAssociation> LeagueAssociations
        {
            get
            {
                return new ObservableCollection<LeagueAssociation>(Game.Instance.FootballUniverse.LeagueAssociations);
            }
        }
        public ObservableCollection<Season> Seasons
        {
            get
            {
                return new ObservableCollection<Season>(Season.AllSeasons);
            }
        }

        public ObservableCollection<Transfer> TransferHistory
        {
            get
            {
                return new ObservableCollection<Transfer>(Game.Instance.FootballUniverse.TransferList.Where(t => t.Year == SelectedSeason.Year && (t.From.Leagues.Contains(SelectedLeague) || (t.To?.Leagues?.Contains(SelectedLeague) ?? false)) && (Filter == "" || (t.From.Name + " " + t.Player.FullName + " " + t.To?.Name ?? "").Contains(Filter))));
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
                if (selectedLeagueAssociation != null)
                {
                    SelectedLeague = selectedLeagueAssociation.Leagues.First();
                }
                NotifyPropertyChanged("SelectedLeagueAssociation");
            }
        }

        private Season selectedSeason;

        public Season SelectedSeason
        {
            get { return selectedSeason; }
            set
            {
                selectedSeason = value;
                NotifyPropertyChanged("SelectedSeason");
                NotifyPropertyChanged("TranferHistory");
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
                NotifyPropertyChanged("TransferHistory");
            }
        }

    }
}
