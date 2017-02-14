using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FM.Common
{
    public static class RichInt
    {

        public static IEnumerable<T> Times<T>(this int times, Func<int, T> function)
        {
            for (int i = 0; i < times; i++)
            {
                yield return function(i);
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
    public class Util 
    {

        public static Random rnd = new Random();
        public static double GetGaussianRandom(double mean, double stdDev)
        {

            double u1 = rnd.NextDouble();
            double u2 = rnd.NextDouble();
            double rndStdNorm = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * rndStdNorm;
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
