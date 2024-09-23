using AE.Graphics.Wpf.Basis;
using FM.Common;
using FM.Common.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FM.ViewModels
{
    public class InfrastructureViewModel : BaseViewModel
    {
        private static InfrastructureViewModel instance;
        public static InfrastructureViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfrastructureViewModel();
                }

                return instance;
            }
        }


        private InfrastructureViewModel()
        {
           
        }
        public Club PlayerClub => Game.Instance.PlayerClub;

        public void Update()
        {
            NotifyPropertyChanged(nameof(PlayerClub));
            NotifyPropertyChanged(nameof(UpgradeStadiumLabel));
            NotifyPropertyChanged(nameof(UpgradeOfficeLabel));
            NotifyPropertyChanged(nameof(UpgradeTrainingGroundsLabel));
            NotifyPropertyChanged(nameof(UpgradeYouthWorkLabel));
            NotifyPropertyChanged(nameof(IsStadiumEnabled));
            NotifyPropertyChanged(nameof(IsOfficeEnabled));
            NotifyPropertyChanged(nameof(IsYouthWorkEnabled));
            NotifyPropertyChanged(nameof(IsTrainingGroundsEnabled));
            MainViewModel.Instance.Update();

        }

        public string UpgradeStadiumLabel => "Upgrade: " + (PlayerClub.ClubAssetLevel[ClubAsset.Stadium] < 5 ? string.Format("{0:#,0}", PlayerClub.AssetCost(ClubAsset.Stadium)) : "-");
        public string UpgradeOfficeLabel => "Upgrade: " + (PlayerClub.ClubAssetLevel[ClubAsset.Office] < 5 ? string.Format("{0:#,0}", PlayerClub.AssetCost(ClubAsset.Office)) : "-");
        public string UpgradeTrainingGroundsLabel => "Upgrade: " + (PlayerClub.ClubAssetLevel[ClubAsset.TrainingGrounds] < 5 ? string.Format("{0:#,0}", PlayerClub.AssetCost(ClubAsset.TrainingGrounds)) : "-");
        public string UpgradeYouthWorkLabel => "Upgrade: " + (PlayerClub.ClubAssetLevel[ClubAsset.YouthWork] < 5 ? string.Format("{0:#,0}", PlayerClub.AssetCost(ClubAsset.YouthWork)) : "-");


        public RelayCommand UpgradeStadium => new RelayCommand((o) => Upgrade(ClubAsset.Stadium));
        public RelayCommand UpgradeYouthWork => new RelayCommand((o) => Upgrade(ClubAsset.YouthWork));
        public RelayCommand UpgradeOffice => new RelayCommand((o) => Upgrade(ClubAsset.Office));
        public RelayCommand UpgradeTrainingGrounds => new RelayCommand((o) => Upgrade(ClubAsset.TrainingGrounds));
        public void Upgrade(ClubAsset clubAsset)
        {
            PlayerClub.Account -= PlayerClub.AssetCost(clubAsset);
            PlayerClub.Savings -= PlayerClub.AssetCost(clubAsset);
            PlayerClub.ClubAssetLevel[clubAsset]++;
            Update();
        }

        public bool IsStadiumEnabled => PlayerClub.Savings > PlayerClub.AssetCost(ClubAsset.Stadium) && PlayerClub.ClubAssetLevel[ClubAsset.Stadium] < 5;
        public bool IsOfficeEnabled => PlayerClub.Savings > PlayerClub.AssetCost(ClubAsset.Office) && PlayerClub.ClubAssetLevel[ClubAsset.Office] < 5;
        public bool IsYouthWorkEnabled => PlayerClub.Savings > PlayerClub.AssetCost(ClubAsset.YouthWork) && PlayerClub.ClubAssetLevel[ClubAsset.YouthWork] < 5;
        public bool IsTrainingGroundsEnabled => PlayerClub.Savings > PlayerClub.AssetCost(ClubAsset.TrainingGrounds) && PlayerClub.ClubAssetLevel[ClubAsset.TrainingGrounds] < 5;
    }
}
