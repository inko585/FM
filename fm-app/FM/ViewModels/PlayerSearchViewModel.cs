using AE.Graphics.Wpf.Basis;
using FM.Common.Generic;
using FM.Entities.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FM.ViewModels
{
    public class PlayerSearchViewModel : BaseViewModel
    {
        public ObservableCollection<Player> FilteredPlayers { get; set; } = new ObservableCollection<Player>();

        public ObservableCollection<NationOption> Nations { get; set; }
        public ObservableCollection<PositionOption> Positions { get; set; }
        public ObservableCollection<string> ContractOptions { get; set; } = new ObservableCollection<string>() { "Egal", "Verfügbar", "Vertrag läuft aus", "Ablösefrei" };
        public ObservableCollection<SortingOption> SortingOptions { get; set; } = new ObservableCollection<SortingOption>()
        {
            new SortingOption("Stärke", p => (int)p.SkillMax, true),
            new SortingOption("Alter", p => p.Age, false),
            new SortingOption("Position", p => (int)p.Position, false),
            new SortingOption("Ablöse", p => p.Price, false),
            new SortingOption("Gehalt", p => p.ExpectedMinSalary, false),
        };

        public string SelectedContractOption { get; set; }
        public SortingOption SelectedSortingOption { get; set; }
        public PositionOption SelectedPosition { get; set; }
        public string FilterText { get; set; }
        public NationOption SelectedNation { get; set; }
        public string MaxSalary { get; set; }
        public string MaxPrice { get; set; }
        public string MinSkill { get; set; }
        public string MaxAge { get; set; }
        public string MinSetPlaySkill { get; set; }

        public PlayerSearchViewModel()
        {
            SelectedSortingOption = SortingOptions.First();
            SelectedContractOption = ContractOptions.First();

            Nations = new ObservableCollection<NationOption>();
            Nations.Add(new NationOption(null));
            foreach(var n in Game.Instance.FootballUniverse.World.Nations.OrderBy(n => n.Name))
            {
                Nations.Add(new NationOption(n));
            }
            SelectedNation = Nations.First();

            Positions = new ObservableCollection<PositionOption>();
            Positions.Add(new PositionOption(null));
            foreach(var p in Enum.GetValues(typeof(Position)).Cast<Position>())
            {
                Positions.Add(new PositionOption(p));
            }
            SelectedPosition = Positions.First();


            SearchCommand = new RelayCommand(x =>
            {
                FilteredPlayers.Clear();
                var players = Game.Instance.FootballUniverse.ActivePlayers.ToList();

                if (SelectedContractOption == ContractOptions[1])
                {
                    players = players.Where(p => (p.WillSignContractWithPlayerClub && (p.ContractCurrent == null || p.IsForSale))).ToList();
                }

                if (SelectedContractOption == ContractOptions[2])
                {
                    players = players.Where(p => p.WillSignContractWithPlayerClub && (p.ContractCurrent == null || (p.ContractComing == null && p.ContractCurrent.RunTime == 1))).ToList();
                }

                if (SelectedContractOption == ContractOptions[3])
                {
                    players = players.Where(p => p.WillSignContractWithPlayerClub && p.ContractCurrent == null).ToList();
                }

                if (SelectedPosition.Position != null)
                {
                    players = players.Where(p => p.Position == SelectedPosition.Position).ToList();
                }

                if (!string.IsNullOrEmpty(FilterText))
                {
                    players = players.Where(p => p.FullName.ToLower().Contains(FilterText.ToLower())).ToList();
                }

                if (SelectedNation.Nation != null)
                {
                    players = players.Where(p => p.Nation == SelectedNation.Nation).ToList();
                }

                if(int.TryParse(MaxSalary, out int maxSal))
                {
                    players = players.Where(p => p.ExpectedMinSalary <= maxSal).ToList();
                }

                if (int.TryParse(MaxPrice, out int maxPrice))
                {
                    players = players.Where(p => p.Price <= maxPrice).ToList();
                }

                if (int.TryParse(MinSkill, out int minSKill))
                {
                    players = players.Where(p => p.SkillMax >= minSKill).ToList();
                }

                if (int.TryParse(MaxAge, out int maxAge))
                {
                    players = players.Where(p => p.Age <= maxAge).ToList();
                }

                if (int.TryParse(MinSetPlaySkill, out int minSetPlaySKill))
                {
                    players = players.Where(p => p.SetPlaySkill >= minSetPlaySKill).ToList();
                }

                if (SelectedSortingOption.Descending)
                {
                    players = players.OrderByDescending(SelectedSortingOption.Sorting).ToList();
                } else
                {
                    players = players.OrderBy(SelectedSortingOption.Sorting).ToList();
                }

                players = players.Take(100).ToList();

                foreach(var p in players)
                {
                    FilteredPlayers.Add(p);
                }

            });
        }

        public RelayCommand SearchCommand { get; set; } 

    }


    public class SortingOption
    {
        public SortingOption(string description, Func<Player, int> sorting, bool descending)
        {
            Description = description;
            Sorting = sorting;
            Descending = descending;
        }

        public string Description { get; set; }
        public Func<Player, int> Sorting { get; set; }
        public bool Descending { get; set; }

    }

    public class NationOption
    {
        public NationOption(Nation nation)
        {
            Nation = nation;
        }

        public string Name => Nation?.Name ?? "Egal";
        public Nation Nation { get; set; }
    }

    public class PositionOption
    {
        private List<string> PositionStrings = new List<string>() { "TW", "VER", "MIT", "ST" };
        public PositionOption(Position? position)
        {
            Position = position;
        }

        public string Name => Position != null ? PositionStrings[(int)Position] : "Egal";
        public Position? Position { get; set; }
            
    }
}
