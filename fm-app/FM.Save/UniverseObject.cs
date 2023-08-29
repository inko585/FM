using FM.Common.Generic;
using FM.Common.Season;
using FM.Generator;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            League = new List<FM_League>();
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

        public List<FM_Coach> Coaches { get; set; }
        public List<FM_Club> Clubs { get; set; }
        public List<FM_Contract> Contracts { get; set; }
        public List<FM_Face> Faces { get; set; }
        public List<FM_FootballWeek> FootballWeeks { get; set; }
        public List<FM_League> League { get; set; }
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
        public bool IsPlayed { get; set; }
        public string League { get; set; }
        public List<string> Matches { get; set; }
        public int Number { get; set; }

    }

    public class FM_Match
    {
        public string Id = IdHelper.NewId();
        public string HomeCompetitor { get; set; }
        public string AwayCompetitor { get; set; }
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

        public string Id = IdHelper.NewId();
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
        public float Moral { get; set; }
        public float SetPlaySkill { get; set; }
        public int Position { get; set; }
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
                    ViewerAttractionEstimation = club.ViewerAttractionEstimation

                };

                ClubCache[club] = club_;
                save.Clubs.Add(club_);

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
                    save.League.Add(league_);

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
                            IsPlayed = md.IsPlayed,
                            League = LeagueCache[md.League].Id,
                            Number = md.Number
                        };

                        fw_.MatchDays.Add(md_.Id);
                        save.MatchDays.Add(md_);

                        foreach (var m in md.Matches)
                        {
                            var match_ = new FM_Match()
                            {
                                HomeCompetitor = LeagueCompetitorCache[m.HomeCompetitor].Id,
                                AwayCompetitor = LeagueCompetitorCache[m.AwayCompetitor].Id
                            };

                            md_.Matches.Add(match_.Id);
                            save.Matches.Add(match_);

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
                                    Minute = se.Minute
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

                foreach (var pl in u.Players)
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
                    };


                    PlayerCache[pl] = player_;
                    save.Players.Add(player_);

                    var face = new FM_Face()
                    {
                        Eye = pl.Face.Eye,
                        EyeColor = pl.Face.EyeColor.Name,
                        HairColor = pl.Face.HairColor.Name,
                        Head = pl.Face.Head,
                        Mouth = pl.Face.Mouth,
                        SkinColor = pl.Face.SkinColor.Name
                    };

                    save.Faces.Add(face);
                    player_.Face = face.Id;

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

                    var contractCurrent = new FM_Contract()
                    {
                        Club = ClubCache[pl.ContractCurrent.Club].Id,
                        Player = player_.Id,
                        RunTime = pl.ContractCurrent.RunTime,
                        Salary = pl.ContractCurrent.Salary,
                        SignedOnSeason = SeasonCache[pl.ContractCurrent.SignedOn].Id,
                    };

                    player_.ContractCurrent = contractCurrent.Id;
                    save.Contracts.Add(contractCurrent);

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

            }

            foreach (var t in u.TransferList)
            {
                var t_ = new FM_Transfer()
                {
                    From = ClubCache[t.From].Id,
                    To = ClubCache[t.To].Id,
                    MarketValue = t.MarketValue,
                    Price = t.Price,
                    Week = t.Week,
                    Player = PlayerCache[t.Player].Id,
                    Year = t.Year
                };

                save.Transfers.Add(t_);
            }

        }



    }


}
