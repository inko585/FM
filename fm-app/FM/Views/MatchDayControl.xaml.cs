using FM.Models.Generic;
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
    /// Interaction logic for MatchDayControl.xaml
    /// </summary>
    public partial class MatchDayControl : UserControl
    {
        public MatchDayControl()
        {
            DataContext = new MatchDayViewModel(); 
            InitializeComponent();
        }


        private void Result_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var m = (sender as Label).DataContext as Match;
            if (m != null && m.IsPlayed)
            {
                var mView = new MatchWindow(m);
                mView.ShowDialog();
            }
        }

        private void Club_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = (sender as Label).DataContext as Club;
            if (c != null)
            {
                var cView = new ClubWindow(c);
                cView.ShowDialog();
            }
        }
    }
}
