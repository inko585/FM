using AE.Graphics.Wpf;
using FM.Common.Generic;
using FM.ViewModels;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LiveWindow_Text.xaml
    /// </summary>
    public partial class LiveWindow_Text : AEWindow
    {
        public LiveWindow_Text(MatchResult mr)
        {
            var liveViewModel = new LiveViewModel(mr);
            DataContext = liveViewModel;

            InitializeComponent();

            new Thread(() => liveViewModel.StartMatch()).Start();
            
        }

        private void Ok_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Player_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as Label).DataContext as MatchPlayer;

            var pView = new PlayerWindow(p.Player);
            pView.ShowDialog();
        }

        private void Club_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var label = (sender as Label);
            Club c;
            if ((string)label.Content == (DataContext as LiveViewModel).HomeClub.Name)
            {
                c = (DataContext as LiveViewModel).HomeClub;
            }
            else
            {
                c = (DataContext as LiveViewModel).AwayClub;
            }

            var cView = new ClubWindow(c);
            cView.ShowDialog();
        }

        private void Scorer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sc = (sender as Label).DataContext as ScoreEvent;

            var pView = new PlayerWindow(sc.Scorer.Player);
            pView.ShowDialog();
        }

        private void Sub_PreviewMouseDown(Object sender, MouseButtonEventArgs e)
        {
            var lbl = sender as Label;
            var su = lbl.DataContext as Substitution;

            PlayerWindow pView;
            if (lbl.Content.ToString() == su.In.Player.FullName)
            {
                pView = new PlayerWindow(su.In.Player);
            }
            else
            {
                pView = new PlayerWindow(su.Out.Player);
            }

            pView.ShowDialog();
        }

    }
}
