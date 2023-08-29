using AE.Graphics.Wpf;
using FM.Common.Generic;
using FM.Entities.Base;
using FM.ViewModels;
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

namespace FM.Views
{
    /// <summary>
    /// Interaction logic for FoundingWindow.xaml
    /// </summary>
    public partial class FoundingWindow : AEWindow
    {
        public FoundingWindow(World w)
        {
            DataContext = new FoundingViewModel(w.Associations, w.AssociationLooks.First());
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
           
            var vm = DataContext as FoundingViewModel;
            vm.FoundingClub.Crest = vm.CrestOptions.Single(c => c.IsSelected).Name;
            vm.FoundingClub.Dress = vm.DressOptions.Single(d => d.IsSelected).Name;
            Result = Tuple.Create(vm.FoundingClub, vm.SelectedAssociation, vm.SelectedDepth);
        }

        public Tuple<Club, Association, int> Result { get; private set; }
    }
}
