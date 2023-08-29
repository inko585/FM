using AE.Graphics.Wpf.Basis;
using FM.Common;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class PriceViewModel : BaseViewModel
    {
        public PriceViewModel(Player player)
        {
            Player = player;
            SliderValue = (int)((player.PlayerPriceAdjustment - 1) * 100);
        }
        public Player Player { get; set; }

        public string RecommendedPriceString => string.Format("{0:#,0}", Player.DefaultPrice);


        private int sliderValue;
        public int SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                NotifyPropertyChanged(nameof(CalculatedPriceString));
            }
        }

        public double CalculatedPrice
        {
            get => Util.GetNiceValue((int)(Player.DefaultPrice * (1 + (double)SliderValue / 100)));
        }

        public string CalculatedPriceString => string.Format("{0:#,0}", CalculatedPrice);

    }
}
