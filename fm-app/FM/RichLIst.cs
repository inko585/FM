using FM.Common;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM
{
    public static class RichList
    {
        public static IEnumerable<Player> Goalies(this IEnumerable<Player> players)
        {
            return GetPlayersForPosition(players, Position.Goalie);
        }


        public static IEnumerable<Player> Defenders(this IEnumerable<Player> players)
        {
            return GetPlayersForPosition(players, Position.Defender);
        }

        public static IEnumerable<Player> Midfielders(this IEnumerable<Player> players)
        {
            return GetPlayersForPosition(players, Position.Midfielder);
        }

        public static IEnumerable<Player> Strikers(this IEnumerable<Player> players)
        {
            return GetPlayersForPosition(players, Position.Striker);
        }

        private static IEnumerable<Player> GetPlayersForPosition(IEnumerable<Player> players, Position p)
        {
            return players.Where(pl => pl.Position == p);
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> input)
        {
            
            List<T> list = input.ToList();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Util.GetRandomInt(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
