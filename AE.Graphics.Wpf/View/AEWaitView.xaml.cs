using AE.Graphics.Wpf.ViewModel;
using System;
using System.Threading;
using System.Windows;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaction logic for AEWaitView.xaml
    /// </summary>
    public partial class AEWaitView : AEWindow
    {
        public AEWaitView(string title, string message, int waitSeconds)
            : base(true)
        {
            InitializeComponent();

            this.Title = title;

            var viewModel = new AEWaitViewModel(message, waitSeconds);
            viewModel.CloseRequest += delegate
            {
                ExecuteInDispatcher(() =>
                {
                    this.DialogResult = true;
                    this.Close();
                });
            };

            this.DataContext = viewModel;
        }

        private void ExecuteInDispatcher(Action action)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA || !Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
