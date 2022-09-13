using FM.Models.Generic;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Models.Season
{
    public class Season
    {
        public Season()
        {
            CurrentSeason = this;
            FootballWeeks = new List<FootballWeek>();
            CurrentWeekIndex = 0;
        }

        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler OnSeasonProgress;

        public static void InitSeasons(List<LeagueAssociation> la_ = null)
        {
            CurrentSeason = CreateSeason(la_);
        }
        public static Season CurrentSeason { get; private set; }

        public static Season CreateSeason(List<LeagueAssociation> la_ = null)
        {
            var season = new Season();
            var leagueAssociations = la_ ?? Game.Instance.FootballUniverse.LeagueAssociations;
            var weekCount = leagueAssociations.Max(la => la.Leagues.First().Clubs.Count - 1) * 2 + 2;

            for (int i = 0; i < weekCount; i++)
            {
                var week = new FootballWeek();
                week.Number = i + 1;
                season.FootballWeeks.Add(week);
            }

            foreach (var la in leagueAssociations)
            {
                foreach (var l in la.Leagues)
                {
                    var joker = new KeyValuePair<LeagueCompetitor, int>(l.Standings.Last(), l.Standings.Count);
                    var dict = l.Standings.Where(c => c != joker.Key).ToDictionary(x => x, x => l.Standings.IndexOf(x) + 1);
                    var firstHalfMatchDays = new List<MatchDay>();

                    for (var i = 1; i < l.Standings.Count; i++)
                    {
                        var md = new MatchDay(i);
                        md.League = l;
                        var pairings = new List<Match>();

                        foreach (var t1 in dict)
                        {

                            if (!pairings.Any(p => p.HomeCompetitor == t1.Key || p.AwayCompetitor == t1.Key))
                            {
                                var pairing = new Match();
                                KeyValuePair<LeagueCompetitor, int> other = new KeyValuePair<LeagueCompetitor, int>(null, 0);
                                foreach (var t2 in dict)
                                {
                                    if (!pairings.Any(p => p.HomeCompetitor == t2.Key || p.AwayCompetitor == t2.Key))
                                    {
                                        if (i == l.Standings.Count - 1)
                                        {
                                            if (t1.Value + t2.Value == i)
                                            {
                                                other = t2;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (t2.Key != t1.Key && (t1.Value + t2.Value) % (l.Clubs.Count - 1) == i)
                                            {
                                                other = t2;

                                                break;
                                            }
                                        }
                                    }

                                }

                                if (other.Key == null)
                                {
                                    other = joker;
                                }

                                if ((t1.Value + other.Value) % 2 == 0)
                                {
                                    pairing.HomeCompetitor = t1.Value > other.Value ? t1.Key : other.Key;
                                    pairing.AwayCompetitor = t1.Value > other.Value ? other.Key : t1.Key;
                                }
                                else
                                {
                                    pairing.HomeCompetitor = t1.Value < other.Value ? t1.Key : other.Key;
                                    pairing.AwayCompetitor = t1.Value < other.Value ? other.Key : t1.Key;
                                }

                                pairings.Add(pairing);
                            }



                        }
                        md.Matches = pairings;
                        firstHalfMatchDays.Add(md);
                        season.FootballWeeks[i].MatchDays.Add(md);
                    }
                    foreach (var md in firstHalfMatchDays)
                    {
                        var md2 = new MatchDay(md.Number + (l.Clubs.Count - 1));
                        md2.League = l;
                        md2.Matches = md.Matches.Select(p => new Match() { HomeCompetitor = p.AwayCompetitor, AwayCompetitor = p.HomeCompetitor }).ToList();
                        season.FootballWeeks[md2.Number].MatchDays.Add(md2);
                    }
                }
            }

            return season;

        }

        private int CurrentWeekIndex { get; set; }
        public FootballWeek CurrentWeek
        {
            get
            {
                return FootballWeeks[CurrentWeekIndex];
            }
        }

        public void Progress()
        {
            foreach (var md in CurrentWeek.MatchDays)
            {
                foreach (var m in md.Matches)
                {
                    m.Simulate(true);
                    AdjustElo(m.MatchResult);
                    if (m.MatchResult.HomeGoals > m.MatchResult.AwayGoals)
                    {
                        m.HomeCompetitor.Points += 3;
                    }
                    else
                    {
                        if (m.MatchResult.AwayGoals > m.MatchResult.HomeGoals)
                        {
                            m.AwayCompetitor.Points += 3;
                        }
                        else
                        {
                            m.HomeCompetitor.Points++;
                            m.AwayCompetitor.Points++;
                        }
                    }

                    m.HomeCompetitor.Goals += m.MatchResult.HomeGoals;
                    m.AwayCompetitor.CounterGoals += m.MatchResult.HomeGoals;
                    m.AwayCompetitor.Goals += m.MatchResult.AwayGoals;
                    m.HomeCompetitor.CounterGoals += m.MatchResult.AwayGoals;
                }
            }

            CurrentWeekIndex++;

            if (OnSeasonProgress == null)
            {
                return;
            }

            EventArgs args = new EventArgs();
            OnSeasonProgress(this, args);
        }

        public void AdjustElo(MatchResult mr)
        {
            var r1 = Math.Pow(10, mr.HomeClub.Elo / 400);
            var r2 = Math.Pow(10, mr.AwayClub.Elo / 400);
            var e1 = r1 / (r1 + r2);
            var e2 = r2 / (r1 + r2);
            var s1 = mr.HomeGoals == mr.AwayGoals ? 0.5 : mr.HomeGoals > mr.AwayGoals ? 1 : 0;
            var s2 = 1 - s1;
            mr.HomeClub.Elo = (int)Math.Round(mr.HomeClub.Elo + 32 * (s1 - e1));
            mr.AwayClub.Elo = (int)Math.Round(mr.AwayClub.Elo + 32 * (s2 - e2));
        }

        public List<FootballWeek> FootballWeeks { get; set; }

        private Dictionary<League, List<MatchDay>> leagueMatchDays;
        public Dictionary<League, List<MatchDay>> LeagueMatchDays
        {
            get
            {
                if (leagueMatchDays == null)
                {
                    if (FootballWeeks.Any())
                    {
                        var matchDays = FootballWeeks.SelectMany(fw => fw.MatchDays);
                        leagueMatchDays = matchDays.GroupBy(md => md.League).ToDictionary(gr => gr.Key, gr => gr.ToList());
                    }
                    else
                    {
                        return null;
                    }
                }

                return leagueMatchDays;
            }
        }
    }

    public class FootballWeek
    {
        public int Number { get; set; }
        public FootballWeek()
        {
            MatchDays = new List<MatchDay>();
        }
        public List<MatchDay> MatchDays { get; set; }
    }

    public class MatchDay
    {
        public MatchDay(int number)
        {
            Number = number;
        }

        public bool IsPlayed
        {
            get
            {
                return Matches.Any(m => m.MatchResult != null);
            }
        }
        public List<Match> Matches { get; set; }
        public ObservableCollection<Match> ObservableMatches
        {
            get
            {
                return new ObservableCollection<Match>(Matches);
            }
        }
        public int Number { get; set; }
        public League League { get; set; }
    }




}
