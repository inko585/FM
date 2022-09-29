﻿using FM.Models;
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

        public static void SignContract(Player p, Club c, int contractRunTime, int salary)
        {

            p.ContractComing = new Contract()
            {
                Club = c,
                Player = p,
                RunTime = contractRunTime,
                Salary = salary
            };

            Console.WriteLine("New Contract " + p.FullName + ": " + p.Club.Name + " => " + c.Name);
            c.JoiningPlayers.Add(p);
        }

        public static void TransferPlayer(Player p, Club c, int contractRunTime, int salary)
        {
            var sellingClub = p.Club;

            sellingClub.Account += p.Price;
            c.Account -= p.Price;

            sellingClub.TransferIncomeCurrentSeason += p.Price;
            c.TransferExpensesCurrentSeason += p.Price;

            Console.WriteLine("Transfer " + p.FullName + ": " + sellingClub.Name + " => " + c.Name + " (" + p.Price + ")");

            p.ContractCurrent = new Contract()
            {
                Club = c,
                Player = p,
                RunTime = contractRunTime,
                Salary = salary
            };
            c.Rooster.Add(p);
            sellingClub.Rooster.Remove(p);
            
        }


    }
}