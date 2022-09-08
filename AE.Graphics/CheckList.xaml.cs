using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AE.Graphics
{
    /// <summary>
    /// Interaction logic for CheckList.xaml
    /// </summary>
    public partial class CheckList : UserControl
    {


        public CheckList()
        {
            this.RootDecription = "All";
            this.CheckCollection = new List<CheckCollection>();
            InitializeComponent();
        }

        public string RootDecription { get; set; }
        public List<CheckItem> Items
            {
            get {
                return this.CheckCollection.First().Items;
            }
            set {
            }
            }
        public List<CheckCollection> CheckCollection { get; set; }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    public class CheckCollection
    {
        public string RootDescription { get; set; }

        public List<CheckItem> Items { get; set; }

    }

    public class CheckItem
    {

        public CheckItem(Bitmap bitmap, object content)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                Image = bitmapImage;
            }
            
            Content = content;
        }

        public bool Checked { get; set; }
        public ImageSource Image { get; set; }
        public object Content { get; set; }
    }
}
