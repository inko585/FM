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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AE.Graphics.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für AELinkView.xaml
    /// </summary>
    public partial class AELinkView : AEWindow, IDisposable
    {
        private AELinkViewModel viewModel;

        public AELinkView(string title, string message, string linkText, string link, LinkType linkType)
        {
            InitializeComponent();

            this.viewModel = new AELinkViewModel(title, message, linkText, link, linkType);
            this.DataContext = this.viewModel;

            this.viewModel.CloseWindow +=
                delegate
                {
                    this.Close();
                };
        }

        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.viewModel.OpenLinkExecute(null);
        }

        // ---------- IDisposable Interface ----------
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual new void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }
}
