using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AE.Graphics.Wpf.ViewModel;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für FileSelectionView.xaml
    /// </summary>
    public partial class AEFileSelectionView : AEWindow
    {
        public AEFileSelectionViewModel ViewModel { get; set; }

        public AEFileSelectionView(string title, string selectionTitle, string filter, string reloadCacheId = null, string reloadCacheToolTipp = "")
        {
            InitializeComponent();

            ViewModel = new AEFileSelectionViewModel(title, selectionTitle, filter, reloadCacheId, reloadCacheToolTipp);
            this.DataContext = ViewModel;

            ViewModel.CloseWindow +=
                delegate
                {
                    this.Close();
                };
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.FileEnter(files.First());
            }

        }

        private void TextBox_PreviewOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
