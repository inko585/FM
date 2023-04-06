using FM;
using FM.Common;
using FM.Common;
using FM.Common.Generic;
using FM.Common.Season;
using FM.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FootballPit
{

    public class Match
    {

        public Match()
        {
            IsPlayed = false;
        }

        public LeagueCompetitor HomeCompetitor { get; set; }
        public LeagueCompetitor AwayCompetitor { get; set; }
        public Club HomeClub
        {
            get
            {
                return HomeCompetitor.Club;
            }
        }
        public Club AwayClub
        {
            get
            {
                return AwayCompetitor.Club;
            }
        }

        public bool AwayDressSwitch
        {
            get
            {
                return HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.MainColorString /*|| HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.SecondColorString*/;
            }
        }

        public bool IsPlayed { get; set; }


        public MatchResult MatchResult { get; set; }

        public static int PENALTY_AREA_BORDER = 2;
        public static int FIELD_AREAS = 7;
        public static double ATTK_FACTOR_INIT = 1.5;
        public static double ATTK_FACTOR_AREA_MULTIPLIER = 0.89;
        public static double GOALSHOT_FACTOR_INIT = 1.2;
        public static double GOALSHOT_AREA_MULTIPLIER = 0.82;
        public static double SWEEPER_PICK_FACTOR = 0.3;
        public static double CENTRALPLAYER_PICK_FACTOR = 0.3;
        public static double FITNESS_DECAY_OFF = 2d;
        public static double FITNESS_DECAY_DEF = 8d;
        public static double FITNESS_DECAY_TACTIC_BONUS = 4d;
        public static double FITNESS_DECAY_PASSIVE_OFF = 1.5d;
        public static double FITNESS_DECAY_PASSIVE_DEF = 5d;
        public static double FITNESS_DECAY_PASSIVE_TACTIC_BONUS = 2d;
        public static double ATTK_FACTOR_OFFENSIVE = 1.2;
        public static double ATTK_FACTOR_NORMAL = 1d;
        public static double DEF_FACTOR_HARD_TACKLING = 1.07;
        public static double DEF_FACTOR_NORMAL_TACKLING = 1d;
        public static double DEF_FACTOR_LOW_TACKING = 0.93;
        //public static double DEF_FACTOR_OFFENSIVE = 0.8;
        //public static double DEF_FACTOR_NORMAL = 1;
        public static double GAME_LENGTH = 40;
        public static int TENSION = 1000;
        public static int TENSION_GOAL_SHOT = 1000;
        public static double LONGSHOT_P_INIT = 0.4;
        public static double LONGSHOT_P_INIT_HIGH = 0.5;
        public static double LONGSHOT_P_INIT_LOW = 0.3;
        public static double LONGSHOT_P_AREA_MULTIPLIER = 0.5;
        public static double FREEKICK_P = 0.1;
        public static double FREEKICK_P_BRUTAL = 0.22;
        public static double FREEKICK_P_CLEAN = 0.01;
        public static double PENALTYSHOT_FACTOR = 1.4;
        public static double FREEKICK_FACTOR_INIT = 1.25;
        public static double FREEKICK_FACTOR_AREA_MULTIPLIER = 0.9;

        public MatchResult Simulate(bool silent, MatchDay md = null)
        {
            var tension = silent ? 0 : TENSION;
            var tension_goalshot = silent ? 0 : TENSION_GOAL_SHOT;
            var res = new MatchResult();
            res.Viewer = Math.Min(HomeClub.ViewerAttraction, HomeClub.StadiumCapacity);
            res.MatchDayString = md == null ? "-" : "# " + md.Number;
            res.ObservableHomeStarters = new ObservableCollection<MatchPlayer>(HomeClub.StartingLineUp.Players.Select(pl => MatchPlayer(pl)));
            res.ObservableAwayStarters = new ObservableCollection<MatchPlayer>(AwayClub.StartingLineUp.Players.Select(pl => MatchPlayer(pl)));
            //res.HomeLineUp = HomeClub.StartingLineUp;
            //res.AwayLineUp = AwayClub.StartingLineUp;
            //res.HomeBench = HomeClub.Bench;
            //res.AwayBench = AwayClub.Bench;
            res.HomeClub = HomeClub;
            res.AwayClub = AwayClub;
            var currentHomeLU = HomeClub.StartingLineUp;
            var currentAwayLU = AwayClub.StartingLineUp;
            var currentHomeBench = HomeClub.Bench;
            var currentAwayBench = AwayClub.Bench;
            var offClub = HomeClub;
            var defClub = AwayClub;
            var offLineUp = currentHomeLU;
            var defLineUp = currentAwayLU;
            var area = FIELD_AREAS / 2;
            var gform = new GameForm(HomeClub, AwayClub, FIELD_AREAS);
            if (!silent)
            {
                gform.Show();
            }
            for (var i = 0; i < GAME_LENGTH; i++)
            {
                if (!silent)
                {
                    gform.UpdateHomeLineup(currentHomeLU);
                    gform.UpdateAwayLineUp(currentAwayLU);
                    gform.SetArea(area, offClub);
                }

                if (i > 5)
                {
                    MakeSubstitutions(res, i, HomeClub, currentHomeLU, currentHomeBench);
                    MakeSubstitutions(res, i, AwayClub, currentAwayLU, currentAwayBench);
                }
                Thread.Sleep(tension);
                if (IsGoalShotFired(offLineUp, area))
                {
                    var isGoal = GoalShot(offLineUp.FieldPlayers, defLineUp.Goalie, area, out Player p);
                    gform.Log(p.LastName + " (" + offClub.Name + ") schießt!", i);
                    Thread.Sleep(tension_goalshot);
                    if (isGoal)
                    {
                        AddGoal(res, offClub, MatchPlayer(p), i);
                        area = FIELD_AREAS / 2;
                        gform.Log("Tor von " + p.LastName + " (" + offClub.Name + ")", i);
                    }
                    else
                    {
                        gform.Log("Daneben!", i);
                        area = FIELD_AREAS;
                    }
                }
                else
                {
                    if (Battle(offLineUp, defLineUp, area))
                    {
                        area++;
                        continue;

                    }
                    else
                    {

                        if (IsFoul(defLineUp))
                        {
                            if (area > (FIELD_AREAS / 2))
                            {
                                var distance = FIELD_AREAS - area;
                                var p = offLineUp.KickTaker;
                                if (distance == 1)
                                {
                                    gform.Log("Elfmeter für " + offClub.Name, i);

                                }
                                else
                                {
                                    gform.Log("Freistoß für " + offClub.Name, i);
                                }

                                gform.Log(p.LastName + " schießt..", i);
                                Thread.Sleep(tension_goalshot);
                                if (FreeKickOrPenalty(offLineUp, defLineUp, area))
                                {
                                    AddGoal(res, offClub, MatchPlayer(offLineUp.KickTaker), i);
                                    area = FIELD_AREAS / 2;
                                    gform.Log("Tor!", i);
                                }
                                else
                                {
                                    area = FIELD_AREAS;
                                    gform.Log("Daneben!", i);
                                }
                            }
                            else
                            {
                                area++;
                                area++;
                                continue;
                            }
                        }


                    }
                }
                SwitchPossession(currentHomeLU, currentAwayLU, out offClub, out defClub, ref offLineUp, out defLineUp, ref area);

            }

            ResetFitness();
            MatchResult = res;
            IsPlayed = true;

            HomeClub.ResetLineup();
            AwayClub.ResetLineup();
            var xpPlayers = new List<Player>();
            foreach (var p in HomeClub.StartingLineUp.Players)
            {
                p.AccountXP(Game.XP_MATCH);
                xpPlayers.Add(p);
                p.PlayerStatistics.Last().Matches++;
            }
            foreach (var p in AwayClub.StartingLineUp.Players)
            {
                p.AccountXP(Game.XP_MATCH);
                xpPlayers.Add(p);
                p.PlayerStatistics.Last().Matches++;
            }

            foreach (var sub in res.Substitutions)
            {
                if (!xpPlayers.Contains(sub.In.Player))
                {
                    sub.In.Player.AccountXP(Game.XP_MATCH_SUB);
                    xpPlayers.Add(sub.In.Player);
                    sub.In.Player.PlayerStatistics.Last().Matches++;
                }
            }
            return res;
        }

        public void MakeSubstitutions(MatchResult mr, int min, Club c, LineUp lineUp, List<Player> bench)
        {
            var playersOrdered = lineUp.Players.OrderBy(pl => pl.ValueForCoachCurrent).ToList();

            foreach (var po in playersOrdered)
            {
                var newPlayer = bench.FirstOrDefault(bp => bp.Position == po.Position && bp.ValueForCoachCurrent > (po.ValueForCoachCurrent * 1.1));
                if (newPlayer != null)
                {
                    lineUp.Players.Remove(po);
                    lineUp.Players.Add(newPlayer);
                    bench.Remove(newPlayer);
                    bench.Add(po);
                    mr.Substitutions.Add(new Substitution(c, MatchPlayer(newPlayer), MatchPlayer(po), min));
                }

            }
        }

        public MatchPlayer MatchPlayer(Player p)
        {
            BitmapImage image;
            if (HomeClub.Rooster.Contains(p) || !AwayDressSwitch)
            {
                image = p.PlayerImage;
            } else
            {
                image = p.PlayerImage_Away;
            }

            return new MatchPlayer()
            {
                Player = p,
                MatchPlayerImage = image
            };
        }

        private void ResetFitness()
        {
            HomeClub.Rooster.ForEach(p => p.Fitness = 100);
            AwayClub.Rooster.ForEach(p => p.Fitness = 100);
        }

        private void SwitchPossession(LineUp currentHomeLU, LineUp currentAwayLU, out Club offClub, out Club defClub, ref LineUp offLineUp, out LineUp defLineUp, ref int area)
        {
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

        public static Random Random = new Random(DateTime.Now.Millisecond);

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




        //internal Player DistributeGoal(LineUp lu)
        //{
        //    var pDIct = new Dictionary<Player, double>();
        //    lu.Players.Where(p => p.Position != Position.Goalie).ToList().ForEach(x => pDIct.Add(x, Util.GetGaussianRandom(x.CurrentSkill, Player.MAX_SKILL / 4)));
        //    return pDIct.OrderByDescending(x => x.Value).First().Key;
        //}


        internal bool IsFoul(LineUp def)
        {
            var dice = Random.NextDouble();
            return (dice < (def.Tackling == Tackling.Brutal ? FREEKICK_P_BRUTAL : def.Tackling == Tackling.Clean ? FREEKICK_P_CLEAN : FREEKICK_P));
        }

        public bool FreeKickOrPenalty(LineUp off, LineUp def, int area)
        {
            var r1 = off.KickTaker.SetPlaySkill + Random.Next(1, 20);
            var check = 20 + def.Goalie.Keeping / 10;
            //var r2 = RollSkill(def.Goalie.Keeping, def.Goalie.Keeping);
            var distance = FIELD_AREAS - area;
            var offFactor = (distance > 1) ? FREEKICK_FACTOR_INIT * (Math.Pow(FREEKICK_FACTOR_AREA_MULTIPLIER, distance)) : PENALTYSHOT_FACTOR;
            return r1 * offFactor > check;

        }





        internal void AddGoal(MatchResult mr, Club c, MatchPlayer scorer, int minute)
        {
            if (c.Equals(HomeClub))
            {
                mr.HomeGoals = mr.HomeGoals + 1;
            }
            else
            {
                mr.AwayGoals = mr.AwayGoals + 1;
            }

            scorer.Player.PlayerStatistics.Last().Goals++;
            mr.Scorers.Add(new ScoreEvent() { Scorer = scorer, Club = c, CurrentScore = mr.ResultString, Minute = minute });

        }

        internal Boolean GoalShot(List<Player> off, Player goalie, int area, out Player shotTaker)
        {
            var offFactor = GOALSHOT_FACTOR_INIT * Math.Pow(GOALSHOT_AREA_MULTIPLIER, FIELD_AREAS - area);
            var offSkillAverage = off.Sum(x => x.GoalThreat) / off.Count;
            var offSingle = off.OrderByDescending(x => x.GoalThreat + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 5)).First();
            shotTaker = offSingle;
            var r1 = RollSkill(offSingle.GoalThreat, offSkillAverage);
            var r2 = RollSkill(goalie.Keeping, goalie.Keeping);

            offSingle.DecayFitness(1f);
            goalie.DecayFitness(3f);

            var goal = r1 * offFactor > r2;

            return goal;
        }



        internal Boolean Battle(LineUp off, LineUp def, int area)
        {
            var offPlayers = new List<Player>();
            var defPlayers = new List<Player>();

            offPlayers.AddRange(off.Midfielders);
            defPlayers.AddRange(def.Midfielders);
            if (area >= (FIELD_AREAS / 2))
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


            var offSingle = off.FieldPlayers.OrderByDescending(x => (x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 10)) * GetPickFactor(off, x, area, true)).First();
            var defSingle = def.FieldPlayers.OrderByDescending(x => (x.Fitness + Util.GetGaussianRandom(defFitAverage, defFitAverage / 10)) * GetPickFactor(def, x, area, false)).First();

            var offBonus = ((off.Tactic == Tactic.Offensive) ? ATTK_FACTOR_OFFENSIVE : ATTK_FACTOR_NORMAL);
            var offAreaFactor = ATTK_FACTOR_INIT * Math.Pow(ATTK_FACTOR_AREA_MULTIPLIER, area);
            var r1 = RollSkill(offSingle.Attk, offSkillAverage) * offAreaFactor * offBonus;
            var defBonus = (def.Tackling == Tackling.Brutal) ? DEF_FACTOR_HARD_TACKLING : (def.Tackling == Tackling.Normal) ? DEF_FACTOR_NORMAL_TACKLING : DEF_FACTOR_LOW_TACKING;
            var r2 = RollSkill(defSingle.Def, defSkillAverage) * defBonus;

            var decayOff = FITNESS_DECAY_OFF;
            var decayDef = (def.Tactic == Tactic.Defensive) ? FITNESS_DECAY_DEF - FITNESS_DECAY_TACTIC_BONUS : FITNESS_DECAY_DEF;
            offSingle.DecayFitness((float)decayOff);
            foreach (var pl in offPlayers.Where(oP => oP != offSingle))
            {
                pl.DecayFitness((float)FITNESS_DECAY_PASSIVE_OFF);
            }
            foreach (var pl in defPlayers.Where(dP => dP != defSingle))
            {
                pl.DecayFitness((float)(def.Tactic == Tactic.Defensive ? FITNESS_DECAY_PASSIVE_TACTIC_BONUS : FITNESS_DECAY_PASSIVE_DEF));
            }
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
            return Util.GetGaussianRandom(singleSkill, Player.MAX_SKILL / 3.08) * 0.5 + Util.GetGaussianRandom(avgSkill, Player.MAX_SKILL / 3.08) * 0.5;
        }

        //internal double RollSkill(double singleSkill, double singleSkill2, double avgSkill)
        //{
        //    return Util.GetGaussianRandom(singleSkill, Player.MAX_SKILL / 3) * 0.4 + Util.GetGaussianRandom(singleSkill2, Player.MAX_SKILL / ) * 0.2 + Util.GetGaussianRandom(avgSkill, Player.MAX_SKILL / 5) * 0.4;
        //}

    }



    public class MatchResult
    {

        public List<ScoreEvent> Scorers { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public int Viewer { get; set; }
        public string MatchDayString { get; set; }

        public Club HomeClub { get; set; }

        public Club AwayClub { get; set; }

        public BitmapImage HomeDress
        {
            get
            {
                return HomeClub.DressImage;
            }
        }

        public BitmapImage AwayDress
        {
            get
            {
                return AwayDressSwitch ? AwayClub.AwayDressImage : AwayClub.DressImage;
            }
        }

        public bool AwayDressSwitch
        {
            get
            {
                return HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.MainColorString /*|| HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.SecondColorString*/;
            }
        }

        public ObservableCollection<MatchPlayer> ObservableHomeStarters { get; set; }
        public ObservableCollection<MatchPlayer> ObservableAwayStarters { get; set; }

        public List<Substitution> Substitutions { get; set; }


        public MatchResult()
        {
            Scorers = new List<ScoreEvent>();
            Substitutions = new List<Substitution>();
        }

        public string ResultString
        {
            get
            {
                return HomeGoals + " : " + AwayGoals;
            }
        }

        //public override string ToString()
        //{
        //    return HomeClub.Name + " (" + HomeLineUp.Players.Average(p => p.SkillMax) + ") - " + AwayClub.Name + " (" + AwayLineUp.Players.Average(p => p.SkillMax) + ") " + ResultString;
        //}


    }

    public class Substitution
    {
        public Club Club { get; set; }
        public MatchPlayer In { get; set; }
        public MatchPlayer Out { get; set; }
        public int Minute { get; set; }
        public Substitution(Club c, MatchPlayer inP, MatchPlayer outP, int minute)
        {
            Club = c;
            In = inP;
            Out = outP;
            Minute = minute;
        }
    }

    public class ScoreEvent
    {
        public MatchPlayer Scorer { get; set; }
        public Club Club { get; set; }
        public string CurrentScore { get; set; }
        public int Minute { get; set; }
    }

}






