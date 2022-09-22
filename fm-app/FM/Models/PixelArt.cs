using FM.Models.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace FM.Models
{
    public static class PixelArt
    {
        public static BitmapImage GetCrestImage(ClubColors cc, string template)
        {
            return BitmapToImageSource(DoubleSize(MakeIntransparent(GetCrest(cc, template))));
        }
        public static Bitmap GetCrest(ClubColors cc, string template)
        {
            var crest = new Bitmap(GetBitmapFromText(BitmapType.Crests, template));

            crest = RecolorBitmap(crest, Color.Red, cc.MainColor);
            crest = RecolorBitmap(crest, Color.Yellow, cc.SecondColor);

            return crest;
        }

        public static BitmapImage GetDressImage(ClubColors cc, string template)
        {
            return BitmapToImageSource(DoubleSize(MakeIntransparent(GetDress(cc, template))));
        }

        public static Bitmap GetDress(ClubColors cc, string template)
        {
            var dress = new Bitmap(GetBitmapFromText(BitmapType.Dresses, template));

            dress = RecolorBitmap(dress, Color.Red, cc.MainColor);
            dress = RecolorBitmap(dress, Color.Yellow, cc.SecondColor);

            return dress;
        }

        public static BitmapImage GetFaceImage(Face f)
        {
            return BitmapToImageSource(DoubleSize(MakeIntransparent(GetFace(f))));
        }

        public static Bitmap GetFace(Face f)
        {
            var head = new Bitmap(GetBitmapFromText(BitmapType.Heads, f.Head));
            var hair = new Bitmap(GetBitmapFromText(BitmapType.Heads, f.Head));

            var mouth = new Bitmap(GetBitmapFromText(BitmapType.Mouths, f.Mouth));
            
            var eye = new Bitmap(GetBitmapFromText(BitmapType.Eyes, f.Eye));

            head = RecolorBitmap(head, Color.Red, f.SkinColor);
            head = RecolorBitmap(head, Color.Yellow, f.HairColor);
            mouth = RecolorBitmap(mouth, Color.Yellow, f.HairColor);
            
            eye = RecolorBitmap(eye, Color.Yellow, f.HairColor);
            eye = RecolorBitmap(eye, Color.Red, f.EyeColor);

            var face = CombineBitmaps(head, mouth);
            face = CombineBitmaps(face, eye);

            return face;
        }

        public static BitmapImage GetPlayerImage(Face face, ClubColors cc, string dress)
        {
            return BitmapToImageSource(DoubleSize(MakeIntransparent(GetPlayer(face, cc, dress))));
        }

        public static Bitmap GetPlayer(Face f, ClubColors cc, string d)
        {
            var face = GetFace(f);
            var dress = GetDress(cc, d);

            var cutDress = CutBitmap(dress, 50, 11);

            var profile = CombineBitmaps(face, cutDress, 0, 39);

            return profile;
        }

        public static BitmapImage GetProfileImage(Face face, ClubColors cc, string dress, int capacity)
        {
            return BitmapToImageSource(DoubleSize(DoubleSize(MakeIntransparent(GetProfile(face, cc, dress, capacity)))));
        }

        public static Bitmap GetProfile(Face f, ClubColors cc, string d, int capacity)
        {
            var player = GetPlayer(f, cc, d);

            var stadium = GetStadium(capacity);

            player = CombineBitmaps(stadium, player);

            return player;
        }

        public static Bitmap GetStadium(int capacity)
        {
            if(capacity >= 12000)
            {
                return GetBitmapFromText(BitmapType.Stadium, "Big");
            }
            else if(capacity >= 4500)
            {
                return GetBitmapFromText(BitmapType.Stadium, "Medium");
            }
            else
            {
                return GetBitmapFromText(BitmapType.Stadium, "Small");
            }
        }

        private static Bitmap GetBitmapFromText(BitmapType type, string text)
        {
            var path = "../../Images/" + type.ToString() + "/" + text + ".png";

            if(File.Exists(path))
            {
                return new Bitmap(path);
            }
            else
            {
                return new Bitmap("../../Images/FileNotFound.png");
            }
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
        private static Bitmap CombineBitmaps(Bitmap under, Bitmap over, int xOffset = 0, int yOffset = 0)
        {
            for (int x = 0; x < over.Width; x++)
            {
                for (int y = 0; y < over.Height; y++)
                {
                    var overColor = over.GetPixel(x, y);
                    if (!IsTransparent(overColor))
                    {
                        under.SetPixel(x + xOffset, y + yOffset, overColor);
                    }
                }
            }

            return under;
        }

        private static Bitmap CutBitmap(Bitmap oldBitmap, int xSize, int ySize)
        {
            var newBitmap = new Bitmap(xSize, ySize);

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    var pixel = oldBitmap.GetPixel(x, y);
                    newBitmap.SetPixel(x, y, pixel);
                }
            }

            return newBitmap;
        }

        private static Bitmap DoubleSize(Bitmap bitmap)
        {
            var newBitmap = new Bitmap(bitmap.Width*2, bitmap.Height*2);

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
       

        private enum BitmapType
        {
            Crests,Eyes,Heads,Mouths,Dresses, Stadium
        }
    }
}
