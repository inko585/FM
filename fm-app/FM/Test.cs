
using FM.Entities.Base;
using FM.Entities.Generic;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM.Generator;
using FM.Common;

namespace FM
{
    public class Test
    {
        World w = World.ReadWorld(@"C:\Users\marshall\Documents\gendata7.xml");
        public void Run()
        {
            var c = new Club();
            c.Name = "Verein 1";
               var c2 = new Club();
            c2.Name = "Verein 2";
            var lu1 = new LineUp(new List<Player>() { NewPlayer(Position.Goalie), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Striker), NewPlayer(Position.Striker) }, null, null, Tactic.Offensive, Tackling.Brutal, Frequency.Normal);
            var lu2 = new LineUp(new List<Player>() { NewPlayer(Position.Goalie), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Defender), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Midfielder), NewPlayer(Position.Striker), NewPlayer(Position.Striker) }, null, null, Tactic.Offensive, Tackling.Brutal, Frequency.Normal);
            c.StartingLineUp = lu1;
            c2.StartingLineUp = lu2;

            var m = new Match();
            m.HomeClub = c;
            m.AwayClub = c2;
            m.Simulate();
            
            
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

        private Player NewPlayer(Position p)
        {
            return WorldGenerator.GenerateRandomPlayer(w, null, w.Nations[0], p, 9, 17, 34);
        }
    }
}
