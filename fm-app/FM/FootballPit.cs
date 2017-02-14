using FM;
using FM.Common;
using FM.Entities.Generic;
using FM.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FootballPit
{

    public class Match
    {

        public Club HomeClub { get; set; }
        public Club AwayClub { get; set; }

        public static int PENALTY_AREA_BORDER = 2;
        public static int FIELD_AREAS = 8;
        public static double ATTK_FACTOR_INIT = 1.25;
        public static double ATTK_FACTOR_AREA_MULTIPLIER = 0.98;
        public static double GOALSHOT_FACTOR_INIT = 1d;
        public static double GOALSHOT_AREA_MULTIPLIER = 0.75;
        public static double SWEEPER_PICK_FACTOR = 0.3;
        public static double CENTRALPLAYER_PICK_FACTOR = 0.3;
        public static double FITNESS_DECAY_OFF = 3d;
        public static double FITNESS_DECAY_DEF = 6d;
        public static double FITNESS_DECAY_TACTIC_BONUS = 2d;
        public static double ATTK_FACTOR_OFFENSIVE = 1.2;
        public static double ATTK_FACTOR_NORMAL = 1d;
        public static double DEF_FACTOR_HARD_TACKLING = 1.4;
        public static double DEF_FACTOR_NORMAL_TACKLING = 1d;
        public static double DEF_FACTOR_LOW_TACKING = 0.8;
        public static double GAME_LENGTH = 90;
        public static double LONGSHOT_P_INIT = 0.4;
        public static double LONGSHOT_P_INIT_HIGH = 0.5;
        public static double LONGSHOT_P_INIT_LOW = 0.3;
        public static double LONGSHOT_P_AREA_MULTIPLIER = 0.5;
        public static double FREEKICK_P = 0.15;
        public static double FREEKICK_P_BRUTAL = 0.2;
        public static double FREEKICK_P_CLEAN = 0.1;
        public static double PENALTYSHOT_FACTOR = 1.4;
        public static double FREEKICK_FACTOR_INIT = 0.9;
        public static double FREEKICK_FACTOR_AREA_MULTIPLIER = 0.25;

        public MatchResult Simulate()
        {
            var res = new MatchResult();
            res.HomeLineUp = HomeClub.StartingLineUp;
            res.AwayLineUp = AwayClub.StartingLineUp;
            var currentHomeLU = HomeClub.StartingLineUp;
            var currentAwayLU = AwayClub.StartingLineUp;
            var offClub = HomeClub;
            var defClub = AwayClub;
            var offLineUp = currentHomeLU;
            var defLineUp = currentAwayLU;
            var area = FIELD_AREAS / 2;
            var gform = new GameForm(HomeClub, AwayClub, FIELD_AREAS);
            gform.Show();
            for (var i = 0; i < GAME_LENGTH; i++)
            {

                gform.UpdateHomeLineup(currentHomeLU);
                gform.UpdateAwayLineUp(currentAwayLU);
                gform.SetArea(area, offClub);
                Thread.Sleep(200);
                if (IsGoalShotFired(offLineUp, area))
                {
                    gform.Log("Schuss!", i);
                    Thread.Sleep(50);
                    if (GoalShot(offLineUp.FieldPlayers, defLineUp.Goalie, area))
                    {
                        var p = DistributeGoal(offLineUp);
                        AddGoal(res, offClub, p);
                        area = FIELD_AREAS / 2;
                        gform.Log("Tor von " + p.LastName + " (" + offClub.Name + ")", i);
                    }
                }
                else
                {
                    if (area > area / 2 && IsFoul(defLineUp))
                    {
                        if (FreeKickOrPenalty(offLineUp, defLineUp, area))
                        {
                            AddGoal(res, offClub, DistributeGoal(offLineUp));
                            area = FIELD_AREAS / 2;
                        }
                    }
                    else
                    {
                        if (Battle(offLineUp, defLineUp, area))
                        {
                            area++;
                            continue;
                        }
                    }


                    area = FIELD_AREAS - area;
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

                }

            }
            
            return res;
        }

        public static Random Random = new Random(1860);

        public bool IsGoalShotFired(LineUp lu, int area)
        {
            if (area == FIELD_AREAS)
            {
                return true;
            }
            var init = lu.LongShots == Frequency.High ? LONGSHOT_P_INIT_HIGH : lu.LongShots == Frequency.Normal ? LONGSHOT_P_INIT : LONGSHOT_P_INIT_LOW;
            var p = init * Math.Pow(LONGSHOT_P_AREA_MULTIPLIER, FIELD_AREAS - area);

            return Random.NextDouble() < p;
        }




        internal Player DistributeGoal(LineUp lu)
        {
            var p = new Dictionary<Player, double>();
            lu.Players.ForEach(x => p.Add(x, Util.GetGaussianRandom(x.CurrentSkill, Player.MAX_SKILL / 5)));
            return p.OrderByDescending(x => x.Value).First().Key;
        }


        internal bool IsFoul(LineUp def)
        {
            var dice = Random.NextDouble();
            return (dice < (def.Tackling == Tackling.Brutal ? FREEKICK_P_BRUTAL : def.Tackling == Tackling.Clean ? FREEKICK_P_CLEAN : FREEKICK_P));
        }

        public bool FreeKickOrPenalty(LineUp off, LineUp def, int area)
        {
            var r1 = RollSkill(off.KickTaker.SetPlaySkill, off.KickTaker.SetPlaySkill);
            var r2 = RollSkill(def.Goalie.Def, def.Goalie.Def);
            var distance = FIELD_AREAS - area;
            var offFactor = (distance > 1) ? FREEKICK_FACTOR_INIT * Math.Pow(FREEKICK_FACTOR_AREA_MULTIPLIER, distance) : PENALTYSHOT_FACTOR;
            return r1 * offFactor > r2;

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

        internal Boolean GoalShot(List<Player> off, Player goalie, int area)
        {
            var offFactor = GOALSHOT_FACTOR_INIT * Math.Pow(GOALSHOT_AREA_MULTIPLIER, FIELD_AREAS - area);
            var offSkillAverage = off.Sum(x => x.Attk) / off.Count;
            var offSingle = off.OrderByDescending(x => x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 5)).First();
            var r1 = RollSkill(offSingle.Attk, offSkillAverage);
            var r2 = RollSkill(goalie.Def, goalie.Def) * 1.4f;

            offSingle.DecayFitness(2f);
            goalie.DecayFitness(2f);

            var goal = r1 * offFactor > r2;
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



        internal Boolean Battle(LineUp off, LineUp def, int area)
        {
            var offPlayers = new List<Player>();
            var defPlayers = new List<Player>();

            offPlayers.AddRange(off.Midfielders);
            defPlayers.AddRange(def.Midfielders);
            if (area >= FIELD_AREAS / 2)
            {
                offPlayers.AddRange(off.Strikers);
                defPlayers.AddRange(def.Defenders);

            }
            else
            {
                offPlayers.AddRange(off.Defenders);
                defPlayers.AddRange(def.Strikers);
            }

            var offSkillAverage = off.FieldPlayers.Sum(x => x.Attk) / Math.Max(offPlayers.Count, defPlayers.Count);
            var defSkillAverage = def.FieldPlayers.Sum(x => x.Def) / Math.Max(offPlayers.Count, defPlayers.Count);
            var defFitAverage = def.FieldPlayers.Sum(x => x.Fitness) / def.FieldPlayers.Count;


            var offSingle = off.FieldPlayers.OrderByDescending(x => (x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 5)) * GetPickFactor(off, x, area, true)).First();
            var defSingle = def.FieldPlayers.OrderByDescending(x => (x.Fitness + Util.GetGaussianRandom(defFitAverage, defFitAverage / 5)) * GetPickFactor(def, x, area, false)).First();

            var offBonus = ((off.Tactic == Tactic.Offensive) ? ATTK_FACTOR_OFFENSIVE : ATTK_FACTOR_NORMAL);
            var offAreaFactor = ATTK_FACTOR_INIT * Math.Pow(ATTK_FACTOR_AREA_MULTIPLIER, area);
            var r1 = RollSkill(offSingle.Attk, offSkillAverage) * offAreaFactor * offBonus;
            var defBonus = (def.Tackling == Tackling.Brutal) ? DEF_FACTOR_HARD_TACKLING : (def.Tackling == Tackling.Normal) ? DEF_FACTOR_NORMAL_TACKLING : DEF_FACTOR_LOW_TACKING;
            var r2 = RollSkill(defSingle.Def, defSkillAverage) * defBonus;

            var decayOff = FITNESS_DECAY_OFF;
            var decayDef = (def.Tactic == Tactic.Defensive) ? FITNESS_DECAY_DEF - FITNESS_DECAY_TACTIC_BONUS : FITNESS_DECAY_DEF;
            offSingle.DecayFitness((float)decayOff);
            defSingle.DecayFitness((float)decayDef);

            return r1 > r2;

        }

        private double GetPickFactor(LineUp l, Player p, int area, bool isOffence)
        {
            if (p == l.Sweeper)
            {
                if (area <= PENALTY_AREA_BORDER)
                {
                    return 1 + SWEEPER_PICK_FACTOR;
                }
                else
                {
                    return 1 - SWEEPER_PICK_FACTOR;
                }
            }

            if (p == l.CentralPlayer)
            {
                if (isOffence)
                {
                    return 1 + CENTRALPLAYER_PICK_FACTOR;
                }
                else
                {
                    return 1 - CENTRALPLAYER_PICK_FACTOR;
                }
            }

            return 1;
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






