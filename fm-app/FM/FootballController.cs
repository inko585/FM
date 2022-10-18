using FM.Models;
using FM.Models.Generic;
using FM.Models.Season;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM
{
    public class FootballHelper
    {

        public static void SignContract(Player p, Club c, int contractRunTime, int salary, bool currentSeason)
        {
            if (p.Club != c)
            {
                p.ClubHistory.Add(c);
            }
            var contract = new Contract()
            {
                Club = c,
                Player = p,
                RunTime = contractRunTime,
                Salary = salary
            };

            if (currentSeason)
            {
                p.ContractCurrent = contract;
                c.Rooster.Add(p);
                p.ResetDress();
            }
            else
            {
                p.ContractComing = contract;
                c.JoiningPlayers.Add(p);
            }


            Console.WriteLine("New Contract " + p.FullName + ": " + p.Club.Name + " => " + c.Name);

        }

        public static void TransferPlayer(Player p, Club c, Season s, int contractRunTime, int salary)
        {
            var sellingClub = p.Club;

            sellingClub.Account += p.Price;
            c.Account -= p.Price;

            sellingClub.TransferIncomeCurrentSeason += p.Price;
            c.TransferExpensesCurrentSeason += p.Price;

            //Console.WriteLine("Transfer " + p.FullName + ": " + sellingClub.Name + " => " + c.Name + " (" + p.Price + ")");
            if (c.StartingLineUp.Players.Contains(p))
            {
                c.PlayersSoldFromStartingLineUp++;
            }

            Game.Instance.FootballUniverse.TransferList.Add(new Transfer(p, sellingClub, c, s.Year, s.CurrentWeek.Number, p.Price, p.MarketValueStandard));
            c.NewPlayersWithFee.Add(p);
            p.ClubHistory.Add(c);

            p.ContractCurrent = new Contract()
            {
                Club = c,
                Player = p,
                RunTime = contractRunTime,
                Salary = salary
            };
            c.Rooster.Add(p);
            sellingClub.Rooster.Remove(p);
            p.ResetDress();
        }


    }
}
