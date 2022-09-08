using AE.Graphics.Wpf;
using AE.Graphics.Wpf.ViewModel;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für InfoView.xaml
    /// </summary>
    public partial class AEInfoView : AE.Graphics.Wpf.AEWindow, IDisposable
    {

        public AEInfoView(InfoViewModel viewModel)
        {

            InitializeComponent();
            DataContext = viewModel;

            if (viewModel.LogoKey != "")
            {
                this.Resources.Source = new Uri("/AE.Graphics.Wpf;component/Styles/AEStyles.xaml", UriKind.RelativeOrAbsolute);
                Logo.Content = Resources[ApplicationIcon];
                //Logo.Content = FindResource(viewModel.LogoKey);
            }
        }

        // ---------- IDisposable ----------
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual new void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
