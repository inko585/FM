using AE.Graphics.Wpf.Basis;
using FM.Common;
using FM.Common.Generic;
using FM.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FM.ViewModels
{
    public class FoundingViewModel : BaseViewModel
    {
        public Club FoundingClub { get; set; }

        public AssociationLook AssociationLook { get; set; }
        public FoundingViewModel(IEnumerable<Association> associations, AssociationLook al)
        {
            SelectedDressId = 0;
            SelectedCrestId = 0;

            AssociationLook = al;
            FoundingClub = new Club();
            ColorOptions = new ObservableCollection<ColorOption>(al.ColorPairs.Select(cp => cp.Text).Concat(al.ColorPairs.Select(cp => cp.Text2)).Distinct().Select(t => new ColorOption(t)));


            AssociationOptions = new ObservableCollection<Association>(associations);

            FoundingClub.Name = "Mein Verein";
            FoundingClub.ClubColors = new Common.Pixels.ClubColors();
            FoundingClub.SecondClubColors = new Common.Pixels.ClubColors();

            SelectedDepth = 1;
            SelectedAssociation = AssociationOptions.First();

            SelectedHomeColor1 = ColorOptions.First();
            SelectedHomeColor2 = ColorOptions.ElementAt(1);

            SelectedAwayColor1 = ColorOptions.ElementAt(2);
            SelectedAwayColor2 = ColorOptions.ElementAt(1);
        }

        private int selectedDepth;
        public int SelectedDepth
        {
            get => selectedDepth;
            set
            {
                selectedDepth = value;
                NotifyPropertyChanged(nameof(SelectedDepth));
            }
        }
        public ObservableCollection<int> DepthOptions
        {
            get
            {
                var ret = new ObservableCollection<int>();
                SelectedAssociation.Depth.Times(i => ret.Add(i + 1));
                return ret;
            }
        }

        private Association selectedAssociation;
        public Association SelectedAssociation
        {
            get => selectedAssociation;
            set
            {
                selectedAssociation = value;
                SelectedDepth = Math.Min(SelectedDepth, value.Depth);
                NotifyPropertyChanged(nameof(DepthOptions));
            }
        }

        private int SelectedCrestId;
        private int SelectedDressId;

        public ObservableCollection<Association> AssociationOptions { get; set; }
        public ObservableCollection<ColorOption> ColorOptions { get; }

        public ObservableCollection<ImageOption> CrestOptions
        {
            get
            {
                var ret = new ObservableCollection<ImageOption>(AssociationLook.Crests.Select(c => new ImageOption(c.Text, FM.Common.Pixels.PixelArt.GetCrestImage(FoundingClub.ClubColors, c.Text), "crest")));
                ret.ElementAt(SelectedCrestId).IsSelected = true;
                foreach (var io in ret)
                {
                    io.SelectionChanged += (s, e) =>
                    {
                        SelectedCrestId = ret.IndexOf((ImageOption)s);
                    };
                }
                return ret;
            }
        }

        public ObservableCollection<ImageOption> DressOptions
        {
            get
            {
                var ret = new ObservableCollection<ImageOption>(AssociationLook.Dresses.Select(d => new ImageOption(d.Text, FM.Common.Pixels.PixelArt.GetDressImage(FoundingClub.ClubColors, d.Text), "dress")));
                ret.ElementAt(SelectedDressId).IsSelected = true;
                foreach (var io in ret)
                {
                    io.SelectionChanged += (s, e) =>
                    {
                        SelectedDressId = ret.IndexOf((ImageOption)s);
                    };
                }
                return ret;
            }
        }

        private ColorOption home1;
        public ColorOption SelectedHomeColor1
        {
            get => home1;
            set
            {
                home1 = value;
                FoundingClub.ClubColors.MainColor = value.Color;
                FoundingClub.ClubColors.MainColorString = value.ColorName;
                NotifyPropertyChanged(nameof(CrestOptions));
                NotifyPropertyChanged(nameof(DressOptions));

            }
        }
        private ColorOption home2;
        public ColorOption SelectedHomeColor2
        {
            get => home2;
            set
            {
                home2 = value;
                FoundingClub.ClubColors.SecondColor = value.Color;
                FoundingClub.ClubColors.SecondColorString = value.ColorName;
                NotifyPropertyChanged(nameof(CrestOptions));
                NotifyPropertyChanged(nameof(DressOptions));

            }
        }

        private ColorOption away1;
        public ColorOption SelectedAwayColor1
        {
            get => away1;
            set
            {
                away1 = value;
                FoundingClub.SecondClubColors.MainColorString = value.ColorName;
                FoundingClub.SecondClubColors.MainColor = value.Color;
            }
        }

        private ColorOption away2;
        public ColorOption SelectedAwayColor2
        {
            get => away2;
            set
            {
                away2 = value;
                FoundingClub.SecondClubColors.SecondColorString = value.ColorName;
                FoundingClub.SecondClubColors.SecondColor = value.Color;

            }
        }

    }


    public class ImageOption : BaseViewModel
    {
        private bool _isSelected;

        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        public string Group { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged(nameof(IsSelected));
                if (_isSelected)
                {
                    OnSelectionChanged();
                }
            }
        }

        public ImageOption(string name, BitmapImage image, string group)
        {
            Name = name;
            Image = image;
            Group = group;
        }

        public event EventHandler SelectionChanged;
        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

    }

    public class ColorOption
    {

        public ColorOption(string name)
        {
            ColorName = name;
            Color = Generator.WorldGenerator.GetColorFromText(name);
            Brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B));
        }
        public string ColorName { get; set; }
        public System.Drawing.Color Color { get; set; }
        public SolidColorBrush Brush { get; set; }

    }




}

