using DocumentFormat.OpenXml.InkML;
using System.Windows;

namespace AE.Graphics.Wpf.ViewModel
{
    public abstract class InfoViewModel
    {

        public abstract string SoftwareName { get; }
        public abstract string Author { get; }

        public virtual string LogoKey { get { return AEWindow.ApplicationIcon; } }

        public abstract string Version { get; }

        public Canvas Canvas
        {
            get
            {
                var x = Application.Current.Resources[LogoKey];
                return x as Canvas;
            }
        }
    }

    public class UserwareInfoViewModel : InfoViewModel
    {
        public override string SoftwareName { get { return "ae UserManager"; } }

        public override string Author { get { return "Jens Marshall"; } }

        public override string LogoKey { get { return "aeUM_logo"; } }

        public override string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
    }
}
