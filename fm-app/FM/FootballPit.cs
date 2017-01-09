using FM.Common;
using FM.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballPit
{

    public class Match
    {
        public int LengthInActions { get; set; }
        public Club HomeClub { get; set; }
        public Club AwayClub { get; set; }


        public MatchResult Simulate()
        {

            var res = new MatchResult();
            res.HomeLineUp = HomeClub.StartingLineUp;
            res.AwayLineUp = AwayClub.StartingLineUp;
            var currentHomeLU = HomeClub.StartingLineUp;
            var currentAwayLU = AwayClub.StartingLineUp;
            //var currentHomeBench = HomeClub.StartingBench;
            //var currentAwayBench = AwayClub.StartingBench;
            var offClub = HomeClub;
            var defClub = AwayClub;
            var offLineUp = currentHomeLU;
            var defLineUp = currentAwayLU;
            for (var i = 0; i < LengthInActions; i++)
            {
                if (FreeKickOrPenaltyGoal(offLineUp, defLineUp))
                {
                    AddGoal(res, offClub, offLineUp.KickTaker);
                }
                else
                {

                    if (TryAttack(offLineUp, defLineUp))
                    {
                        var scorer = DistributeGoal(offLineUp, true);
                        AddGoal(res, offClub, scorer);

                    }


                }
                if (offLineUp.Equals(currentHomeLU))
                {
                    offLineUp = currentAwayLU;
                    offClub = AwayClub;
                    defLineUp = currentHomeLU;
                    defClub = HomeClub;
                }
                else
                {
                    defLineUp = currentAwayLU;
                    defClub = AwayClub;
                    offLineUp = currentHomeLU;
                    offClub = HomeClub;
                }
                //Substitute(ref currentHomeLU, ref currentHomeBench, HomeClub, res);
                //Substitute(ref currentAwayLU, ref currentAwayBench, AwayClub, res);
            }

            return res;
        }


        //internal void Substitute(ref LineUp lineUp, ref LineUp bench, Club c, MatchResult mr)
        //{
        //    Substitute(ref lineUp.Goalies, ref bench.Goalies, c, mr);
        //    Substitute(ref lineUp.Defenders, ref bench.Defenders,c, mr);
        //    Substitute(ref lineUp.Midfielders, ref bench.Midfielders, c, mr);
        //    Substitute(ref lineUp.Strikers, ref bench.Strikers,c, mr);
        //}

        //internal void Substitute(ref List<Player> a, ref List<Player> b, Club c, MatchResult mr)
        //{
        //    if (b.Count > 0)
        //    {
        //        var playerA = a.OrderBy(x => x.CurrentSKill).First();
        //        var playerB = b.OrderByDescending(x => x.CurrentSKill).First();

        //        if (playerA.CurrentSKill * 1.3 <= playerB.CurrentSKill)
        //        {
        //            a.Remove(playerA);
        //            b.Add(playerA);
        //            a.Add(playerB);
        //            b.Remove(playerB);
        //            mr.Substitutions.Add(new Substitution(c, playerB, playerA));
        //        }
        //    }
        //}

        internal Player DistributeGoal(LineUp lu, bool goalgetterBonus)
        {
            var p = new Dictionary<Player, double>();
            lu.Players.ForEach(x => p.Add(x, Util.GetGaussianRandom(x.CurrentSkill, Player.MAX_SKILL / 5)));
            return p.OrderByDescending(x => x.Value).First().Key;

        }

        internal bool FreeKickOrPenaltyGoal(LineUp off, LineUp def)
        {
            var dice = Util.GetGaussianRandom(0.5, 0.25);
            var freekickmargin = 0.2 * def.FreeKickRisk;
            var penaltymargin = 0.05 * def.FreeKickRisk;
            var r1 = RollSkill(off.KickTaker.SetPlaySkill, off.KickTaker.SetPlaySkill);
            var r2 = RollSkill(def.Goalie.Def, def.Goalie.Def);
            if (dice < penaltymargin)
            {
                Console.WriteLine("Penalty " + off.KickTaker.LastName);
                var goal = r1 * 1.4 > r2;
                Console.WriteLine(goal ? "Goal!" : "No Goal");
                return goal;
            }
            else
            {
                if (dice < freekickmargin)
                {
                    Console.WriteLine("Freekick " + off.KickTaker.LastName);
                    var goal = r1 > r2 * 1.2;
                    Console.WriteLine(goal ? "Goal!" : "No Goal");
                    return goal;
                }
                else
                {
                    return false;
                }
            }
        }

        internal Boolean TryAttack(LineUp off, LineUp def)
        {

            if (Battle(off, def))
            {
                if (GoalShot(off.FieldPlayers, def.Goalie))
                {
                    return true;
                }

            }

            return false;

        }

        internal void AddGoal(MatchResult mr, Club c, Player scorer)
        {
            if (c.Equals(HomeClub))
            {
                mr.HomeGoals = mr.HomeGoals + 1;
            }
            else
            {
                mr.AwayGoals = mr.AwayGoals + 1;
            }

            mr.Scorers.Add(scorer);

        }

        internal Boolean GoalShot(List<Player> off, Player goalie)
        {

            var offSkillAverage = off.Sum(x => x.Attk) / off.Count;
            var offSingle = off.OrderByDescending(x => x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 5)).First();
            Console.WriteLine("Oppurtunity " + offSingle.LastName);
            var r1 = RollSkill(offSingle.Attk, offSkillAverage);
            var r2 = RollSkill(goalie.Def, goalie.Def) * 1.4f;

            offSingle.DecayFitness(2f);
            goalie.DecayFitness(2f);

            var goal = r1 > r2;
            if (goal)
            {
                Console.WriteLine("Goal!");
            }
            else
            {
                Console.WriteLine("No Goal");
            }
            return goal;
        }

        internal Boolean Battle(LineUp off, LineUp def)
        {

            var offSkillAverage = off.FieldPlayers.Sum(x => x.Attk) / off.FieldPlayers.Count;
            var defSkillAverage = def.FieldPlayers.Sum(x => x.Def) / def.FieldPlayers.Count;
            var defFitAverage = def.FieldPlayers.Sum(x => x.Fitness) / def.FieldPlayers.Count;


            var offSingle = off.FieldPlayers.OrderByDescending(x => (x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 5)) * ((x == off.CentralPlayer || x == def.Sweeper) ? 0.8 : 1)).First();
            var defSingle = def.FieldPlayers.OrderByDescending(x => (x.Fitness + Util.GetGaussianRandom(defFitAverage, defFitAverage / 5)) * ((x == def.Sweeper || x.Position == Position.Striker) ? 0.8 : 1)).First();

            var offBonus = (off.Tactic == Tactic.Offensive) ? 1.2f : 1f;
            var r1 = (off.CentralPlayer == null ? RollSkill(offSingle.Attk, offSkillAverage) : RollSkill(offSingle.Attk, off.CentralPlayer.Attk, offSkillAverage)) * offBonus;
            var defBonus = (def.Tackling == Tackling.Brutal) ? 1.6f : (def.Tackling == Tackling.Normal) ? 1.4f : 1.2f;
            var r2 = (def.Sweeper == null ? RollSkill(defSingle.Def, defSkillAverage) : RollSkill(defSingle.Def, def.Sweeper.Def, defSkillAverage)) * defBonus;

            var decayOff = 6f;
            var decayDef = (def.Tactic == Tactic.Defensive) ? 4f : 6f;
            offSingle.DecayFitness(decayOff);
            defSingle.DecayFitness(decayDef);

            return r1 > r2;

        }

        internal double RollSkill(double singleSkill, double avgSkill)
        {
            return Util.GetGaussianRandom(singleSkill, Player.MAX_SKILL / 5) * 0.5 + Util.GetGaussianRandom(avgSkill, Player.MAX_SKILL / 5) * 0.5;
        }

        internal double RollSkill(double singleSkill, double singleSkill2, double avgSkill)
        {
            return Util.GetGaussianRandom(singleSkill, Player.MAX_SKILL / 5) * 0.4 + Util.GetGaussianRandom(singleSkill2, Player.MAX_SKILL / 5) * 0.2 + Util.GetGaussianRandom(avgSkill, Player.MAX_SKILL / 5) * 0.4;
        }

    }



    public class MatchResult
    {

        public List<Player> Scorers { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public LineUp HomeLineUp { get; set; }
        public LineUp AwayLineUp { get; set; }
        public List<Substitution> Substitutions { get; set; }

        public MatchResult()
        {
            Scorers = new List<Player>();
            Substitutions = new List<Substitution>();
        }
    }

    public class Substitution
    {
        public Club Club { get; set; }
        public Player In { get; set; }
        public Player Out { get; set; }
        public Substitution(Club c, Player inP, Player outP)
        {
            Club = c;
            In = inP;
            Out = outP;
        }
    }

}






