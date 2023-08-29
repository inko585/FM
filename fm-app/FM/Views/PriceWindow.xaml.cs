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
    public partial class PriceWindow : AEWindow
    {
        public PriceWindow(Player player)
        {
            ViewModel = new  PriceViewModel(player);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var pricetVM = ViewModel as PriceViewModel;
            pricetVM.Player.PlayerPriceAdjustment = Math.Round(1+ (double)pricetVM.SliderValue / 100, 2);

           

            this.Close();
        }
    }
}
