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
    public partial class AE2FileSelectionView : AEWindow
    {
        public AE2FileSelectionViewModel ViewModel { get; set; }

        public AE2FileSelectionView(string title, string selectionTitle, string firstFileNameLabel, string secondFileNameLabel, string filter, string reloadCacheId = null, string reloadCacheToolTipp = "")
        {
            InitializeComponent();

            ViewModel = new AE2FileSelectionViewModel(title, selectionTitle, firstFileNameLabel, secondFileNameLabel, filter, reloadCacheId, reloadCacheToolTipp);
            this.DataContext = ViewModel;

            ViewModel.CloseWindow +=
                delegate
                {
                    this.Close();
                };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void TextBoxA_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.FileEnterA(files.First());
            }
           
        }

        private void TextBoxB_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.FileEnterB(files.First());
            }

        }

        private void TextBox_PreviewOver (object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
