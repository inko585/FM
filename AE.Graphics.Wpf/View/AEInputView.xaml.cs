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
    /// Interaktionslogik für AEInputView.xaml
    /// </summary>
    public partial class AEInputView : AEWindow
    {
        public AEInputViewModel ViewModel { get; set; }

        public AEInputView(string title, string msg, ViewModel.InputType inputType)
            : base(true)
        {
            InitializeComponent();

            this.ViewModel = new AEInputViewModel(title, msg, inputType);
            this.DataContext = this.ViewModel;

            this.ViewModel.CloseWindow +=
                delegate
                {
                    this.Close();
                };
        }
    }
}