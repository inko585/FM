using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AE.Graphics.Wpf.FontAwesome
{
    public class IconTextBlock : TextBlock
    {
        public static readonly DependencyProperty IconPackageTypeProperty = DependencyProperty.Register("IconPackageType",
            typeof(FontAwesomeIconPackageType), typeof(IconTextBlock), new PropertyMetadata(FontAwesomeIconPackageType.SOLID, IconPackageTypePropertyChanged));

        public FontAwesomeIconPackageType IconPackageType
        {
            get
            {
                return (FontAwesomeIconPackageType)GetValue(IconPackageTypeProperty);
            }
            set
            {
                SetValue(IconPackageTypeProperty, value);
                var pack = "pack://application:,,,/";
                FontFamily = new FontFamily(new Uri(pack), FontAwesomeUtils.GetFontFamilyForPackageType(IconPackageType));
            }
        }

        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.Register("IconType",
            typeof(FontAwesomeIconType), typeof(IconTextBlock), new PropertyMetadata(FontAwesomeIconType.REGULAR_SMILE, IconTypePropertyChanged));

        public FontAwesomeIconType IconType
        {
            get
            {
                return (FontAwesomeIconType)GetValue(IconTypeProperty);
            }
            set
            {
                SetValue(IconTypeProperty, value);
                Text = FontAwesomeUtils.GetUnicodeByIconType(IconType);
            }
        }

        public IconTextBlock()
        {
            var pack = "pack://application:,,,/";
            FontFamily = new FontFamily(new Uri(pack), FontAwesomeUtils.GetFontFamilyForPackageType(IconPackageType));
            Text = FontAwesomeUtils.GetUnicodeByIconType(IconType);
        }

        private static void IconPackageTypePropertyChanged(DependencyObject bindable, DependencyPropertyChangedEventArgs e)
        {
            IconTextBlock iconTextBlock = (IconTextBlock)bindable;
            iconTextBlock.IconPackageTypePropertyChanged((FontAwesomeIconPackageType)e.NewValue);
        }

        private void IconPackageTypePropertyChanged(FontAwesomeIconPackageType fontAwesomeIconPackageType)
        {
            IconPackageType = fontAwesomeIconPackageType;
        }

        private static void IconTypePropertyChanged(DependencyObject bindable, DependencyPropertyChangedEventArgs e)
        {
            IconTextBlock iconTextBlock = (IconTextBlock)bindable;
            iconTextBlock.IconTypePropertyChanged((FontAwesomeIconType)e.NewValue);
        }

        private void IconTypePropertyChanged(FontAwesomeIconType fontAwesomeIconType)
        {
            IconType = fontAwesomeIconType;
        }
    }
}
