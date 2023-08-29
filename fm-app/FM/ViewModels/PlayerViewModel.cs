using FM.Common.Generic;
using FM.Common.Season;
using FM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public PlayerViewModel(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public bool ContractEnabled => Player.WillSignContractWithPlayerClub && ((Season.CurrentSeason.CurrentWeek.Number == Game.CONTRACT_BORDER_WEEK && Player.Club == Game.Instance.PlayerClub) || Season.CurrentSeason.CurrentWeek.Number > Game.CONTRACT_BORDER_WEEK);

        public bool BuyEnabled => !Game.Instance.PlayerClub.Rooster.Contains(Player) && Player.IsForSale && Player.WillSignContract && Season.CurrentSeason.CurrentWeek.Number <= 4;



    }
}
