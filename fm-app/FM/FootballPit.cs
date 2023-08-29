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
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FootballPit
{

    public class Match
    {


        public LeagueCompetitor HomeCompetitor { get => homeCompetitor; set { homeCompetitor = value; HomeClub = value.Club; } }
        public LeagueCompetitor AwayCompetitor { get => awayCompetitor; set { awayCompetitor = value; AwayClub = value.Club; } }
        public Club HomeClub { get; set; }

        public Club AwayClub { get; set; }

        public bool AwayDressSwitch
        {
            get
            {
                
                return HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.MainColorString || HomeClub.ClubColors.ComparableString == AwayClub.ClubColors.ComparableString /*|| HomeClub.ClubColors.MainColorString == AwayClub.ClubColors.SecondColorString*/;
            }
        }

        public bool IsPlayed
        {
            get
            {
                return MatchResult != null;
            }
        }


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
        public static double ATTK_FACTOR_NORMAL = 1;
        public static double DEF_FACTOR_HARD_TACKLING = 1.065;
        public static double DEF_FACTOR_NORMAL_TACKLING = 1d;
        public static double DEF_FACTOR_LOW_TACKING = 0.935;
        //public static double DEF_FACTOR_OFFENSIVE = 0.8;
        //public static double DEF_FACTOR_NORMAL = 1;
        public static double GAME_LENGTH = 60;
        public static int TENSION = 500;
        public static int TENSION_GOAL_SHOT = 3000;
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

        private List<Player> PosessionBackLog = new List<Player>();
        public MatchResult Simulate(bool silent, MatchDay md = null)
        {
            var tension = silent ? 0 : TENSION;
            var tension_goalshot = silent ? 0 : TENSION_GOAL_SHOT;
            var res = new MatchResult();
            res.Viewer = Math.Min(HomeClub.ViewerAttraction, HomeClub.StadiumCapacity);
            res.MatchDayString = md == null ? "-" : "# " + md.Number;
            res.ObservableHomeStarters = new ObservableCollection<MatchPlayer>(HomeClub.StartingLineUp.Players.Select(pl => MatchPlayer(pl)));
            res.HomeGoalieStarter = MatchPlayer(HomeClub.StartingLineUp.Goalie);
            res.HomeDefenderStarters = HomeClub.StartingLineUp.Defenders.Select(pl => MatchPlayer(pl)).ToList();
            res.HomeMidFielderStarters = HomeClub.StartingLineUp.Midfielders.Select(pl => MatchPlayer(pl)).ToList();
            res.HomeStrikerStarters = HomeClub.StartingLineUp.Strikers.Select(pl => MatchPlayer(pl)).ToList();
            res.ObservableAwayStarters = new ObservableCollection<MatchPlayer>(AwayClub.StartingLineUp.Players.Select(pl => MatchPlayer(pl)));
            res.AwayGoalieStarter = MatchPlayer(AwayClub.StartingLineUp.Goalie);
            res.AwayDefenderStarters = AwayClub.StartingLineUp.Defenders.Select(pl => MatchPlayer(pl)).ToList();
            res.AwayMidFielderStarters = AwayClub.StartingLineUp.Midfielders.Select(pl => MatchPlayer(pl)).ToList();
            res.AwayStrikerStarters = AwayClub.StartingLineUp.Strikers.Select(pl => MatchPlayer(pl)).ToList();
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

                if (i > 15)
                {
                    if (offClub != HomeClub)
                    {
                        MakeSubstitutions(res, i, HomeClub, currentHomeLU, currentHomeBench);
                    }
                    if (offClub != AwayClub)
                    {
                        MakeSubstitutions(res, i, AwayClub, currentAwayLU, currentAwayBench);
                    }
                }
                Thread.Sleep(tension);
                if (IsGoalShotFired(offLineUp, area))
                {

                    var isGoal = GoalShot(offLineUp.FieldPlayers, defLineUp.Goalie, area, out Player p);
                    PosessionBackLog.Add(p);

                    gform.Log(p.LastName + " (" + offClub.Name + ") schießt!", i);
                    Thread.Sleep(tension_goalshot);
                    if (isGoal)
                    {
                        AddGoal(res, offClub, MatchPlayer(p), i, out ScoreEvent se);
                        res.MatchEvents.Add(new MatchEvent(res.CreateGoalCommentary(true, PosessionBackLog, defLineUp.Goalie, (FIELD_AREAS - area) >= 3, offClub == HomeClub, i), null, se, i, true));
                        area = FIELD_AREAS / 2;
                        gform.Log("Tor von " + p.LastName + " (" + offClub.Name + ")", i);
                    }
                    else
                    {
                        gform.Log("Daneben!", i);
                        res.MatchEvents.Add(new MatchEvent(res.CreateGoalCommentary(false, PosessionBackLog, defLineUp.Goalie, (FIELD_AREAS - area) >= 3, offClub == HomeClub, i), null, null, i, true));
                        area = FIELD_AREAS;
                    }
                }
                else
                {
                    if (Battle(offLineUp, defLineUp, area, out Player off, out Player def))
                    {
                        PosessionBackLog.Add(off);
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
                                var text = new List<string>();
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
                                var isGoal = false;
                                ScoreEvent se = null;
                                if (FreeKickOrPenalty(offLineUp, defLineUp, area))
                                {
                                    isGoal = true;
                                    AddGoal(res, offClub, MatchPlayer(offLineUp.KickTaker), i, out se);
                                    area = FIELD_AREAS / 2;
                                    gform.Log("Tor!", i);
                                }
                                else
                                {
                                    area = FIELD_AREAS;
                                    gform.Log("Daneben!", i);
                                }

                                res.MatchEvents.Add(new MatchEvent(res.CreateSetPlayCommentary(p, defLineUp.Goalie, isGoal, distance == 1, offClub == HomeClub, i), null, se, i, true));
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
                PosessionBackLog.Clear();
                SwitchPossession(currentHomeLU, currentAwayLU, out offClub, out defClub, ref offLineUp, out defLineUp, ref area);

            }

            ResetFitness();
            MatchResult = res;


            HomeClub.ResetLineup();
            AwayClub.ResetLineup();
            var xpPlayers = new List<Player>();
            foreach (var p in HomeClub.StartingLineUp.Players)
            {
                p.AccountXP(Game.XP_MATCH);
                xpPlayers.Add(p);
                if (p.PlayerStatistics.Any())
                {
                    p.PlayerStatistics.Last().Matches++;
                }
            }
            foreach (var p in AwayClub.StartingLineUp.Players)
            {
                p.AccountXP(Game.XP_MATCH);
                xpPlayers.Add(p);
                if (p.PlayerStatistics.Any())
                {
                    p.PlayerStatistics.Last().Matches++;
                }
            }

            foreach (var sub in res.Substitutions)
            {
                if (!xpPlayers.Contains(sub.In.Player))
                {
                    sub.In.Player.AccountXP(Game.XP_MATCH_SUB);
                    xpPlayers.Add(sub.In.Player);
                    if (sub.In.Player.PlayerStatistics.Any())
                    {
                        sub.In.Player.PlayerStatistics.Last().Matches++;
                    }
                }
            }

            var r = new Run(Match.GAME_LENGTH + ": Das Spiel ist aus!");
            r.FontWeight = FontWeights.Bold;
            var tb = new TextBlock();
            tb.Inlines.Add(r);
            res.MatchEvents.Add(new MatchEvent(new List<TextBlock> { tb }, null, null, (int)Match.GAME_LENGTH, false));
            return res;
        }



        private Dictionary<Player, int> MinuteIn = new Dictionary<Player, int>();
        public void MakeSubstitutions(MatchResult mr, int min, Club c, LineUp lineUp, List<Player> bench)
        {
            var playersOrdered = lineUp.Players.Where(p =>
            {
                int minuteIn;
                if (!MinuteIn.TryGetValue(p, out minuteIn))
                {
                    minuteIn = 0;
                }

                return min - minuteIn > 15;
            }).OrderBy(pl => pl.ValueForCoachCurrent).ToList();

            foreach (var po in playersOrdered)
            {
                var newPlayer = bench.FirstOrDefault(bp => bp.Position == po.Position && bp.ValueForCoachCurrent > (po.ValueForCoachCurrent * 1.15));
                if (newPlayer != null)
                {
                    MinuteIn[newPlayer] = min;
                    lineUp.Players.Remove(po);
                    lineUp.Players.Add(newPlayer);
                    bench.Remove(newPlayer);
                    bench.Add(po);
                    var substitution = new Substitution(c, MatchPlayer(newPlayer), MatchPlayer(po), min);
                    mr.MatchEvents.Add(new MatchEvent(mr.CreateSubstitutionCommentary(substitution, c == HomeClub), substitution, null, min, false));
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
            }
            else
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
        private LeagueCompetitor homeCompetitor;
        private LeagueCompetitor awayCompetitor;

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





        internal void AddGoal(MatchResult mr, Club c, MatchPlayer scorer, int minute, out ScoreEvent scoreEvent)
        {
            if (c.Equals(HomeClub))
            {
                mr.HomeGoals = mr.HomeGoals + 1;
            }
            else
            {
                mr.AwayGoals = mr.AwayGoals + 1;
            }

            if (scorer.Player.PlayerStatistics.Any())
            {
                scorer.Player.PlayerStatistics.Last().Goals++;
            }
            scoreEvent = new ScoreEvent() { Scorer = scorer, Club = c, CurrentScore = mr.ResultString, Minute = minute, CurrentHomeGoals = mr.HomeGoals, CurrentAwayGoals = mr.AwayGoals };
            mr.Scorers.Add(scoreEvent);

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



        internal Boolean Battle(LineUp off, LineUp def, int area, out Player offSingle, out Player defSingle)
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

            offSingle = off.FieldPlayers.OrderByDescending(x => (x.Attk + Util.GetGaussianRandom(offSkillAverage, offSkillAverage / 10)) * GetPickFactor(off, x, area, true)).First();
            defSingle = def.FieldPlayers.OrderByDescending(x => (x.Fitness + Util.GetGaussianRandom(defFitAverage, defFitAverage / 10)) * GetPickFactor(def, x, area, false)).First();

            var offBonus = ((off.Tactic == Tactic.Offensive) ? ATTK_FACTOR_OFFENSIVE : ATTK_FACTOR_NORMAL);
            var offAreaFactor = ATTK_FACTOR_INIT * Math.Pow(ATTK_FACTOR_AREA_MULTIPLIER, area);
            var r1 = RollSkill(offSingle.Attk, offSkillAverage) * offAreaFactor * offBonus;
            var defBonus = (def.Tackling == Tackling.Brutal) ? DEF_FACTOR_HARD_TACKLING : (def.Tackling == Tackling.Normal) ? DEF_FACTOR_NORMAL_TACKLING : DEF_FACTOR_LOW_TACKING;
            var r2 = RollSkill(defSingle.Def, defSkillAverage) * defBonus;

            var decayOff = FITNESS_DECAY_OFF;
            var decayDef = (def.Tactic == Tactic.Defensive) ? FITNESS_DECAY_DEF - FITNESS_DECAY_TACTIC_BONUS : FITNESS_DECAY_DEF;
            offSingle.DecayFitness((float)decayOff);
            var tmp = offSingle;
            foreach (var pl in offPlayers.Where(oP => oP != tmp))
            {
                pl.DecayFitness((float)FITNESS_DECAY_PASSIVE_OFF);
            }
            tmp = defSingle;
            foreach (var pl in defPlayers.Where(dP => dP != tmp))
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


    public class MatchEvent
    {
        public MatchEvent(List<TextBlock> text, Substitution sub, ScoreEvent scoreEvent, int buffer, bool addTensionToLastAction)
        {
            Text = text;
            Substitution = sub;
            ScoreEvent = scoreEvent;
            Buffer = buffer;
            TensionOnLastAction = addTensionToLastAction;
        }
        public List<TextBlock> Text { get; set; }
        public Substitution Substitution { get; set; }
        public ScoreEvent ScoreEvent { get; set; }
        public int Buffer { get; set; }
        public bool TensionOnLastAction { get; set; }
    }

    public class MatchResult
    {
        public List<MatchEvent> MatchEvents { get; set; }
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

        public MatchPlayer HomeGoalieStarter { get; set; }
        public List<MatchPlayer> HomeDefenderStarters { get; set; }
        public List<MatchPlayer> HomeMidFielderStarters { get; set; }
        public List<MatchPlayer> HomeStrikerStarters { get; set; }

        public MatchPlayer AwayGoalieStarter { get; set; }
        public List<MatchPlayer> AwayDefenderStarters { get; set; }
        public List<MatchPlayer> AwayMidFielderStarters { get; set; }
        public List<MatchPlayer> AwayStrikerStarters { get; set; }

        public List<Substitution> Substitutions { get; set; }


        public MatchResult()
        {
            Scorers = new List<ScoreEvent>();
            Substitutions = new List<Substitution>();
            MatchEvents = new List<MatchEvent>();
        }


        private List<TextBlock> ConvertToTextBlocks(string s, Run player0, Run player1)
        {
            var ret = new List<TextBlock>();
            var parts = s.Split(';');

            foreach (var p in parts)
            {
                var tb = new TextBlock();
                tb.Inlines.AddRange(ConvertToInlines(p, player0, player1));
                ret.Add(tb);
            }
            return ret;
        }
        private List<Run> ConvertToInlines(string s, Run player0, Run player1)
        {
            var ret = new List<Run>();

            if (s.Contains("@0"))
            {
                var partL0 = s.Left("@0", false);
                var partR0 = s.Right("@0", false);

                ret.Add(new Run(partL0));
                ret.Add(player0);

                if (partR0.Contains("@1"))
                {
                    var partL1 = partR0.Left("@1", false);
                    var partR1 = partR0.Right("@1", false);

                    ret.Add(new Run(partL1));
                    ret.Add(player1);
                    ret.Add(new Run(partR1));
                }
                else
                {
                    ret.Add(new Run(partR0));
                }
            }
            else
            {
                if (s.Contains("@1"))
                {
                    var partL1 = s.Left("@1", false);
                    var partR1 = s.Right("@1", false);

                    ret.Add(new Run(partL1));
                    ret.Add(player1);
                    ret.Add(new Run(partR1));
                }
                else
                {
                    ret.Add(new Run(s));
                }

            }

            return ret;

        }

        internal List<TextBlock> CreateSubstitutionCommentary(Substitution s, bool home)
        {
            var ret = new List<TextBlock>();
            var tb = new TextBlock();
            ret.Add(tb);
            var clubName = new Run(home ? HomeClub.Name : AwayClub.Name);
            var color = home ? HomeClub.ClubColors.TextColor : (AwayDressSwitch ? AwayClub.SecondClubColors.TextColor : AwayClub.ClubColors.TextColor);
            var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            clubName.Foreground = brush;
            clubName.FontWeight = FontWeights.Bold;
            var player1 = new Run(s.In.Player.LastName);
            player1.Foreground = brush;
            player1.FontWeight = FontWeights.Bold;
            var player2 = new Run(s.Out.Player.LastName);
            player2.Foreground = brush;
            player2.FontWeight = FontWeights.Bold;
            tb.Inlines.Add(new Run("Wechsel bei "));
            tb.Inlines.Add(clubName);
            tb.Inlines.Add(new LineBreak());
            tb.Inlines.Add(player1);
            tb.Inlines.Add(new Run(" kommt für "));
            tb.Inlines.Add(player2);

            return ret;

        }
        internal List<TextBlock> CreateSetPlayCommentary(Player shotTaker, Player goalie, bool isGoal, bool isPenalty, bool home, int minute)
        {
            var ret = new List<TextBlock>();
            var tb = new TextBlock();
            ret.Add(tb);
            var tb2 = new TextBlock();
            ret.Add(tb2);
            var tb3 = new TextBlock();
            ret.Add(tb3);
            var offClub = home ? HomeClub : AwayClub;
            var offColor = home ? HomeClub.ClubColors.TextColor : (AwayDressSwitch ? AwayClub.SecondClubColors.TextColor : AwayClub.ClubColors.TextColor);
            var offBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
            var defClub = home ? AwayClub : HomeClub;
            var defColor = !home ? HomeClub.ClubColors.TextColor : (AwayDressSwitch ? AwayClub.SecondClubColors.TextColor : AwayClub.ClubColors.TextColor);
            var defBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(defColor.A, defColor.R, defColor.G, defColor.B));
            var clubNameOff = new Run(offClub.Name);
            clubNameOff.Foreground = offBrush;
            clubNameOff.FontWeight = FontWeights.Bold;
            var playerG = new Run(goalie.ShortName);
            playerG.Foreground = defBrush;
            playerG.FontWeight = FontWeights.Bold;
            var playerS = new Run(shotTaker.ShortName);
            playerS.Foreground = offBrush;
            playerS.FontWeight = FontWeights.Bold;
            tb.Inlines.Add(new Run(minute.ToString() + ": " + (isPenalty ? "Elfmeter für " : "Freistoß für ")));
            tb.Inlines.Last().FontWeight = FontWeights.Bold;
            tb.Inlines.Add(clubNameOff);
            tb2.Inlines.Add(playerS);
            tb2.Inlines.Add(new Run(" schießt.."));
            var diceResult = Util.GetRandomInt(0, 4);
            tb3.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
            return ret;
        }

        internal List<TextBlock> CreateGoalCommentary(bool isGoal, IEnumerable<Player> posession, Player goalie, bool isLongShot, bool home, int minute)
        {

            var posessions = GetPlayerPosession(posession);
            posessions = posessions.Skip(Math.Max(0, posessions.Count() - 2)).ToList();
            var diceAction = Util.GetRandomInt(0, 4);
            var diceShot = Util.GetRandomInt(0, 4);
            var diceResult = Util.GetRandomInt(0, 4);
            var diceHigh = Util.GetRandomInt(0, 2);

            var offClub = home ? HomeClub : AwayClub;
            var defClub = home ? AwayClub : HomeClub;
            var offColor = home ? HomeClub.ClubColors.TextColor : (AwayDressSwitch ? AwayClub.SecondClubColors.TextColor : AwayClub.ClubColors.TextColor);
            var offBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
            var defColor = !home ? HomeClub.ClubColors.TextColor : (AwayDressSwitch ? AwayClub.SecondClubColors.TextColor : AwayClub.ClubColors.TextColor);
            var defBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(defColor.A, defColor.R, defColor.G, defColor.B));

            var clubNameOff = new Run(offClub.Name);
            clubNameOff.Foreground = offBrush;
            clubNameOff.FontWeight = FontWeights.Bold;
            var clubNameDef = new Run(defClub.Name);
            clubNameDef.Foreground = defBrush;
            clubNameDef.FontWeight = FontWeights.Bold;

            var player0 = new Run(posessions.First().Item1.ShortName);
            player0.Foreground = offBrush;
            player0.FontWeight = FontWeights.Bold;

            var player1 = new Run(posessions.Last().Item1.ShortName);
            player1.Foreground = offBrush;
            player1.FontWeight = FontWeights.Bold;

            var playerG = new Run(goalie.ShortName);
            playerG.Foreground = defBrush;
            playerG.FontWeight = FontWeights.Bold;

            var ret = new List<TextBlock>();

            var tb = new TextBlock();
            var tb2 = new List<TextBlock>();
            var tb3 = new TextBlock();

            TextBlock tb4 = null;

            tb.Inlines.Add(minute + ": Chance für ");
            tb.Inlines.Last().FontWeight = FontWeights.Bold;
            tb.Inlines.Add(clubNameOff);
            //tb.Inlines.Add("!");
            //tb.Inlines.Last().FontWeight = FontWeights.Bold;
            //tb.Inlines.Add(new LineBreak());
            if (isLongShot)
            {
                tb2.AddRange(ConvertToTextBlocks(ShotLong[diceAction], player1, null));
                tb3.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
            }
            else
            {
                tb4 = new TextBlock();

                if (posessions.Count > 1 && posessions.First().Item2 == 1 && posessions.Last().Item2 < 3)
                {
                    if (diceHigh == 0)
                    {

                        tb2.AddRange(ConvertToTextBlocks(PassHigh[diceAction], player0, player1));
                        tb3.Inlines.AddRange(ConvertToInlines(ShotHigh[diceShot], player1, null));
                        tb4.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
                    }
                    else
                    {
                        tb2.AddRange(ConvertToTextBlocks(PassLow[diceAction], player0, player1));
                        tb3.Inlines.AddRange(ConvertToInlines(ShotLow[diceShot], player1, null));
                        tb4.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
                    }
                }
                else
                {
                    if (posessions.Count > 1 && posessions.First().Item2 > 1)
                    {
                        if (diceHigh == 0)
                        {
                            tb2.AddRange(ConvertToTextBlocks(DribblingAndPassHigh[diceAction], player0, player1));
                            tb3.Inlines.AddRange(ConvertToInlines(ShotHigh[diceShot], player1, null));
                            tb4.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
                        }
                        else
                        {
                            tb2.AddRange(ConvertToTextBlocks(DribblingAndPassLow[diceAction], player0, player1));
                            tb3.Inlines.AddRange(ConvertToInlines(ShotLow[diceShot], player1, null));
                            tb4.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
                        }

                    }
                    else
                    {
                        tb2.AddRange(ConvertToTextBlocks(SingleDribbling[diceAction], player1, null));

                        tb3.Inlines.AddRange(ConvertToInlines(ShotLow[diceShot], player1, null));
                        tb4.Inlines.AddRange(ConvertToInlines(isGoal ? Goal[diceResult] : NoGoal[diceResult], playerG, null));
                    }
                }
            }

            ret.Add(tb);
            ret.AddRange(tb2);
            ret.Add(tb3);
            if (tb4 != null)
            {
                ret.Add(tb4);
            }
            return ret;

        }

        private List<Tuple<Player, int>> GetPlayerPosession(IEnumerable<Player> players)
        {
            var ret = new List<Tuple<Player, int>>();
            Player tmp = null;
            var n = 0;
            foreach (var p in players)
            {
                if (tmp == null)
                {
                    tmp = p;
                    n = 1;
                }
                else
                {
                    if (tmp == p)
                    {
                        n++;
                    }
                    else
                    {
                        ret.Add(Tuple.Create(tmp, n));
                        tmp = p;
                        n = 1;
                    }
                }
            }
            ret.Add(Tuple.Create(tmp, n));
            return ret;
        }

        private List<string> Goal = new List<string>
        {
            "Tor!",
            "Der passt genau! Tor!",
            "Keine Chance für @0! Klasse Tor!",
            "Und der Ball zappelt im Netz!",
            "Der ist drin! Tor!"
        };

        private List<string> NoGoal = new List<string>
        {
            "Gehalten!",
            "Pfosten!",
            "Doch der geht vorbei...",
            "Glanztat von @0!",
            "Doch den kann @0 irgendwie halten!"
        };

        private List<string> ShotLow = new List<string>
        {
            "Er setzt zum Heber an...",
            "Er schießt!",
            "Er sucht den Abschluss!",
            "Er versucht den Ball am Tormann vorbei zu schieben...",
            "Er zieht ab!",

        };


        private List<string> ShotHigh = new List<string>
        {
            "Er versucht den Ball per Kopf ins Tor zu verlängern...",
            "Er nimmt den Ball Volley!",
            "Kopfball aufs Tor!",
            "Schöner Dropkick!",
            "Seitfallzieher!"

        };

        private List<string> ShotLong = new List<string>
        {
            "@0 hält einfach mal drauf!",
            "@0 versucht es aus der Distanz!",
            "@0 feuert einen Schuss aufs Tor!",
            "@0 versucht es mit einem Distanzschuss!",
            "@0 zieht einfach ab!"
        };

        private List<string> SingleDribbling = new List<string>
        {
            "@0 setzt sich durch und geht plötzlich alleine aus Tor zu!",
            "@0 lässt seine Gegner aussteigen und hat freie Bahn zum Tor!",
            "@0 ist nicht zu halten!;Er hat jetzt nur noch den Tormann vor sich...",
            "@0 geht an der Verteidigung vorbei. Er zieht Richtung Tor!",
            "@0 mit einem gutem Dribbling;Er geht alleine aufs Tor zu!"
        };

        private List<string> DribblingAndPassLow = new List<string>
        {
            "@0 setzt sich gekonnt durch...;Jetzt mit einem Steilpass auf @1!",
            "@0 kann den Ball behaupten und geht mit Tempo Richtung Tor!;Er legt quer auf @1!",
            "@0 kann den Ball unter Druck irgendwie behaupten...;Steckt durch zu @1...",
            "@0 dribbelt mit Tempo nach vorne!;Legt den Ball zurück auf @1... ",
            "@0 mit einem gutem Dribbling...;Er steckt durch zu @1..."
        };

        private List<string> DribblingAndPassHigh = new List<string>
        {
            "@0 setzt sich gekonnt durch;Jetzt mit der Flanke auf @1...",
            "@0 kann den Ball gekonnt behaupten!;Spielt einen hohen Ball auf @1...",
            "@0 kann den Ball unter Druck irgendwie behaupten...;Chipt den Ball auf @1...",
            "@0 dribbelt mit Tempo nach vorne!;Er flankt auf @1... ",
            "@0 mit einem gutem Dribbling...;Er packt einen Lupfer auf @1 aus..."
        };

        private List<string> PassLow = new List<string>
        {
            "Wunderschöner Pass von @0 in die Spitze!;@1 hat nur noch den Tormann vor sich...",
            "@0 steckt den Ball durch zu @1...;Der könnte jetzt schießen...",
            "@0 mit einem gefühlvollen Pass auf @1...;Der hat jetzt freie Schussbahn!",
            "@0 spielt den Ball gekonnt zu @1...;Der setzt sich vor dem Tor durch...",
            "@0 spielt den Ball Steil vors Tor!;@1 kommt an den Ball..."
        };

        private List<string> PassHigh = new List<string>
        {
            "Wunderschöner Chip von @0 über die Abwehr hinweg!;Der Ball kommt genau auf @1...",
            "@0 verlängert einen hohen Ball per Kopf auf @1!",
            "@0 chipt den Ball aus dem Stand hoch auf @1...",
            "@0 mit einem hohen Ball vors Tor...;@1 kommt an den Ball!",
            "@0 kann den Ball unter Druck hoch vors Tor bringen...;Dort lauert bereits @1!",
            //"@0 kommt irgendwie noch an den Ball und verlängert diesen hoch vors Tor! Der Ball kommt auf @1!"
        };

        private List<string> Combination = new List<string>
        {
            "@0 baut das Spiel auf. Sieht @1! Der spielt den Ball direkt weiter vors Tor! Das ist eine Chance für @2!",
            "@0 mit einem langem Ball nach Vorne! @1 kann den Ball auf @2 ablegen! Der könnte jetzt schießen...",
            "Schöner Ball von @0 auf @1! Der legt nochmal zur Seite ab auf @2! Der hat nur noch den Tormann vor sich...",
            "@0 grätscht einen Ball im Mittelfeld ab. @1 schnappt sich den Ball und treibt ihn nach vorne! Steilpass auf @2!",
            "Unter druck steckt @0 den Ball zu @1 durch.. Der hat jetzt Platz... Schöner Pass vors Tor auf @2!"
        };

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

        public Substitution() { }
    }

    public class ScoreEvent
    {
        public MatchPlayer Scorer { get; set; }
        public Club Club { get; set; }
        public string CurrentScore { get; set; }
        public int CurrentHomeGoals { get; set; }
        public int CurrentAwayGoals { get; set; }
        public int Minute { get; set; }
    }

}






