using FM.Common;
using FM.Common.Generic;
using FM.Common.Pixels;
using FM.Common.Season;
using FM.Entities.Base;
using FM.Generator;
using FootballPit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FM.Save
{

    public class FM_Universe
    {


        public FM_Universe()
        {
            Coaches = new List<FM_Coach>();
            Clubs = new List<FM_Club>();
            Contracts = new List<FM_Contract>();
            Faces = new List<FM_Face>();
            FootballWeeks = new List<FM_FootballWeek>();
            Leagues = new List<FM_League>();
            LeagueAssociations = new List<FM_LeagueAssociation>();
            LeagueCompetitors = new List<FM_LeagueCompetitor>();
            Matches = new List<FM_Match>();
            MatchDays = new List<FM_MatchDay>();
            MatchesResults = new List<FM_MatchResult>();
            Players = new List<FM_Player>();
            PlayerStatistics = new List<FM_PlayerStatistics>();
            ScoreEvents = new List<FM_ScoreEvent>();
            Seasons = new List<FM_Season>();
            Sponsors = new List<FM_Sponsor>();
            Substitutions = new List<FM_Substitution>();
            Transfers = new List<FM_Transfer>();
        }

        public string PlayerClub;
        public List<FM_Player> RetiredPlayers { get; set; }
        public List<FM_Coach> Coaches { get; set; }
        public List<FM_Club> Clubs { get; set; }
        public List<FM_Contract> Contracts { get; set; }
        public List<FM_Face> Faces { get; set; }
        public List<FM_FootballWeek> FootballWeeks { get; set; }
        public List<FM_League> Leagues { get; set; }
        public List<FM_LeagueAssociation> LeagueAssociations { get; set; }
        public List<FM_LeagueCompetitor> LeagueCompetitors { get; set; }
        public List<FM_Match> Matches { get; set; }
        public List<FM_MatchDay> MatchDays { get; set; }
        public List<FM_MatchResult> MatchesResults { get; set; }
        public List<FM_Player> Players { get; set; }
        public List<FM_PlayerStatistics> PlayerStatistics { get; set; }
        public List<FM_ScoreEvent> ScoreEvents { get; set; }
        public List<FM_Season> Seasons { get; set; }
        public List<FM_Sponsor> Sponsors { get; set; }
        public List<FM_Substitution> Substitutions { get; set; }
        public List<FM_Transfer> Transfers { get; set; }
    }

    public class FM_Season
    {
        public FM_Season()
        {
            FootballWeeks = new List<string>();
        }
        public string Id = IdHelper.NewId();
        public int Year { get; set; }
        public int CurrentWeekIndex { get; set; }
        public List<string> FootballWeeks { get; set; }
    }

    public class FM_FootballWeek
    {
        public FM_FootballWeek()
        {
            MatchDays = new List<string>();
        }
        public string Id = IdHelper.NewId();
        public int Number { get; set; }
        public List<string> MatchDays { get; set; }
    }

    public class FM_MatchDay
    {
        public FM_MatchDay()
        {
            Matches = new List<string>();
        }
        public string Id = IdHelper.NewId();
        public string League { get; set; }
        public List<string> Matches { get; set; }
        public int Number { get; set; }

    }

    public class FM_Match
    {
        public string Id = IdHelper.NewId();
        public string HomeCompetitor { get; set; }
        public string AwayCompetitor { get; set; }
        public string HomeClub { get; set; }
        public string AwayClub { get; set; }
        public string MatchResult { get; set; }

    }

    public class FM_MatchResult
    {
        public FM_MatchResult()
        {
            ScoreEvents = new List<string>();
            Substitutions = new List<string>();
        }
        public string Id = IdHelper.NewId();
        public int Viewer { get; set; }
        public string HomeClub { get; set; }
        public string AwayClub { get; set; }
        public List<string> ScoreEvents { get; set; }
        public List<string> Substitutions { get; set; }
    }

    public class FM_ScoreEvent
    {
        public string Id = IdHelper.NewId();
        public string Club { get; set; }
        public int Minute { get; set; }
        public string Scorer { get; set; }
        public string CurrentScore { get; set; }
        public int CurrentHomeGoals { get; set; }
        public int CurrentAwayGoals { get; set; }
    }

    public class FM_Substitution
    {
        public string Id = IdHelper.NewId();
        public string Club { get; set; }
        public string PlayerIn { get; set; }
        public string PlayerOut { get; set; }
        public int Minute { get; set; }
    }
    //public class LeagueMatchDay
    //{
    //    public string League { get; set; }
    //    public string MatchDay { get; set; }
    //}

    public class IdHelper
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
    public class FM_LeagueAssociation
    {
        public FM_LeagueAssociation()
        {
            Leagues = new List<string>();
        }
        public string Id = IdHelper.NewId();
        public string Name { get; set; }
        public List<string> Leagues { get; set; }
    }

    public class FM_League
    {
        public FM_League()
        {
            Clubs = new List<string>();
            LeagueCompetitors = new List<string>();
        }

        public string Id = IdHelper.NewId();
        public int Depth { get; set; }
        public double Power { get; set; }
        public List<string> Clubs { get; set; }
        public List<string> LeagueCompetitors { get; set; }
    }

    public class FM_LeagueCompetitor
    {
        public string Id = IdHelper.NewId();
        public string League { get; set; }
        public string Club { get; set; }
        public int Rank { get; set; }
        public int Goals { get; set; }
        public int CounterGoals { get; set; }
        public int Points { get; set; }
    }

    public class FM_Club
    {

        public FM_Club()
        {
            NewPlayersFromYouth = new List<string>();
            NewPlayersWithFee = new List<string>();
            NewPlayersWithoutFee = new List<string>();
            LastYearLineUp = new List<string>();
            Rooster = new List<string>();
            JoiningPlayers = new List<string>();

        }
        public string Id = IdHelper.NewId();
        public string Name { get; set; }
        public string Nation { get; set; }
        public int StadiumLevel { get; set; }
        public int TrainingGroundsLevel { get; set; }
        public int OfficeLevel { get; set; }
        public int YoutWorkLevel { get; set; }
        public List<string> NewPlayersWithoutFee { get; set; }
        public List<string> NewPlayersWithFee { get; set; }
        public List<string> NewPlayersFromYouth { get; set; }
        public List<string> LastYearLineUp { get; set; }
        public int PlayersSoldFromStartingLineUp { get; set; }
        public List<string> Rooster { get; set; }
        public List<string> JoiningPlayers { get; set; }
        public string Coach { get; set; }
        public string MainColor1 { get; set; }
        public string MainColor2 { get; set; }
        public string SecondaryColor1 { get; set; }
        public string SecondaryColor2 { get; set; }
        public string Crest { get; set; }
        public string Dress { get; set; }
        public bool IsClimber { get; set; }
        public string CurrentSponsor { get; set; }
        public int TransferExpensesCurrentSeason { get; set; }
        public int TransferIncomeCurrentSeason { get; set; }
        public int Account { get; set; }
        public int Savings { get; set; }
        public int Elo { get; set; }
        public int SponsorMoneyCurrentSeason { get; set; }
        public double ViewerAttractionEstimation { get; set; }

    }

    public class FM_Player
    {
        public FM_Player()
        {
            ClubHistory = new List<string>();
            PlayerStatistics = new List<string>();
        }

        public bool IsRetired { get; set; }

        public string Id = IdHelper.NewId();

        public double PlayerPriceAdjustment { get; set; }
        public int DressNumber { get; set; }
        public List<string> ClubHistory { get; set; }
        public List<string> PlayerStatistics { get; set; }
        public string Face { get; set; }
        public string ContractCurrent { get; set; }
        public string ContractComing { get; set; }
        public int Age { get; set; }
        public float SkillBase { get; set; }
        public float XPLevel { get; set; }
        public int XP { get; set; }
        public double TalentFactor { get; set; }
        public float Fitness { get; set; }
        public float Constitution { get; set; }
        public float ConstitutionBase { get; set; }
        public float Moral { get; set; }
        public float SetPlaySkill { get; set; }
        public int Position { get; set; }
        public string Nation { get; set; }
        public float Charisma { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool? WantsToLeaveClub { get; set; }
    }

    public class FM_PlayerStatistics
    {
        public string Id = IdHelper.NewId();
        public string Player { get; set; }
        public string Club { get; set; }
        public int Goals { get; set; }
        public int Matches { get; set; }
        public int Skill { get; set; }
        public int Year { get; set; }
        public int LeagueDepth { get; set; }
    }

    public class FM_Contract
    {
        public string Id = IdHelper.NewId();
        public int RunTime { get; set; }
        public int Salary { get; set; }
        public string Player { get; set; }
        public string Club { get; set; }
        public string SignedOnSeason { get; set; }

    }

    public class FM_Face
    {
        public string Id = IdHelper.NewId();
        public string SkinColor { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string Head { get; set; }
        public string Mouth { get; set; }
        public string Eye { get; set; }
    }

    public class FM_Coach
    {
        public string Id = IdHelper.NewId();
        public string Club { get; set; }
        public int Philosophie { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nation { get; set; }
    }

    public class FM_Transfer
    {
        public string Id = IdHelper.NewId();
        public string Player { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int Price { get; set; }
        public int MarketValue { get; set; }

    }

    public class FM_Sponsor
    {
        public string Id = IdHelper.NewId();
        public string Name { get; set; }
        public double InvestRate { get; set; }
        public int YearsInClub { get; set; }
    }

    public class IO
    {
        private static Dictionary<Player, FM_Player> PlayerCache = new Dictionary<Player, FM_Player>();
        private static Dictionary<Club, FM_Club> ClubCache = new Dictionary<Club, FM_Club>();
        private static Dictionary<League, FM_League> LeagueCache = new Dictionary<League, FM_League>();
        private static Dictionary<LeagueCompetitor, FM_LeagueCompetitor> LeagueCompetitorCache = new Dictionary<LeagueCompetitor, FM_LeagueCompetitor>();
        private static Dictionary<ScoreEvent, FM_ScoreEvent> ScoreEventCache = new Dictionary<ScoreEvent, FM_ScoreEvent>();
        private static Dictionary<Substitution, FM_Substitution> SubstitutionCache = new Dictionary<Substitution, FM_Substitution>();
        private static Dictionary<Season, FM_Season> SeasonCache = new Dictionary<Season, FM_Season>();

        private static void ClearSaveCache()
        {
            PlayerCache.Clear();
            LeagueCache.Clear();
            ClubCache.Clear();
            LeagueCompetitorCache.Clear();
            ScoreEventCache.Clear();
            SubstitutionCache.Clear();
            SeasonCache.Clear();

        }

        private static Dictionary<string, Player> PlayerLoadCache = new Dictionary<string, Player>();
        private static Dictionary<string, Club> ClubLoadCache = new Dictionary<string, Club>();
        private static Dictionary<string, League> LeagueLoadCache = new Dictionary<string, League>();
        private static Dictionary<string, LeagueCompetitor> CompetitorLoadCache = new Dictionary<string, LeagueCompetitor>();
        private static Dictionary<string, ScoreEvent> SELoadCache = new Dictionary<string, ScoreEvent>();
        private static Dictionary<string, Substitution> SubLoadCache = new Dictionary<string, Substitution>();
        private static Dictionary<string, Season> SeasonLoadCache = new Dictionary<string, Season>();
        private static Dictionary<string, Sponsor> SponsorLoadCache = new Dictionary<string, Sponsor>();
        private static Dictionary<string, Coach> CoachLoadCache = new Dictionary<string, Coach>();
        private static Dictionary<string, FootballWeek> WeekLoadCache = new Dictionary<string, FootballWeek>();
        private static Dictionary<string, MatchDay> MatchDayLoadCache = new Dictionary<string, MatchDay>();
        private static Dictionary<string, Match> MatchLoadCache = new Dictionary<string, Match>();
        private static Dictionary<string, MatchResult> MatchResultLoadCache = new Dictionary<string, MatchResult>();
        private static Dictionary<string, Face> FaceLoadCache = new Dictionary<string, Face>();
        private static Dictionary<string, PlayerStatistics> StatisticsLoadCache = new Dictionary<string, PlayerStatistics>();
        private static Dictionary<ScoreEvent, Player> SEPlayerBuffer = new Dictionary<ScoreEvent, Player>();
        private static Dictionary<Substitution, Tuple<Player, Player>> SubPlayerBuffer = new Dictionary<Substitution, Tuple<Player, Player>>();
        private static List<string> CurrentContractIds = new List<string>();

        private static void ClearLoadCache()
        {
            PlayerLoadCache.Clear();
            ClubLoadCache.Clear();
            LeagueLoadCache.Clear();
            CompetitorLoadCache.Clear();
            SELoadCache.Clear();
            SubLoadCache.Clear();
            SeasonLoadCache.Clear();
            SponsorLoadCache.Clear();
            CoachLoadCache.Clear();
            WeekLoadCache.Clear();
            MatchLoadCache.Clear();
            MatchDayLoadCache.Clear();
            MatchResultLoadCache.Clear();
            FaceLoadCache.Clear();
            StatisticsLoadCache.Clear();
            SEPlayerBuffer.Clear();
            SubPlayerBuffer.Clear();
            CurrentContractIds.Clear();
        }



        public static FootballUniverse Load(World w, out Club playerClub)
        {

            FootballUniverse ret = null;

            if (Util.TryGetXMLOpenPath(out string path))
            {
                var serializer = new XmlSerializer(typeof(FM_Universe));

                FM_Universe load;
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    load = (FM_Universe)serializer.Deserialize(fileStream);
                }

                ret = new FootballUniverse(w);
                ret.LeagueAssociations = new List<LeagueAssociation>();

                foreach (var coach in load.Coaches)
                {
                    var actualCoach = new Coach()
                    {
                        FirstName = coach.FirstName,
                        LastName = coach.LastName,
                        Philospophie = WorldGenerator.Philospophies[coach.Philosophie],
                        Nation = w.GetNationByName(coach.Nation)
                    };
                    CoachLoadCache[coach.Id] = actualCoach;
                }

                foreach (var sponsor in load.Sponsors)
                {
                    var actualSponsor = new Sponsor()
                    {
                        InvestRate = sponsor.InvestRate,
                        Name = sponsor.Name,
                        YearsInClub = sponsor.YearsInClub,
                    };
                    SponsorLoadCache[sponsor.Id] = actualSponsor;
                }


                foreach (var club in load.Clubs)
                {
                    var actualClub = new Club()
                    {
                        Account = club.Account,
                        Elo = club.Elo,
                        IsClimber = club.IsClimber,
                        Name = club.Name,
                        Savings = club.Savings,
                        SponsorMoneyCurrentSeason = club.SponsorMoneyCurrentSeason,
                        TransferExpensesCurrentSeason = club.TransferExpensesCurrentSeason,
                        TransferIncomeCurrentSeason = club.TransferIncomeCurrentSeason,
                        ViewerAttractionEstimation = club.ViewerAttractionEstimation,
                        Crest = club.Crest,
                        Dress = club.Dress,
                        Nation = w.GetNationByName(club.Nation),
                        Coach = CoachLoadCache[club.Coach],
                        CurrentSponsor = SponsorLoadCache[club.CurrentSponsor],
                        PlayersSoldFromStartingLineUp = club.PlayersSoldFromStartingLineUp
                    };

                    actualClub.JoiningPlayers = new List<Player>();

                    actualClub.ClubColors = new Common.Pixels.ClubColors()
                    {
                        MainColorString = club.MainColor1,
                        SecondColorString = club.MainColor2,
                        MainColor = WorldGenerator.GetColorFromText(club.MainColor1),
                        SecondColor = WorldGenerator.GetColorFromText(club.MainColor2)
                    };

                    actualClub.SecondClubColors = new Common.Pixels.ClubColors()
                    {
                        MainColorString = club.SecondaryColor1,
                        SecondColorString = club.SecondaryColor2,
                        MainColor = WorldGenerator.GetColorFromText(club.SecondaryColor1),
                        SecondColor = WorldGenerator.GetColorFromText(club.SecondaryColor2)
                    };

                    actualClub.ClubAssetLevel[ClubAsset.TrainingGrounds] = club.TrainingGroundsLevel;
                    actualClub.ClubAssetLevel[ClubAsset.YouthWork] = club.YoutWorkLevel;
                    actualClub.ClubAssetLevel[ClubAsset.Stadium] = club.StadiumLevel;
                    actualClub.ClubAssetLevel[ClubAsset.Office] = club.OfficeLevel;
                    actualClub.Rooster = new List<Player>();
                    ClubLoadCache[club.Id] = actualClub;

                }

                

                foreach (var league in load.Leagues)
                {
                    var actualLeague = new League()
                    {
                        Depth = league.Depth,
                        Power = league.Power
                    };

                    actualLeague.Standings = new List<LeagueCompetitor>();
                    LeagueLoadCache[league.Id] = actualLeague;

                }

                foreach (var lc in load.LeagueCompetitors)
                {
                    var actualLC = new LeagueCompetitor()
                    {
                        Club = ClubLoadCache[lc.Club],
                        CounterGoals = lc.CounterGoals,
                        Goals = lc.Goals,
                        League = LeagueLoadCache[lc.League],
                        Points = lc.Points,
                    };


                    actualLC.Club.Leagues.Add(actualLC.League);
                    actualLC.League.Clubs.Add(actualLC.Club);
                    actualLC.League.Standings.Add(actualLC);
                    CompetitorLoadCache[lc.Id] = actualLC;
                }

                foreach (var la in load.LeagueAssociations)
                {
                    var acutalLA = new LeagueAssociation() { Name = la.Name };

                    ret.LeagueAssociations.Add(acutalLA);


                    foreach (var league in la.Leagues)
                    {
                        acutalLA.Leagues.Add(LeagueLoadCache[league]);
                        LeagueLoadCache[league].Association = acutalLA;
                    }
                }

                foreach (var face in load.Faces)
                {
                    var actualFace = new Face()
                    {
                        Eye = face.Eye,
                        EyeColor = WorldGenerator.GetColorFromText(face.EyeColor),
                        EyeColorString = face.EyeColor,
                        Head = face.Head,
                        HairColor = WorldGenerator.GetColorFromText(face.HairColor),
                        HairColorString = face.HairColor,
                        Mouth = face.Mouth,
                        SkinColor = WorldGenerator.GetColorFromText(face.SkinColor),
                        SkinColorString = face.SkinColor
                    };

                    FaceLoadCache[face.Id] = actualFace;
                }

                foreach (var playerStatistic in load.PlayerStatistics)
                {
                    var actualStatistic = new PlayerStatistics()
                    {
                        Club = ClubLoadCache[playerStatistic.Club],
                        Goals = playerStatistic.Goals,
                        LeagueDepth = playerStatistic.LeagueDepth,
                        Matches = playerStatistic.Matches,
                        Skill = playerStatistic.Skill,
                        Year = playerStatistic.Year
                    };

                    StatisticsLoadCache[playerStatistic.Id] = actualStatistic;

                }





                foreach (var pl in load.Players)
                {
                    var actualPlayer = new Player()
                    {
                        Age = pl.Age,
                        Constitution = pl.Constitution,
                        DressNumber = pl.DressNumber,
                        Fitness = pl.Fitness,
                        Moral = pl.Moral,
                        Position = (Position)pl.Position,
                        SetPlaySkill = pl.SetPlaySkill,
                        SkillBase = pl.SkillBase,
                        TalentFactor = pl.TalentFactor,
                        XP = pl.XP,
                        XPLevel = pl.XPLevel,
                        Nation = w.GetNationByName(pl.Nation),
                        Face = FaceLoadCache[pl.Face],
                        Charisma = pl.Charisma,
                        FirstName = pl.FirstName,
                        LastName = pl.LastName,
                        ConstitutionBase = pl.ConstitutionBase,
                        PlayerPriceAdjustment = pl.PlayerPriceAdjustment,
                        WantsToLeavePlayerClub = pl.WantsToLeaveClub

                    };

                    if (pl.IsRetired != null && pl.IsRetired)
                    {
                        ret.RetiredPlayers.Add(actualPlayer);
                    }

                    CurrentContractIds.Add(pl.ContractCurrent);
                    actualPlayer.ClubHistory = pl.ClubHistory.Select(ch => ClubLoadCache[ch]).ToList();
                    actualPlayer.PlayerStatistics = pl.PlayerStatistics.Select(ps => StatisticsLoadCache[ps]).ToList();
                    actualPlayer.PlayerStatistics.ForEach(ps => ps.Player = actualPlayer);
                    PlayerLoadCache[pl.Id] = actualPlayer;

                }


                foreach (var se in load.ScoreEvents)
                {
                    var actualSE = new ScoreEvent()
                    {
                        Club = ClubLoadCache[se.Club],
                        Minute = se.Minute,
                        CurrentScore = se.CurrentScore,
                        CurrentHomeGoals = se.CurrentHomeGoals,
                        CurrentAwayGoals = se.CurrentAwayGoals
                    };
                    SEPlayerBuffer[actualSE] = PlayerLoadCache[se.Scorer];
                    SELoadCache[se.Id] = actualSE;
                }

                foreach (var su in load.Substitutions)
                {
                    var actualSu = new Substitution()
                    {
                        Club = ClubLoadCache[su.Club],
                        Minute = su.Minute

                    };
                    SubPlayerBuffer[actualSu] = Tuple.Create(PlayerLoadCache[su.PlayerIn], PlayerLoadCache[su.PlayerOut]);
                    SubLoadCache[su.Id] = actualSu;
                }


                foreach (var mr in load.MatchesResults)
                {
                    var actualMR = new MatchResult()
                    {
                        AwayClub = ClubLoadCache[mr.AwayClub],
                        HomeClub = ClubLoadCache[mr.HomeClub],
                        Viewer = mr.Viewer

                    };

                    foreach (var se in mr.ScoreEvents)
                    {
                        actualMR.Scorers.Add(SELoadCache[se]);
                    }

                    foreach (var su in mr.Substitutions)
                    {
                        actualMR.Substitutions.Add(SubLoadCache[su]);
                    }

                    MatchResultLoadCache[mr.Id] = actualMR;
                }

                foreach (var m in load.Matches)
                {
                    var actualMatch = new Match()
                    {
                        HomeClub = ClubLoadCache[m.HomeClub],
                        AwayClub = ClubLoadCache[m.AwayClub]
                    };

                    if (m.HomeCompetitor != null)
                    {
                        actualMatch.AwayCompetitor = CompetitorLoadCache[m.AwayCompetitor];
                        actualMatch.HomeCompetitor = CompetitorLoadCache[m.HomeCompetitor];
                    }

                    if (MatchResultLoadCache.TryGetValue(m.MatchResult ?? "", out MatchResult actualMR))
                    {
                        actualMatch.MatchResult = actualMR;
                    }

                    MatchLoadCache[m.Id] = actualMatch;
                }

                foreach (var md in load.MatchDays)
                {
                    var actualMD = new MatchDay(md.Number)
                    {
                        League = LeagueLoadCache[md.League]
                    };

                    actualMD.Matches = md.Matches.Select(m => MatchLoadCache[m]).ToList();

                    MatchDayLoadCache[md.Id] = actualMD;
                }

                foreach (var fw in load.FootballWeeks)
                {
                    var actualFW = new FootballWeek()
                    {
                        Number = fw.Number
                    };

                    WeekLoadCache[fw.Id] = actualFW;

                    foreach (var md in fw.MatchDays)
                    {
                        actualFW.MatchDays.Add(MatchDayLoadCache[md]);
                    }
                }

                Season.AllSeasons.Clear();

                foreach (var season in load.Seasons)
                {
                    var actualSeason = new Season()
                    {
                        CurrentWeekIndex = season.CurrentWeekIndex,
                        Year = season.Year,
                        FootballWeeks = load.FootballWeeks.Where(fw => season.FootballWeeks.Contains(fw.Id)).Select(fw => WeekLoadCache[fw.Id]).ToList(),
                    };

                    SeasonLoadCache[season.Id] = actualSeason;
                    Season.AllSeasons.Add(actualSeason);
                }



                Season.CurrentSeason = Season.AllSeasons.Last();

                foreach (var c in load.Contracts)
                {
                    var actualContract = new Contract()
                    {
                        Club = ClubLoadCache[c.Club],
                        RunTime = c.RunTime,
                        Salary = c.Salary,
                        SignedOn = c.SignedOnSeason == null ? null : SeasonLoadCache[c.SignedOnSeason],
                        Player = PlayerLoadCache[c.Player]
                    };

                    if (CurrentContractIds.Contains(c.Id))
                    {
                        actualContract.Player.ContractCurrent = actualContract;
                        actualContract.Club.Rooster.Add(actualContract.Player);
                    }
                    else
                    {
                        actualContract.Player.ContractComing = actualContract;
                    }
                }

                foreach (var m in MatchLoadCache.Values)
                {
                    if (m.MatchResult != null)
                    {
                        foreach (var se in m.MatchResult.Scorers)
                        {
                            se.Scorer = m.MatchPlayer(SEPlayerBuffer[se]);
                        }

                        foreach (var su in m.MatchResult.Substitutions)
                        {
                            su.In = m.MatchPlayer(SubPlayerBuffer[su].Item1);
                            su.Out = m.MatchPlayer(SubPlayerBuffer[su].Item2);
                        }
                    }
                }

                foreach (var transfer in load.Transfers)
                {
                    var actualTransfer = new Transfer()
                    {
                        From = ClubLoadCache[transfer.From],
                        To = transfer.To == null ? null : ClubLoadCache[transfer.To],
                        MarketValue = transfer.MarketValue,
                        Player = PlayerLoadCache[transfer.Player],
                        Price = transfer.Price,
                        Week = transfer.Week,
                        Year = transfer.Year

                    };

                    ret.TransferList.Add(actualTransfer);
                }

                foreach (var pl in PlayerLoadCache.Values)
                {
                    if (pl.ContractComing != null)
                    {
                        pl.ContractComing.Club.JoiningPlayers.Add(pl);
                    }
                }

                playerClub = ClubLoadCache[load.PlayerClub];
                ClearLoadCache();
                ret.ResetCache();
                return ret;

            }

            playerClub = null;
            return null;

        }


        public static void Save(FootballUniverse u)
        {
            var save = new FM_Universe();


            foreach (var club in u.Clubs)
            {
                var club_ = new FM_Club()
                {
                    Account = club.Account,
                    Elo = club.Elo,
                    IsClimber = club.IsClimber,
                    MainColor1 = club.ClubColors.MainColorString,
                    MainColor2 = club.ClubColors.SecondColorString,
                    SecondaryColor1 = club.SecondClubColors.MainColorString,
                    SecondaryColor2 = club.SecondClubColors.SecondColorString,
                    Name = club.Name,
                    Nation = club.Nation.Name,
                    Savings = club.Savings,
                    OfficeLevel = club.ClubAssetLevel[ClubAsset.Office],
                    SponsorMoneyCurrentSeason = club.SponsorMoneyCurrentSeason,
                    StadiumLevel = club.ClubAssetLevel[ClubAsset.Stadium],
                    TrainingGroundsLevel = club.ClubAssetLevel[ClubAsset.TrainingGrounds],
                    YoutWorkLevel = club.ClubAssetLevel[ClubAsset.YouthWork],
                    TransferExpensesCurrentSeason = club.TransferExpensesCurrentSeason,
                    TransferIncomeCurrentSeason = club.TransferIncomeCurrentSeason,
                    ViewerAttractionEstimation = club.ViewerAttractionEstimation,
                    Crest = club.Crest,
                    Dress = club.Dress,
                    PlayersSoldFromStartingLineUp = club.PlayersSoldFromStartingLineUp

                };

                ClubCache[club] = club_;
                save.Clubs.Add(club_);

                var sponsor = new FM_Sponsor()
                {
                    InvestRate = club.CurrentSponsor.InvestRate,
                    Name = club.CurrentSponsor.Name,
                    YearsInClub = club.CurrentSponsor.YearsInClub
                };

                club_.CurrentSponsor = sponsor.Id;
                save.Sponsors.Add(sponsor);

                var coach_ = new FM_Coach()
                {
                    FirstName = club.Coach.FirstName,
                    LastName = club.Coach.LastName,
                    Nation = club.Coach.Nation.Name,
                    Philosophie = WorldGenerator.Philospophies.IndexOf(club.Coach.Philospophie)
                };

                club_.Coach = coach_.Id;
                save.Coaches.Add(coach_);
            }

            save.PlayerClub = ClubCache[Game.Instance.PlayerClub].Id;
            foreach (var la in u.LeagueAssociations)
            {
                var la_ = new FM_LeagueAssociation() { Name = la.Name };
                save.LeagueAssociations.Add(la_);

                foreach (var league in la.Leagues)
                {
                    var league_ = new FM_League()
                    {
                        Depth = league.Depth,
                        Power = league.Power
                    };

                    LeagueCache[league] = league_;
                    la_.Leagues.Add(league_.Id);
                    save.Leagues.Add(league_);

                    foreach (var lc in league.Standings)
                    {
                        var lc_ = new FM_LeagueCompetitor()
                        {
                            Club = ClubCache[lc.Club].Id,
                            CounterGoals = lc.CounterGoals,
                            Goals = lc.Goals,
                            League = league_.Id,
                            Points = lc.Points,
                            Rank = lc.Rank
                        };

                        LeagueCompetitorCache[lc] = lc_;
                        league_.LeagueCompetitors.Add(lc_.Id);
                        save.LeagueCompetitors.Add(lc_);
                    }
                }
            }

            foreach (var season in Season.AllSeasons)
            {
                var season_ = new FM_Season()
                {
                    CurrentWeekIndex = season == Season.CurrentSeason ? season.FootballWeeks.IndexOf(season.CurrentWeek) : season.FootballWeeks.Count - 1,
                    Year = season.Year
                };

                save.Seasons.Add(season_);
                SeasonCache[season] = season_;

                foreach (var fw in season.FootballWeeks)
                {
                    var fw_ = new FM_FootballWeek()
                    {
                        Number = fw.Number
                    };

                    season_.FootballWeeks.Add(fw_.Id);
                    save.FootballWeeks.Add(fw_);

                    foreach (var md in fw.MatchDays)
                    {
                        var md_ = new FM_MatchDay()
                        {
                            League = LeagueCache[md.League].Id,
                            Number = md.Number
                        };

                        fw_.MatchDays.Add(md_.Id);
                        save.MatchDays.Add(md_);

                        foreach (var m in md.Matches)
                        {
                            var match_ = new FM_Match()
                            {
                                HomeClub = ClubCache[m.HomeClub].Id,
                                AwayClub = ClubCache[m.AwayClub].Id
                            };

                            if (!m.IsPlayed)
                            {
                                match_.HomeCompetitor = LeagueCompetitorCache[m.HomeCompetitor].Id;
                                match_.AwayCompetitor = LeagueCompetitorCache[m.AwayCompetitor].Id;
                            }


                            md_.Matches.Add(match_.Id);
                            save.Matches.Add(match_);

                            if (m.MatchResult != null)
                            {
                                var mr_ = new FM_MatchResult()
                                {
                                    HomeClub = ClubCache[m.MatchResult.HomeClub].Id,
                                    AwayClub = ClubCache[m.MatchResult.AwayClub].Id,
                                    Viewer = m.MatchResult.Viewer
                                };


                                save.MatchesResults.Add(mr_);
                                match_.MatchResult = mr_.Id;


                                foreach (var se in m.MatchResult.Scorers)
                                {
                                    var se_ = new FM_ScoreEvent()
                                    {
                                        Club = ClubCache[se.Club].Id,
                                        Minute = se.Minute,
                                        CurrentScore = se.CurrentScore,
                                        CurrentAwayGoals = se.CurrentAwayGoals,
                                        CurrentHomeGoals = se.CurrentHomeGoals
                                    };

                                    ScoreEventCache[se] = se_;
                                    save.ScoreEvents.Add(se_);
                                    mr_.ScoreEvents.Add(se_.Id);
                                }

                                foreach (var su in m.MatchResult.Substitutions)
                                {
                                    var su_ = new FM_Substitution()
                                    {
                                        Club = ClubCache[su.Club].Id,
                                        Minute = su.Minute,
                                    };

                                    SubstitutionCache[su] = su_;
                                    save.Substitutions.Add(su_);
                                    mr_.Substitutions.Add(su_.Id);
                                }
                            }

                        }

                    }
                }
            }

            foreach (var pl in u.Players.Concat(u.RetiredPlayers))
            {
                var player_ = new FM_Player()
                {
                    Age = pl.Age,
                    Constitution = pl.Constitution,
                    DressNumber = pl.DressNumber,
                    Fitness = pl.Fitness,
                    Moral = pl.Moral,
                    Position = (int)pl.Position,
                    SetPlaySkill = pl.SetPlaySkill,
                    SkillBase = pl.SkillBase,
                    TalentFactor = pl.TalentFactor,
                    XP = pl.XP,
                    XPLevel = pl.XPLevel,
                    Nation = pl.Nation.Name,
                    Charisma = pl.Charisma,
                    FirstName = pl.FirstName,
                    LastName = pl.LastName,
                    ConstitutionBase = pl.ConstitutionBase,
                    IsRetired = u.RetiredPlayers.Contains(pl),
                    PlayerPriceAdjustment = pl.PlayerPriceAdjustment,
                    WantsToLeaveClub = pl.WantsToLeavePlayerClub


                };


                PlayerCache[pl] = player_;
                save.Players.Add(player_);

                var face = new FM_Face()
                {
                    Eye = pl.Face.Eye,
                    EyeColor = pl.Face.EyeColorString,
                    HairColor = pl.Face.HairColorString,
                    Head = pl.Face.Head,
                    Mouth = pl.Face.Mouth,
                    SkinColor = pl.Face.SkinColorString
                };

                save.Faces.Add(face);
                player_.Face = face.Id;

                if (pl.ContractComing != null)
                {
                    var contractComing = new FM_Contract()
                    {
                        Club = ClubCache[pl.ContractComing.Club].Id,
                        Player = player_.Id,
                        RunTime = pl.ContractComing.RunTime,
                        Salary = pl.ContractComing.Salary,
                        SignedOnSeason = SeasonCache[pl.ContractComing.SignedOn].Id,
                    };

                    player_.ContractComing = contractComing.Id;
                    save.Contracts.Add(contractComing);
                }

                if (pl.ContractCurrent != null)
                {
                    var contractCurrent = new FM_Contract()
                    {
                        Club = ClubCache[pl.ContractCurrent.Club].Id,
                        Player = player_.Id,
                        RunTime = pl.ContractCurrent.RunTime,
                        Salary = pl.ContractCurrent.Salary,

                    };


                    if (pl.ContractCurrent.SignedOn != null)
                    {
                        contractCurrent.SignedOnSeason = SeasonCache[pl.ContractCurrent.SignedOn].Id;
                    }


                    player_.ContractCurrent = contractCurrent.Id;
                    save.Contracts.Add(contractCurrent);
                }

            }




            foreach (var keyValuePlayer in PlayerCache)
            {
                foreach (var ps in keyValuePlayer.Key.PlayerStatistics)
                {
                    var ps_ = new FM_PlayerStatistics()
                    {
                        Goals = ps.Goals,
                        LeagueDepth = ps.LeagueDepth,
                        Matches = ps.Matches,
                        Player = keyValuePlayer.Value.Id,
                        Skill = ps.Skill,
                        Year = ps.Year,
                        Club = ClubCache[ps.Club].Id
                    };
                    save.PlayerStatistics.Add(ps_);
                    keyValuePlayer.Value.PlayerStatistics.Add(ps_.Id);
                }

                keyValuePlayer.Value.ClubHistory = keyValuePlayer.Key.ClubHistory.Select(c => ClubCache[c].Id).ToList();
            }

            foreach (var keyValueSE in ScoreEventCache)
            {
                keyValueSE.Value.Scorer = PlayerCache[keyValueSE.Key.Scorer.Player].Id;
            }

            foreach (var keyValueSu in SubstitutionCache)
            {
                keyValueSu.Value.PlayerIn = PlayerCache[keyValueSu.Key.In.Player].Id;
                keyValueSu.Value.PlayerOut = PlayerCache[keyValueSu.Key.Out.Player].Id;
            }



            foreach (var t in u.TransferList)
            {
                var t_ = new FM_Transfer()
                {
                    From = ClubCache[t.From].Id,
                    To = t.To == null? null : ClubCache[t.To].Id,
                    MarketValue = t.MarketValue,
                    Price = t.Price,
                    Week = t.Week,
                    Player = PlayerCache[t.Player].Id,
                    Year = t.Year
                };

                save.Transfers.Add(t_);
            }

            var xs = new XmlSerializer(typeof(FM_Universe));
            string path;

            if (Util.TryGetXMLSavePath("fm_save.xml", out path))
            {
                using (var tw = new StreamWriter(path))
                {
                    xs.Serialize(tw, save);
                }

            }

            ClearSaveCache();



        }



    }


}
