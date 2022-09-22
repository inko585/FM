
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
using FM.Models.Season;

namespace FM
{
    public class Test
    {
        World w = World.ReadWorld(@"C:\Users\marshall\Documents\gendata11_5.xml");
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

            Game.InitNewGame(w, 12);
            var mw = new FM.Views.MainWindow();

            var totalCount = new Dictionary<string, int>();
            var winCount = new Dictionary<string, int>();
            var winGoals = new List<int>();
            var loseGoals = new List<int>();
            var strongerTeamWinCount = 0;
            var strongerTeamLoseCount = 0;
            var setPlaySkillWinner = new List<int>();
            var setPlaySkillLoser = new List<int>();


            //var la = new LeagueAssociation();
            //var l = new League();
            //l.Association = la;
            //la.Leagues.Add(l);
            //for (var j = 0; j < Game.Instance.LeagueSize; j++)
            //{
            //    l.Clubs.Add(WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.Nations.First(), w.Associations.First().Power));
            //}
            //l.ResetStandings();

            //Season.InitSeasons(new List<LeagueAssociation> { la });

            //foreach (var md in l.CurrentSeasonMatchDays)
            //{
            //    foreach (var m in md.Matches)
            //    {
            //        var c = m.HomeClub;
            //        var c2 = m.AwayClub;

            
            
            //c2.Coach.Philospophie = new OffensivePhilopshie();
            //c.Coach.Philospophie = new DefensivePhilosophie();

            50.Times(() =>
            {
                var c = WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.AssociationLooks.First(), w.Nations.First(), w.Associations.First().Power);
                var c2 = WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.AssociationLooks.First(), w.Nations.First(), w.Associations.First().Power);
                //WorldGenerator.GenerateRandomClub(w, w.Associations.First(), w.Nations.First(), w.Associations.First().Power);
                var m = new Match();
                m.HomeCompetitor = new LeagueCompetitor() { Club = c, Points = 0 };
                m.AwayCompetitor = new LeagueCompetitor() { Club = c2, Points = 0 };
                var mr = m.Simulate(true);
                var p1 = c.Coach.Philospophie.GetType().Name;
                var p2 = c2.Coach.Philospophie.GetType().Name;

                if (!totalCount.ContainsKey(p1))
                {
                    totalCount[p1] = 1;
                }
                else
                {
                    totalCount[p1]++;
                }

                if (!totalCount.ContainsKey(p2))
                {
                    totalCount[p2] = 1;
                }
                else
                {
                    totalCount[p2]++;
                }

                var winP = m.MatchResult.HomeGoals > m.MatchResult.AwayGoals ? p1 : m.MatchResult.AwayGoals > m.MatchResult.HomeGoals ? p2 : "";
                if (winP != "")
                {
                    if (!winCount.ContainsKey(winP))
                    {
                        winCount[winP] = 1;
                    }
                    else
                    {
                        winCount[winP]++;
                    }
                }

                if (m.MatchResult.HomeGoals != m.MatchResult.AwayGoals)
                {
                    winGoals.Add(m.MatchResult.HomeGoals > m.MatchResult.AwayGoals ? m.MatchResult.HomeGoals : m.MatchResult.AwayGoals);
                    setPlaySkillWinner.Add(m.MatchResult.HomeGoals > m.MatchResult.AwayGoals ? m.HomeClub.StartingLineUp.Players.Max(x => (int)x.SetPlaySkill) : m.AwayClub.StartingLineUp.Players.Max(x => (int)x.SetPlaySkill));
                    loseGoals.Add(m.MatchResult.HomeGoals < m.MatchResult.AwayGoals ? m.MatchResult.HomeGoals : m.MatchResult.AwayGoals);
                    setPlaySkillLoser.Add(m.MatchResult.HomeGoals < m.MatchResult.AwayGoals ? m.HomeClub.StartingLineUp.Players.Max(x => (int)x.SetPlaySkill) : m.AwayClub.StartingLineUp.Players.Max(x => (int)x.SetPlaySkill));

                    if (m.HomeClub.StartingLineUp.Players.Average(x => x.ValueBase) > m.AwayClub.StartingLineUp.Players.Average(x => x.ValueBase))
                    {
                        if (m.MatchResult.HomeGoals > m.MatchResult.AwayGoals)
                        {
                            strongerTeamWinCount++;
                        }
                        else
                        {
                            strongerTeamLoseCount++;
                        }
                    }
                    else
                    {
                        if (m.MatchResult.AwayGoals > m.MatchResult.HomeGoals)
                        {
                            strongerTeamWinCount++;
                        }
                        else
                        {
                            strongerTeamLoseCount++;
                        }
                    }
                }
                //Console.WriteLine(mr);
            });
            //}
            //}

            foreach (var k in totalCount.Keys)
            {
                Console.WriteLine(k + ": " + ((double)((double)winCount[k] / (double)totalCount[k])));
            }

            Console.WriteLine("AverageWinGoals: " + winGoals.Average());
            Console.WriteLine("AverageLoseGoals: " + loseGoals.Average());
            Console.WriteLine("StrongerTeamWin: " + (double)(strongerTeamWinCount / 1000d));
            Console.WriteLine("StrongerTeamLose: " + (double)(strongerTeamLoseCount / 1000d));
            Console.WriteLine("SetPlaySkillWinner: " + setPlaySkillWinner.Average());
            Console.WriteLine("SetPlaySkillLoser: " + setPlaySkillLoser.Average());


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

        //int lvl = 6;
        //private Player NewPlayer(Position p)
        //{
        //    return WorldGenerator.GenerateRandomPlayer(w, null, w.Nations[0], p, lvl, 17, 37);
        //}

    }
}
