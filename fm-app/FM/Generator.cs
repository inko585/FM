using FM.Common;
using FM.Entities.Base;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FM.Common.Pixels;
using FM.Common.Season;

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
        public static int MAX_GEN_LEVEL = 9;
        //public static int LEAGUE_SIZE = 12;

        public static int GEN_MIN_AGE = 17;
        public static int GEN_MAX_AGE = 36;

        //public static int MAX_PLAYER_GEN_LEVEL = 10;

        public static int MAX_ELO = 3000;

        public static Sponsor GetNewSponsorForClub(Club c)
        {
            var potentialSponsors = new List<Occurrence>();
            potentialSponsors.AddRange(c.Nation.Sponsors.Where(s => s.Size == 2));
            if (c.SponsorMoneyPotential > 10000000)
            {
                potentialSponsors.AddRange(c.Nation.Sponsors.Where(s => s.Size == 3));
            }
            else
            {
                potentialSponsors.AddRange(c.Nation.Sponsors.Where(s => s.Size == 1));
            }

            var sponsorOcc = GetRandomOccurrence(potentialSponsors, STANDARD_DEV) as SponsorOccurrence;
            return new Sponsor()
            {
                Name = sponsorOcc.Text,
                InvestRate = Util.GetGaussianRandom(1d, 0.1),
                YearsInClub = 0
            };

        }

        public static LeagueAssociation GenerateRandomLeagueAssociation(World w, Association a, AssociationLook al, Club playerClub = null, int playerDepth = -1)
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
                var leagueSize = Game.Instance.LeagueSize;
                if (playerClub != null && playerDepth == i + 1)
                {
                    playerClub.Nation = w.GetNationByName(a.Nations.First().Text);
                    leagueSize--;
                    GenerateRandomPlayersAndCoachForClub(w, playerClub.Nation, curLevel, playerClub);
                    l.Clubs.Add(playerClub);
                    playerClub.Leagues.Add(l);

                }
                for (var j = 0; j < leagueSize; j++)
                {
                    var n = GetRandomOccurrence(a.Nations, STANDARD_DEV).Text;
                    var club = GenerateRandomClub(w, a, al, w.GetNationByName(n), curLevel);
                    l.Clubs.Add(club);
                    club.Leagues.Add(l);
                }
                l.ResetStandings();

                curLevel--;
                la.Leagues.Add(l);
            }

            return la;
        }

        public static List<string> TakenNames = new List<string>();
        public static Club GenerateRandomClub(World w, Association a, AssociationLook al, Nation n, double lvl)
        {
            var c = new Club();
            string name = CreateRandomClubName(n, n.CombineSuffixAndPrefix);
            while (TakenNames.Contains(name))
            {
                name = CreateRandomClubName(n, n.CombineSuffixAndPrefix);
            }
            c.Name = name;
            c.Nation = n;
            if (name.Contains("Rot/Weiß") || name.Contains("Rouge") || name.Contains("Ferrari") || name.Contains("Red Star") || name.Contains("Red Sox"))
            {
                c.ClubColors = new ClubColors()
                {
                    MainColor = GetColorFromText("Red"),
                    SecondColor = GetColorFromText("White"),
                    MainColorString = "Red",
                    SecondColorString = "White"
                };
            }
            else if (name.Contains("Blau/Weiß"))
            {
                c.ClubColors = new ClubColors()
                {
                    MainColor = GetColorFromText("RoyalBlue"),
                    SecondColor = GetColorFromText("White"),
                    MainColorString = "RoyalBlue",
                    SecondColorString = "White"
                };
            }
            else if (name.Contains("Schwarz/Weiß"))
            {
                c.ClubColors = new ClubColors()
                {
                    MainColor = GetColorFromText("#2d2e2e"),
                    SecondColor = GetColorFromText("White"),
                    MainColorString = "#2d2e2e",
                    SecondColorString = "White"
                };
            }
            else if (name.Contains("SAP") || name.Contains("Sanofi"))
            {
                c.ClubColors = new ClubColors()
                {
                    MainColor = GetColorFromText("SteelBlue"),
                    SecondColor = GetColorFromText("White"),
                    MainColorString = "SteelBlue",
                    SecondColorString = "White"
                };
            }
            else if (name.Contains("Total") || name.Contains("Red Bull") || name.Contains("Tesco"))
            {
                c.ClubColors = new ClubColors()
                {
                    MainColor = GetColorFromText("Red"),
                    SecondColor = GetColorFromText("SteelBlue"),
                    MainColorString = "Red",
                    SecondColorString = "SteelBlue"
                };
            }
            else
            {
                c.ClubColors = GenerateRandomClubColors(al);
            }

            c.SecondClubColors = GenerateRandomClubColors(al, c.ClubColors.MainColorString, obligatoryColorSecond: c.ClubColors.SecondColorString);

            c.Crest = GenerateRandomCrest(al);
            c.Dress = GenerateRandomDress(al);
            TakenNames.Add(name);
            GenerateRandomPlayersAndCoachForClub(w, n, lvl, c);

            return c;
        }

        private static void GenerateRandomPlayersAndCoachForClub(World w, Nation n, double lvl, Club c)
        {
            c.Coach = GenerateRandomCoach(w, n);
            c.Coach.Club = c;

            var trueLvl = Util.GetGaussianRandom(lvl, 0.4);
            c.ClubAssetLevel[ClubAsset.Stadium] = Math.Max(1, (int)Util.GetGaussianRandom(trueLvl * 1.5, 1));
            c.ClubAssetLevel[ClubAsset.Office] = Math.Max(1, (int)Util.GetGaussianRandom(trueLvl * 1.5, 1));
            c.ClubAssetLevel[ClubAsset.YouthWork] = Math.Max(1, Math.Max(1, (int)Util.GetGaussianRandom(trueLvl * 1.5, 1)));
            c.ClubAssetLevel[ClubAsset.TrainingGrounds] = Math.Max(1, (int)Util.GetGaussianRandom(trueLvl * 1.5, 1));

            var relativeLevel = trueLvl / MAX_GEN_LEVEL;
            c.Elo = (int)Math.Round(relativeLevel * MAX_ELO);
            c.LookForSponsor();
            c.SponsorMoneyCurrentSeason = Util.GetNiceValue((int)(c.SponsorMoneyPotential * c.CurrentSponsor.ActualSponsoringRate));
            c.ViewerAttractionEstimation = c.ViewerAttraction;

            var posN = c.GetLineUpCountForPosition();

            GeneratePlayersForPositionAndClub(w, n, trueLvl, c, Position.Goalie, (int)Math.Ceiling(posN[Position.Goalie] * 2.5d));
            GeneratePlayersForPositionAndClub(w, n, trueLvl, c, Position.Defender, (int)Math.Ceiling(posN[Position.Defender] * 2.5d));
            GeneratePlayersForPositionAndClub(w, n, trueLvl, c, Position.Midfielder, (int)Math.Ceiling(posN[Position.Midfielder] * 2.5d));
            GeneratePlayersForPositionAndClub(w, n, trueLvl, c, Position.Striker, (int)Math.Ceiling(posN[Position.Striker] * 2.5d));
        }

        public static string GenerateRandomCrest(AssociationLook al)
        {
            return GetRandomOccurrence(al.Crests, STANDARD_DEV).Text;
        }

        public static string GenerateRandomDress(AssociationLook al)
        {
            return GetRandomOccurrence(al.Dresses, STANDARD_DEV).Text;
        }

        public static ClubColors GenerateRandomClubColors(AssociationLook al, string excludedColor = null, string obligatoryColorMain = null, string obligatoryColorSecond = null)
        {
            var validColorPairs = al.ColorPairs.ToList();
            if (excludedColor != null)
            {
                var excludedColors = new List<string>() { excludedColor };

                var blueColors = new List<string>() { "RoyalBlue", "SteelBlue", "SkyBlue" };
                var redColors = new List<string>() { "Red", "FireBrick" };

                if (blueColors.Contains(excludedColor))
                {
                    excludedColors.AddRange(blueColors);
                }
                if (redColors.Contains(excludedColor))
                {
                    excludedColors.AddRange(redColors);
                }

                validColorPairs = validColorPairs.Where(cp => !excludedColors.Contains(cp.Text) && !excludedColors.Contains(cp.Text2)).ToList();

            }

            if (obligatoryColorMain != null && validColorPairs.Any(cp => cp.Text == obligatoryColorMain))
            {
                validColorPairs = validColorPairs.Where(cp => cp.Text == obligatoryColorMain).ToList();
            }

            if (obligatoryColorSecond != null && validColorPairs.Any(cp => cp.Text2 == obligatoryColorSecond))
            {
                validColorPairs = validColorPairs.Where(cp => cp.Text2 == obligatoryColorSecond).ToList();
            }

            var colors = GetRandomOccurrence(validColorPairs.Select(cp => (Occurrence)cp).ToList(), STANDARD_DEV) as ColorPairOccurrence;
            return new ClubColors()
            {
                MainColor = GetColorFromText(colors.Text),
                SecondColor = GetColorFromText(colors.Text2),
                MainColorString = colors.Text,
                SecondColorString = colors.Text2
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
                SkinColorString = skinColor.Text,
                EyeColorString = eyeColor.Text,
                HairColorString = hairColor.Text,
                Head = head.Text,
                Mouth = mouth.Text,
                Eye = eye.Text
            };
        }

        public static Color GetColorFromText(string name)
        {
            try
            {
                return (Color)converter.ConvertFromString(name);
            }
            catch (Exception e)
            {
                //not existing Color was used in FM Editor
                return Color.Pink;
            }
        }

        private static List<string> suffixCache = new List<string>();
        private static List<string> prefixCache = new List<string>();

        static List<string> ExcludedCities = new List<string>();
        static List<string> TakenPrefixCity = new List<string>();
        private static string CreateRandomClubName(Nation n, bool combineSuffixAndPrefix)
        {

            var cityOcc = GetRandomOccurrence(n.Cities, STANDARD_DEV);
            while (ExcludedCities.Contains(cityOcc.Text))
            {
                cityOcc = GetRandomOccurrence(n.Cities, STANDARD_DEV);
            }

            if (cityOcc.ScaleValue <= 3)
            {
                ExcludedCities.Add(cityOcc.Text);
            }
            var city = cityOcc.Text;
            cityOcc.ScaleValue = Math.Max(4, cityOcc.ScaleValue - 2);
            //TakenCities.Add(city);
            var prefix = "";
            var prefix2 = "";
            var suffix = "";
            var name = "";
            if (combineSuffixAndPrefix)
            {

                prefix = GetRandomOccurrence(n.FirstPrefixes, STANDARD_DEV).Text;

                var secondPrefixDice = rnd.NextDouble();
                if (secondPrefixDice <= 0.2 && n.SecondPrefixes.Any())
                {
                    prefix2 = GetPrefix2(n);
                    name = prefix + " " + prefix2 + " " + city;
                }
                else if (secondPrefixDice >= 0.8 && n.Suffixes.Any())
                {
                    suffix = GetSuffix(n);
                    name = prefix + " " + city + " " + suffix;
                }
                else
                {
                    name = prefix + " " + city;
                }

            }
            else
            {
                var prefixDice = rnd.NextDouble();
                if (prefixDice > 0.3)
                {
                    prefix = GetRandomOccurrence(n.FirstPrefixes, STANDARD_DEV).Text;
                    if (prefixDice > 0.8 && n.SecondPrefixes.Any())
                    {
                        prefix2 = " " + GetPrefix2(n);

                    }

                    name = prefix + prefix2 + " " + city;
                }
                else
                {
                    if (n.Suffixes.Any())
                    {
                        suffix = " " + GetSuffix(n);
                    }
                    name = prefix + " " + city + suffix;
                }
            }

            return name;
        }

        private static string GetPrefix2(Nation n)
        {
            string prefix2;
            var prefix2Occ = GetRandomOccurrence(n.SecondPrefixes, STANDARD_DEV);
            while (prefixCache.Contains(n.Short + "_" + prefix2Occ.Text) && prefixCache.Where(c => c.StartsWith(n.Short + "_")).Count() < n.SecondPrefixes.Count)
            {
                prefix2Occ = GetRandomOccurrence(n.SecondPrefixes, STANDARD_DEV);
            }

            if (prefix2Occ.ScaleValue == 1)
            {
                prefixCache.Add(n.Short + "_" + prefix2Occ.Text);
            }

            prefix2 = prefix2Occ.Text;
            return prefix2;
        }

        private static string GetSuffix(Nation n)
        {
            string suffix;
            var suffixOcc = GetRandomOccurrence(n.Suffixes, STANDARD_DEV);
            while (suffixCache.Contains(n.Short + "_" + suffixOcc.Text) && suffixCache.Where(c => c.StartsWith(n.Short + "_")).Count() < n.Suffixes.Count)
            {
                suffixOcc = GetRandomOccurrence(n.Suffixes, STANDARD_DEV);
            }
            if (suffixOcc.ScaleValue == 1)
            {
                suffixCache.Add(n.Short + "_" + suffixOcc.Text);
            }
            suffix = suffixOcc.Text;
            return suffix;
        }

        private static void GeneratePlayersForPositionAndClub(World w, Nation n, double lvl, Club c, Position p, int number)
        {
            for (var i = 0; i < number; i++)
            {
                var player = GenerateRandomPlayer(w, n, p, lvl, GEN_MIN_AGE, GEN_MAX_AGE);
                player.DressNumber = c.GetFreeNumber(p);
                player.ContractCurrent = new Contract() { Club = c, Player = player, RunTime = rnd.Next(1, 4), Salary = player.SalaryStandard };
                player.ClubHistory.Add(c);
                c.Rooster.Add(player);
            }
        }

        public static List<IPhilospophie> Philospophies = new List<IPhilospophie> { new DefensivePhilosophie(), new OffensivePhilopshie(), new PossessionPhilosophie(), new BalancedPhilosophie() };
        public static Coach GenerateRandomCoach(World w, Nation n)
        {
            var c = new Coach();
            c.Nation = n;
            var etOcc = GetRandomOccurrence(n.SubEthnies.Concat(new List<Occurrence>() { new Occurrence() { Text = n.MainEthnie, ScaleValue = 10 } }).ToList(), 5);
            GetRandomName(w, n, etOcc, out string fn, out string ln);
            c.FirstName = fn;
            c.LastName = ln;

            var philDice = rnd.Next(0, 4);
            c.Philospophie = Philospophies[philDice];

            return c;
        }

        public static Player GenerateRandomPlayer(World w, Nation n, Position p, double lvl, int fromAge, int toAge)
        {
            var nationDice = rnd.NextDouble();
            //var otherLeagues = w.Associations.Where(ol => ol != l).ToList();
            Nation origin;
            //if (otherLeagues.Count == 0 || nationDice <= 0.7)
            //{
            if (nationDice <= 0.3)
            {
                var key = GetRandomValue(n.SubNations, 5);
                origin = w.GetNationByName(key);
            }
            else
            {
                origin = n;
            }
            //}
            //else
            //{

            //    var leagueDice = rnd.Next(0, otherLeagues.Count - 1);
            //    var originLeague = otherLeagues[leagueDice];
            //    var key = GetRandomValue(originLeague.Nations, STANDARD_DEV);
            //    origin = w.GetNationByName(key);
            //}


            string firstName;
            string lastName;
            var playerEtOcc = GetRandomOccurrence(origin.SubEthnies.Concat(new List<Occurrence>() { new Occurrence() { Text = origin.MainEthnie, ScaleValue = 12 } }).ToList(), 5);
            var eth = w.GetEthnieByName(playerEtOcc.Text);
            GetRandomName(w, origin, playerEtOcc, out firstName, out lastName);

            var pl = GetRandomOccurrence(eth.Appearences, STANDARD_DEV);
            var age = rnd.Next(fromAge, toAge);
            var baseskill = Math.Min(Player.MAX_BASE_SKILL, Util.GetGaussianRandom((Player.MAX_BASE_SKILL / MAX_GEN_LEVEL) * lvl, 5));
            var charisma = Math.Round(Math.Min(Util.GetGaussianRandom(15, 3), 20));
            var baseConst = (float)Math.Round(Math.Max(10, Math.Min(20, Util.GetGaussianRandom(18 - MAX_GEN_LEVEL + lvl, 2))), 0);

            var talent = Math.Min(2.2, Math.Max(1.1, Util.GetGaussianRandom(1.6, 0.1)));
            var xpLevel = 0;
            var consti = (float)baseConst;
            var xpGains = new int[] { 2, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0 };
            for (int i = Player.MIN_AGE; i <= age; i++)
            {
                if (i < 30 && i > 17)
                {
                    xpLevel += xpGains[i - (Player.MIN_AGE + 1)];
                }
                //xpLevel += 2 * Math.Max(0, 30d - i) / (30d - Player.MIN_AGE);
                if (i >= 31)
                {
                    consti -= consti * 0.03f * (i - 30);
                }
            }

            var xpLevel_adj = (int)Math.Round(xpLevel * Math.Pow((3 - talent), 2));
            var xp = (int)(Math.Pow(xpLevel_adj, talent) * 100);

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
                XPLevel = xpLevel_adj,
                Fitness = Player.MAX_FITNESS,
                Moral = Player.MAX_MORAL,
                ConstitutionBase = (float)baseConst,
                Constitution = consti,
                Charisma = (float)charisma,
                SetPlaySkill = setplayskill,
                XP = xp,
                TalentFactor = talent,
                Face = GenerateRandomFace(w.GetPlayerLookByName(pl.Text))
            };

            //player.XP = (int)Math.Floor(player.LevelCap() * (xpLevel - player.XPLevel));

            return player;

        }

        private static void GetRandomName(World w, Nation n, Occurrence etOcc, out string firstName, out string lastName)
        {

            var playerEt = w.GetEthnieByName(etOcc.Text);
            if (!playerEt.Equals(w.GetEthnieByName(n.MainEthnie)))
            {
                var subEt = etOcc as SubEthnieOccurrence;
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
                        firstName = GetRandomValue(w.GetEthnieByName(n.MainEthnie).FirstNames, STANDARD_DEV);
                        lastName = GetRandomValue(playerEt.LastNames, 5);
                    }
                    else
                    {
                        firstName = GetRandomValue(playerEt.FirstNames, STANDARD_DEV);
                        lastName = GetRandomValue(w.GetEthnieByName(n.MainEthnie).LastNames, STANDARD_DEV);
                    }
                }
            }
            else
            {
                firstName = GetRandomValue(w.GetEthnieByName(n.MainEthnie).FirstNames, STANDARD_DEV);
                lastName = GetRandomValue(w.GetEthnieByName(n.MainEthnie).LastNames, STANDARD_DEV);
            }
        }
    }


}
