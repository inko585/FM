
using FM.Entities.Base;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM.Generator;
using FM.Common;
using FM.Models.Generic;

namespace FM
{
    public class Test
    {
        World w = World.ReadWorld(@"C:\Users\marshall\Documents\gendata11.xml");
        public void Run()
        {
            //var c = new Club();
            //c.Name = "Verein 1";
            //var c2 = new Club();
            //c2.Name = "Verein 2";
            //var lu1 = new LineUp(new List<Player>() { NewPlayer(Position.Goalie), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Midfielder), NewPlayer(Position.Striker), NewPlayer(Position.Striker) }, null, null, Tactic.Offensive, Tackling.Clean, Frequency.Normal);
            //lvl = 5;
            //var lu2 = new LineUp(new List<Player>() { NewPlayer(Position.Goalie), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Striker) }, null, null, Tactic.Defensive, Tackling.Brutal, Frequency.High);
            //c.StartingLineUp = lu1;
            //c2.StartingLineUp = lu2;



            //var matches = new List<MatchResult>();
            //20.Times(() =>
            //{
            //var c = WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.Nations.First(), 6);
            //c.Coach.Philospophie = new PossessionPhilosophie();
            //var c2 = WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.Nations.First(), 6);
            //c2.Coach.Philospophie = new BalancedPhilosophie();
            //var m = new Match();
            //m.HomeCompetitor = new LeagueCompetitor() { Club = c, Points = 0 };
            //m.AwayCompetitor = new LeagueCompetitor() { Club = c, Points = 0 };
            //var mr = m.Simulate(false);
            //Console.WriteLine(mr);
            //});



            Game.InitNewGame(w, 12);
            var mw = new FM.Views.MainWindow();
            mw.ShowDialog();


            //var p = Generator.Generator.GenerateRandomPlayer()

            //var m = new Match()
            //{
            //    LengthInActions = 20,
            //    HomeClub = clubA,
            //    AwayClub = clubB,
            //};
            //var res = m.Simulate();
            //Console.WriteLine(p2.Def);

            //Console.WriteLine(res.HomeGoals + ":" + res.AwayGoals);
            //foreach (var s in res.Scorers)
            //{
            //    Console.WriteLine(s.LastName);
            //}
            //Console.WriteLine("-----------------------------------");
            //foreach (var s in res.Substitutions)
            //{
            //    Console.WriteLine(s.In.LastName + " für " + s.Out.LastName);
            //}

        }

        int lvl = 6;
        private Player NewPlayer(Position p)
        {
            return WorldGenerator.GenerateRandomPlayer(w, null, w.Nations[0], p, lvl, 17, 37);
        }

    }
}
