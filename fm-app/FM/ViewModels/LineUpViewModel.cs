using AE.Graphics.Wpf.Basis;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class LineUpViewModel : BaseViewModel
    {

        public static LineUpViewModel Instance { get; set; }

        public LineUpViewModel()
        {
            Instance = this;
        }

        public ObservableCollection<PlayerPositionContainer> StartingLineup { get; set; }

        private Club Club { get; set; }

        public ObservableCollection<Player> Rooster => new ObservableCollection<Player>(Club.Rooster.Where(p => !Club.StartingLineUp.Players.Contains(p) && !Club.Bench.Contains(p)));


    }

    public class PlayerPositionContainer : BaseViewModel
    {

        private Player player;

        public Position Position { get; set; }


        public Player Player
        {
            get => player;
            set
            {
                player = value;
                NotifyPropertyChanged(nameof(Player));
            }
        }

        public PlayerPositionContainer(Position position)
        {
            Position = position;
        }
    }
}
