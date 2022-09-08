using FM.Models.Generic;
using FM.ViewModels;
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
    /// Interaction logic for LeagueControl.xaml
    /// </summary>
    public partial class LeagueControl : UserControl
    {
        public LeagueControl()
        {
            DataContext = new LeagueViewModel(); 
            InitializeComponent();
        }



        private void NameLabel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            var lc = (sender as Label).DataContext as LeagueCompetitor ;
            
            var cView = new ClubWindow(lc.Club);
            cView.ShowDialog();
        }

    }
}
