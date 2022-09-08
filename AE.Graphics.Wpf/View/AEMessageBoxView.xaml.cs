using System;
using System.Runtime.InteropServices;
using AE.Graphics.Wpf.ViewModel;
using Microsoft.Win32.SafeHandles;
using System.Windows;
using System.Linq;

namespace AE.Graphics.Wpf.View
{
    public enum MessageBoxIcon
    {
        Default,
        Question,
        Warning,
        Error,
        Information
    }

    /// <summary>
    /// Interaktionslogik für AEMessageBoxView.xaml
    /// </summary>
    public partial class AEMessageBoxView : AEWindow, IDisposable
    {
        public AEMessageBoxViewModel ViewModel { get; set; }

        public AEMessageBoxView()
        {
            InitializeComponent();
        }

        public AEMessageBoxView(bool closeButtonHidden)
            : base(closeButtonHidden)
        {
            InitializeComponent();
        }
        
        public AEMessageBoxView(string message, string title, MessageBoxIcon icon, MessageBoxMode mode, string yesButtonText = "Yes", string noButtonText = "No", bool hideCloseButton = false, int timeOutLength = -1, Action onTimeOut = null)
            : this(hideCloseButton)
        {
            InitializeComponent();
            var viewModel = new AEMessageBoxViewModel(message, title, icon, mode, yesButtonText, noButtonText, timeOutLength, onTimeOut);
            AssignViewModel(viewModel);
        }

        public void AssignViewModel(AEMessageBoxViewModel viewModel)
        {
            viewModel.CloseWindow
                += delegate (object sender, EventArgs args)
                {
                    try
                    {
                        if (DialogResult == null)
                        {
                            if (viewModel.DialogResult)
                            {
                                DialogResult = true;
                            }
                            else
                            {
                                DialogResult = false;
                            }
                        }
                    }
                    catch { }

                    this.Close();
                };

            this.DataContext = viewModel;
            this.ViewModel = viewModel;
        }

        // ---------- IDisposable Interface ----------
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public  new void Dispose()
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
