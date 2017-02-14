using FM.Common;
using FM.Entities.Base;
using FM.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Generator
{
    public class WorldGenerator
    {
        internal static Random rnd = new Random();


        public static string GetRandomValue(List<Occurrence> occurrences, double variant)
        {
            return occurrences.OrderByDescending(x => Util.GetGaussianRandom(x.ScaleValue, variant)).First().Text;
        }

        public static Occurrence GetRandomOccurrence(List<Occurrence> occurrences, double variant)
        {
            return occurrences.OrderByDescending(x => Util.GetGaussianRandom(x.ScaleValue, variant)).First();
        }

        public static int STANDARD_DEV = 5;
        public static Player GenerateRandomPlayer(World w, League l, Nation n, Position p, int lvl, int fromAge, int toAge)
        {
            var nationDice = rnd.NextDouble();
            var otherLeagues = w.Leagues.Where(ol => ol != l).ToList();
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

            var playerEtOcc = GetRandomOccurrence(n.SubEthnies.Concat(new List<Occurrence>() { new Occurrence() { Text = n.MainEthnie.Name, ScaleValue = 10 } }).ToList(), 5);
            var playerEt = w.GetEthnieByName(playerEtOcc.Text);
            string firstName;
            string lastName;

            if (!playerEt.Equals(n.MainEthnie))
            {
                var subEt = (SubEthnieOccurrence)playerEtOcc as SubEthnieOccurrence;
                double divider = subEt.FirstAndLastNamesRate + subEt.FirstNameRate + subEt.LastNameRate;
                var nameCombiDice = rnd.NextDouble();

                if (nameCombiDice <= ((double)subEt.FirstAndLastNamesRate)/divider)
                {
                    firstName = GetRandomValue(playerEt.FirstNames, STANDARD_DEV);
                    lastName = GetRandomValue(playerEt.LastNames, STANDARD_DEV);
                }
                else
                {
                    if (nameCombiDice <= ((double)subEt.FirstAndLastNamesRate + (double)subEt.LastNameRate)/divider)
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
            } else
            {
                firstName = GetRandomValue(n.MainEthnie.FirstNames, STANDARD_DEV);
                lastName = GetRandomValue(n.MainEthnie.LastNames, STANDARD_DEV);
            }

            var age = rnd.Next(fromAge, toAge);
            var baseskill = Math.Min(Player.MAX_BASE_SKILL, Util.GetGaussianRandom((Player.MAX_BASE_SKILL / Player.MAX_BASE_SKILL_GEN_LEVEL) * lvl, 5));
            var charisma = rnd.Next(1, Player.MAX_CHARISMA);
            var baseConst = rnd.Next(15, Player.MAX_CONSTITUTION);
            var consti = baseConst - ((age - Player.MIN_AGE) / 2);
            var xpLevel = 0d;
            for (int i = 0; i < (age- Player.MIN_AGE); i++)
            {
                xpLevel += 1 - Math.Log(i + 1, Player.LEVEL_CAP_LOG_BASE);
            }
            var setplayskill = rnd.Next(1, Player.MAX_SET_PLAY_SKILL);

            Player player = new Player()
            {
                Country = origin,
                FirstName = firstName,
                LastName = lastName,
                Position = p,
                Age = age,
                BaseSkill = (int)Math.Floor(baseskill),
                XPLevel = (int)Math.Floor(xpLevel),
                Fitness = Player.MAX_FITNESS,
                Moral = Player.MAX_MORAL, 
                Constitution = consti,
                Charisma = charisma,
                SetPlaySkill = setplayskill       
            };

            player.XP = (int)Math.Floor(player.LevelCap() * (xpLevel - player.XPLevel));

            return player;

        }


    }
}
