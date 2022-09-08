using FM.Models.Generic;
using FM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class ClubViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public ClubViewModel(Club c)
        {
            Club = c;
        }
        public Club Club { get; set; }
        public Player SelectedPlayer
        {
            set
            {
                if (value != null)
                {
                    var pw = new PlayerWindow(value);
                    pw.ShowDialog();
                }
            }
            get
            {
                return null;
            }
        }
    }
}
