using FM.Common;
using FM.Entities.Base;
using FM.Models.Season;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FM.Models.Generic
{
    public class Game
    {
        public static Game Instance { get; private set; }
        private Game(World w, int leagueSize)
        {
            LeagueSize = leagueSize;
            FootballUniverse = new FootballUniverse(w);
        }

        public static void InitNewGame(World w, int leagueSize)
        {
            Instance = new Game(w, leagueSize);
            Instance.FootballUniverse.LeagueAssociations = w.Associations.Select(a => Generator.WorldGenerator.GenerateRandomLeagueAssociation(w, a)).ToList();
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
        }
        public World World { get; set; }
        public List<LeagueAssociation> LeagueAssociations { get; set; }
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

        public BitmapImage AssociationPic
        {
            get
            {
                return PixelArt.GetRandomCrest();
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
        public League()
        {
            Clubs = new List<Club>();
        }
        public LeagueAssociation Association { get; set; }
        public List<Club> Clubs { get; set; }
        public List<LeagueCompetitor> Standings { get; set; }
        public int Depth { get; set; }
        public int Power { get; set; }

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
                var avClub = players.Average(pl => pl.ValueOnField);
                clubAverages.Add(avClub);
            }

            return clubAverages.Average();
        }
    }

    public class Club
    {

        public Club()
        {
            Rooster = new List<Player>();
            Leagues = new List<League>();
        }
        public String Name { get; set; }
        
        public LineUp StartingLineUp
        {
            get
            {
                return Coach.CreateLineUp(Rooster);
            }
        }
        public List<Player> Rooster { get; set; }
        public ObservableCollection<Player> ObservableRooster
        {
            get
            {
                return new ObservableCollection<Player>(Rooster);
            }
        }

        public Coach Coach { get; set; }
        public int SeasonIncomeEstimation
        {
            get
            {
                var stadiumEarnings = (int)Math.Round((Leagues.SelectMany(l => l.CurrentSeasonMatchDays).Count() / 2) * Game.ENTREE_FEE * Math.Min(StadiumCapacity, ViewerAttractionEstimation));
                var sponsorEarnings = SponsorMoneyCurrentSeason;

                return stadiumEarnings + sponsorEarnings;
            }
        }

        public int Account { get; set; }
        public int Savings { get; set; }
        public int StadiumLevel { get; set; }

        public int StadiumCapacity
        {
            get
            {
                return StadiumLevel * 1500;
            }
        }

        public int Elo { get; set; }

        public int SponsorMoneyCurrentSeason { get; set; }

        public int SponsorMoneyPotential
        {
            get
            {
                return (int)(Math.Round(Math.Pow(Elo, 2.8) / 1000000) * 1500);
            }
        }

        public double ViewerAttractionEstimation { get; set; }


        public double ViewerAttraction
        {
            get
            {
                return (int)(Math.Round(Math.Pow(Elo, 1.8) / 70));
            }
        }

        public Crest Crest { get; set; }

        public int Budget { get; set; }



        public double Attraction { get; set; }

        public double Publicity { get; set; }

        //public double GetInterestFor(Player p)
        //{

        //}

        public void LookForPlayers()
        {
            var positions = new List<Position> { Position.Goalie, Position.Defender, Position.Midfielder, Position.Striker };
            var avLeague = positions.ToDictionary(p => p, p => this.Leagues.OrderBy(l => l.Power).First().GetAverageValueForPosition(p));
            var need4pos = positions.ToDictionary(p => p, p => GetNeedForPosition(p, avLeague[p]));

        }

        private double GetNeedForPosition(Position p, double avLeague)
        {
            var avLineup = this.StartingLineUp.Players.Where(pl => pl.Position == p).Average(pl => pl.ValueOnField);
            var qualityFactor = avLeague / avLineup;

            var roosterCountPos = this.Rooster.Where(pl => pl.Position == p).Count();
            var lineUpCountPos = this.StartingLineUp.Players.Where(pl => pl.Position == p).Count();
            var quantityFactor = (lineUpCountPos * 2) / roosterCountPos;

            return qualityFactor * quantityFactor;
        }

        public List<League> Leagues { get; set; }

    }

    public class Crest
    {
        public System.Drawing.Color MainColor { get; set; }
        public System.Drawing.Color SecondColor { get; set; }
        public string Motive { get; set; }
    }



    public class LineUp
    {
        public Club Club { get; set; }
        public LineUp(List<Player> players, Player centralPlayer, Player sweeper, Tactic tactic, Tackling tackling, Frequency longshots)
        {
            this.Players = players;
            this.Tackling = tackling;
            this.Tactic = tactic;
            this.CentralPlayer = centralPlayer;
            this.Sweeper = sweeper;
            this.LongShots = longshots;
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
                return Players.First(p => p.Position == Position.Goalie);
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
                players.Add(rooster.Goalies().OrderByDescending(g => g.ValueOnField).First());
            }
            players.AddRange(rooster.Defenders().OrderByDescending(d => d.ValueOnField).Take(Math.Min(rooster.Defenders().Count(), formation[0])));
            players.AddRange(rooster.Midfielders().OrderByDescending(m => m.ValueOnField).Take(Math.Min(rooster.Midfielders().Count(), formation[1])));
            players.AddRange(rooster.Strikers().OrderByDescending(m => m.ValueOnField).Take(Math.Min(rooster.Strikers().Count(), formation[2])));

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
        public int RunTime { get; set; }
        public int Salary { get; set; }
        public Player Player { get; set; }
        public Club Club { get; set; }
    }

    public class ContractOffer
    {

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


        public Face Face { get; set; }
        public Contract CurrentContract { get; set; }
        public Club Club
        {
            get
            {
                return CurrentContract.Club;
            }
        }
        public int Age { get; set; }
        public float SkillBase { get; set; }
        public float XPLevel { get; set; }
        public int XP { get; set; }

        public float ValueBase
        {
            get
            {
                return (SkillMax / 100) * 0.8f + (SkillMax / 100) * ((Constitution + 10) / Player.MAX_CONSTITUTION) * 0.2f;
            }

        }

        public float ValueOnField
        {
            get
            {
                return (SkillMax / 100) * 0.7f + (SkillMax / (Age * 3)) * 0.2f + (SkillMax / 100) * (Position == Position.Goalie ? 1 : ((Constitution + 10) / Player.MAX_CONSTITUTION)) * 0.1f;
            }
        }

        public float ValueAbsolute
        {
            get
            {
                return ValueOnField * 0.7f + ValueOnField * ((Charisma + 10) / Player.MAX_CHARISMA) * 0.2f + ValueOnField * (Position == Position.Goalie ? 1 : ((SetPlaySkill + 10) / Player.MAX_SET_PLAY_SKILL)) * 0.1f;
            }
        }

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

        public BitmapImage ProfilePic
        {
            get
            {
                return PixelArt.GetRandomProfilePic();
            }
        }
        public BitmapImage TricotPic
        {
            get
            {
                return PixelArt.GetRandomTricotPic();
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

            return (int)Math.Floor(XP_FOR_FIRST_LEVEL / Math.Log(XPLevel, LEVEL_CAP_LOG_BASE));
        }

        public void AccountXP(int xp)
        {
            XP += xp;
            if (XP >= LevelCap())
            {
                XP -= LevelCap();
                XPLevel++;
            }
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
                return (float)Math.Round(0.85f * (XPLevel + SkillBase) + 0.15f * (XPLevel + SkillBase) * (Constitution / MAX_CONSTITUTION), 0);
            }
        }

        public int SalaryStandard
        {
            get
            {
                return (int)Math.Round(Math.Pow(ValueOnField*10, 2), 1) * 2000;
            }
        }

        public double ExampleContractP
        {
            get
            {
                return GetSalaryExpectationForClub(this.Club);
            }
        }

        public double GetContractP(Club c, double salaryOffer)
        {
            var teamPowerFactor = (c.StartingLineUp.Players.Sum(p => p.ValueOnField) / 6) / ValueOnField;
            var playersForPos = c.StartingLineUp.Players.Where(p => p.Position == Position);
            var posCompetitionFactor = (playersForPos.Sum(p => p.ValueOnField) / playersForPos.Count()) > ValueOnField ? 0.8 : 1;
            var moralFactor = 1d;
            if (c == this.Club)
            {
                moralFactor = 1.25 - (1 - (Moral / MAX_MORAL));
            }
            var moneyfactor = salaryOffer / SalaryStandard;
            var timeFactor = 1d;
            if (CurrentContract != null)
            {
                if (CurrentContract.RunTime > 1)
                {
                    timeFactor = timeFactor * (CurrentContract.RunTime - 1) * Math.Pow(0.97, Season.Season.CurrentSeason.FootballWeeks.Count());
                }
                timeFactor = timeFactor * (Math.Pow(0.97, Season.Season.CurrentSeason.CurrentWeek.Number / Season.Season.CurrentSeason.FootballWeeks.Count()));
            }


            return teamPowerFactor * posCompetitionFactor * moralFactor * timeFactor;
        }
        public int GetSalaryExpectationForClub(Club c)
        {
            if (CurrentContract != null && CurrentContract.RunTime > 1)
            {
                return -1;
            }
            var teamPowerFactor = ValueOnField / (c.StartingLineUp.Players.Sum(p => p.ValueOnField) / 6);

            var playersForPos = c.StartingLineUp.Players.Where(p => p.Position == Position);
            var posCompetitionFactor = (playersForPos.Sum(p => p.ValueOnField) / playersForPos.Count()) > ValueOnField ? 1.2 : 1;
            var moralFactor = 1d;
            if (c == this.Club)
            {
                moralFactor = 0.75 + (1 - (Moral / MAX_MORAL));
            }

            var timeFactor = 1d;
            if (CurrentContract != null)
            {
                timeFactor = timeFactor * Math.Pow(1.02, (Season.Season.CurrentSeason.FootballWeeks.Count() - Season.Season.CurrentSeason.CurrentWeek.Number));
            }


            var salaryRaw = SalaryStandard * teamPowerFactor * posCompetitionFactor * moralFactor * timeFactor;
            return (int)Math.Round(salaryRaw / 1000, 1) * 1000;
        }

        public void DecayFitness(float level)
        {
            Fitness = Fitness - level * MAX_CONSTITUTION / Constitution;
        }

        //public bool EvaluateContractOffer(Contract c)
        //{

        //}

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
        Goalie, Striker, Midfielder, Defender
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
