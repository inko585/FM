using AE.Graphics.Wpf.ViewModel;
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

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für AEResultView.xaml
    /// </summary>
    public partial class AEResultView : AEWindow
    {
        public AEResultView(string title, List<string> addedItems = null, List<string> changedItems = null, List<string> upToDateItems = null)
        {
            InitializeComponent();

            this.DataContext = new AEResultViewModel(title, addedItems, changedItems, upToDateItems);
        }
    }
}
