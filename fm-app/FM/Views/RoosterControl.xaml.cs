using AE.Graphics.Wpf;
using FM.Common.Generic;
using FM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Interaction logic for RoosterControl.xaml
    /// </summary>
    public partial class RoosterControl : UserControl
    {
        public RoosterControl()
        {
            DataContext = new RoosterViewModel();
            InitializeComponent();
        }

        private void Price_Click(object sender, RoutedEventArgs e)
        {
            var p = (sender as Button).DataContext as Player;

            var priceView = new PriceWindow(p);

            priceView.ShowDialog();
            RoosterViewModel.Instance.Update();

        }

        private void NameLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = (sender as Label).DataContext as Player;

            var pView = new PlayerWindow(p);
            pView.ShowDialog();
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            var p = (sender as Button).DataContext as Player;
            if (p.Club == Game.Instance.PlayerClub && !p.WantsToLeavePlayerClub.HasValue)
            {
                p.WantsToLeavePlayerClub = p.CheckIfPlayerWantsToLeavePlayerClub();
                if (p.WantsToLeavePlayerClub.Value)
                {
                    AEGraphics.ShowMessage(p.FullName + " teilt ihnen mit dass er den Club verlassen will. Er will sich eine neue Herausforderung suchen.");
                    RoosterViewModel.Instance.Update();
                    return;
                }
            }
            var dia = new ContractWindow(p, Game.Instance.PlayerClub, false, false);
            dia.ShowDialog();
            RoosterViewModel.Instance.Update();
        }
    }
}
