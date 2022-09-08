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
    /// Interaktionslogik für AEMultipleInputView.xaml
    /// </summary>
    public partial class AEMultipleInputView : AEWindow
    {
        public Dictionary<string, string> Result;

        public AEMultipleInputView(string title, string inputTitle, params string[] labels)
            : base(true)
        {
            InitializeComponent();
            
            var viewModel = new AEMultipleInputViewModel(title, inputTitle, labels);
            this.DataContext = viewModel;

            viewModel.CloseWindow +=
                delegate
                {
                    this.DialogResult = viewModel.DialogResult;
                    this.Result = viewModel.Result;

                    this.Close();
                };
        }
    }
}