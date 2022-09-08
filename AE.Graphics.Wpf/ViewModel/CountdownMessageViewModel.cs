using System;
using AE.Graphics.Wpf.View;
using System.Windows.Threading;

namespace AE.Graphics.Wpf.ViewModel
{
    public class LDCountdownMessageViewModel : AE.Graphics.Wpf.ViewModel.AEMessageBoxViewModel
    {

        private int RemainingSeconds = 60;
        public LDCountdownMessageViewModel(string textEn, string textDe) : base("", "", MessageBoxIcon.Warning, MessageBoxMode.Ask)
        {
            DispatcherTimer CountdownStep;
            var vestigoLang = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Comsa\LDorado Vestigo", "Language", "en-US");
            if (vestigoLang == "en-US")
            {
                YesButtonText = endEn + " (60)";
                NoButtonText = cancelEn;
                Message = BuildMessage(textEn);
                Title = titleEn;
                buttonText = endEn;
            }
            else
            {
                YesButtonText = endDe + " (60)";
                NoButtonText = cancelDe;
                Message = BuildMessage(textDe);
                Title = titleDe;
                buttonText = endDe;
            }

            CountdownStep = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            CountdownStep.Tick += CountdownStep_Elapsed;
            CountdownStep.Start();
        }

        private void CountdownStep_Elapsed(object sender, EventArgs e)
        {
            if (--RemainingSeconds == 0)
            {
                DialogResult = true;
                OnCloseWindow();
            }
            else
            {
                YesButtonText = buttonText + " (" + RemainingSeconds.ToString() + ")";
            }
        }

        private readonly string buttonText;
        private readonly string cancelEn = "Cancel";
        private readonly string cancelDe = "Abbrechen";
        private readonly string endEn = "End Makro";
        private readonly string endDe = "Beenden";
        public readonly string titleEn = "Makro Idle";
        public readonly string titleDe = "Makro inaktiv";
    }


}
