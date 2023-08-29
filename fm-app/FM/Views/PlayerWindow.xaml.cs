using AE.Graphics.Wpf;
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
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : AEWindow
    {
        public PlayerWindow(Player player)
        {
            DataContext = new PlayerViewModel(player);
            InitializeComponent();
        }

        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            var pvm = (sender as Button).DataContext as PlayerViewModel;

            if (pvm.Player.Club == Game.Instance.PlayerClub && !pvm.Player.WantsToLeavePlayerClub.HasValue)
            {
                pvm.Player.WantsToLeavePlayerClub = pvm.Player.CheckIfPlayerWantsToLeavePlayerClub();
                if (pvm.Player.WantsToLeavePlayerClub.Value)
                {
                    AEGraphics.ShowMessage(pvm.Player.FullName + " teilt ihnen mit dass er den Club verlassen will. Er will sich eine neue Herausforderung suchen.");
                    pvm.NotifyPropertyChanged("Player");
                    pvm.NotifyPropertyChanged("ContractEnabled");
                    pvm.NotifyPropertyChanged("BuyEnabled");
                    return;
                }
            }

            var dia = new ContractWindow(pvm.Player, Game.Instance.PlayerClub, false);

            dia.ShowDialog();
            pvm.NotifyPropertyChanged("Player");
            pvm.NotifyPropertyChanged("ContractEnabled");
            pvm.NotifyPropertyChanged("BuyEnabled");
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            var pvm = (sender as Button).DataContext as PlayerViewModel;

            var dia = new ContractWindow(pvm.Player, Game.Instance.PlayerClub, true);

            dia.ShowDialog();
            pvm.NotifyPropertyChanged("Player");
            pvm.NotifyPropertyChanged("ContractEnabled");
            pvm.NotifyPropertyChanged("BuyEnabled");
        }
    }
}
