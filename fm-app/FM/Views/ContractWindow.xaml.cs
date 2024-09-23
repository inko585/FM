using AE.Graphics.Wpf;
using FM.Common;
using FM;
using FM.Common.Generic;
using FM.Common.Season;
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
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : AEWindow
    {

        private bool IsWithinTransferWindow;
        public ContractWindow(Player player, Club club, bool isTransfer, bool isWithinTransferWindow)
        {
            ViewModel = new ContractViewModel(player, club, isTransfer, isWithinTransferWindow);
            DataContext = ViewModel;
            InitializeComponent();
            IsWithinTransferWindow = isWithinTransferWindow;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var contractVM = ViewModel as ContractViewModel;

            if (contractVM.IsTransfer)
            {
                FootballHelper.TransferPlayer(contractVM.Player, contractVM.Club, Season.CurrentSeason, contractVM.SelectedRunTimeIndex + 2, contractVM.Salary);
            }
            else
            {
                
                if (contractVM.Club != contractVM.Player.Club)
                {
                    Game.Instance.FootballUniverse.TransferList.Add(new Transfer(contractVM.Player, contractVM.Player.Club ?? contractVM.Player.ClubHistory.Last(), contractVM.Club, Season.CurrentSeason.Year + (IsWithinTransferWindow ? 0 : 1), 1, 0, contractVM.Player.MarketValueStandard));
                }
                FootballHelper.SignContract(contractVM.Player, contractVM.Club, contractVM.SelectedRunTimeIndex + 2, contractVM.Salary, contractVM.Club != contractVM.Player.Club && IsWithinTransferWindow);

            }

            MainViewModel.Instance.Update();
            this.Close();
        }
    }
}
