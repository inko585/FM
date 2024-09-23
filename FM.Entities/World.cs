using FM.Common;
using FM.Entities.Base;
using FM.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using FM.Common.Pixels;

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
            Associations = new List<Association>();
            AssociationLooks = new List<AssociationLook>();
            PlayerLooks = new List<PlayerLook>();
        }
        public List<Nation> Nations { get; set; }
        public List<Ethnie> Ethnies { get; set; }
        public List<Association> Associations { get; set; }
        public List<AssociationLook> AssociationLooks { get; set; }
        public List<PlayerLook> PlayerLooks { get; set; }

        public Nation GetNationByName(string name)
        {
            return Nations.FirstOrDefault(n => n.Name.Equals(name));
        }


        public Ethnie GetEthnieByName(string name)
        {
            return Ethnies.FirstOrDefault(e => e.Name.Equals(name));
        }

        public PlayerLook GetPlayerLookByName(string name)
        {
            return PlayerLooks.FirstOrDefault(pl => pl.Name.Equals(name));
        }



    }

    public class Association
    {
        public string Name { get; set; }
        public int Depth { get; set; }
        public double Power { get; set; }
        public List<Occurrence> Nations { get; set; }
    }

    public class Nation
    {
        public string Name { get; set; }
        public string Short { get; set; }
        public int LeagueLevel { get; set; }

        private BitmapImage flag;
        public BitmapImage Flag
        {
            get
            {
                if (flag == null)
                {
                    flag = PixelArt.GetFlagImage(Short);
                }

                return flag;
            }
        }

        public bool CombineSuffixAndPrefix { get; set; }

        public List<Occurrence> Cities { get; set; }

        public List<Occurrence> FirstPrefixes { get; set; }

        public List<Occurrence> SecondPrefixes { get; set; }

        public List<Occurrence> Suffixes { get; set; }

        public List<SponsorOccurrence> Sponsors { get; set; }

        public List<Occurrence> SubNations { get; set; }

        public string MainEthnie { get; set; }
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

        public List<Occurrence> Appearences { get; set; }

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

    public class AssociationLook
    {
        public string Name { get; set; }

        public List<ColorPairOccurrence> ColorPairs { get; set; }

        public List<Occurrence> Crests { get; set; }

        public List<Occurrence> Dresses { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AssociationLook))
            {
                return false;
            }
            return Name.Equals((obj as AssociationLook).Name);
        }
    }

    public class PlayerLook
    {
        public string Name { get; set; }

        public List<Occurrence> SkinColors { get; set; }
        public List<Occurrence> HairColors { get; set; }
        public List<Occurrence> EyeColors { get; set; }
        public List<Occurrence> Heads { get; set; }
        public List<Occurrence> Mouths { get; set; }
        public List<Occurrence> Eyes { get; set; }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PlayerLook))
            {
                return false;
            }
            return Name.Equals((obj as PlayerLook).Name);
        }
    }


    public class Occurrence
    {
        public Occurrence()
        {
        }

        public Occurrence(string text, int scaleValue)
        {
            Text = text;
            ScaleValue = scaleValue;
        }

        public string Text { get; set; }

        public int ScaleValue { get; set; }
    }

    public class ColorPairOccurrence : Occurrence
    {
        public ColorPairOccurrence()
        {
        }

        public ColorPairOccurrence(string text, int scaleValue) : base(text, scaleValue)
        {
        }

        public string Text2 { get; set; }
    }

    public class SponsorOccurrence : Occurrence
    {
        public SponsorOccurrence()
        {
        }

        public SponsorOccurrence(string text, int scaleValue) : base(text, scaleValue)
        {
        }

        public int Size { get; set; }
    }

    public class SubEthnieOccurrence : Occurrence
    {
        public SubEthnieOccurrence()
        {
        }

        public SubEthnieOccurrence(string text, int scaleValue) : base(text, scaleValue)
        {
        }

        public int FirstAndLastNamesRate { get; set; }
        public int FirstNameRate { get; set; }
        public int LastNameRate { get; set; }
    }

    public class PlayerLookOccurrence : Occurrence
    {
        public PlayerLook PlayerLook { get; set; }
    }




}





