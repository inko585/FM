using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Save
{
    //public class FM_Universe
    //{
    //    public List<string> LeagueAssociations { get; set; }
    //    public List<string> Transfers { get; set; }
    //    public string CurrentSeason { get; set; }
    //    public List<string> Seasons { get; set; }
    //}

    public class FM_Season
    {
        //public List<LeagueMatchDay> LeagueMatchDays { get; set; }
        public string Id = IdHelper.NewId();
        public int Year { get; set; }
        public int CurrentWeekIndex { get; set; }
        public List<string> FootballWeeks { get; set; }
    }

    public class FM_FootballWeek
    {
        public string Id = IdHelper.NewId();
        public int Number { get; set; }
        public List<string> MatchDays { get; set; }
    }

    public class FM_MatchDay
    {
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
        public string Id = IdHelper.NewId();
        public int Viewer { get; set; }
        public string HomeClub { get; set; }
        public string AwayClub { get; set; }
        public List<string> ScoreEvents { get; set; }
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
        public string Id = IdHelper.NewId();

        public string Name { get; set; }
        public List<string> Leagues { get; set; }
    }

    public class FM_League
    {
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
        public string MainColor { get; set; }
        public string SecondaryColor { get; set; }
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
        //public List<string> Leagues { get; set; }

    }

    public class FM_Player
    {
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
    }

    public class FM_Coach
    {
        public string Id = IdHelper.NewId();
        public string Club { get; set; }
    }

    public class FM_Transfer
    {
        public string Id = IdHelper.NewId();
    }

    public class FM_Sponsor
    {
        public string Id = IdHelper.NewId();
    }


}
