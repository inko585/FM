
using FM.Entities.Base;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FM
{
    public class Test
    {
        public void Run()
        {


                var w = World.ReadWorld(@"C:\Users\marshall\Documents\gendata7.xml");
            100.Times(() =>
            {
                var p = Generator.Generator.GenerateRandomPlayer(w, null, w.Nations[0], Entities.Generic.Position.Striker, 9, 17, 34);
                Console.WriteLine(p.FirstName + " " + p.LastName + " " +  " (" + p.Age + ") " + p.BaseSkill + "/" + p.MaxSkill);
            });
            
            
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
    }
}
