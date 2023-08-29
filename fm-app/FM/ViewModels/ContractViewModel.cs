using AE.Graphics.Wpf.Basis;
using FM.Common;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Navigation;

namespace FM.ViewModels
{
    public class ContractViewModel : BaseViewModel
    {

        public Player Player { get; set; }
        public Club Club { get; set; }

        public bool IsTransfer { get; set; }

        public ContractViewModel(Player player, Club club, bool isTransfer)
        {
            Player = player;
            Club = club;
            IsTransfer = isTransfer;
            SelectedRunTimeIndex = 0;
        }

        private int selectedRunTimeIndex;
        public int SelectedRunTimeIndex
        {
            get => selectedRunTimeIndex; set
            {
                selectedRunTimeIndex = value;
                NotifyPropertyChanged(nameof(SalaryString));
                NotifyPropertyChanged(nameof(SumString));
                NotifyPropertyChanged(nameof(RestBudgetString));
                NotifyPropertyChanged(nameof(RestBudgetColor));
            }
        }

        public string Title => "Vertrag mit " + Player.FullName;
        public Brush PriceColor => Price > 0 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Black);
        public Brush RestBudgetColor => RestBudget > 0 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        public bool OkButtonEnabled => RestBudget > 0;
        public bool RunTimeSelectionEnabled => Player.Age < 30;

        public ObservableCollection<string> RunTimeSelection => new ObservableCollection<string> { "2 Jahre", "3 Jahre", "4 Jahre", "5 Jahre" };


        public int Price
        {
            get
            {
                return IsTransfer ? Player.Price : 0;
            }
        }
        public string PriceString
        {
            get
            {
                return Format(Price, true);
            }
        }

        public int Salary
        {
            get
            {
                return Player.GetSalaryExpectationForClub(Club, IsTransfer, SelectedRunTimeIndex + 2);
            }
        }
        public string SalaryString
        {
            get
            {
                return Format(Salary, true);
            }
        }
        public string SumString
        {
            get
            {
                return Format(Salary + Price, true);
            }
        }

        private int RestBudget => (IsTransfer ? Club.BudgetCurrentSeason : Club.BudgetNextSeason) - (Price + Salary);
        

        
        public string RestBudgetString
        {
            get
            {
                return Format(RestBudget, false);
            }
        }

        private string Format(int input, bool isCost)
        {
            return ((isCost && input > 0) ? "-" : "") + string.Format("{0:#,0}", input);
        }


    }
}
