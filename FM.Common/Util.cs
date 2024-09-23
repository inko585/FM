using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using static System.Resources.ResXFileRef;

namespace FM.Common
{


    public static class RichInt
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static IEnumerable<T> Times<T>(this int times, Func<int, T> function)
        {
            for (int i = 0; i < times; i++)
            {
                yield return function(i);
            }
        }

        public static void Times(this int times, Action<int> action)
        {
            for (int i = 0; i < times; i++)
            {
                action(i);
            }
        }


        public static void Times(this int times, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();
            }
        }

    }

    public static class RichString
    {
        public static string Left(this string val, string delimiter, bool includeDelimiter = true, string defaultValue = null)
        {
            if (!val.Contains(delimiter))
            {
                return defaultValue ?? val;
            }

            var i = val.IndexOf(delimiter);
            i = includeDelimiter ? i + delimiter.Length : i;
            return val.Substring(0, i);
        }

        public static string Right(this string val, string delimiter, bool includeDelimiter = false, string defaultValue = "")
        {
            if (!val.Contains(delimiter))
            {
                return defaultValue ?? val;
            }

            var i = val.LastIndexOf(delimiter);
            i = includeDelimiter ? i : i + delimiter.Length;
            return val.Substring(i);
        }

    }

    public enum BitmapType
    {
        Crests, Eyes, Heads, Mouths, Dresses, Stadium, Flags, Sponsors
    }

    public class Util
    {
        private static long seed = DateTime.Now.Ticks;
        public static double GetGaussianRandom(double mean, double stdDev)
        {

            double u1 = GetRandomDouble();
            double u2 = GetRandomDouble();
            double rndStdNorm = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * rndStdNorm;
        }

        public static int GetRandomInt(int from, int to)
        {
            if (from == to)
            {
                return from;
            }
            if (from > to)
            {
                throw new ArgumentException("The 'min' value must be less than or equal to the 'max' value.");
            }

           
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                
                byte[] seedBytes = BitConverter.GetBytes(seed);
                rng.GetBytes(seedBytes);

                int randomNumber = Math.Abs(BitConverter.ToInt32(seedBytes, 0));
                return (int)((randomNumber / (double)int.MaxValue) * (to - from) + from);
            }
        }

        public static double GetRandomDouble()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] seedBytes = BitConverter.GetBytes(seed);
                rng.GetBytes(seedBytes);

                var longValue = BitConverter.ToInt64(seedBytes, 0);
                return (double)(longValue & long.MaxValue) / long.MaxValue;
            }
        }


        public static int GetMinMargin(int raw)
        {
            if (raw <= 10000)
            {
                return 1000;
            }
            else if (raw <= 100000)
            {
                return 25000;
            }
            else if (raw <= 1000000)
            {
                return 50000;
            }
            else
            {
                return 250000;
            }
        }

        public static int GetNiceValue(int raw)
        {
            if (raw <= 10000)
            {
                return CleanValue(raw, 1000, 1000);
            }
            else if (raw <= 100000)
            {
                return CleanValue(raw, 25000, 10000);
            } else if (raw <= 1000000)
            {
                return CleanValue(raw, 50000, 100000);
            }
            else
            {
                return CleanValue(raw, 250000, 100000);
            }
        }

        public static void RandomizeList<T>(List<T> list)
        {
            Random rand = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        public static Color GetColorFromText(string name)
        {
            try
            {
                var converter = new ColorConverter();
                return (Color)converter.ConvertFromString(name);
            }
            catch (Exception e)
            {
                return Color.Pink;
            }
        }

        public static Bitmap GetBitmapFromText(BitmapType type, string text)
        {
            var path = "../../Images/" + type.ToString() + "/" + text + ".png";

            if (File.Exists(path))
            {
                return new Bitmap(path);
            }
            else
            {
                return new Bitmap("../../Images/FileNotFound.png");
            }
        }



        public static int CleanValue(int raw, int potency, int minValue)
        {
            return Math.Max(minValue, (int)Math.Round(raw / (double)potency) * potency);
        }

        public static Boolean TryGetXMLOpenPath(out string path)
        {
            return TryGetFileOpenPath("XML Dateien| *xml", out path);
        }

        public static Boolean TryGetFileOpenPath(string filter, out string path)
        {
            var openDia = new OpenFileDialog();
            openDia.Filter = filter;
            openDia.RestoreDirectory = true;
            if (openDia.ShowDialog() == DialogResult.OK)
            {
                path = openDia.FileName;
                return true;
            }
            else
            {
                path = null;
                return false;
            }
        }

        public static bool TryGetXMLSavePath(string defaultFileName, out string path)
        {
            var saveDia = new SaveFileDialog();
            saveDia.Filter = "XML Dateien| *xml";
            saveDia.RestoreDirectory = true;
            if (defaultFileName.EndsWith(".xml") || defaultFileName.Equals(""))
            {
                saveDia.FileName = defaultFileName;
            }
            else
            {
                saveDia.FileName = defaultFileName + ".xml";
            }
            if (saveDia.ShowDialog() == DialogResult.OK)
            {

                path = saveDia.FileName;
                return true;
            }
            else
            {
                path = null;
                return false;
            }
        }

    }
}
