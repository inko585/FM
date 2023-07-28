using AE.Graphics.Wpf.Basis;
using AE.Logging;
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
    public class ClubViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ClubViewModel(Club c)
        {
            Club = c;
            SelectedJoiningSeason = Season.CurrentSeason;
            SelectedLeavingSeason = Season.CurrentSeason;
            WatchClubCommand = new RelayCommand(x => { Club.Watch = !Club.Watch; });
        }

        public Club Club { get; set; }

        public RelayCommand WatchClubCommand { get; set; } 

        private Season selectedJoiningSeason;

        public Season SelectedJoiningSeason
        {
            get { return selectedJoiningSeason; }
            set { selectedJoiningSeason = value; NotifyPropertyChanged(nameof(JoiningHistory)); }
        }

        private Season selectedLeavingSeason;

        public Season SelectedLeavingSeason
        {
            get { return selectedLeavingSeason; }
            set { selectedLeavingSeason = value; NotifyPropertyChanged(nameof(LeavingHistory)); }
        }

        public ObservableCollection<Season> Seasons
        {
            get
            {
                return new ObservableCollection<Season>(Season.AllSeasons);
            }
        }


        public ObservableCollection<Transfer> JoiningHistory
        {
            get
            {
                return new ObservableCollection<Transfer>(Game.Instance.FootballUniverse.TransferList.Where(t => t.Year == SelectedJoiningSeason.Year && t.To == Club));
            }
        }

        public ObservableCollection<Transfer> LeavingHistory
        {
            get
            {
                return new ObservableCollection<Transfer>(Game.Instance.FootballUniverse.TransferList.Where(t => t.Year == SelectedLeavingSeason.Year && t.From == Club));
            }
        }

    }
}
