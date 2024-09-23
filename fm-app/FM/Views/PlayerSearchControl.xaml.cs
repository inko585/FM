using FM.Common.Generic;
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
    /// Interaction logic for PlayerSearchControl.xaml
    /// </summary>
    public partial class PlayerSearchControl : UserControl
    {
        public PlayerSearchControl()
        {
            InitializeComponent();
            DataContext = new PlayerSearchViewModel();
        }

        private void NameLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as Label).DataContext as Player;

            var pView = new PlayerWindow(p);
            pView.ShowDialog();
        }

        private void Label_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Player;
            if (t != null)
            {
                if (t.Club != null)
                {
                    var cView = new ClubWindow(t.Club);
                    cView.ShowDialog();
                }
            }
        }
    }
}
