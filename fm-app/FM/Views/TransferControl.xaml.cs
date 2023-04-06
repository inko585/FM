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
    /// Interaction logic for TransferControl.xaml
    /// </summary>
    public partial class TransferControl : UserControl
    {
        public TransferControl()
        {
            DataContext = new TransferViewModel(); 
            InitializeComponent();
        }

        private void Player_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;

            var pView = new PlayerWindow(t.Player);
            pView.ShowDialog();
        }

        private void From_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;
            if (t != null)
            {
                var cView = new ClubWindow(t.From);
                cView.ShowDialog();
            }
        }

        private void To_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as Label).DataContext as Transfer;
            if (t != null)
            {
                var cView = new ClubWindow(t.To);
                cView.ShowDialog();
            }
        }
    }
}
