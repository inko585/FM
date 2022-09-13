using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace FM.Models
{
    public static class PixelArt
    {
        private static Random rnd = new Random();
        private static ColorConverter converter = new ColorConverter();

        private static List<Tuple<Color, Color>> ClubColors = new List<Tuple<Color, Color>>()
        {
            new Tuple<Color, Color>(Color.Crimson, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Blue, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Black, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Brown, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Salmon, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Green, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Gold, Color.Black),
            new Tuple<Color, Color>(Color.Gold, Color.Blue),
            new Tuple<Color, Color>(Color.Green, Color.Black),
            new Tuple<Color, Color>(Color.Purple, Color.FloralWhite),
            new Tuple<Color, Color>(Color.Crimson, Color.Blue),
            new Tuple<Color, Color>(Color.Green, Color.Crimson),
            new Tuple<Color, Color>(Color.Crimson, Color.Blue)
        };

        private static List<Color> SkinColors = new List<Color>()
        {
            Color.AntiqueWhite,
            Color.Bisque,
            Color.PeachPuff,
            Color.PapayaWhip,
            Color.DarkSalmon,
            Color.MistyRose,
            Color.BurlyWood,
            Color.Tan,
            Color.Chocolate,
            Color.SaddleBrown,
            Color.Peru,
            Color.Sienna
        };

        private static List<Color> HairColors = new List<Color>()
        {
            Color.Black,
            Color.DimGray,
            Color.Gold,
            Color.Goldenrod,
            Color.DarkRed,
            Color.Sienna,
            Color.Peru,
            Color.PaleGoldenrod,
            Color.Orange
        };

        private static List<Color> EyeColors = new List<Color>()
        {
            Color.LimeGreen,
            Color.DeepSkyBlue,
            Color.CadetBlue,
            Color.SaddleBrown,
            Color.PaleTurquoise,
            Color.YellowGreen,
            Color.DarkOliveGreen
        };

        public static BitmapImage GetRandomProfilePic()
        {
            var skinColor = GetRandomSkinColor();
            var hairColor = GetRandomHairColor();
            var eyeColor = GetRandomEyeColor();

            return CreatePlayer(skinColor, hairColor, eyeColor);
        }

        public static BitmapImage GetRandomTricotPic()
        {
            var clubColors = GetClubColor();
            var clubColor1 = clubColors.Item1;
            var clubColor2 = clubColors.Item2;

            return CreateTricot(clubColor1, clubColor2);
        }

        public static BitmapImage GetRandomCrest()
        {
            var clubColors = GetClubColor();
            var clubColor1 = clubColors.Item1;
            var clubColor2 = clubColors.Item2;

            return CreateCrest(clubColor1, clubColor2);
        }

        private static BitmapImage CreatePlayer(Color skinColor, Color hairColor, Color eyeColor)
        {
            
            var allHeads = Directory.GetFiles("../../Images/Heads/", "*.png");
            var headPath = allHeads[rnd.Next(allHeads.Length)];
            var head = new Bitmap(headPath);

            var allEyes = Directory.GetFiles("../../Images/Eyes/", "*.png");
            var eyePath = allEyes[rnd.Next(allEyes.Length)];
            var eye = new Bitmap(eyePath);

            var allMouths = Directory.GetFiles("../../Images/Mouths/", "*.png");
            var mouthPath = allMouths[rnd.Next(allMouths.Length)];
            var mouth = new Bitmap(mouthPath);

            head = RecolorBitmap(head, Color.Red, skinColor);
            head = RecolorBitmap(head, Color.Yellow, hairColor);

            eye = RecolorBitmap(eye, Color.Red, eyeColor);
            eye = RecolorBitmap(eye, Color.Yellow, hairColor);

            mouth = RecolorBitmap(mouth, Color.Yellow, hairColor);

            var player = CombineBitmaps(head, eye);

            player = CombineBitmaps(player, mouth);

            return BitmapToImageSource(DoubleSize(MakeIntransparent(player)));
        }

        private static BitmapImage CreateTricot(Color darkColor, Color lightColor)
        {
            var allTricots = Directory.GetFiles("../../Images/Tricots/", "*.png");
            var tricotPath = allTricots[rnd.Next(allTricots.Length)];
            var tricot = new Bitmap(tricotPath);


            tricot = RecolorBitmap(tricot, Color.Red, darkColor);
            tricot = RecolorBitmap(tricot, Color.Yellow, lightColor);

            return BitmapToImageSource(DoubleSize(MakeIntransparent(tricot)));
        }

        private static BitmapImage CreateCrest(Color darkColor, Color lightColor)
        {
            var allCrests = Directory.GetFiles("../../Images/Crests/", "*.png");
            var crestPath = allCrests[rnd.Next(allCrests.Length)];

            var crest = new Bitmap(crestPath);


            crest = RecolorBitmap(crest, Color.Red, darkColor);
            crest = RecolorBitmap(crest, Color.Yellow, lightColor);

            return BitmapToImageSource(DoubleSize(MakeIntransparent(crest)));
        }

        private static Bitmap RecolorBitmap(Bitmap bitmap, Color oldColor, Color newColor)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (CompareColor(bitmap.GetPixel(x, y), oldColor))
                    {
                        bitmap.SetPixel(x, y, newColor);
                    }
                }
            }

            return bitmap;
        }

        private static bool CompareColor(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B && c1.A == c2.A;
        }

        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        private static Bitmap CombineBitmaps(Bitmap under, Bitmap over)
        {
            for (int x = 0; x < under.Width; x++)
            {
                for (int y = 0; y < under.Height; y++)
                {
                    var overColor = over.GetPixel(x, y);
                    if (!IsTransparent(overColor))
                    {
                        under.SetPixel(x, y, overColor);
                    }
                }
            }

            return under;
        }

        private static Bitmap DoubleSize(Bitmap bitmap)
        {
            var newBitmap = new Bitmap(100, 100);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);

                    newBitmap.SetPixel(x * 2, y * 2, pixel);
                    newBitmap.SetPixel(x * 2 + 1, y * 2, pixel);
                    newBitmap.SetPixel(x * 2, y * 2 + 1, pixel);
                    newBitmap.SetPixel(x * 2 + 1, y * 2 + 1, pixel);
                }
            }

            return newBitmap;
        }

        private static Bitmap MakeIntransparent(Bitmap bitmap)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (IsTransparent(color))
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }
            }

            return bitmap;
        }
        private static bool IsTransparent(Color c)
        {
            return c.A == 0;
        }

        private static Tuple<Color, Color> GetClubColor()
        {
            return ClubColors[rnd.Next(0, ClubColors.Count)];
        }

        private static Color GetRandomSkinColor()
        {
            return (Color)converter.ConvertFromString("PeachPuff");

            //return SkinColors[rnd.Next(0, SkinColors.Count)];
        }

        private static Color GetRandomHairColor()
        {
            return HairColors[rnd.Next(0, HairColors.Count)];
        }

        private static Color GetRandomEyeColor()
        {
            return EyeColors[rnd.Next(0, EyeColors.Count)];
        }
    }
}
