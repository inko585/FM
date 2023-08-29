
using AE.Logging;
using FM.Common.Pixels;
using FM.Common.Season;
using FM.Entities.Base;
using FM.Generator;
using FM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FM.Common.Generic
{
    public class Game
    {
        public static readonly int ENTREE_FEE = 30;
        public static readonly int XP_MATCH = 14;
        public static readonly int XP_MATCH_SUB = 9;
        public static readonly int CONTRACT_BORDER_WEEK = 11;

        public static Game Instance { get; private set; }
        private Game(World w, int leagueSize)
        {
            LeagueSize = leagueSize;
            FootballUniverse = new FootballUniverse(w);
        }

        public Club PlayerClub { get; set; }

        public static void InitNewGame(World w, int leagueSize)
        {
            Instance = new Game(w, leagueSize);
            Instance.FootballUniverse.LeagueAssociations = new List<LeagueAssociation>();
            //TODO use the correct looks here
            var fw = new FoundingWindow(w);
            if (fw.ShowDialog() == true)
            {
                Instance.PlayerClub = fw.Result.Item1;
                var al = w.AssociationLooks.First();
                WorldGenerator.TakenNames.Add(fw.Result.Item1.Name);
                foreach (var a in w.Associations)
                {
                    Club pc = null;
                    int depth = -1;

                    if (fw.Result.Item2 == a)
                    {
                        pc = fw.Result.Item1;
                        depth = fw.Result.Item3;
                    }
                    Instance.FootballUniverse.LeagueAssociations.Add(WorldGenerator.GenerateRandomLeagueAssociation(w, a, al, pc, depth));
                }

                Season.Season.InitSeasons();
            }

        }



        public FootballUniverse FootballUniverse { get; set; }
        public LeagueAssociation PlayerLeagueAssociation => PlayerClub.Leagues.FirstOrDefault().Association;


        public int LeagueSize { get; set; }
        public League PlayerLeague => PlayerClub.Leagues.FirstOrDefault();

    }
    public class FootballUniverse
    {
        public FootballUniverse(World w)
        {
            World = w;
            TransferList = new List<Transfer>();
            PlayersWithoutContract = new List<Player>();
            RetiredPlayers = new List<Player>();
        }
        public World World { get; set; }
        public List<LeagueAssociation> LeagueAssociations { get; set; }
        private List<Club> clubs;
        public List<Transfer> TransferList { get; set; }

        public List<Player> PlayersWithoutContract { get; set; }

        public List<Player> RetiredPlayers { get; set; }

        public void ResetCache()
        {
            clubs = null;
        }

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
                return (int)Math.Pow(2, Power) * 75000;
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
                return CurrentSeasonMatchDays.FirstOrDefault(md => !md.IsPlayed);
            }
        }

        public List<MatchDay> CurrentSeasonMatchDays
        {
            get
            {
                return FM.Common.Season.Season.CurrentSeason.LeagueMatchDays[this];
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



        public int AverageBudgetCurrentSeason
        {
            get
            {
                return (int)Clubs.Select(c => c.BudgetCurrentSeason).Average();
            }
        }

        public int AverageBudgetNextSeason
        {
            get
            {
                return (int)Clubs.Select(c => c.BudgetNextSeason).Average();
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


    public class Sponsor
    {
        public string Name { get; set; }
        public double InvestRate { get; set; }
        public BitmapImage Image
        {
            get
            {
                return PixelArt.GetSponsorImage(Name);
            }
        }
        public double ClubSympathyRate
        {
            get
            {
                return 1 + Math.Min(0.2, (YearsInClub * 0.05));
            }
        }

        public double ActualSponsoringRate
        {
            get
            {
                return InvestRate * ClubSympathyRate;
            }
        }
        public int YearsInClub { get; set; }

    }


    public class Club
    {

        public override string ToString()
        {
            return Name;
        }
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

        public static List<int> GoalieNumbers = new List<int> { 1, 12, 23 };
        public static List<int> DefenderNumbers = new List<int> { 2, 3, 4, 5, 6, 13, 14, 15 };
        public static List<int> MidfielderNumbers = new List<int> { 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18 };
        public static List<int> StrikerNumbers = new List<int> { 7, 9, 10, 11, 13, 14, 17, 18 };
        public static List<int> OverallNumbers = new List<int> { 20, 21, 22, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 };

        public int GetFreeNumber(Position p)
        {
            List<int> SpecificNumbers;

            switch (p)
            {
                case Position.Goalie: SpecificNumbers = GoalieNumbers; break;
                case Position.Defender: SpecificNumbers = DefenderNumbers; break;
                case Position.Midfielder: SpecificNumbers = MidfielderNumbers; break;
                default: SpecificNumbers = StrikerNumbers; break;
            }

            var dice = Util.GetRandomInt(0, 10);

            var freeSpec = SpecificNumbers.Where(sn => !Rooster.Any(pl => pl.DressNumber == sn));
            if (freeSpec.Any() && dice < 9)
            {
                return freeSpec.ElementAt(Util.GetRandomInt(0, freeSpec.Count() - 1));
            }
            else
            {
                var freeOverall = OverallNumbers.Where(on => !Rooster.Any(pl => pl.DressNumber == on));
                if (freeOverall.Any())
                {
                    return freeOverall.ElementAt(Util.GetRandomInt(0, freeOverall.Count() - 1));
                }
                return 0;
            }
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

        public bool Watch { get; set; } = false;

        public string BudgetSizeString
        {
            get
            {
                if (BudgetCurrentSeason < 1000000)
                {
                    return "Sehr niedrig";
                }
                else if (BudgetCurrentSeason < 2000000)
                {
                    return "Niedrig";
                }
                else if (BudgetCurrentSeason < 3000000)
                {
                    return "Eher Niedrig";
                }
                else if (BudgetCurrentSeason < 5000000)
                {
                    return "Mittel";
                }
                else if (BudgetCurrentSeason < 800000)
                {
                    return "Eher Hoch";
                }
                else if (BudgetCurrentSeason < 14000000)
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

        public void ResetLineup()
        {
            lineup = null;
            bench = null;
            startingLineUpNextSeason = null;
        }
        private LineUp lineup;

        public LineUp StartingLineUp
        {
            get
            {
                if (lineup == null)
                {
                    lineup = Coach.CreateLineUp(Rooster, p => p.ValueForCoach);
                }
                return lineup;
            }
        }

        private List<Player> bench;
        public List<Player> Bench
        {
            get
            {
                if (bench == null)
                {
                    bench = new List<Player>();
                    foreach (var pos in GetPositions())
                    {
                        bench.AddRange(Rooster.Where(pl => !StartingLineUp.Players.Contains(pl) && pl.Position == pos).OrderByDescending(pl => pl.ValueForCoach).Take(pos == Position.Goalie ? 1 : Coach.Philospophie.GetPreferredFormation()[((int)pos) - 1]));
                    }
                }
                return bench;
            }
        }

        private LineUp startingLineUpNextSeason;

        public LineUp StartingLineUpNextSeason
        {
            get
            {
                if (startingLineUpNextSeason == null)
                {
                    startingLineUpNextSeason = Coach.CreateLineUp(RoosterNextYear, p => p.ValueForCoach);
                }
                return startingLineUpNextSeason;
            }
        }

        //public List<Player> BenchNextSeason
        //{
        //    get
        //    {
        //        var res = new List<Player>();
        //        foreach (var pos in GetPositions())
        //        {
        //            res.AddRange(RoosterNextYear.Where(pl => !StartingLineUpNextSeason.Players.Contains(pl) && pl.Position == pos).Take(pos == Position.Goalie ? 1 : Coach.Philospophie.GetPreferredFormation()[((int)pos) + 1]));
        //        }
        //        return res;
        //    }
        //}
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
                return new ObservableCollection<Player>(Rooster.OrderBy(p => p.Position).ThenByDescending(p => p.MarketValueStandard));
            }
        }

        public Coach Coach { get; set; }
        public ClubColors ClubColors { get; set; }
        public ClubColors SecondClubColors { get; set; }
        public string Crest { get; set; }
        public string Dress { get; set; }
        public bool IsClimber { get; set; }

        public Sponsor CurrentSponsor { get; set; }

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

        public bool PositionSufficient(Position p)
        {

            return Rooster.Count(pl => pl.Position == p && pl.ValueForCoach > GetAverageValueForTeam() * 0.8) > StartingLineUp.PlayersForPosition[p].Count * 2;
        }

        public bool PositionSomeWhatSufficient(Position p)
        {
            return Rooster.Count(pl => pl.Position == p) > StartingLineUp.PlayersForPosition[p].Count * 2;
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
                return 1 + ClubAssetLevel[ClubAsset.YouthWork] * 0.5;
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
                return (int)(Math.Round(Math.Pow(1.5, (Attraction / 160))) * 3200);
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
                    crestImage = FM.Common.Pixels.PixelArt.GetCrestImage(this.ClubColors, this.Crest);
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
                return (int)(IncomeEstimationCurrentSeason * (IsClimber ? 1 : 0.75) - ExpenseEstimationCurrentSeason);
            }
        }

        public int budgetNextSeason = -1;
        public int BudgetNextSeason
        {
            get
            {
                return (int)((IncomeEstimationNextSeason * 0.6 - ExpenseEstimationNextSeason));
            }
        }

        public float BudgetPowerNextSeason
        {
            get
            {
                return (float)BudgetNextSeason / Leagues.First().AverageBudgetNextSeason;
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
                return 75 * ClubAssetLevel[ClubAsset.Office];
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
                var assetCosts = ClubAssetLevel.Select(cal => Tuple.Create(cal.Key, AssetCost(cal.Key))).OrderBy(t => t.Item2).ToList();
                if (ViewerAttraction < StadiumCapacity)
                {
                    assetCosts = assetCosts.Where(ac => ac.Item1 != ClubAsset.Stadium).ToList();
                }
                if (Account > assetCosts.First().Item2)
                {
                    var asset = assetCosts.First().Item1;
                    Account -= assetCosts.First().Item2;
                    ClubAssetLevel[asset]++;

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
            var roosterCountPos = positions.ToDictionary(p => p, p => this.Rooster.Where(pl => pl.Position == p).Count());
            var lineUpCountPos = GetLineUpCountForPosition();
            var nPlayersNeededPos = positions.ToDictionary(p => p, p => Math.Max(0, (lineUpCountPos[p] * 2) - roosterCountPos[p]));

            var youthPlayers = new List<Player>();
            var newPlayers = new List<Player>();
            var neededPlayers = new List<Player>();

            foreach (var pos in nPlayersNeededPos.Where(np => np.Value > 0))
            {
                pos.Value.Times(() =>
                {
                    neededPlayers.Add(Generator.WorldGenerator.GenerateRandomPlayer(Game.Instance.FootballUniverse.World, Nation, pos.Key, Util.GetGaussianRandom(TalentGenerationLevel, 0.5), 17, 17));
                });
            }

            if (neededPlayers.Count < 3)
            {
                (5 - neededPlayers.Count).Times(() =>
                {
                    var pos = Util.GetRandomInt(0, 3);
                    newPlayers.Add(Generator.WorldGenerator.GenerateRandomPlayer(Game.Instance.FootballUniverse.World, Nation, positions.ElementAt(pos), Util.GetGaussianRandom(TalentGenerationLevel, 0.5), 17, 17));
                });
                //foreach (var p in positions)
                //{
                //    var playersForPos = new List<Player>();
                //    lineUpCountPos[p].Times(() => playersForPos.Add(Generator.WorldGenerator.GenerateRandomPlayer(Game.Instance.FootballUniverse.World, Nation, p, Util.GetGaussianRandom(TalentGenerationLevel, 0.5), 17, 17)));
                //    newPlayers.AddRange(playersForPos.OrderByDescending(pl => pl.ValueWithPotential).Take(nPlayersNeededPos[p]));
                //    youthPlayers.AddRange(playersForPos.Where(pl => !newPlayers.Contains(pl)));
                //}

                //newPlayers.AddRange(youthPlayers.OrderByDescending(pl => pl.ValueWithPotential).Take(Math.Max(0, 3 - newPlayers.Count)));

                newPlayers = newPlayers.OrderByDescending(p => p.ValueWithPotential).Take(3 - neededPlayers.Count).ToList();
            }

            newPlayers.AddRange(neededPlayers);

            foreach (var pl in newPlayers)
            {
                pl.ContractCurrent = new Contract()
                {
                    Club = this,
                    Player = pl,
                    RunTime = 2,
                    Salary = (int)(pl.GetSalaryExpectationForClub(this, true) * 0.7)
                };
                this.Rooster.Add(pl);
                pl.DressNumber = GetFreeNumber(pl.Position);
                this.NewPlayersFromYouth.Add(pl);
                pl.PlayerStatistics.Add(new PlayerStatistics(pl, (int)pl.SkillMax, Season.Season.CurrentSeason.Year, Leagues.First().Depth));
            }

        }


        public void PlanNextYearRooster()
        {
            if (Season.Season.CurrentSeason.CurrentWeek.Number >= Game.CONTRACT_BORDER_WEEK)
            {
                if (Season.Season.CurrentSeason.CurrentWeek.Number == Game.CONTRACT_BORDER_WEEK)
                {
                    LookForImprovementWithoutFee(1);
                }
                else
                {


                    Dictionary<Player, int> priceCache = new Dictionary<Player, int>();
                    Dictionary<Player, float> valueCache = new Dictionary<Player, float>();

                    var positions = GetPositions();
                    var roosterCountPos = positions.ToDictionary(p => p, p => this.RoosterNextYear.Where(pl => pl.Position == p).Count());
                    var lineUpCountPos = GetLineUpCountForPosition();
                    var nPlayersNeededPos = positions.ToDictionary(p => p, p => Math.Max(0, (int)Math.Ceiling(lineUpCountPos[p] * 2.5d) - roosterCountPos[p]));

                    var avValueOnField = RoosterNextYear.Any() ? RoosterNextYear.Average(p => p.ValueWithPotential) : 0.01;

                    var nPlayersNeeded = nPlayersNeededPos.Select(x => x.Value).Sum();

                    if (nPlayersNeeded > 0)
                    {
                        var budget1 = (int)(BudgetNextSeason * 0.8);
                        var budgetPlayer = budget1 / nPlayersNeeded;
                        var interestingPlayers_base = Game.Instance.FootballUniverse.Players.Where(p => p.WillSignContract && (p.WillSignContractWithoutFee && ((p.Club == this && Season.Season.CurrentSeason.CurrentWeek.Number == Game.CONTRACT_BORDER_WEEK) || Season.Season.CurrentSeason.CurrentWeek.Number > Game.CONTRACT_BORDER_WEEK)) && p.GetSalaryExpectationForClub(this, false) <= budgetPlayer && avValueOnField < (1.2 * p.GetValueAndPreferenceForClub(this, false))).OrderByDescending(p => p.GetValueAndPreferenceForClub(this, false));

                        RoosterFillHillClimping(interestingPlayers_base, budget1, nPlayersNeededPos, priceCache, valueCache, out Dictionary<Player, int> candidates, out Dictionary<Player, int> alternatives);


                        foreach (var c in candidates)
                        {
                            var nFactor = (double)nPlayersNeeded / 6;

                            var loyaltyFactor = c.Key.Club == this ? 1 : 0;

                            var budgetPowerFactor = BudgetPowerNextSeason;

                            var posAlternatives = alternatives.Where(a => a.Key.Position == c.Key.Position);
                            var alternativeFactor = 1;
                            if (posAlternatives.Any())
                            {
                                alternativeFactor = 1 - Math.Min(1, 10 / alternatives.Count);
                            }

                            var timeFactor = (float)Season.Season.CurrentSeason.CurrentWeek.Number / Season.Season.CurrentSeason.FootballWeeks.Count;

                            var riskScore = budgetPowerFactor * 0.2 + loyaltyFactor * 0.2 + nFactor * 0.2 + timeFactor * 0.4;

                            if (riskScore > 0.55)
                            {
                                nPlayersNeeded--;
                                FootballHelper.SignContract(c.Key, this, c.Key.Age > 30 ? 2 : Util.GetRandomInt(2, 5), c.Key.GetSalaryExpectationForClub(this, false), false);
                                if (c.Key.Club != this)
                                {
                                    Logger.RootLogger.Info("(RH) Transfer without Fee: " + c.Key.FullName + " => " + this.Name);
                                    Game.Instance.FootballUniverse.TransferList.Add(new Transfer(c.Key, c.Key.Club, this, Season.Season.CurrentSeason.Year + 1, 1, 0, c.Key.MarketValueStandard));
                                }
                                else
                                {
                                    Logger.RootLogger.Info("(RH) New Contract: " + c.Key.FullName + " C" + this.Name + ")");
                                }


                            }

                        }
                    }
                    else
                    {
                        LookForImprovementWithoutFee(1.1);
                    }
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

            return 1 / (av * 0.5 + min * 0.5);
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
                return player.Average(pl => pl.GetValueForClub(0.97f));
            }
        }

        public double GetAverageValueForTeam()
        {
            return Rooster.Average(p => p.ValueForCoach);
        }

        public float GetAverageValueForStartingLineUp()
        {
            return StartingLineUp.Players.Average(p => p.ValueForCoach);
        }
        private double GetMinValueForPosition(Position p, bool currentSeason)
        {
            var players = currentSeason ? StartingLineUp.PlayersForPosition[p] : StartingLineUpNextSeason.PlayersForPosition[p];

            return players.OrderBy(pl => pl.GetValueForClub(0.97f)).FirstOrDefault()?.GetValueForClub(0.97f) ?? 0;
        }

        public void LookForSponsor()
        {
            var picks = new List<Sponsor>();
            if (CurrentSponsor != null && Util.GetRandomInt(1, 100) > 20)
            {
                var s = CurrentSponsor;
                s.YearsInClub++;
                picks.Add(s);
            }

            while (picks.Count < 3)
            {
                var newSponsor = WorldGenerator.GetNewSponsorForClub(this);
                while (picks.Any(p => p.Name == newSponsor.Name))
                {
                    newSponsor = WorldGenerator.GetNewSponsorForClub(this);
                }

                picks.Add(newSponsor);
            }

            CurrentSponsor = picks.OrderByDescending(p => p.ActualSponsoringRate).First();

        }

        private int Price(Player p, bool includeTransferFee, Dictionary<Player, int> cache)
        {
            if (!cache.TryGetValue(p, out int price))
            {
                if (p.Club == this && includeTransferFee)
                {
                    price = 0;
                }
                else
                {
                    price = p.GetSalaryExpectationForClub(this, includeTransferFee) + (includeTransferFee ? p.Price : 0);
                }


                cache[p] = price;
            }

            return price;

        }

        public float InvestInterest(Player p, bool currentSeason, Dictionary<Player, float> cache)
        {
            if (!cache.TryGetValue(p, out float v))
            {
                //var nfac = p.Nation == Nation ? 1 : 0.97f;
                v = p.GetValueAndPreferenceForClub(this, currentSeason);
                cache[p] = v;
            }

            return v;

        }


        public void LookForImprovementWithoutFee(double valueMargin)
        {

            var playerPool = Game.Instance.FootballUniverse.Players.Where(p => p.WillSignContract && p.WillJoinClub(this)).ToList();

            Dictionary<Player, int> priceCache = new Dictionary<Player, int>();
            Dictionary<Player, float> valueCache = new Dictionary<Player, float>();

            playerPool = playerPool.Where(p => p.ContractComing == null && p.ContractCurrent.RunTime == 1 && ((p.Club == this && Season.Season.CurrentSeason.CurrentWeek.Number == Game.CONTRACT_BORDER_WEEK) || (Season.Season.CurrentSeason.CurrentWeek.Number > Game.CONTRACT_BORDER_WEEK)) && Price(p, false, priceCache) < BudgetNextSeason).ToList();
            var nextYearTeam = Coach.CreateLineUp(Rooster, p => InvestInterest(p, false, valueCache));
            var oldTeam = nextYearTeam.Players.ToList();
            var currentTeam = nextYearTeam.Players.ToDictionary(p => p, p => 0);
            var minValue = oldTeam.Min(p => p.GetValueForClub(0.97f));
            playerPool = playerPool.Where(p => p.GetSalaryExpectationForClub(this, false) < (p.SalaryStandard * 1.5) && InvestInterest(p, false, valueCache) > minValue * valueMargin).ToList();

            ImprovementHillClimping(playerPool, BudgetNextSeason, oldTeam, false, priceCache, valueCache, out Dictionary<Player, int> improvedTeam, out Dictionary<Player, int> alternatives);

            var bestFit = improvedTeam.Where(t => t.Value != 0).Select(t => t.Key);
            foreach (var bf in bestFit)
            {
                var oldValue = oldTeam.Where(p => p.Position == bf.Position).Min(p => InvestInterest(p,false, valueCache));
                //var improvementFactor = InvestInterest(bf, valueCache)/oldValue;

                var loyaltyFactor = bf.Club == this ? 1 : 0;

                var budgetPowerFactor = BudgetPowerNextSeason;

                var nextBestValue = oldValue;
                var posAlternatives = alternatives.Where(a => a.Key.Position == bf.Position);
                if (posAlternatives.Any())
                {
                    nextBestValue = posAlternatives.Select(a => InvestInterest(a.Key, false,valueCache)).Max();
                }
                var valueDropFactor = (InvestInterest(bf, false, valueCache) / nextBestValue) - 1;
                //var costFactor = Leagues.First().AverageBudgetNextSeason / bf.GetSalaryExpectationForClub(this, false);
                var timeFactor = (float)Season.Season.CurrentSeason.CurrentWeek.Number / Season.Season.CurrentSeason.FootballWeeks.Count;

                var riskScore = budgetPowerFactor * 0.25 + loyaltyFactor * 0.2 + valueDropFactor * 0.2 + timeFactor * 0.35;

                if (riskScore > 0.55)
                {
                    var sal = bf.GetSalaryExpectationForClub(this, false);

                    FootballHelper.SignContract(bf, this, bf.Age > 30 ? 2 : Util.GetRandomInt(2, 5), sal, false);
                    if (bf.Club != this)
                    {
                        Logger.RootLogger.Info("Tranfser without Fee: " + bf.FullName + " => " + this.Name);
                        Game.Instance.FootballUniverse.TransferList.Add(new Transfer(bf, bf.Club ?? bf.ClubHistory.Last(), this, Season.Season.CurrentSeason.Year + 1, 1, 0, bf.MarketValueStandard));

                    }
                    else
                    {
                        Logger.RootLogger.Info("new Contract: " + bf.FullName + " (" + this.Name + ")");
                    }
                }


            }

            //var sal = bf.GetSalaryExpectationForClub(this, false);
            //if (sal < BudgetNextSeason && sal < (bf.SalaryStandard * 1.5))
            //{
            //    FootballHelper.SignContract(bf, this, bf.Age > 30 ? 2 : Util.GetRandomInt(2, 5), sal, false);
            //    if (bf.Club != this)
            //    {
            //        Game.Instance.FootballUniverse.TransferList.Add(new Transfer(bf, bf.Club ?? bf.ClubHistory.Last(), this, Season.Season.CurrentSeason.Year + 1, 1, 0, bf.MarketValueStandard));

            //    }
            //    break;
            //}
        }

        public void LookForImprovement(double valueMargin)
        {

            var playerPool = Game.Instance.FootballUniverse.Players.Where(p => p.WillSignContract && p.WillJoinClub(this)).ToList();

            Dictionary<Player, int> priceCache = new Dictionary<Player, int>();
            Dictionary<Player, float> valueCache = new Dictionary<Player, float>();

            playerPool = playerPool.Where(p => p.IsForSale && p.Club != this && Price(p, false, priceCache) < BudgetCurrentSeason).ToList();

            var thisYearTeam = Coach.CreateLineUp(Rooster, p => InvestInterest(p, true, valueCache));
            var oldTeam = thisYearTeam.Players.ToList();
            var minValue = oldTeam.Min(p => p.GetValueForClub(0.97f));
            playerPool = playerPool.Where(p => InvestInterest(p, true, valueCache) > minValue * valueMargin).ToList();

            ImprovementHillClimping(playerPool, BudgetCurrentSeason, oldTeam, true, priceCache, valueCache, out Dictionary<Player, int> improvedTeam, out Dictionary<Player, int> alternatives);

            var bestFit = improvedTeam.Where(t => t.Value != 0).Select(t => t.Key);
            foreach (var bf in bestFit)
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
        }

        public void RoosterFillHillClimping(IEnumerable<Player> availablePlayers, int budget, Dictionary<Position, int> need, Dictionary<Player, int> priceCache, Dictionary<Player, float> valueCache, out Dictionary<Player, int> candidates, out Dictionary<Player, int> alternatives)
        {
            var candidates_ = new Dictionary<Player, int>();
            alternatives = new Dictionary<Player, int>();
            availablePlayers = availablePlayers.OrderBy(p => Price(p, false, priceCache)).ToList();

            while (true)
            {
                var listUpdate = false;
                foreach (var n in need.Where(n => n.Value > 0))
                {
                    if (!candidates_.Any(c => c.Key.Position == n.Key))
                    {
                        var nextCand = availablePlayers.Where(ap => ap.Position == n.Key).Take(Math.Min(n.Value, availablePlayers.Count()));
                        if (nextCand.Any())
                        {
                            foreach (var c in nextCand)
                            {
                                var sal = c.GetSalaryExpectationForClub(this, false);
                                budget -= sal;
                                if (budget < 0)
                                {
                                    candidates = candidates_;
                                    return;
                                }
                                candidates_[c] = sal;

                            }
                            listUpdate = true;
                        }
                    }
                    else
                    {
                        foreach (var c in candidates_.Where(c => c.Key.Position == n.Key).OrderBy(c => InvestInterest(c.Key,false, valueCache)))
                        {
                            var nextCand = availablePlayers.Where(ap => !candidates_.Any(x => ap == x.Key) && ap.Position == n.Key && InvestInterest(ap, false, valueCache) > InvestInterest(c.Key, false, valueCache)).ToList();
                            if (nextCand.Any())
                            {
                                var newC = nextCand.First();
                                //nextCand.Remove(newC);
                                candidates_[newC] = Price(newC, false, priceCache);
                                budget = (budget + c.Value) - candidates_[newC];

                                if (budget < 0)
                                {
                                    candidates = candidates_;
                                    return;
                                }

                                var alternative = c.Key;
                                alternatives[c.Key] = c.Value;

                                candidates_.Remove(c.Key);

                                listUpdate = true;

                            }
                        }
                    }

                }

                if (!listUpdate)
                {
                    break;
                }
            }

            candidates = candidates_;


        }

        private void ImprovementHillClimping(List<Player> availablePlayers, int budget, List<Player> playerInput, bool includeTransferFee, Dictionary<Player, int> priceCache, Dictionary<Player, float> valueCache, out Dictionary<Player, int> improvedTeam, out Dictionary<Player, int> alternatives)
        {
            improvedTeam = playerInput.ToDictionary(p => p, p => 0);


            availablePlayers = availablePlayers.OrderBy(p => Price(p, includeTransferFee, priceCache)).ToList();
            var buffer = new List<Tuple<Player, Player, float, int>>();
            alternatives = new Dictionary<Player, int>();

            while (true)
            {
                foreach (var player in improvedTeam)
                {
                    var pV = InvestInterest(player.Key, includeTransferFee, valueCache);
                    var other = availablePlayers.FirstOrDefault(o => InvestInterest(o, includeTransferFee, valueCache) > pV && o.Position == player.Key.Position);
                    if (other != null)
                    {
                        buffer.Add(Tuple.Create(player.Key, other, InvestInterest(other, includeTransferFee, valueCache) - pV, Price(other, includeTransferFee, priceCache) - player.Value));
                    }
                }
                if (buffer.Any())
                {
                    var best = buffer.OrderByDescending(b => b.Item3 / (Math.Max(1, b.Item4))).First();
                    budget += improvedTeam[best.Item1];
                    if (improvedTeam[best.Item1] > 0)
                    {
                        alternatives[best.Item1] = improvedTeam[best.Item1];
                    }
                    improvedTeam.Remove(best.Item1);

                    improvedTeam[best.Item2] = Price(best.Item2, includeTransferFee, priceCache);
                    budget -= improvedTeam[best.Item2];

                    availablePlayers = availablePlayers.Where(p => Price(p, includeTransferFee, priceCache) < budget).ToList();
                    buffer.Clear();
                }
                else
                {
                    break;
                }

            }
        }

        public List<League> Leagues { get; set; }

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
            //ObservableStartingPlayers = new ObservableCollection<Player>(players);
        }

        //public ObservableCollection<Player> ObservableStartingPlayers { get; set; }
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
        public LineUp CreateLineUp(IEnumerable<Player> rooster, Func<Player, float> evalFunc)
        {
            var formation = Philospophie.GetPreferredFormation();
            var players = new List<Player>();
            if (rooster.Goalies().Any())
            {
                players.Add(rooster.Goalies().OrderByDescending(g => evalFunc(g)).First());
            }
            players.AddRange(rooster.Defenders().OrderByDescending(d => evalFunc(d)).Take(Math.Min(rooster.Defenders().Count(), formation[0])));
            players.AddRange(rooster.Midfielders().OrderByDescending(m => evalFunc(m)).Take(Math.Min(rooster.Midfielders().Count(), formation[1])));
            players.AddRange(rooster.Strikers().OrderByDescending(m => evalFunc(m)).Take(Math.Min(rooster.Strikers().Count(), formation[2])));

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

        public string ShortName
        {
            get
            {
                return FirstName[0] + ". " + LastName;
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

        public bool IsSignedThisYear
        {
            get
            {
                return SignedOn == Season.Season.CurrentSeason;
            }
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
        public PlayerStatistics(Player player, int skill, int year, int league)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Skill = skill;
            Year = year;
            Goals = 0;
            Matches = 0;
            Club = Player.Club;
            LeagueDepth = league;
        }

        public PlayerStatistics()
        {

        }

        public Player Player { get; set; }
        public Club Club { get; set; }
        public int Goals { get; set; }
        public int Matches { get; set; }
        public int Skill { get; set; }
        public int Year { get; set; }
        public int LeagueDepth { get; set; }
    }



    public class MatchPlayer
    {
        public Player Player { get; set; }
        public BitmapImage MatchPlayerImage { get; set; }
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


        public override string ToString()
        {
            return FullName + " (" + Club?.Name ?? "-" + ")";
        }
        public Player()
        {
            ClubHistory = new List<Club>();
            PlayerStatistics = new List<PlayerStatistics>();
            PlayerPriceAdjustment = 1;
        }

        public int DressNumber { get; set; }
        public string DressNumberString
        {
            get
            {
                return "# " + DressNumber;
            }
        }

        //public string FieldNameString
        //{
        //    get
        //    {
        //        return DressNumberString + " " + LastName; 
        //    }
        //}

        public bool? WantsToLeavePlayerClub { get; set; }

        public bool WillSignContractWithPlayerClub
        {
            get
            {
                return WillSignContractWithoutFee && !(Club == Game.Instance.PlayerClub && (WantsToLeavePlayerClub ?? false));
            }
        }

        public bool CheckIfPlayerWantsToLeavePlayerClub()
        {
            var valEloRatingsAsc = Season.Season.CurrentSeason.ValueEloRatio.OrderBy(kv => kv.Key).Where(kv => kv.Key > ValueForCoach);
            var valEloRatingsDesc = Season.Season.CurrentSeason.ValueEloRatio.OrderByDescending(kv => kv.Key).Where(kv => kv.Key <= ValueForCoach);
            var specimen = new List<int>();
            specimen.AddRange(valEloRatingsAsc.Take(Math.Min(valEloRatingsAsc.Count(), 2)).Select(kv => kv.Value));
            specimen.AddRange(valEloRatingsDesc.Take(Math.Min(valEloRatingsDesc.Count(), 2)).Select(kv => kv.Value));

            var avControlElo = specimen.Average();

            var baseP = 0.8;
            var actualP = baseP + ((Club.Elo / avControlElo) - 1);

            var dice = (double)Util.GetRandomInt(1, 100) / 100d;
            return dice >= actualP;

        }

        public bool WillSignContract
        {
            get
            {
                return (Age < 30 || Constitution > 10) && !ContractCurrent.IsSignedThisYear;
            }
        }

        public string ContractString
        {
            get
            {
                return (ContractComing != null && ContractComing.Club == Club) ? (Season.Season.CurrentSeason.Year + ContractComing.RunTime).ToString() :
                    (Season.Season.CurrentSeason.Year + ContractCurrent.RunTime - 1).ToString() + (ContractComing != null ? " (T)" : "");
            }
        }

        public bool WillSignContractWithoutFee
        {
            get
            {
                return (ContractCurrent == null || ContractCurrent.RunTime == 1) && ContractComing == null && WillSignContract;
            }
        }

        public bool IsForSale
        {
            get
            {
                return Club == null || Club.PositionSufficient(Position) || (Club.PositionSomeWhatSufficient(Position) && ValueForCoach < Club.GetAverageValueForTeam() * 0.5);
            }
        }

        public bool WillJoinClub(Club c)
        {
            return !this.Club.StartingLineUp.Players.Contains(this) || Club.Elo <= (c.Elo * 1.1);
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

        public bool IsLeftFoot { get; set; }

        public void ResetDress()
        {
            this.playerImage = null;
            this.playerImage_away = null;
        }

        public double PlayerPriceAdjustment { get; set; }

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
                return (SkillMax / 100) * 0.75f + (SkillMax / (Age * 3)) * 0.15f + (SkillMax / 100) * (Position == Position.Goalie ? 1 : ((Constitution + 10) / Player.MAX_CONSTITUTION)) * 0.1f;
            }
        }



        public float GetValueAndPreferenceForClub(Club c, bool currentSeason)
        {
            var nfac = c.Nation == Nation ? 1 : 0.97f;
            var lfac = Club == c || currentSeason ? 1 : 0.85f;
            return ValueWithPotential * nfac * lfac;
        }

        //public float GetInvestInterestForClub(Club c, Dictionary<Player, float> cache = null)
        //{
        //    var nfac = c.Nation == Nation ? 1 : 0.97f;
        //    return GetValueForClub(0.97f) * nfac;
        //}

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

        public float GetValueForClub(float ageDecline)
        {
            var overAge = Age - 30;
            var ageDeclineFactor = 1d;
            if (overAge > 0)
            {
                ageDeclineFactor = Math.Pow(ageDecline, overAge);
            }

            return ValueWithPotential * (float)ageDeclineFactor;
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
                    if (PlayerStatistics.Any())
                    {
                        playerImage = PixelArt.GetPlayerImage(this.Face, this.PlayerStatistics.Last().Club.ClubColors, this.PlayerStatistics.Last().Club.Dress);
                    }
                }
                return playerImage;
            }
        }

        private BitmapImage playerImage_away;
        public BitmapImage PlayerImage_Away
        {
            get
            {
                if (playerImage_away == null)
                {
                    if (PlayerStatistics.Any())
                    {
                        playerImage_away = PixelArt.GetPlayerImage(this.Face, this.PlayerStatistics.Last().Club.SecondClubColors, this.PlayerStatistics.Last().Club.Dress);
                    }
                }
                return playerImage_away;
            }
        }

        private BitmapImage profileImage;
        public BitmapImage ProfileImage
        {
            get
            {
                if (profileImage == null)
                {
                    profileImage = PixelArt.GetProfileImage(this.Face, this.PlayerStatistics.Last().Club.ClubColors, this.PlayerStatistics.Last().Club.Dress, this.PlayerStatistics.Last().Club.StadiumCapacity);
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

            return (int)Math.Pow(XPLevel + 1, TalentFactor) * 100;
        }

        public void AccountXP(int xp)
        {
            if (Age <= 30)
            {
                XP += xp;
                if (XP >= LevelCap())
                {
                    XPLevel++;
                }
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
                return Util.GetNiceValue((int)Math.Round(Math.Pow(2.75, GetValueForClub(0.96f) * 10), 1) * 500);
            }
        }

        public int MarketValueStandard
        {
            get
            {
                return Util.GetNiceValue((int)Math.Round(Math.Pow(2.75, GetValueForClub(0.96f) * 10), 1) * 1000);
            }
        }

        public string MarketValueStandardString
        {
            get
            {
                return string.Format("{0:#,0}", Util.GetNiceValue(MarketValueStandard));
            }
        }

        public int Price => Util.GetNiceValue((int)Math.Round(DefaultPrice * PlayerPriceAdjustment));

        public int DefaultPrice
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

                if (Club.BudgetCurrentSeason > 0)
                {
                    if (Club.PlayersSoldFromStartingLineUp > 0)
                    {
                        startingLineupFactor = 1d + 0.45 * Club.PlayersSoldFromStartingLineUp;
                    }

                    if (Club.StartingLineUp.Players.Contains(this) && Season.Season.CurrentSeason.CurrentWeek.Number == 3)
                    {
                        startingLineupFactor *= 2;
                    }
                    contractFactor = Club.StartingLineUp.Players.Contains(this) ? (Math.Pow(1.2, ContractCurrent.RunTime)) : 0.85;
                    eloFactor = eloFactor + (Club.StartingLineUp.Players.Contains(this) ? ((double)Club.Attraction / 10000) : 0);
                }
                else
                {
                    budgetFactor = 0d;
                }

                if (!Club.StartingLineUp.Players.Contains(this))
                {
                    if (!Club.Bench.Contains(this))
                    {
                        startingLineupFactor = 0d;
                    }
                }



                return Util.GetNiceValue((int)Math.Round((MarketValueStandard * (0.2 * budgetFactor + 0.2 * eloFactor + 0.3 * contractFactor + 0.3 * startingLineupFactor) / 1000), 0) * 1000);
            }
        }


        public string PriceString
        {
            get
            {
                return IsForSale && WillSignContract ? string.Format("{0:#,0}", Util.GetNiceValue(Price)) : "-";
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
        public int GetSalaryExpectationForClub(Club c, bool isTransfer, int? years = null)
        {
            if (!isTransfer && ContractCurrent != null && ContractCurrent.RunTime > 1)
            {
                return -1;
            }
            //var teamPowerFactor = Math.Pow(ValueWithPotential / (c.StartingLineUp.Players.Any() ? (c.StartingLineUp.Players.Average(p => p.ValueWithPotential)) : 0.01), 3);

            //var playersForPos = c.StartingLineUp.Players.Where(p => p.Position == Position);
            //var posCompetitionFactor = (playersForPos.Sum(p => p.ValueWithPotential) / playersForPos.Count()) > ValueWithPotential ? 1.2 : 1;

            var moralFactor = 1d;
            if (c == this.Club)
            {
                moralFactor = (Age == 18 ? 0.8 : 0.9) + (1 - (Moral / MAX_MORAL));
            }

            var timeFactor = 0.3;
            if (ContractCurrent != null && !isTransfer)
            {
                timeFactor = timeFactor * Math.Pow(1.12, (Season.Season.CurrentSeason.FootballWeeks.Count() - Math.Max(Game.CONTRACT_BORDER_WEEK + 1, Season.Season.CurrentSeason.CurrentWeek.Number)));
            }
            else
            {
                timeFactor = 1;
            }


            var salaryRaw = SalaryStandard * moralFactor * timeFactor;
            if (years.HasValue)
            {
                salaryRaw *= 0.8;
                if (Age <= 27 && years > 2)
                {
                    salaryRaw = salaryRaw * (1 + ((Math.Min(30 - Age, years.Value) - 2) * (Age <= 21 ? 0.3 : 0.2)));
                }
            }

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

        public string MarketValueString
        {
            get
            {
                return string.Format("{0:#,0}", MarketValue);
            }
        }

        public string PriceString
        {
            get
            {
                return string.Format("{0:#,0}", Price);
            }
        }

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

        public Transfer()
        {

        }
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
