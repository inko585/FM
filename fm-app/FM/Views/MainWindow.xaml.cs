using AE.Graphics.Wpf;
using FM.Common.Generic;
using FM.Common.Season;
using FM.ViewModels;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AEWindow
    {
        public MainWindow()
        {
            
        }

        public void Init()
        {
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            ViewModel.View = this;
            InitializeComponent();
            ((MainViewModel)ViewModel).SelectedNavItem = ((MainViewModel)ViewModel).NavItems.First();
        }

        private void ContinueButtonClick(object sender, RoutedEventArgs e)
        {
            Season.CurrentSeason.Progress();
        }

        private void Club_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var cView = new ClubWindow((ViewModel as MainViewModel).NextOpponent);
            cView.ShowDialog();
        }
    }
}
