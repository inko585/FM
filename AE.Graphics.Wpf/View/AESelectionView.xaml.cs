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
    /// Interaktionslogik für AESelectionView.xaml
    /// </summary>
    public partial class AESelectionView : AEWindow
    {
        private static AESelectionView current;
        public static AESelectionView Current
        {
            get { return current; }
            set { current = value; }
        }

        private AESelectionView()
            : base(true)
        {
            InitializeComponent();
        }

        public static T GetUserItemSelection<T>(string title, string selectionTitle, Dictionary<string, T> items)
        {
            T selectedItem = default(T);

            if(current == null)
            { current = new AESelectionView(); }

            AESelectionViewModel<T> viewModel = new AESelectionViewModel<T>(title, selectionTitle, items);
            current.DataContext = viewModel;
            viewModel.CloseWindow += 
                delegate (object sender, EventArgs args)
                {
                    selectedItem = viewModel.SelectedItem;
                    current.Close();
                };

            current.Owner = Application.Current.MainWindow;
            current.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            current.ShowDialog();

            return selectedItem;
        }
    }
}
