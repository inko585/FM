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
    /// Interaktionslogik für AEMultiSelectionView.xaml
    /// </summary>
    public partial class AEMultiSelectionView : AEWindow
    {
        private static AEMultiSelectionView current;
        public static AEMultiSelectionView Current
        {
            get { return current; }
            set { current = value; }
        }

        private AEMultiSelectionView()
            : base(true)
        {
            InitializeComponent();
        }

        public static List<T> GetUserItemMultiSelection<T>(string title, string selectionTitle, string selectAllText,Dictionary<string, T> items)
        {
            if(current == null)
            { current = new AEMultiSelectionView(); }


            AEMultiSelectionViewModel<T> viewModel = new AEMultiSelectionViewModel<T>(title, selectionTitle, selectAllText, items);
            current.DataContext = viewModel;

            current.ShowDialog();

            return viewModel.SelectedItems;
        }

        private void ItemsControl_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
