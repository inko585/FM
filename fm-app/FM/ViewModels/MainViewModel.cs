using AE.Graphics.Wpf.Basis;
using FM.Common;
using FM.Common.Generic;
using FM.Common.Season;
using FM.Save;
using FM.Views;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; set; }

        private void Subscribe()
        {
            Season.CurrentSeason.OnSeasonProgress += (s, e) => NotifyPropertyChanged(nameof(NextOpponent));
            NotifyPropertyChanged(nameof(PlayerClub));
            NotifyPropertyChanged(nameof(NextOpponent));
        }

        public MainViewModel()
        {
            Instance = this;
            NavItems = new ObservableCollection<NavItem>();
            CollectionView = CollectionViewSource.GetDefaultView(NavItems);
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

            AdjustColor();
            Season.OnSeasonChange += (se, e) => Subscribe();

            NavItems.Add(new NavItem()
            {
                Category = "VEREIN",
                Name = "Kader",
                UserControl = new RoosterControl()
            });
            NavItems.Add(new NavItem()
            {
                Category = "STATISTIKEN",
                Name = "Tabellen",
                UserControl = new LeagueControl()
            });

            NavItems.Add(new NavItem()
            {
                Category = "STATISTIKEN",
                Name = "Spieltage",
                UserControl = new MatchDayControl()
            });

            NavItems.Add(new NavItem()
            {
                Category = "STATISTIKEN",
                Name = "Transfers",
                UserControl = new TransferControl()
            });

            NotifyPropertyChanged("NavItems");

            CollectionView.Refresh();
            //SaveCommand = new RelayCommand();

            SaveCommand = new RelayCommand(o => IO.Save(Game.Instance.FootballUniverse));
            LoadCommand = new RelayCommand(o =>
            {
                var universe = IO.Load(Game.Instance.FootballUniverse.World, out Club playerClub);
                if (universe != null)
                {
                    Game.Instance.FootballUniverse = universe;
                    Game.Instance.PlayerClub = playerClub;
                    NotifyPropertyChanged(nameof(Universe));
                    LeagueViewModel.Instance.NotifyPropertyChanged(nameof(LeagueViewModel.Instance.LeagueAssociations));
                    LeagueViewModel.Instance.SelectedLeagueAssociation = LeagueViewModel.Instance.LeagueAssociations.FirstOrDefault();
                    LeagueViewModel.Instance.SelectedLeague = LeagueViewModel.Instance.SelectedLeagueAssociation.Leagues.FirstOrDefault();
                    LeagueViewModel.Instance.Subscribe();
                    MatchDayViewModel.Instance.NotifyPropertyChanged(nameof(MatchDayViewModel.Instance.LeagueAssociations));
                    MatchDayViewModel.Instance.SelectedLeagueAssociation = MatchDayViewModel.Instance.LeagueAssociations.FirstOrDefault();
                    MatchDayViewModel.Instance.SelectedLeague = MatchDayViewModel.Instance.SelectedLeagueAssociation.Leagues.FirstOrDefault();
                    MatchDayViewModel.Instance.Subscribe();
                    TransferViewModel.Instance.NotifyPropertyChanged(nameof(TransferViewModel.Instance.LeagueAssociations));
                    TransferViewModel.Instance.SelectedLeagueAssociation = TransferViewModel.Instance.LeagueAssociations.FirstOrDefault();
                    TransferViewModel.Instance.SelectedLeague = TransferViewModel.Instance.SelectedLeagueAssociation.Leagues.FirstOrDefault();
                    TransferViewModel.Instance.Subscribe();
                    RoosterViewModel.Instance.Subscribe();
                    Subscribe();
                    AdjustColor();
                }
            });
            Subscribe();
        }

        private void AdjustColor()
        {
            var playerColor = Game.Instance.PlayerClub.ClubColors.MainColor;
            var playerColor2 = Game.Instance.PlayerClub.ClubColors.SecondColor;
            FlavorColor = new SolidColorBrush(Color.FromArgb(playerColor.A, playerColor.R, playerColor.G, playerColor.B));
            ButtonColor = new SolidColorBrush(Color.FromArgb(playerColor.A, playerColor.R, playerColor.G, playerColor.B));
            ButtonColor2 = new SolidColorBrush(Color.FromArgb(playerColor2.A, playerColor2.R, playerColor2.G, playerColor2.B));
            NotifyPropertyChanged(nameof(FlavorColor));
            NotifyPropertyChanged(nameof(ButtonColor));
            NotifyPropertyChanged(nameof(ButtonColor2));
        }

        public Club PlayerClub => Game.Instance.PlayerClub;

        public Club NextOpponent
        {
            get
            {
                if (Game.Instance.PlayerLeague.NextMatchDay == null)
                {
                    return null;
                }
                var pm = Game.Instance.PlayerLeague.NextMatchDay.Matches.First(m => m.HomeClub == PlayerClub || m.AwayClub == PlayerClub);
                return pm.HomeClub == PlayerClub ? pm.AwayClub : pm.HomeClub;

            }
        }

        public FootballUniverse Universe
        {
            get
            {
                return Game.Instance.FootballUniverse;
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }

        public SolidColorBrush FlavorColor { get; set; }
        public SolidColorBrush ButtonColor { get; set; }
        public SolidColorBrush ButtonColor2 { get; set; }
        public ICollectionView CollectionView { get; set; }

        public ObservableCollection<NavItem> NavItems { get; set; }

        private NavItem navItem;
        public NavItem SelectedNavItem
        {
            get
            {
                return navItem;
            }
            set
            {
                navItem = value;
                ((MainWindow)View).ContentControl.Content = navItem.UserControl;
                NotifyPropertyChanged("SelectedNavItem");
            }
        }





    }
}
