using FM.Common;
using FM.Entities.Base;
using FM.Generator;
using FM.Models.Season;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FM.Models.Generic
{
    public class Game
    {
        public static readonly int ENTREE_FEE = 20;
        public static readonly int XP_MATCH = 14;
        public static readonly int XP_MATCH_SUB = 9;

        public static Game Instance { get; private set; }
        private Game(World w, int leagueSize)
        {
            LeagueSize = leagueSize;
            FootballUniverse = new FootballUniverse(w);
        }

        public static void InitNewGame(World w, int leagueSize)
        {
            Instance = new Game(w, leagueSize);
            //TODO use the correct looks here
            var al = w.AssociationLooks.First();
            Instance.FootballUniverse.LeagueAssociations = w.Associations.Select(a => Generator.WorldGenerator.GenerateRandomLeagueAssociation(w, a, al)).ToList();
            Season.Season.InitSeasons();
        }

        public FootballUniverse FootballUniverse { get; set; }
        public LeagueAssociation PlayerLeagueAssociation
        {
            get
            {
                return FootballUniverse.LeagueAssociations.First();
            }
        }

        public int LeagueSize { get; set; }
        public League PlayerLeague
        {
            get
            {
                return PlayerLeagueAssociation.Leagues.First();
            }
        }
    }
    public class FootballUniverse
    {
        public FootballUniverse(World w)
        {
            World = w;
            TransferList = new List<Transfer>();
            PlayersWithoutContract = new List<Player>();
        }
        public World World { get; set; }
        public List<LeagueAssociation> LeagueAssociations { get; set; }
        private List<Club> clubs;
        public List<Transfer> TransferList { get; set; }

        public List<Player> PlayersWithoutContract { get; set; }


        public List<Club> Clubs
        {
            get
            {
                if (clubs == null)
                {
                    clubs = LeagueAssociations.SelectMany(la => la.Leagues).SelectMany(l => l.Clubs).ToList();
                }

                return clubs;
            }
        }
        public List<Player> Players
        {
            get
            {
                return Clubs.SelectMany(c => c.Rooster).Concat(PlayersWithoutContract).ToList();
            }
        }
    }

    public class LeagueAssociation
    {
        public LeagueAssociation()
        {
            Leagues = new List<League>();
        }
        public string Name { get; set; }
        public List<League> Leagues { get; set; }

        public ObservableCollection<League> ObservableLeagues
        {
            get
            {
                return new ObservableCollection<League>(Leagues);
            }
        }
    }

    public class LeagueCompetitor
    {
        public League League { get; set; }
        public Club Club { get; set; }
        public int Rank
        {
            get
            {
                return League.RankedClubs.IndexOf(this) + 1;
            }
        }



        public Brush RankBackGroundColor
        {
            get
            {
                if (Rank < (League.Depth == 1 ? 2 : 3))
                {
                    return new SolidColorBrush(Colors.YellowGreen);
                }
                else if ((League.Depth) < League.Association.Leagues.Count && Rank > (Game.Instance.LeagueSize - 2))
                {
                    return new SolidColorBrush(Colors.Tomato);
                }
                else
                {
                    return Brushes.White;
                }
            }
        }

        public Brush RankForeGroundColor
        {
            get
            {
                return Brushes.Black;
            }
        }

        public int Goals { get; set; }
        public int CounterGoals { get; set; }
        public int Points { get; set; }
    }

    public class League
    {

        public List<double> AverageValues { get; set; }
        public List<double> AverageAttractions { get; set; }
        public List<double> AverageNewPlayers { get; set; }

        public League()
        {
            Clubs = new List<Club>();
            AverageAttractions = new List<double>();
            AverageValues = new List<double>();
            AverageNewPlayers = new List<double>();
        }
        public LeagueAssociation Association { get; set; }
        public List<Club> Clubs { get; set; }
        public List<LeagueCompetitor> Standings { get; set; }
        public int Depth { get; set; }
        public double Power { get; set; }
        public int ClubSupport
        {
            get
            {
                return (int)Math.Pow(2, Power) * 35000;
            }
        }
        public MatchDay LatestMatchDay
        {
            get
            {
                return CurrentSeasonMatchDays.LastOrDefault(md => md.IsPlayed) ?? CurrentSeasonMatchDays.First();
            }
        }
        public MatchDay NextMatchDay
        {
            get
            {
                return CurrentSeasonMatchDays.First(md => !md.IsPlayed);
            }
        }

        public List<MatchDay> CurrentSeasonMatchDays
        {
            get
            {
                return FM.Models.Season.Season.CurrentSeason.LeagueMatchDays[this];
            }

        }

        public ObservableCollection<LeagueCompetitor> RankedClubs
        {
            get
            {
                var rankedClubs = Standings.OrderByDescending(lc => lc.Points).ThenByDescending(lc => lc.Goals - lc.CounterGoals);
                return new ObservableCollection<LeagueCompetitor>(rankedClubs);
            }
        }







        public void ResetStandings()
        {
            Standings = Clubs.Select(c => new LeagueCompetitor() { Club = c, Points = 0, Goals = 0, CounterGoals = 0, League = this }).ToList();
        }

        public double GetAverageValueForPosition(Position p)
        {
            var clubAverages = new List<double>();
            foreach (var c in Clubs)
            {
                var players = c.StartingLineUp.Players.Where(pl => pl.Position == p);
                var avClub = 0d;
                if (players.Any())
                {
                    avClub = players.Average(pl => pl.ValueWithPotential);
                }
                clubAverages.Add(avClub);
            }

            return clubAverages.Average();
        }
    }

    public enum ClubAsset
    {
        Stadium, Office, YouthWork, TrainingGrounds
    }



    public class Club
    {

        public Club()
        {
            Rooster = new List<Player>();
            JoiningPlayers = new List<Player>();
            Leagues = new List<League>();
            TransferExpensesCurrentSeason = 0;
            TransferIncomeCurrentSeason = 0;
            Account = 0;
            IsClimber = false;
            LastYearLineUp = new List<Player>();
            NewPlayersWithFee = new List<Player>();
            NewPlayersWithoutFee = new List<Player>();
            PlayersSoldFromStartingLineUp = 0;
            ClubAssetLevel = new Dictionary<ClubAsset, int>();
            NewPlayersFromYouth = new List<Player>();
        }

        public int GetRank(League l)
        {
            var rank = l.RankedClubs.FirstOrDefault(rc => rc.Club == this);
            if (rank != null)
            {
                return rank.Rank;
            }

            return -1;
        }

        public string LeagueDepthString
        {
            get
            {
                return Leagues.First().Association.Name + " " + Leagues.First().Depth;
            }
        }

        public string BudgetSizeString
        {
            get
            {
                if (BudgetCurrentSeason < 250000)
                {
                    return "Sehr niedrig";
                }
                else if (BudgetCurrentSeason < 1000000)
                {
                    return "Niedrig";
                }
                else if (BudgetCurrentSeason < 2000000)
                {
                    return "Mittel";
                }
                else if (BudgetCurrentSeason < 4000000)
                {
                    return "Hoch";
                }
                else
                {
                    return "Sehr Hoch";
                }
            }
        }

        public int AssetCost(ClubAsset ca)
        {
            var lvl = ClubAssetLevel[ca];
            return 50000 * (int)Math.Pow(1.6, lvl);
        }

        public Dictionary<ClubAsset, int> ClubAssetLevel { get; set; }

        public List<Player> NewPlayersWithoutFee { get; set; }
        public List<Player> NewPlayersWithoutFee_LU
        {
            get
            {
                return NewPlayersWithoutFee.Where(p => StartingLineUp.Players.Contains(p)).ToList();
            }
        }
        public List<Player> NewPlayersWithFee { get; set; }
        public List<Player> NewPlayersWithFee_LU
        {
            get
            {
                return NewPlayersWithFee.Where(p => StartingLineUp.Players.Contains(p)).ToList();
            }
        }

        public List<Player> NewPlayersFromYouth { get; set; }

        public List<Player> NewPlayersFromYouth_LU
        {
            get
            {
                return NewPlayersFromYouth.Where(p => StartingLineUp.Players.Contains(p)).ToList();
            }
        }

        public List<Player> NewPlayersFromBench_LU
        {
            get
            {
                return StartingLineUp.Players.Where(p => !LastYearLineUp.Contains(p) && !NewPlayersFromYouth.Concat(NewPlayersWithFee).Concat(NewPlayersWithoutFee).Contains(p)).ToList();
            }
        }

        public List<Player> LastYearLineUp { get; set; }

        public int PlayersSoldFromStartingLineUp { get; set; }
        public String Name { get; set; }

        public Nation Nation { get; set; }

        public LineUp StartingLineUp
        {
            get
            {
                return Coach.CreateLineUp(Rooster);
            }
        }

        public List<Player> Bench
        {
            get
            {
                var res = new List<Player>();
                foreach (var pos in GetPositions())
                {
                    res.AddRange(Rooster.Where(pl => !StartingLineUp.Players.Contains(pl) && pl.Position == pos).OrderByDescending(pl => pl.ValueForCoach).Take(pos == Position.Goalie ? 1 : Coach.Philospophie.GetPreferredFormation()[((int)pos) - 1]));
                }
                return res;
            }
        }

        public LineUp StartingLineUpNextSeason
        {
            get
            {
                return Coach.CreateLineUp(RoosterNextYear);
            }
        }

        public List<Player> BenchNextSeason
        {
            get
            {
                var res = new List<Player>();
                foreach (var pos in GetPositions())
                {
                    res.AddRange(RoosterNextYear.Where(pl => !StartingLineUpNextSeason.Players.Contains(pl) && pl.Position == pos).Take(pos == Position.Goalie ? 1 : Coach.Philospophie.GetPreferredFormation()[((int)pos) + 1]));
                }
                return res;
            }
        }
        public List<Player> Rooster { get; set; }
        public List<Player> JoiningPlayers { get; set; }

        public List<Player> RoosterNextYear
        {
            get
            {
                return Rooster.Where(p => p.ContractCurrent.RunTime > 1).Concat(JoiningPlayers).ToList();
            }
        }

        public ObservableCollection<Player> ObservableRooster
        {
            get
            {
                return new ObservableCollection<Player>(Rooster);
            }
        }

        public Coach Coach { get; set; }
        public ClubColors ClubColors { get; set; }
        public ClubColors SecondClubColors { get; set; }
        public string Crest { get; set; }
        public string Dress { get; set; }
        public bool IsClimber { get; set; }



        public void ClearDepth()
        {
            var lineupCountPos = GetLineUpCountForPosition();
            while ((Account + StadiumIncomeEstimation - SalaryExpenseEstimationCurrentSeason) < 0)
            {
                var top = Rooster.Where(o => lineupCountPos[o.Position] < Rooster.Where(pl => pl.Position == o.Position).Count()).OrderByDescending(pl => pl.ContractCurrent.Salary).First();
                var x = top.PlayerImage;
                top.ContractCurrent = null;
                Rooster.Remove(top);
                Game.Instance.FootballUniverse.PlayersWithoutContract.Add(top);
            }

        }

        public int IncomeEstimationCurrentSeason
        {
            get
            {

                return StadiumIncomeEstimation + SponsorMoneyCurrentSeason + TransferIncomeCurrentSeason + Leagues.Sum(l => l.ClubSupport);
            }
        }
        public int IncomeEstimationNextSeason
        {
            get
            {
                return StadiumIncomeEstimation + SponsorMoneyCurrentSeason + Leagues.Sum(l => l.ClubSupport);
            }
        }

        public int ExpenseEstimationNextSeason
        {
            get
            {
                return JoiningPlayers.Sum(p => p.ContractComing.Salary) + Rooster.Where(p => p.ContractCurrent.RunTime > 1).Sum(p => p.ContractCurrent.Salary);
            }
        }

        public int SalaryExpenseEstimationCurrentSeason
        {
            get
            {
                return Rooster.Sum(p => p.ContractCurrent.Salary);
            }
        }

        public int ExpenseEstimationCurrentSeason
        {
            get
            {
                return TransferExpensesCurrentSeason + SalaryExpenseEstimationCurrentSeason;
            }
        }

        public int TransferExpensesCurrentSeason
        {
            get; set;
        }

        public int TransferIncomeCurrentSeason
        {
            get; set;
        }

        public int Account { get; set; }
        public int Savings { get; set; }

        public double TalentGenerationLevel
        {
            get
            {
                return ClubAssetLevel[ClubAsset.YouthWork] * 0.5;
            }
        }

        public int StadiumCapacity
        {
            get
            {
                return ClubAssetLevel[ClubAsset.Stadium] * 1500;
            }
        }

        public string StadiumCapacityString
        {
            get
            {
                return StadiumCapacity + " Plätze";
            }
        }

        public int TrainingXP
        {
            get
            {
                return 2 + ClubAssetLevel[ClubAsset.TrainingGrounds];
            }
        }

        public int Elo { get; set; }

        public int StadiumIncomeEstimation
        {
            get
            {
                return (int)Math.Round((Leagues.SelectMany(l => l.CurrentSeasonMatchDays).Count() / 2) * Game.ENTREE_FEE * Math.Min(StadiumCapacity, ViewerAttractionEstimation));
            }
        }


        public int SponsorMoneyCurrentSeason { get; set; }

        public int SponsorMoneyPotential
        {
            get
            {
                return (int)(Math.Round(Math.Pow(Attraction, 2.84) / 1000000) * 1200);
            }
        }

        public double ViewerAttractionEstimation { get; set; }


        public int ViewerAttraction
        {
            get
            {
                return (int)(Math.Round(Math.Pow(Attraction, 1.8) / 115));
            }
        }

        private BitmapImage crestImage;
        public BitmapImage CrestImage
        {
            get
            {
                if (crestImage == null)
                {
                    crestImage = PixelArt.GetCrestImage(this.ClubColors, this.Crest);
                }
                return crestImage;
            }
        }

        public BitmapImage DressImage
        {
            get
            {
                return PixelArt.GetDressImage(this.ClubColors, this.Dress);
            }
        }

        public BitmapImage AwayDressImage
        {
            get
            {
                return PixelArt.GetDressImage(this.SecondClubColors, this.Dress);
            }
        }

        public int BudgetCurrentSeason
        {
            get
            {
                return (int)((IncomeEstimationCurrentSeason - ExpenseEstimationCurrentSeason) * ((IsClimber || ExpenseEstimationCurrentSeason > IncomeEstimationCurrentSeason) ? 1 : 0.7));
            }
        }

        public int BudgetNextSeason
        {
            get
            {
                return (int)((IncomeEstimationNextSeason - ExpenseEstimationNextSeason) * 0.7);
            }
        }



        public int Attraction
        {
            get
            {
                return Elo + Publicity;
            }
        }

        public int Publicity
        {
            get
            {
                return 50 * ClubAssetLevel[ClubAsset.Office];
            }
        }


        public string OfficeLevelString
        {
            get
            {
                return "Stufe " + ClubAssetLevel[ClubAsset.Office];
            }
        }

        public string YouthWorkLevelString
        {
            get
            {
                return "Stufe " + ClubAssetLevel[ClubAsset.YouthWork];
            }
        }

        public string TrainingGroundsLevelString
        {
            get
            {
                return "Stufe " + ClubAssetLevel[ClubAsset.TrainingGrounds];
            }
        }
        //public double GetInterestFor(Player p)
        //{

        //}

        public Dictionary<Position, int> GetLineUpCountForPosition()
        {
            var formation = Coach.Philospophie.GetPreferredFormation();
            var ret = new Dictionary<Position, int>();
            for (int i = 0; i < formation.Length; i++)
            {
                ret[(Position)(i + 1)] = formation[i];
            }

            ret[Position.Goalie] = 1;
            return ret;
        }

        public void UpgradeAssets()
        {
            while (true)
            {
                var assetCosts = ClubAssetLevel.Select(cal => Tuple.Create(cal.Key, AssetCost(cal.Key))).OrderBy(t => t.Item2);
                if (Account > assetCosts.First().Item2)
                {
                    var asset = assetCosts.First().Item1;
                    Account -= assetCosts.First().Item2;
                    ClubAssetLevel[asset]++;
                    //Console.WriteLine(Name + ": " + asset.ToString() + " " + (ClubAssetLevel[asset] - 1) + " => " + ClubAssetLevel[asset]);
                }
                else
                {
                    break;
                }
            }
        }

        public void TalentPromotion()
        {
            var positions = GetPositions();
            var roosterCountPos = positions.ToDictionary(p => p, p => this.RoosterNextYear.Where(pl => pl.Position == p).Count());
            var lineUpCountPos = GetLineUpCountForPosition();
            var nPlayersNeededPos = positions.ToDictionary(p => p, p => Math.Max(0, (lineUpCountPos[p] * 2) - roosterCountPos[p]));

            var youthPlayers = new List<Player>();
            var newPlayers = new List<Player>();
            foreach (var p in positions)
            {
                var playersForPos = new List<Player>();
                lineUpCountPos[p].Times(() => playersForPos.Add(Generator.WorldGenerator.GenerateRandomPlayer(Game.Instance.FootballUniverse.World, Nation, p, Util.GetGaussianRandom(TalentGenerationLevel, 0.5), 17, 17)));
                newPlayers.AddRange(playersForPos.OrderByDescending(pl => pl.ValueWithPotential).Take(nPlayersNeededPos[p]));
                youthPlayers.AddRange(playersForPos.Where(pl => !newPlayers.Contains(pl)));
            }

            newPlayers.AddRange(youthPlayers.OrderByDescending(pl => pl.ValueWithPotential).Take(Math.Max(0, 3 - newPlayers.Count)));

            foreach (var pl in newPlayers)
            {
                pl.ContractCurrent = new Contract()
                {
                    Club = this,
                    Player = pl,
                    RunTime = 1,
                    Salary = (int)(pl.GetSalaryExpectationForClub(this, true) * 0.7)
                };
                this.Rooster.Add(pl);
                this.NewPlayersFromYouth.Add(pl);
            }

        }


        public void PlanNextYearRooster()
        {
            if (Season.Season.CurrentSeason.CurrentWeek.Number == 10)
            {
                LookForImprovement(false, 1);
            }

            var positions = GetPositions();
            var roosterCountPos = positions.ToDictionary(p => p, p => this.RoosterNextYear.Where(pl => pl.Position == p).Count());
            var lineUpCountPos = GetLineUpCountForPosition();
            var nPlayersNeededPos = positions.ToDictionary(p => p, p => Math.Max(0, (int)Math.Ceiling(lineUpCountPos[p] * 2.5) - roosterCountPos[p]));

            var avValueOnField = RoosterNextYear.Any() ? RoosterNextYear.Average(p => p.ValueWithPotential) : 0.01;

            var nPlayersNeeded = nPlayersNeededPos.Select(x => x.Value).Sum();

            if (nPlayersNeeded > 0)
            {
                var budget1 = BudgetNextSeason * 0.7;
                var budgetPlayer = budget1 / nPlayersNeeded;
                var interestingPlayers_base = Game.Instance.FootballUniverse.Players.Where(p => (p.ContractCurrent.RunTime == 1 && p.ContractComing == null && ((p.Club == this && Season.Season.CurrentSeason.CurrentWeek.Number == 11) || Season.Season.CurrentSeason.CurrentWeek.Number > 11)) && p.GetSalaryExpectationForClub(this, false) <= budgetPlayer && avValueOnField < (1.2 * p.GetValueAndPreferenceForClub(this))).OrderByDescending(p => p.GetValueAndPreferenceForClub(this));
                foreach (var p in positions)
                {

                    var interestingPlayers_forPos = interestingPlayers_base.Where(pl => pl.Position == p);
                    var fitting = interestingPlayers_forPos.Take(Math.Min(interestingPlayers_forPos.Count(), nPlayersNeededPos[p]));

                    foreach (var f in fitting)
                    {
                        FootballHelper.SignContract(f, this, f.Age > 30 ? 2 : Util.GetRandomInt(2, 5), f.GetSalaryExpectationForClub(this, false), false);
                        if (f.Club != this)
                        {
                            Game.Instance.FootballUniverse.TransferList.Add(new Transfer(f, f.Club, this, Season.Season.CurrentSeason.Year + 1, 1, 0, f.MarketValueStandard));
                        }
                    }
                }
            }
            else
            {
                if (Season.Season.CurrentSeason.CurrentWeek.Number > 10)
                {
                    LookForImprovement(false, 1.1);
                }
            }

        }

        private static List<Position> GetPositions()
        {
            return new List<Position> { Position.Goalie, Position.Defender, Position.Midfielder, Position.Striker };
        }

        private double GetNeedForPosition(Position p, bool currentSeason)
        {
            var av = GetAverageValueForPosition(p, currentSeason);
            var min = GetMinimumForPosition(p, currentSeason);

            return av * 0.5 + min * 0.5;
        }

        private double GetMinimumForPosition(Position p, bool currentSeason)
        {
            var player = currentSeason ? StartingLineUp.PlayersForPosition[p] : StartingLineUpNextSeason.PlayersForPosition[p];
            if (p == Position.Goalie)
            {
                if (player.Any())
                {
                    return player[0].ValueWithPotential;
                }
                else
                {
                    return 0.01;
                }
            }

            if (player.Count < Coach.Philospophie.GetPreferredFormation()[((int)p) - 1])
            {
                return 0.01;
            }
            else
            {
                return player.Min(pl => pl.ValueWithPotential);
            }
        }

        private double GetAverageValueForPosition(Position p, bool currentSeason)
        {
            var player = currentSeason ? StartingLineUp.PlayersForPosition[p] : StartingLineUpNextSeason.PlayersForPosition[p];
            if (p == Position.Goalie)
            {
                if (player.Any())
                {
                    return player[0].ValueWithPotential;
                }
                else
                {
                    return 0.01;
                }
            }
            if (player.Count < Coach.Philospophie.GetPreferredFormation()[((int)p) - 1])
            {
                return 0.01;
            }
            else
            {
                return player.Average(pl => pl.ValueWithPotential);
            }
        }

        public void LookForImprovement(bool currentSeason, double valueMargin)
        {
            var positions = GetPositions();
            var avLeague = positions.ToDictionary(p => p, p => this.Leagues.OrderBy(l => l.Power).First().GetAverageValueForPosition(p));
            var need4pos = positions.ToDictionary(p => p, p => GetNeedForPosition(p, currentSeason) / avLeague[p]);

            var posOrder = need4pos.ToList().OrderByDescending(x => x.Value);

            foreach (var pos in posOrder)
            {
                var av = GetAverageValueForPosition(pos.Key, currentSeason);

                var fittingPlayersForPosition = Game.Instance.FootballUniverse.Players.Where(p =>
                p.WillSignContract && p.Position == pos.Key && p.GetValueAndPreferenceForClub(this) >= (av * valueMargin) &&
                (currentSeason || (p.ContractComing == null && p.ContractCurrent.RunTime == 1 && ((p.Club == this && Season.Season.CurrentSeason.CurrentWeek.Number == 10) || Season.Season.CurrentSeason.CurrentWeek.Number > 11))) &&
                (!currentSeason || (p.IsForSale && p.Club != this && p.Price < (p.MarketValueStandard * 1.5)))
                && (currentSeason ? (p.Price + p.GetSalaryExpectationForClub(this, true)) < BudgetCurrentSeason : p.GetSalaryExpectationForClub(this, false) < BudgetNextSeason));

                var bestFit = fittingPlayersForPosition.OrderByDescending(pl => pl.GetValueAndPreferenceForClub(this) / (pl.GetSalaryExpectationForClub(this, true) + pl.Price));
                foreach (var bf in bestFit)
                {

                    if (currentSeason)
                    {
                        var sal = bf.GetSalaryExpectationForClub(this, true);
                        if ((sal + bf.Price) < BudgetCurrentSeason)
                        {
                            if (Game.Instance.FootballUniverse.PlayersWithoutContract.Contains(bf))
                            {
                                Game.Instance.FootballUniverse.PlayersWithoutContract.Remove(bf);
                                Game.Instance.FootballUniverse.TransferList.Add(new Transfer(bf, bf.Club ?? bf.ClubHistory.Last(), this, Season.Season.CurrentSeason.Year, Season.Season.CurrentSeason.CurrentWeek.Number, 0, bf.MarketValueStandard));
                                FootballHelper.SignContract(bf, this, bf.Age > 30 ? 2 : Util.GetRandomInt(2, 5), sal, true);

                            }
                            else
                            {
                                FootballHelper.TransferPlayer(bf, this, Season.Season.CurrentSeason, bf.Age > 30 ? 2 : Util.GetRandomInt(2, 5), sal);
                            }
                            break;
                        }
                    }
                    else
                    {
                        var sal = bf.GetSalaryExpectationForClub(this, false);
                        if (sal < BudgetNextSeason && sal < (bf.SalaryStandard * 1.5))
                        {
                            FootballHelper.SignContract(bf, this, bf.Age > 30 ? 2 : Util.GetRandomInt(2, 5), sal, false);
                            if (bf.Club != this)
                            {
                                Game.Instance.FootballUniverse.TransferList.Add(new Transfer(bf, bf.Club ?? bf.ClubHistory.Last(), this, Season.Season.CurrentSeason.Year + 1, 1, 0, bf.MarketValueStandard));

                            }
                            break;
                        }
                    }
                }
            }
        }

        //private double GetNeedForPosition(Position p, double avLeague)
        //{

        //    return avLeague / avLineup;
        //    //var qualityFactor = avLeague / avLineup;

        //    //int quantityFactor = GetQuantityNeedForPosition(p);

        //    //return qualityFactor * quantityFactor;
        //}

        //private int GetQuantityNeedForPosition(Position p)
        //{
        //    var roosterCountPos = this.Rooster.Where(pl => pl.Position == p).Count();
        //    var lineUpCountPos = GetLineUpCountForPosition()[p];
        //    if (roosterCountPos == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        var quantityFactor = (lineUpCountPos * 2) / roosterCountPos;
        //        return quantityFactor;
        //    }

        //}

        public List<League> Leagues { get; set; }

    }

    public class ClubColors
    {
        public System.Drawing.Color MainColor { get; set; }
        public System.Drawing.Color SecondColor { get; set; }

        public string MainColorString { get; set; }
        public string SecondColorString { get; set; }
    }


    public class LineUp
    {
        public Dictionary<Position, List<Player>> PlayersForPosition { get; set; }
        public Club Club { get; set; }
        public LineUp(List<Player> players, Player centralPlayer, Player sweeper, Tactic tactic, Tackling tackling, Frequency longshots)
        {
            this.Players = players;
            this.Tackling = tackling;
            this.Tactic = tactic;
            this.CentralPlayer = centralPlayer;
            this.Sweeper = sweeper;
            this.LongShots = longshots;
            PlayersForPosition = new Dictionary<Position, List<Player>>();
            PlayersForPosition[Position.Goalie] = Goalie == null ? new List<Player>() : new List<Player> { Goalie };
            PlayersForPosition[Position.Defender] = Defenders;
            PlayersForPosition[Position.Midfielder] = Midfielders;
            PlayersForPosition[Position.Striker] = Strikers;
            ObservableStartingPlayers = new ObservableCollection<Player>(players);
        }

        public ObservableCollection<Player> ObservableStartingPlayers { get; set; }
        public List<Player> Players { get; set; }
        public Player CentralPlayer { get; set; }

        public Player Sweeper { get; set; }

        public Frequency LongShots { get; set; }

        public List<Player> Strikers
        {
            get
            {
                return Players.Where(p => p.Position == Position.Striker).ToList();
            }
        }

        public List<Player> Midfielders
        {
            get
            {
                return Players.Where(p => p.Position == Position.Midfielder).ToList();
            }
        }
        public List<Player> Defenders
        {
            get
            {
                return Players.Where(p => p.Position == Position.Defender).ToList();
            }
        }
        public Tactic Tactic { get; set; }
        public Tackling Tackling { get; set; }

        public Player KickTaker
        {
            get
            {
                return FieldPlayers.OrderByDescending(x => x.SetPlaySkill).First();
            }
        }


        public List<Player> FieldPlayers
        {
            get
            {
                return Players.Where(p => p.Position != Position.Goalie).ToList();
            }
        }

        public Player Goalie
        {
            get
            {
                return Players.FirstOrDefault(p => p.Position == Position.Goalie);
            }
        }


        public float FreeKickRisk
        {
            get
            {
                return (Tackling == Tackling.Brutal) ? 0.4f : (Tackling == Tackling.Normal) ? 0.2f : 0f;

            }


        }

    }

    public interface IPhilospophie
    {
        int[] GetPreferredFormation();
        Tackling GetPreferredTackling();
        Frequency GetPreferredShotFrequency();
        Tactic GetPreferredTactic();
    }

    public class DefensivePhilosophie : IPhilospophie
    {
        public int[] GetPreferredFormation()
        {
            return new int[] { 2, 2, 1 };
        }

        public Frequency GetPreferredShotFrequency()
        {
            return Frequency.High;
        }

        public Tackling GetPreferredTackling()
        {
            return Tackling.Clean;
        }

        public Tactic GetPreferredTactic()
        {
            return Tactic.Defensive;
        }
    }

    public class OffensivePhilopshie : IPhilospophie
    {
        int[] IPhilospophie.GetPreferredFormation()
        {
            return new int[] { 1, 2, 2 };
        }

        Frequency IPhilospophie.GetPreferredShotFrequency()
        {
            return Frequency.High;
        }

        Tackling IPhilospophie.GetPreferredTackling()
        {
            return Tackling.Brutal;
        }

        Tactic IPhilospophie.GetPreferredTactic()
        {
            return Tactic.Offensive;
        }
    }

    public class PossessionPhilosophie : IPhilospophie
    {
        int[] IPhilospophie.GetPreferredFormation()
        {
            return new int[] { 1, 3, 1 };
        }

        Frequency IPhilospophie.GetPreferredShotFrequency()
        {
            return Frequency.Seldom;
        }

        Tackling IPhilospophie.GetPreferredTackling()
        {
            return Tackling.Normal;
        }

        Tactic IPhilospophie.GetPreferredTactic()
        {
            return Tactic.Defensive;
        }
    }

    public class BalancedPhilosophie : IPhilospophie
    {
        int[] IPhilospophie.GetPreferredFormation()
        {
            return new int[] { 2, 1, 2 };
        }

        Frequency IPhilospophie.GetPreferredShotFrequency()
        {
            return Frequency.Normal;
        }

        Tackling IPhilospophie.GetPreferredTackling()
        {
            return Tackling.Normal;
        }

        Tactic IPhilospophie.GetPreferredTactic()
        {
            return Tactic.Offensive;
        }
    }

    public class Manager : Human
    {
        public Club Club { get; set; }


    }

    public class Coach : Human
    {
        public IPhilospophie Philospophie { get; set; }
        public LineUp CreateLineUp(IEnumerable<Player> rooster)
        {
            var formation = Philospophie.GetPreferredFormation();
            var players = new List<Player>();
            if (rooster.Goalies().Any())
            {
                players.Add(rooster.Goalies().OrderByDescending(g => g.ValueForCoach).First());
            }
            players.AddRange(rooster.Defenders().OrderByDescending(d => d.ValueForCoach).Take(Math.Min(rooster.Defenders().Count(), formation[0])));
            players.AddRange(rooster.Midfielders().OrderByDescending(m => m.ValueForCoach).Take(Math.Min(rooster.Midfielders().Count(), formation[1])));
            players.AddRange(rooster.Strikers().OrderByDescending(m => m.ValueForCoach).Take(Math.Min(rooster.Strikers().Count(), formation[2])));

            return new LineUp(players, null, null, Philospophie.GetPreferredTactic(), Philospophie.GetPreferredTackling(), Philospophie.GetPreferredShotFrequency()) { Club = this.Club };
        }

        public Club Club { get; set; }
    }

    public class Human
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public Nation Nation { get; set; }
    }

    public class Contract
    {
        public Contract()
        {
            SignedOn = Season.Season.CurrentSeason;
        }
        public int RunTime { get; set; }
        public string RunTimeString
        {

            get
            {
                return RunTime.ToString() + (RunTime > 1 ? " Jahre" : " Jahr");
            }
        }

        public int Salary { get; set; }
        public Player Player { get; set; }
        public Club Club { get; set; }
        public Season.Season SignedOn { get; set; }
    }

    public class ContractOffer
    {

    }

    public class PlayerStatistics
    {
        public PlayerStatistics(Player player, int skill, int year)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Skill = skill;
            Year = year;
            Goals = 0;
            Matches = 0;
            Club = Player.Club;
        }

        public Player Player { get; set; }
        public Club Club { get; set; }
        public int Goals { get; set; }
        public int Matches { get; set; }
        public int Skill { get; set; }
        public int Year { get; set; }
    }



    public class Player : Human
    {
        public static int MIN_STARTING_CONSITUTION = 15;
        public static int MIN_AGE = 17;
        public static int XP_FOR_FIRST_LEVEL = 150;
        public static int MAX_FITNESS = 100;
        public static int MAX_MORAL = 100;
        public static int MAX_BASE_SKILL = 80;
        public static int MAX_XPLEVEL = 20;
        public static int MAX_CONSTITUTION = 20;
        public static int MAX_CHARISMA = 20;
        public static int MAX_SET_PLAY_SKILL = 20;
        public static float MAX_RATING = 10f;
        public static float INIT_RATING = 5f;
        public static int MAX_SKILL = MAX_BASE_SKILL + MAX_XPLEVEL;
        public static int LEVEL_CAP_LOG_BASE = 30;
        public static float CONSTITUTION_DECLINE_RATE = 0.4f;



        public Player()
        {
            ClubHistory = new List<Club>();
            PlayerStatistics = new List<PlayerStatistics>();
        }

        public bool WillSignContract
        {
            get
            {
                return (Age < 30 || Constitution > 10) && (ContractCurrent == null || ContractCurrent.SignedOn != Season.Season.CurrentSeason);
            }
        }

        public bool IsForSale
        {
            get
            {
                return Club == null || Club.Rooster.Count(p => p.Position == Position) > 4;
            }
        }

        public List<Club> ClubHistory { get; set; }
        public List<PlayerStatistics> PlayerStatistics { get; set; }

        public Face Face { get; set; }
        public Contract ContractCurrent { get; set; }
        public Contract ContractComing { get; set; }
        public Club Club
        {
            get
            {
                return ContractCurrent?.Club;
            }
        }
        public int Age { get; set; }
        public float SkillBase { get; set; }
        public float XPLevel { get; set; }
        public int XP { get; set; }

        public void ResetDress()
        {
            this.playerImage = null;
        }

        public float ValueBase
        {
            get
            {
                return (SkillMax / 100) * 0.8f + (SkillMax / 100) * ((Constitution + 10) / Player.MAX_CONSTITUTION) * 0.2f;
            }

        }

        public float ValueWithPotential
        {
            get
            {
                return (SkillMax / 100) * 0.7f + (SkillMax / (Age * 3)) * 0.2f + (SkillMax / 100) * (Position == Position.Goalie ? 1 : ((Constitution + 10) / Player.MAX_CONSTITUTION)) * 0.1f;
            }
        }

        public float GetValueAndPreferenceForClub(Club c)
        {
            var nfac = c.Nation == Nation ? 1 : 0.97f;
            return ValueWithPotential * nfac;
        }

        public float ValueForCoachCurrent
        {
            get
            {
                return (SkillCurrent / 100) * 0.8f + (SkillCurrent / (Age * 3)) * 0.1f + (SkillCurrent / 100) * (Position == Position.Goalie ? 1 : ((Constitution + 10) / Player.MAX_CONSTITUTION)) * 0.1f;
            }
        }

        public float ValueForCoach
        {
            get
            {
                return (SkillMax / 100) * 0.8f + (SkillMax / (Age * 3)) * 0.1f + (SkillMax / 100) * (Position == Position.Goalie ? 1 : ((Constitution + 10) / Player.MAX_CONSTITUTION)) * 0.1f;
            }
        }

        public float ValueForClub
        {
            get
            {
                var overAge = Age - 30;
                var ageDeclineFactor = 1d;
                if (overAge > 0)
                {
                    ageDeclineFactor = Math.Pow(0.95f, overAge);
                }

                return ValueWithPotential * (float)ageDeclineFactor;
            }
        }

        public float ValueAbsolute
        {
            get
            {
                return ValueWithPotential * 0.7f + ValueWithPotential * ((Charisma + 10) / Player.MAX_CHARISMA) * 0.2f + ValueWithPotential * (Position == Position.Goalie ? 1 : ((SetPlaySkill + 10) / Player.MAX_SET_PLAY_SKILL)) * 0.1f;
            }
        }

        public double TalentFactor { get; set; }
        public float Fitness { get; set; }
        public float Constitution { get; set; }
        public int ConstitutionDisplayed
        {
            get
            {
                return (int)Math.Round(Constitution, 0);
            }
        }
        public float ConstitutionBase { get; set; }
        public float Charisma { get; set; }

        private float rating = INIT_RATING;
        public float Rating
        {
            get
            {
                return rating;
            }
            set
            {
                if (value < 0)
                {
                    rating = 0;
                }
                else
                {
                    if (value > MAX_RATING)
                    {
                        rating = MAX_RATING;
                    }
                    else
                    {
                        rating = value;
                    }
                }

            }
        }

        //public float MoralBase
        public float Moral { get; set; }
        public float SetPlaySkill { get; set; }
        public Position Position { get; set; }

        private BitmapImage faceImage;
        public BitmapImage FaceImage
        {
            get
            {
                if (faceImage == null)
                {
                    faceImage = PixelArt.GetFaceImage(this.Face);
                }
                return faceImage;
            }
        }

        private BitmapImage playerImage;
        public BitmapImage PlayerImage
        {
            get
            {
                if (playerImage == null)
                {
                    playerImage = PixelArt.GetPlayerImage(this.Face, this.Club.ClubColors, this.Club.Dress);
                }
                return playerImage;
            }
        }

        private BitmapImage profileImage;
        public BitmapImage ProfileImage
        {
            get
            {
                if (profileImage == null)
                {
                    profileImage = PixelArt.GetProfileImage(this.Face, this.Club.ClubColors, this.Club.Dress, this.Club.StadiumCapacity);
                }
                return profileImage;
            }


        }

        public string PositionString
        {
            get
            {
                switch (Position)
                {
                    case Position.Goalie: return "Torwart";
                    case Position.Defender: return "Verteidiger";
                    case Position.Midfielder: return "Mittelfeldspieler";
                    default: return "Stürmer";
                }
            }
        }
        public string PositionStringShort
        {
            get
            {
                switch (Position)
                {
                    case Position.Goalie: return "TW";
                    case Position.Defender: return "VER";
                    case Position.Midfielder: return "MIT";
                    default: return "ST";
                }
            }
        }

        public int LevelCap()
        {
            if (XPLevel == null)
            {
                return 0;
            }

            return (int)Math.Pow(XPLevel, TalentFactor) * 100;
        }

        public void AccountXP(int xp)
        {
            XP += xp;
            if (XP >= LevelCap())
            {
                XPLevel++;
            }
        }


        public void Train()
        {
            AccountXP(Club?.TrainingXP ?? 0);
        }

        public float GoalThreat
        {
            get
            {
                return SkillMax * (Moral / MAX_MORAL) * ((Position == Position.Defender) ? 0.75f : (Position == Position.Striker) ? 1.25f : 1f);
            }
        }

        public float Attk
        {
            get
            {

                return Position == Position.Goalie ? 0f : SkillCurrent * ((Position == Position.Defender) ? 0.75f : (Position == Position.Midfielder) ? 1.25f : 1f);


            }
        }

        public float Def
        {

            get
            {

                return Position == Position.Goalie ? 0f : SkillCurrent * ((Position == Position.Defender) ? 1.25f : (Position == Position.Striker) ? 0.85f : 1f);
            }

        }

        public float Keeping
        {
            get
            {
                return Position == Position.Goalie ? SkillMax * 1.15f : 0f;
            }
        }


        public float SkillCurrent
        {
            get
            {
                return SkillMax - (SkillMax * (1 - (Fitness / MAX_FITNESS)) * 0.3f);
            }
        }

        public float SkillMax
        {
            get
            {
                return (float)Math.Round(0.7f * (XPLevel + SkillBase) + 0.3f * (XPLevel + SkillBase) * (Constitution / MAX_CONSTITUTION), 0);
            }
        }

        public int SalaryStandard
        {
            get
            {
                return Util.GetNiceValue((int)Math.Round(Math.Pow(ValueBase * 10, 3.25), 1) * 1200);
            }
        }

        public int MarketValueStandard
        {
            get
            {
                return Util.GetNiceValue((int)Math.Round(Math.Pow(ValueForClub * 10, 3.2), 1) * 2500);
            }
        }

        public int Price
        {
            get
            {
                //var contractFactor = 1d;
                //var s = SalaryStandard * 18d;


                //if (this.ContractCurrent.RunTime == 1 && Club.IncomeEstimationCurrentSeason < s)
                //{
                //    contractFactor = (double)Club.IncomeEstimationCurrentSeason / s;
                //}

                if (ContractCurrent == null)
                {
                    return 0;
                }

                var startingLineupFactor = 1d;
                var eloFactor = 1d;
                var budgetFactor = 1d;
                var contractFactor = 1d;

                if (Club.IncomeEstimationCurrentSeason > Club.ExpenseEstimationCurrentSeason)
                {
                    if (Club.PlayersSoldFromStartingLineUp > 0)
                    {
                        startingLineupFactor = 1d + 0.3 * Club.PlayersSoldFromStartingLineUp;
                    }
                    contractFactor = Club.StartingLineUp.Players.Contains(this) ? (Math.Pow(1.2, ContractCurrent.RunTime)) : 0.85;
                    budgetFactor = (double)Club.IncomeEstimationCurrentSeason / (double)Club.ExpenseEstimationCurrentSeason;
                    eloFactor = eloFactor + (Club.StartingLineUp.Players.Contains(this) ? ((double)Club.Attraction / 10000) : 0);
                }
                else
                {
                    budgetFactor = 0d;
                }


                return Util.GetNiceValue((int)Math.Round((MarketValueStandard * (0.2 * budgetFactor + 0.2 * eloFactor + 0.3 * contractFactor + 0.3 * startingLineupFactor) / 1000), 0) * 1000);
            }
        }

        public double ExampleContractP
        {
            get
            {
                return GetSalaryExpectationForClub(this.Club, false);
            }
        }

        //public double GetContractP(Club c, double salaryOffer)
        //{
        //    var teamPowerFactor = (c.StartingLineUp.Players.Sum(p => p.ValueOnField) / 6) / ValueOnField;
        //    var playersForPos = c.StartingLineUp.Players.Where(p => p.Position == Position);
        //    var posCompetitionFactor = (playersForPos.Sum(p => p.ValueOnField) / playersForPos.Count()) > ValueOnField ? 0.8 : 1;
        //    var moralFactor = 1d;
        //    if (c == this.Club)
        //    {
        //        moralFactor = 1.25 - (1 - (Moral / MAX_MORAL));
        //    }
        //    var moneyfactor = salaryOffer / SalaryStandard;
        //    var timeFactor = 1d;
        //    if (CurrentContract != null)
        //    {
        //        if (CurrentContract.RunTime > 1)
        //        {
        //            timeFactor = timeFactor * (CurrentContract.RunTime - 1) * Math.Pow(0.97, Season.Season.CurrentSeason.FootballWeeks.Count());
        //        }
        //        timeFactor = timeFactor * (Math.Pow(0.97, Season.Season.CurrentSeason.CurrentWeek.Number / Season.Season.CurrentSeason.FootballWeeks.Count()));
        //    }


        //    return teamPowerFactor * posCompetitionFactor * moralFactor * timeFactor;
        //}
        public int GetSalaryExpectationForClub(Club c, bool isTransfer)
        {
            if (!isTransfer && ContractCurrent != null && ContractCurrent.RunTime > 1)
            {
                return -1;
            }
            var teamPowerFactor = Math.Pow(ValueWithPotential / (c.StartingLineUp.Players.Any() ? (c.StartingLineUp.Players.Average(p => p.ValueWithPotential)) : 0.01), 3);

            var playersForPos = c.StartingLineUp.Players.Where(p => p.Position == Position);
            var posCompetitionFactor = (playersForPos.Sum(p => p.ValueWithPotential) / playersForPos.Count()) > ValueWithPotential ? 1.2 : 1;
            var moralFactor = 1d;
            if (c == this.Club)
            {
                moralFactor = (Age == 17 ? 0.8 : 0.9) + (1 - (Moral / MAX_MORAL));
            }

            var timeFactor = 1d;
            if (ContractCurrent != null && !isTransfer)
            {
                timeFactor = timeFactor * Math.Pow(1.03, (Season.Season.CurrentSeason.FootballWeeks.Count() - Season.Season.CurrentSeason.CurrentWeek.Number));
            }


            var salaryRaw = SalaryStandard * teamPowerFactor * posCompetitionFactor * moralFactor * timeFactor;
            return Util.GetNiceValue((int)Math.Round(salaryRaw, 0));
        }

        public void DecayFitness(float level)
        {
            Fitness = Fitness - level * MAX_CONSTITUTION / Constitution;
        }

        //public bool EvaluateContractOffer(Contract c)
        //{

        //}

    }

    public class Transfer
    {
        public Player Player { get; set; }
        public Club From { get; set; }
        public Club To { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int Price { get; set; }
        public int MarketValue { get; set; }

        public Transfer(Player player, Club from, Club to, int year, int week, int price, int marketValue)
        {
            Player = player;
            From = from;
            To = to;
            Year = year;
            Week = week;
            Price = price;
            MarketValue = marketValue;
        }
    }

    public class Face
    {
        public System.Drawing.Color SkinColor { get; set; }
        public System.Drawing.Color HairColor { get; set; }
        public System.Drawing.Color EyeColor { get; set; }
        public string Head { get; set; }
        public string Mouth { get; set; }
        public string Eye { get; set; }
    }

    public enum Position
    {
        Goalie, Defender, Midfielder, Striker
    }

    public enum Tactic
    {
        Offensive, Defensive
    }

    public enum Frequency
    {
        High, Normal, Seldom
    }


    public enum Tackling
    {
        Clean, Normal, Brutal
    }
}
