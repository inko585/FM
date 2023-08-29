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
        public ContractWindow(Player player, Club club, bool isTransfer)
        {
            ViewModel = new ContractViewModel(player, club, isTransfer);
            DataContext = ViewModel;
            InitializeComponent();
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
                FootballHelper.SignContract(contractVM.Player, contractVM.Club, contractVM.SelectedRunTimeIndex + 2, contractVM.Salary, false);
            }
            this.Close();
        }
    }
}
