using AE.Graphics.Wpf.Basis;
using FM.Models;
using FM.Models.Generic;
using FM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            Universe = Game.Instance.FootballUniverse;
            NavItems = new ObservableCollection<NavItem>();
            CollectionView = CollectionViewSource.GetDefaultView(NavItems);
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription("Category"));


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

            NotifyPropertyChanged("NavItems");

            CollectionView.Refresh();

        }

        public FootballUniverse Universe { get; set; }


        public ICollectionView CollectionView { get; set; }

        public ObservableCollection<NavItem> NavItems { get; set; }

        private NavItem navItem;
        public NavItem SelectedNavItem
        {
            get
            {
                return navItem;
            } set
            {
                navItem = value;
                ((MainWindow) View).ContentControl.Content = navItem.UserControl;
                NotifyPropertyChanged("SelectedNavItem");
            }
        }





    }
}
