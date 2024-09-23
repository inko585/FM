using FM.Common;
using FM.Common.Generic;
using FM.Common.Pixels;
using FM.Entities.Base;
using FM.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FM
{
    public class PredefinedClubs
    {
        public const string COLOR_RED = "Red";
        public const string COLOR_WHITE = "White";
        public const string COLOR_GREEN = "Green";
        public const string COLOR_BLACK = "#2d2e2e";
        public const string COLOR_LIGHT_GREEN = "YellowGreen";
        public const string COLOR_GOLD = "Goldenrod";
        public const string COLOR_PURPLE = "Purple";
        public const string COLOR_BLUE = "RoyalBlue";
        public const string COLOR_STEEL_BLUE = "SteelBlue";
        public const string COLOR_BROWN = "Sienna";
        public const string COLOR_YELLOW = "Gold";
        public const string COLOR_SKY_BLUE = "SkyBlue";
        public const string COLOR_FIRE_BRICK = "FireBrick";
        public const string COLOR_GRAY = "LightGray";
        public const string COLOR_ORANGE = "DarkOrange";
        public const string COLOR_PINK = "Pink";

        public const string DRESS_BIG_STRIPES = "BigStripes";
        public const string DRESS_BIG_STRIPES_2 = "BigStripes2";
        public const string DRESS_CUT = "Cut";
        public const string DRESS_CUT_2 = "Cut2";
        public const string DRESS_HILLS = "Hill";
        public const string DRESS_HILLS_2 = "Hill2";
        public const string DRESS_LINES = "Lines";
        public const string DRESS_LINES_2 = "Lines2";
        public const string DRESS_SHOULDER = "Shoulder";
        public const string DRESS_SHOULDER_2 = "Shoulder2";
        public const string DRESS_SIMPLE = "Simple";
        public const string DRESS_SIMPLE_2 = "Simple2";
        public const string DRESS_SLEEVE = "Sleeve";
        public const string DRESS_SLEEVE_2 = "Sleeve2";
        public const string DRESS_STRIPES = "Stripes";
        public const string DRESS_STRIPES_2 = "Stripes2";
        public const string DRESS_SLASH = "Slash3";
        public const string DRESS_SLASH_2 = "Slash4";
        public const string DRESS_SINGLE_LINE = "SingleLine";
        public const string DRESS_SINGLE_LINE_2 = "SingleLine2";

        public const string CREST_SIMPLE = "Crest";
        public const string CREST_CARO = "CrestCaro";
        public const string CREST_SLASH = "CrestSlash";
        public const string CREST_CROSS = "CrestCross2";
        public const string CREST_DIAMOND = "Diamond";
        public const string CREST_DIAMOND_CROSS = "DiamondCross";
        public const string CREST_SHIELD = "Shield";
        public const string CREST_SHIELD_STRIPE = "ShieldStripe";
        public const string CREST_SHIELD_SLASH = "ShieldSlash";
        public const string CREST_FANCY = "Fancy";
        public const string CREST_FANCY_CROSS = "FancyCross";
        public const string CREST_FANCY_SLASH = "FancySlash";
        public const string CREST_FANCY_SLASH_2 = "FancySlash2";

        public static LeagueAssociation GenerateGermanLeagueAssociation(World w, Association a)
        {
            var la = new LeagueAssociation();
            la.Name = a.Name;


            var germanTeams = GetGermanClubs(w);
            5.Times((i) =>
            {
                var l = new League();
                l.Association = la;
                l.Depth = i + 1;
                l.Power = a.Power - i;
                var startIndex = i * 12;
                12.Times((j) =>
                {
                    var c = germanTeams.Skip(startIndex).ElementAt(j);
                    l.Clubs.Add(c);
                    c.Leagues.Add(l);
                });
                la.Leagues.Add(l);
            });

            return la;
        }

        public static List<Club> GetGermanClubs(World w)
        {
            var n = w.GetNationByName("Deutschland");
            return new List<Club>
            {
                BuildClub(w, n, "FC Bayern München", COLOR_RED, COLOR_STEEL_BLUE, COLOR_GOLD, COLOR_GREEN, DRESS_STRIPES, CREST_CROSS, 8.7),
                BuildClub(w, n, "Borussia Dortmund", COLOR_YELLOW, COLOR_BLACK, COLOR_BLACK, COLOR_YELLOW, DRESS_SINGLE_LINE, CREST_FANCY_SLASH_2, 8.4),
                BuildClub(w, n, "Bayer 04 Leverkusen", COLOR_RED, COLOR_BLACK, COLOR_WHITE, COLOR_BLACK, DRESS_STRIPES, CREST_CARO, 8.3),
                BuildClub(w, n, "RasenBallsport Leipzig", COLOR_WHITE, COLOR_RED, COLOR_STEEL_BLUE, COLOR_WHITE, DRESS_SHOULDER, CREST_CROSS, 8.2),
                BuildClub(w, n, "Eintracht Frankfurt", COLOR_RED, COLOR_BLACK, COLOR_YELLOW, COLOR_BLUE, DRESS_HILLS, CREST_SHIELD_SLASH, 8.0),
                BuildClub(w, n, "VfB Stuttgart", COLOR_WHITE, COLOR_RED, COLOR_BLACK, COLOR_RED, DRESS_SINGLE_LINE, CREST_SIMPLE, 7.9),
                BuildClub(w, n, "TSG Hoffenheim", COLOR_STEEL_BLUE, COLOR_WHITE, COLOR_WHITE, COLOR_STEEL_BLUE, DRESS_SIMPLE_2, CREST_FANCY_SLASH, 7.9),
                BuildClub(w, n, "SV Werder Bremen", COLOR_GREEN, COLOR_WHITE, COLOR_ORANGE, COLOR_GREEN, DRESS_SHOULDER, CREST_DIAMOND, 7.7),
                BuildClub(w, n, "SC Freiburg", COLOR_WHITE, COLOR_BLACK, COLOR_RED, COLOR_BLACK, DRESS_SIMPLE, CREST_SLASH, 7.7),
                BuildClub(w, n, "Borussia Mönchengladbach", COLOR_WHITE, COLOR_BLACK, COLOR_GREEN, COLOR_BLACK, DRESS_BIG_STRIPES_2, CREST_DIAMOND_CROSS, 7.6),
                BuildClub(w, n, "VFL Wolfsburg", COLOR_LIGHT_GREEN, COLOR_WHITE, COLOR_BLACK, COLOR_LIGHT_GREEN, DRESS_SIMPLE_2, CREST_FANCY_SLASH_2 , 7.6),
                BuildClub(w, n, "SV Augsburg", COLOR_WHITE, COLOR_GREEN, COLOR_GREEN, COLOR_RED, DRESS_SLASH_2, CREST_SLASH, 7.5),

                BuildClub(w, n, "1. FSV Mainz 05", COLOR_RED, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_HILLS, CREST_FANCY_CROSS, 7.5),
                BuildClub(w, n, "FC Heidenheim", COLOR_RED, COLOR_STEEL_BLUE, COLOR_STEEL_BLUE, COLOR_WHITE, DRESS_SHOULDER, CREST_CROSS, 7.3),
                BuildClub(w, n, "1. FC Union Berlin", COLOR_RED, COLOR_WHITE, COLOR_WHITE, COLOR_RED, DRESS_SIMPLE_2, CREST_DIAMOND_CROSS, 7.4),
                BuildClub(w, n, "VFL Bochum", COLOR_BLUE, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_CUT_2, CREST_SLASH, 7.2),
                BuildClub(w, n, "1. FC Köln", COLOR_WHITE, COLOR_RED, COLOR_BLACK, COLOR_RED, DRESS_CUT_2, CREST_SIMPLE, 7.2),
                BuildClub(w, n, "SV Darmstadt 98", COLOR_STEEL_BLUE, COLOR_WHITE, COLOR_GREEN, COLOR_WHITE, DRESS_SLEEVE, CREST_FANCY_SLASH_2, 7.0),
                BuildClub(w, n, "FC St. Pauli", COLOR_BROWN, COLOR_WHITE, COLOR_RED, COLOR_BLACK, DRESS_LINES, CREST_FANCY_SLASH_2, 6.9),
                BuildClub(w, n, "Holstein Kiel", COLOR_BLUE, COLOR_WHITE, COLOR_RED, COLOR_WHITE, DRESS_HILLS, CREST_DIAMOND, 6.7),
                BuildClub(w, n, "Fortuna Düsseldorf", COLOR_RED, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_LINES, CREST_SHIELD_STRIPE, 6.6),
                BuildClub(w, n, "Hamburger SV", COLOR_WHITE, COLOR_RED, COLOR_BLUE, COLOR_WHITE, DRESS_SIMPLE, CREST_DIAMOND, 6.9),
                BuildClub(w, n, "Karlsruher SC", COLOR_BLUE, COLOR_WHITE, COLOR_RED, COLOR_YELLOW, DRESS_BIG_STRIPES, CREST_SHIELD_STRIPE , 6.5),
                BuildClub(w, n, "Hannover 96", COLOR_GREEN, COLOR_WHITE, COLOR_RED, COLOR_WHITE, DRESS_SIMPLE, CREST_FANCY_SLASH_2, 6.5),

                BuildClub(w, n, "SC Paderborn 07", COLOR_BLUE, COLOR_BLACK, COLOR_YELLOW, COLOR_RED, DRESS_STRIPES, CREST_SIMPLE, 6.3),
                BuildClub(w, n, "Spvgg Greuther Fürth", COLOR_GREEN, COLOR_WHITE, COLOR_PURPLE, COLOR_WHITE, DRESS_SHOULDER, CREST_FANCY_SLASH, 6.4),
                BuildClub(w, n, "Hertha BSC", COLOR_BLUE, COLOR_WHITE, COLOR_RED, COLOR_BLACK, DRESS_LINES, CREST_FANCY_CROSS, 6.4),
                BuildClub(w, n, "FC Schalke 04", COLOR_BLUE, COLOR_WHITE, COLOR_WHITE, COLOR_SKY_BLUE, DRESS_SHOULDER, CREST_SHIELD_STRIPE, 6.7),
                BuildClub(w, n, "SC Elversberg", COLOR_WHITE, COLOR_BLACK, COLOR_BLACK, COLOR_WHITE, DRESS_SIMPLE_2, CREST_CARO, 6.2),
                BuildClub(w, n, "1. FC Nürnberg", COLOR_FIRE_BRICK, COLOR_WHITE, COLOR_WHITE, COLOR_FIRE_BRICK, DRESS_SIMPLE, CREST_FANCY_SLASH_2, 6.2),
                BuildClub(w, n, "1. FC Kaiserslautern", COLOR_RED, COLOR_WHITE, COLOR_WHITE, COLOR_RED, DRESS_SLASH_2, CREST_SIMPLE, 6.2),
                BuildClub(w, n, "1. FC Magdeburg", COLOR_WHITE, COLOR_BLUE, COLOR_BLUE, COLOR_WHITE, DRESS_HILLS_2, CREST_SLASH, 6.0),
                BuildClub(w, n, "Eintracht Braunschweig", COLOR_YELLOW, COLOR_BLUE, COLOR_BLUE, COLOR_RED, DRESS_SLASH, CREST_SHIELD_SLASH, 5.9),
                BuildClub(w, n, "SV Wehen", COLOR_RED, COLOR_BLACK, COLOR_GRAY, COLOR_SKY_BLUE, DRESS_BIG_STRIPES, CREST_SHIELD, 5.7),
                BuildClub(w, n, "FC Hansa Rostock", COLOR_BLUE, COLOR_WHITE, COLOR_RED, COLOR_WHITE, DRESS_BIG_STRIPES, CREST_CROSS , 5.6),
                BuildClub(w, n, "VFL Osnabrück", COLOR_PURPLE, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_SLEEVE, CREST_FANCY_SLASH_2, 5.5),

                BuildClub(w, n, "SSV Ulm", COLOR_WHITE, COLOR_BLACK, COLOR_YELLOW, COLOR_BLACK, DRESS_SIMPLE, CREST_SLASH, 5.3),
                BuildClub(w, n, "Preußen Münster", COLOR_BLACK, COLOR_GREEN, COLOR_RED, COLOR_WHITE, DRESS_STRIPES_2, CREST_SHIELD, 5.1),
                BuildClub(w, n, "SSV Jahn Regensburg", COLOR_RED, COLOR_WHITE, COLOR_BLACK, COLOR_RED, DRESS_SIMPLE_2, CREST_FANCY_SLASH, 5.4),
                BuildClub(w, n, "Dynamo Dresden", COLOR_YELLOW, COLOR_BLACK, COLOR_FIRE_BRICK, COLOR_WHITE, DRESS_SLEEVE, CREST_FANCY_CROSS, 5.5),
                BuildClub(w, n, "1. FC Saarbrücken", COLOR_BLUE, COLOR_BLACK, COLOR_YELLOW, COLOR_BLACK, DRESS_STRIPES, CREST_CARO, 5.2),
                BuildClub(w, n, "Erzgebirge Aue", COLOR_PURPLE, COLOR_WHITE, COLOR_WHITE, COLOR_PURPLE, DRESS_SIMPLE_2, CREST_FANCY_SLASH_2, 5.1),
                BuildClub(w, n, "SV Sandhausen", COLOR_WHITE, COLOR_BLACK, COLOR_BLACK, COLOR_WHITE, DRESS_SIMPLE, CREST_SLASH, 5.1),
                BuildClub(w, n, "TSV 1860 München", COLOR_STEEL_BLUE, COLOR_WHITE, COLOR_BLACK, COLOR_YELLOW, DRESS_SIMPLE, CREST_SHIELD_STRIPE, 5.0),
                BuildClub(w, n, "Rot/Weiß Essen", COLOR_RED, COLOR_WHITE, COLOR_SKY_BLUE, COLOR_WHITE, DRESS_BIG_STRIPES, CREST_CROSS, 5.0),
                BuildClub(w, n, "FC Ingolstadt", COLOR_RED, COLOR_BLACK, COLOR_GRAY, COLOR_BLACK, DRESS_STRIPES, CREST_CARO, 4.9),
                BuildClub(w, n, "Spvgg Unterhaching", COLOR_RED, COLOR_BLUE, COLOR_GRAY, COLOR_BLACK, DRESS_SIMPLE, CREST_FANCY_SLASH , 4.8),
                BuildClub(w, n, "SC Verl", COLOR_WHITE, COLOR_BLACK, COLOR_RED, COLOR_WHITE, DRESS_SHOULDER, CREST_FANCY_SLASH_2, 4.8),


                BuildClub(w, n, "Viktoria Köln", COLOR_WHITE, COLOR_RED, COLOR_BLACK, COLOR_RED, DRESS_SLASH, CREST_SHIELD_SLASH, 4.6),
                BuildClub(w, n, "Arminia Bielefeld", COLOR_BLUE, COLOR_BLACK, COLOR_RED, COLOR_WHITE, DRESS_SLEEVE, CREST_SHIELD, 4.6),
                BuildClub(w, n, "SV Waldhof Mannheim", COLOR_BLACK, COLOR_BLUE, COLOR_YELLOW, COLOR_WHITE, DRESS_STRIPES_2, CREST_SHIELD_SLASH, 4.4),
                BuildClub(w, n, "Hallescher FC", COLOR_RED, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_BIG_STRIPES, CREST_CROSS, 4.3),
                BuildClub(w, n, "MSV Duisburg", COLOR_BLUE, COLOR_WHITE, COLOR_YELLOW, COLOR_RED, DRESS_LINES, CREST_SHIELD_STRIPE, 4.4),
                BuildClub(w, n, "VfB Lübeck", COLOR_GREEN, COLOR_WHITE, COLOR_RED, COLOR_WHITE, DRESS_STRIPES, CREST_SLASH, 4.1),
                BuildClub(w, n, "FC Energie Cottbus", COLOR_RED, COLOR_WHITE, COLOR_BLACK, COLOR_WHITE, DRESS_SLASH_2, CREST_SLASH, 4.0),
                BuildClub(w, n, "SV Meppen", COLOR_BLUE, COLOR_WHITE, COLOR_YELLOW, COLOR_BLACK, DRESS_SIMPLE, CREST_SHIELD_STRIPE, 3.9),
                BuildClub(w, n, "Allemania Aachen", COLOR_YELLOW, COLOR_BLACK, COLOR_RED, COLOR_WHITE, DRESS_STRIPES, CREST_DIAMOND, 3.8),
                BuildClub(w, n, "Stuttgarter Kickers", COLOR_SKY_BLUE, COLOR_WHITE, COLOR_YELLOW, COLOR_WHITE, DRESS_SLEEVE, CREST_SIMPLE, 3.8),
                BuildClub(w, n, "Würzburger Kickers", COLOR_RED, COLOR_WHITE, COLOR_GREEN, COLOR_WHITE, DRESS_HILLS, CREST_DIAMOND_CROSS , 3.7),
                BuildClub(w, n, "Wuppertaler SV", COLOR_RED, COLOR_BLUE, COLOR_BLACK, COLOR_WHITE, DRESS_CUT, CREST_FANCY_SLASH_2, 3.6)

            };
        }

        public static Club BuildClub(World w, Nation n, string name, string color1, string color2, string color3, string color4, string jersey, string crest, double power, bool playerClub = false)
        {
            Club club = playerClub ? new PlayerClub() : new Club();
            club.Name = name;
            club.ClubColors = new ClubColors(color1, color2);
            club.SecondClubColors = new ClubColors(color3, color4);
            club.Crest = crest;
            club.Dress = jersey;
            club.Nation = n;
            club.StartingPowerLevel = power;

            WorldGenerator.GenerateRandomPlayersAndCoachForClub(w, n, power, club, false);

            return club;
        }


    }
}
