using FM.Common;
using FM.Entities.Base;
using FM.Models.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FM.Generator
{
    public class WorldGenerator
    {
        internal static Random rnd = new Random(DateTime.Now.Millisecond);
        private static ColorConverter converter = new ColorConverter();

        public static string GetRandomValue(List<Occurrence> occurrences, double variant)
        {
            return occurrences.OrderByDescending(x => Util.GetGaussianRandom(x.ScaleValue, variant)).First().Text;
        }

        public static Occurrence GetRandomOccurrence(List<Occurrence> occurrences, double variant)
        {
            return occurrences.OrderByDescending(x => Util.GetGaussianRandom(x.ScaleValue, variant)).First();
        }

        public static int STANDARD_DEV = 5;
        public static int ROOSTER_GOALIE_MIN = 2;
        public static int ROOSTER_GOALIE_MAX = 3;
        public static int ROOSTER_DEFENDER_MIN = 4;
        public static int ROOSTER_DEFENDER_MAX = 6;
        public static int ROOSTER_MIDFIELD_MIN = 4;
        public static int ROOSTER_MIDFIELD_MAX = 6;
        public static int ROOSTER_STRIKER_MIN = 4;
        public static int ROOSTER_STRIKER_MAX = 6;
        public static int MAX_GEN_LEVEL = 7;
        //public static int LEAGUE_SIZE = 12;

        public static int GEN_MIN_AGE = 17;
        public static int GEN_MAX_AGE = 36;

        //public static int MAX_PLAYER_GEN_LEVEL = 10;

        public static int MAX_ELO = 3000;

        public static LeagueAssociation GenerateRandomLeagueAssociation(World w, Association a, AssociationLook al, PlayerLook pl)
        {
            var la = new LeagueAssociation();
            la.Name = a.Name;
            var curLevel = a.Power;
            for (var i = 0; i < a.Depth; i++)
            {
                var l = new League();
                l.Association = la;
                l.Depth = i + 1;
                l.Power = a.Power - i;
                for (var j = 0; j < Game.Instance.LeagueSize; j++)
                {
                    var n = GetRandomOccurrence(a.Nations, STANDARD_DEV).Text;
                    var club = GenerateRandomClub(w, a, al, pl, w.GetNationByName(n), curLevel);
                    l.Clubs.Add(club);
                    club.Leagues.Add(l);
                }
                l.ResetStandings();

                curLevel--;
                la.Leagues.Add(l);
            }

            return la;
        }

        private static List<string> TakenNames = new List<string>();
        public static Club GenerateRandomClub(World w, Association a, AssociationLook al, PlayerLook pl, Nation n, int lvl)
        {
            var c = new Club();
            string name = CreateRandomClubName(n);
            while (TakenNames.Contains(name))
            {
                name = CreateRandomClubName(n);
            }
            c.Name = name;
            c.ClubColors = GenerateRandomClubColors(al);
            c.Crest = GenerateRandomCrest(al);
            c.Dress = GenerateRandomDress(al);
            TakenNames.Add(name);

            c.Coach = GenerateRandomCoach(w, n);
            c.Coach.Club = c;
            
            var trueLvl = lvl - 0.5 + rnd.NextDouble();
            c.StadiumLevel = Math.Max(1, (int)Util.GetGaussianRandom(trueLvl * 1.5, 1));

            var relativeLevel = trueLvl / MAX_GEN_LEVEL;
            c.Elo = (int)Math.Round(relativeLevel * MAX_ELO);
            c.SponsorMoneyCurrentSeason = c.SponsorMoneyPotential;
            c.ViewerAttractionEstimation = c.ViewerAttraction;

            GeneratePlayersForPositionAndClub(w, a, n, lvl, c, pl, Position.Goalie, ROOSTER_GOALIE_MIN, ROOSTER_GOALIE_MAX);
            GeneratePlayersForPositionAndClub(w, a, n, lvl, c, pl, Position.Defender, ROOSTER_DEFENDER_MIN, ROOSTER_DEFENDER_MAX);
            GeneratePlayersForPositionAndClub(w, a, n, lvl, c, pl, Position.Midfielder, ROOSTER_MIDFIELD_MIN, ROOSTER_MIDFIELD_MAX);
            GeneratePlayersForPositionAndClub(w, a, n, lvl, c, pl, Position.Striker, ROOSTER_STRIKER_MIN, ROOSTER_STRIKER_MAX);

            return c;
        }

        public static string GenerateRandomCrest(AssociationLook al)
        {
            return GetRandomOccurrence(al.Crests, STANDARD_DEV).Text;
        }

        public static string GenerateRandomDress(AssociationLook al)
        {
            return GetRandomOccurrence(al.Dresses, STANDARD_DEV).Text;
        }

        public static ClubColors GenerateRandomClubColors(AssociationLook al)
        {
            var colors = GetRandomOccurrence(al.ColorPairs.Select(cp => (Occurrence)cp).ToList(), STANDARD_DEV) as ColorPairOccurrence;
            return new ClubColors()
            {
                MainColor = GetColorFromText(colors.Text),
                SecondColor = GetColorFromText(colors.Text2)
            };
        }

        public static Face GenerateRandomFace(PlayerLook pl)
        {
            var skinColor = GetRandomOccurrence(pl.SkinColors, STANDARD_DEV);
            var hairColor = GetRandomOccurrence(pl.HairColors, STANDARD_DEV);
            var eyeColor = GetRandomOccurrence(pl.EyeColors, STANDARD_DEV);
            var head = GetRandomOccurrence(pl.Heads, STANDARD_DEV);
            var mouth = GetRandomOccurrence(pl.Mouths, STANDARD_DEV);
            var eye = GetRandomOccurrence(pl.Eyes, STANDARD_DEV);

            return new Face()
            {
                SkinColor = GetColorFromText(skinColor.Text),
                HairColor = GetColorFromText(hairColor.Text),
                EyeColor = GetColorFromText(eyeColor.Text),
                Head = head.Text,
                Mouth = mouth.Text,
                Eye = eye.Text
            };
        }

        private static Color GetColorFromText(string name)
        {
            try
            {
                return (Color)converter.ConvertFromString(name);
            }
            catch(Exception e)
            {
                //not existing Color was used in FM Editor
                return Color.Pink;
            }
        }

        private static string CreateRandomClubName(Nation n)
        {
            var city = GetRandomOccurrence(n.Cities, STANDARD_DEV).Text;
            var prefix = GetRandomOccurrence(n.FirstPrefixes, STANDARD_DEV).Text;
            var prefix2 = "";
            var suffix = "";
            var name = "";
            var secondPrefixDice = rnd.NextDouble();
            if (secondPrefixDice <= 0.2)
            {
                prefix2 = GetRandomOccurrence(n.SecondPrefixes, STANDARD_DEV).Text;
                name = prefix + " " + prefix2 + " " + city;
            }
            else if (secondPrefixDice >= 0.8)
            {
                suffix = GetRandomOccurrence(n.Suffixes, STANDARD_DEV).Text;
                name = prefix + " " + city + " " + suffix;
            }
            else
            {
                name = prefix + " " + city;
            }

            return name;
        }

        private static void GeneratePlayersForPositionAndClub(World w, Association a, Nation n, double lvl, Club c, PlayerLook pl, Position p, int min, int max)
        {
            var dice = rnd.Next(min, max);
            for (var i = 0; i < dice; i++)
            {
                var player = GenerateRandomPlayer(w, a, n, pl, p, lvl, GEN_MIN_AGE, GEN_MAX_AGE);
                player.CurrentContract = new Contract() { Club = c, Player = player, RunTime = rnd.Next(1, 3), Salary = player.SalaryStandard };
                c.Rooster.Add(player);
            }
        }

        private static List<IPhilospophie> Philospophies = new List<IPhilospophie> { new DefensivePhilosophie(), new OffensivePhilopshie(), new PossessionPhilosophie(), new BalancedPhilosophie() };
        public static Coach GenerateRandomCoach(World w, Nation n)
        {
            var c = new Coach();
            c.Nation = n;
            GetRandomNameForNation(w, n, out string fn, out string ln);
            c.FirstName = fn;
            c.LastName = ln;

            var philDice = rnd.Next(0, 4);
            c.Philospophie = Philospophies[philDice];

            return c;
        }

        public static Player GenerateRandomPlayer(World w, Association l, Nation n, PlayerLook pl, Position p, double lvl, int fromAge, int toAge)
        {
            var nationDice = rnd.NextDouble();
            var otherLeagues = w.Associations.Where(ol => ol != l).ToList();
            Nation origin;
            if (otherLeagues.Count == 0 || nationDice <= 0.7)
            {

                var key = GetRandomValue(n.SubNations.Concat(new List<Occurrence> { new Occurrence() { Text = n.Name, ScaleValue = 10 } }).ToList(), 5);
                origin = w.GetNationByName(key);
            }
            else
            {

                var leagueDice = rnd.Next(0, otherLeagues.Count - 1);
                var originLeague = otherLeagues[leagueDice];
                var key = GetRandomValue(originLeague.Nations, STANDARD_DEV);
                origin = w.GetNationByName(key);
            }


            string firstName;
            string lastName;
            GetRandomNameForNation(w, n, out firstName, out lastName);

            var age = rnd.Next(fromAge, toAge);
            var baseskill = Math.Min(Player.MAX_BASE_SKILL, Util.GetGaussianRandom((Player.MAX_BASE_SKILL / MAX_GEN_LEVEL) * lvl, 5));
            var charisma = Math.Round(Math.Min(Util.GetGaussianRandom(15, 3), 20));
            var baseConst = (float)Math.Round(Math.Max(10, Math.Min(20, Util.GetGaussianRandom(18 - MAX_GEN_LEVEL + lvl, 2))), 0);


            var xpLevel = 0d;
            var consti = (float)baseConst;
            for (int i = Player.MIN_AGE; i <= age; i++)
            {
                xpLevel += 2 * Math.Max(0, 30d - i) / (30d - Player.MIN_AGE);
                if (i >= 31)
                {
                    consti -= consti * 0.01f * (i - 30);
                }
            }
            var setplayskill = 100f;

            while (setplayskill > 20)
            {
                setplayskill = (float)Math.Round(Math.Max(1, Util.GetGaussianRandom(18 - MAX_GEN_LEVEL + lvl, 5)), 0);
            }

            Player player = new Player()
            {
                Nation = origin,
                FirstName = firstName,
                LastName = lastName,
                Position = p,
                Age = age,
                SkillBase = (int)Math.Floor(baseskill),
                XPLevel = (int)Math.Floor(xpLevel),
                Fitness = Player.MAX_FITNESS,
                Moral = Player.MAX_MORAL,
                ConstitutionBase = (float)baseConst,
                Constitution = consti,
                Charisma = (float)charisma,
                SetPlaySkill = setplayskill,
                Face = GenerateRandomFace(pl)
            };

            //player.XP = (int)Math.Floor(player.LevelCap() * (xpLevel - player.XPLevel));

            return player;

        }

        private static void GetRandomNameForNation(World w, Nation n, out string firstName, out string lastName)
        {
            var playerEtOcc = GetRandomOccurrence(n.SubEthnies.Concat(new List<Occurrence>() { new Occurrence() { Text = n.MainEthnie.Name, ScaleValue = 10 } }).ToList(), 5);
            var playerEt = w.GetEthnieByName(playerEtOcc.Text);
            if (!playerEt.Equals(n.MainEthnie))
            {
                var subEt = (SubEthnieOccurrence)playerEtOcc as SubEthnieOccurrence;
                double divider = subEt.FirstAndLastNamesRate + subEt.FirstNameRate + subEt.LastNameRate;
                var nameCombiDice = rnd.NextDouble();

                if (nameCombiDice <= ((double)subEt.FirstAndLastNamesRate) / divider)
                {
                    firstName = GetRandomValue(playerEt.FirstNames, STANDARD_DEV);
                    lastName = GetRandomValue(playerEt.LastNames, STANDARD_DEV);
                }
                else
                {
                    if (nameCombiDice <= ((double)subEt.FirstAndLastNamesRate + (double)subEt.LastNameRate) / divider)
                    {
                        firstName = GetRandomValue(n.MainEthnie.FirstNames, STANDARD_DEV);
                        lastName = GetRandomValue(playerEt.LastNames, 5);
                    }
                    else
                    {
                        firstName = GetRandomValue(playerEt.FirstNames, STANDARD_DEV);
                        lastName = GetRandomValue(n.MainEthnie.LastNames, STANDARD_DEV);
                    }
                }
            }
            else
            {
                firstName = GetRandomValue(n.MainEthnie.FirstNames, STANDARD_DEV);
                lastName = GetRandomValue(n.MainEthnie.LastNames, STANDARD_DEV);
            }
        }
    }


}
