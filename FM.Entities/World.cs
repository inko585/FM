using FM.Entities.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FM.Entities.Base
{
    public class World
    {

        public static World ReadWorld(string path)
        {
            World world;
            var xs = new XmlSerializer(typeof(World));
            using (var sr = new StreamReader(path))
            {
                world = (World)xs.Deserialize(sr);


            }
            return world;
        }

        public World()
        {
            Nations = new List<Nation>();
            Ethnies = new List<Ethnie>();
        }
        public List<Nation> Nations { get; set; }
        public List<Ethnie> Ethnies { get; set; }

        public List<League> Leagues { get; set; }

        public Nation GetNationByName(string name)
        {
            return Nations.FirstOrDefault(n => n.Name.Equals(name));
        }


        public Ethnie GetEthnieByName(string name)
        {
            return Ethnies.FirstOrDefault(e => e.Name.Equals(name));
        }

        public static int MAX_PLAYER_GEN_LEVEL = 10;

    }

    public class League
    {
        public string Name { get; set; }
        public List<Occurrence> Nations { get; set; }
    }

    public class Nation
    {


        public string Name { get; set; }
        public int LeagueLevel { get; set; }



        public List<Occurrence> Cities { get; set; }

        public List<Occurrence> FirstPrefixes { get; set; }

        public List<Occurrence> SecondPrefixes { get; set; }

        public List<Occurrence> Suffixes { get; set; }

        public List<Occurrence> Sponsors { get; set; }

        public List<Occurrence> SubNations { get; set; }

        public Ethnie MainEthnie { get; set; }
        public List<SubEthnieOccurrence> SubEthnies { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    public class Ethnie
    {
        public string Name { get; set; }

        public List<Occurrence> FirstNames { get; set; }

        public List<Occurrence> LastNames { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ethnie))
            {
                return false;
            }
            return Name.Equals((obj as Ethnie).Name);
        }
    }


    public class Occurrence
    {
        public string Text { get; set; }

        public int ScaleValue { get; set; }
    }

    public class SubEthnieOccurrence : Occurrence
    {

        public int FirstAndLastNamesRate { get; set; }
        public int FirstNameRate { get; set; }
        public int LastNameRate { get; set; }
    }




}

namespace FM.Entities.Generic
{


    public class Club
    {
        public String Name { get; set; }
        public LineUp StartingLineUp { get; set; }
    }



    public class LineUp
    {
        public LineUp(List<Player> players, Player centralPlayer, Player sweeper, Tactic tactic, Tackling tackling, Frequency longshots)
        {
            this.Players = players;
            this.Tackling = tackling;
            this.Tactic = tactic;
            this.CentralPlayer = centralPlayer;
            this.Sweeper = sweeper;
            this.LongShots = longshots;
        }

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

    public class Player
    {
        public static int MIN_STARTING_CONSITUTION = 15;
        public static int MIN_AGE = 17;
        public static int XP_FOR_FIRST_LEVEL = 150;
        public static int MAX_BASE_SKILL_GEN_LEVEL = 10;
        public static int MAX_FITNESS = 20;
        public static int MAX_MORAL = 20;
        public static int MAX_BASE_SKILL = 80;
        public static int MAX_XPLEVEL = 20;
        public static int MAX_CONSTITUTION = 20;
        public static int MAX_CHARISMA = 20;
        public static int MAX_SET_PLAY_SKILL = 20;
        public static float MAX_RATING = 10f;
        public static float INIT_RATING = 5f;
        public static int MAX_SKILL = MAX_BASE_SKILL + MAX_XPLEVEL;
        public static int LEVEL_CAP_LOG_BASE = 30;


        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public float BaseSkill { get; set; }
        public float XPLevel { get; set; }
        public int XP { get; set; }

        public float Fitness { get; set; }
        public float Constitution { get; set; }
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

        public float Moral { get; set; }
        public float SetPlaySkill { get; set; }
        public Nation Country { get; set; }
        public Position Position { get; set; }


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

        public float Attk
        {
            get
            {

                return Position == Position.Goalie ? 0f : CurrentSkill * ((Position == Position.Defender) ? 0.8f : (Position == Position.Striker) ? 1.2f : 1f);

            }
        }

        public float Def
        {

            get
            {

                return Position == Position.Goalie ? 0f : CurrentSkill * ((Position == Position.Defender) ? 1.2f : (Position == Position.Striker) ? 0.8f : 1f);
            }

        }


        public float CurrentSkill
        {
            get
            {
                return (MaxSkill / MAX_FITNESS * Fitness) / MAX_MORAL * Moral; ;
            }
        }

        public float MaxSkill
        {
            get
            {
                return XPLevel + BaseSkill;
            }
        }

        public void DecayFitness(float level)
        {
            Fitness = Fitness - level / Constitution;
        }

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
