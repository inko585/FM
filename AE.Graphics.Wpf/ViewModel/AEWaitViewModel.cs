using AE.Graphics.Wpf.Basis;
using System.Threading;

namespace AE.Graphics.Wpf.ViewModel
{
    public class AEWaitViewModel : BaseViewModel
    {
        // ---------- private fields ----------
        private string message;
        private double waitSeconds;
        private double progressValue;

        // ---------- public properties ----------
        public string Message
        {
            get { return this.message; }
            set
            {
                if (this.message == value)
                    return;

                this.message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }
        public double WaitSeconds
        {
            get { return this.waitSeconds; }
            set
            {
                if (this.waitSeconds == value)
                    return;

                this.waitSeconds = value;
                NotifyPropertyChanged(nameof(WaitSeconds));
            }
        }
        public double ProgressValue
        {
            get { return this.progressValue; }
            set
            {
                if (this.progressValue == value)
                    return;

                this.progressValue = value;
                NotifyPropertyChanged(nameof(ProgressValue));
            }
        }

        // ---------- public constructor ----------
        public AEWaitViewModel(string message, int waitSeconds)
        {
            this.Message = message;
            this.waitSeconds = waitSeconds;

            ThreadStart waitThreadStarter = new ThreadStart(() =>
            {
                this.Wait();
            });

            waitThreadStarter += () =>
            {
                this.OnCloseRequest();
            };

            var waitThread = new Thread(waitThreadStarter);
            waitThread.Start();
        }

        private void Wait()
        {
            for (int i = 0; i < waitSeconds; i++)
            {
                Thread.Sleep(1000);
                this.ProgressValue += 1;
            }
        }
    }
}
