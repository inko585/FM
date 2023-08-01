using FM.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace FM.Common.Pixels
{
    public class Face
    {
        public System.Drawing.Color SkinColor { get; set; }
        public System.Drawing.Color HairColor { get; set; }
        public System.Drawing.Color EyeColor { get; set; }
        public string Head { get; set; }
        public string Mouth { get; set; }
        public string Eye { get; set; }
    }


    public class ClubColors
    {
        public System.Drawing.Color MainColor { get; set; }
        public System.Drawing.Color SecondColor { get; set; }

        public System.Drawing.Color TextColor
        {
            get
            {
                if (MainColor == Color.White)
                {
                    return SecondColor;
                }

                return MainColor;
            }
        }

        public string MainColorString { get; set; }
        public string SecondColorString { get; set; }
    }

    public static class PixelArt
    {
        public static BitmapImage GetCrestImage(ClubColors cc, string template)
        {
            return Util.BitmapToImageSource(DoubleSize(MakeIntransparent(GetCrest(cc, template))).Bitmap);
        }

        public static BitmapImage GetFlagImage(string nation)
        {
            return Util.BitmapToImageSource(MakeIntransparent(new LockBitmap(Util.GetBitmapFromText(BitmapType.Flags, nation))).Bitmap);
        }

        public static BitmapImage GetSponsorImage(string sponsor)
        {
            return Util.BitmapToImageSource(Util.GetBitmapFromText(BitmapType.Sponsors, sponsor));
        }

        public static LockBitmap BorderAroundRect(LockBitmap bitmap)
        {
            bitmap.LockBits();

            var x_1 = 0;
            var x_2 = 0;
            var y_1 = 0;
            var y_2 = 0;

            for (int x = 0; x < bitmap.Width; x++)
            {
                if (!IsTransparent(bitmap.GetPixel(x, (int)bitmap.Height/2)))
                {
                    x_1 = Math.Max(0, x - 1);
                    break;
                }
            }

            for (int x = bitmap.Width-1; x > 0; x--)
            {
                if (!IsTransparent(bitmap.GetPixel(x, (int)bitmap.Height / 2)))
                {
                    x_2 = Math.Min(bitmap.Width-1, x + 1);
                    break;
                }
            }

            for (int y = 0; y < bitmap.Height; y++)
            {
                if (!IsTransparent(bitmap.GetPixel((int)bitmap.Width / 2, y)))
                {
                    y_1 = Math.Max(0, y - 1);
                    break;
                }
            }

            for (int y = bitmap.Height-1; y > 0; y--)
            {
                if (!IsTransparent(bitmap.GetPixel((int)bitmap.Width / 2,y)))
                {
                    y_2 = Math.Min(bitmap.Height-1, y + 1);
                    break;
                }
            }

            for (int x = x_1; x <= x_2; x++)
            {
                bitmap.SetPixel(x, y_1, Color.Black);
                bitmap.SetPixel(x, y_2, Color.Black);
            }
            for (int y = y_1; y < y_2; y++)
            {
                bitmap.SetPixel(x_1, y, Color.Black);
                bitmap.SetPixel(x_2, y, Color.Black);
            }

            bitmap.UnlockBits();
            return bitmap;
        }

        public static LockBitmap GetCrest(ClubColors cc, string template)
        {
            var crest = new LockBitmap(Util.GetBitmapFromText(BitmapType.Crests, template));

            crest = RecolorBitmap(crest, Color.Red, cc.MainColor);
            crest = RecolorBitmap(crest, Color.Yellow, cc.SecondColor);

            return crest;
        }

        public static BitmapImage GetDressImage(ClubColors cc, string template)
        {
            return Util.BitmapToImageSource(DoubleSize(MakeIntransparent(GetDress(cc, template))).Bitmap);
        }

        public static LockBitmap GetDress(ClubColors cc, string template)
        {
            var dress = new LockBitmap(Util.GetBitmapFromText(BitmapType.Dresses, template));

            dress = RecolorBitmap(dress, Color.Red, cc.MainColor);
            dress = RecolorBitmap(dress, Color.Yellow, cc.SecondColor);

            return dress;
        }

        public static BitmapImage GetFaceImage(Face f)
        {
            return Util.BitmapToImageSource(DoubleSize(MakeIntransparent(GetFace(f))).Bitmap);
        }

        private static Dictionary<Tuple<BitmapType, string>, Color[,]> PixelMapCache = new Dictionary<Tuple<BitmapType, string>, Color[,]>();

        private static Color[,] GetPixelMap(BitmapType bmt, string pattern)
        {
            var key = Tuple.Create(bmt, pattern);
            if (!PixelMapCache.TryGetValue(key, out Color[,] map))
            {
                var bitmap = new Bitmap(Util.GetBitmapFromText(bmt, pattern));
                map = new Color[bitmap.Height, bitmap.Width];
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        map[y, x] = bitmap.GetPixel(x, y);
                    }
                }
            }
            return map;
        }

        private static Color[,] RecolorPixelMap(Color[,] map, Color? firstColor, Color? secondColor)
        {
            var mapNew = new Color[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (firstColor.HasValue && map[y, x] == Color.Red)
                    {
                        mapNew[y, x] = firstColor.Value;
                    }
                    else if (secondColor.HasValue && map[y, x] == Color.Yellow)
                    {
                        mapNew[y, x] = secondColor.Value;
                    }
                }
            }
            return mapNew;
        }

        private static Color[,] CombinePixelMaps(Color[,] over, Color[,] under, int xOffset = 0, int yOffset = 0)
        {
            var mapNew = new Color[under.GetLength(0), under.GetLength(1)];
            under.CopyTo(mapNew, 0);

            for (int y = 0; y < over.GetLength(0); y++)
            {
                for (int x = 0; x < over.GetLength(1); x++)
                {
                    var overColor = over[y, x];
                    if (!IsTransparent(overColor))
                    {
                        mapNew[y + yOffset, x + xOffset] = overColor;
                    }
                }
            }

            return mapNew;
        }

        private static Color[,] CutPixelmap(Color[,] oldMap, int xSize, int ySize)
        {
            var newMap = new Color[ySize, xSize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    newMap[y, x] = oldMap[y, x];
                }
            }

            return newMap;
        }

        private static Color[,] DoubleSize(Color[,] map)
        {
            var newMap = new Color[map.GetLength(0) * 2, map.GetLength(1) * 2];

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    var pixel = map[y, x];
                    newMap[y * 2, x * 2] = pixel;
                    newMap[y * 2 + 1, x * 2] = pixel;
                    newMap[y * 2, x * 2 + 1] = pixel;
                    newMap[y * 2 + 1, x * 2 + 1] = pixel;
                }
            }

            return newMap;
        }

        private static Color[,] MakeIntransparent(Color[,] map)
        {
            var newMap = new Color[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    var color = map[y, x];
                    if (IsTransparent(color))
                    {
                        newMap[y, x] = Color.White;
                        //mapNew[y, x] = Color.White);
                    }
                    else
                    {
                        newMap[y, x] = color;
                    }
                }
            }


            return newMap;
        }

        public static LockBitmap GetFace(Face f)
        {
            var head = new LockBitmap(Util.GetBitmapFromText(BitmapType.Heads, f.Head));
            var hair = new LockBitmap(Util.GetBitmapFromText(BitmapType.Heads, f.Head));

            var mouth = new LockBitmap(Util.GetBitmapFromText(BitmapType.Mouths, f.Mouth));

            var eye = new LockBitmap(Util.GetBitmapFromText(BitmapType.Eyes, f.Eye));

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
            return Util.BitmapToImageSource(DoubleSize(MakeIntransparent(GetPlayer(face, cc, dress))).Bitmap);
        }

        public static LockBitmap GetPlayer(Face f, ClubColors cc, string d)
        {
            var face = GetFace(f);
            var dress = GetDress(cc, d);

            var cutDress = CutBitmap(dress, 50, 11);

            var profile = CombineBitmaps(face, cutDress, 0, 39);

            return profile;
        }

        public static BitmapImage GetProfileImage(Face face, ClubColors cc, string dress, int capacity)
        {
            return Util.BitmapToImageSource(DoubleSize(DoubleSize(MakeIntransparent(GetProfile(face, cc, dress, capacity)))).Bitmap);
        }

        public static LockBitmap GetProfile(Face f, ClubColors cc, string d, int capacity)
        {
            var player = GetPlayer(f, cc, d);

            var stadium = GetStadium(capacity);

            player = CombineBitmaps(stadium, player);

            return player;
        }

        public static LockBitmap GetStadium(int capacity)
        {
            if (capacity >= 15000)
            {
                return new LockBitmap(Util.GetBitmapFromText(BitmapType.Stadium, "Big"));
            }
            else if (capacity >= 10000)
            {
                return new LockBitmap(Util.GetBitmapFromText(BitmapType.Stadium, "Medium"));
            }
            else
            {
                return new LockBitmap(Util.GetBitmapFromText(BitmapType.Stadium, "Small"));
            }
        }



        private static LockBitmap RecolorBitmap(LockBitmap bitmap, Color oldColor, Color newColor)
        {
            bitmap.LockBits();
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
            bitmap.UnlockBits();
            return bitmap;
        }

        private static bool CompareColor(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B && c1.A == c2.A;
        }


        private static LockBitmap CombineBitmaps(LockBitmap under, LockBitmap over, int xOffset = 0, int yOffset = 0)
        {
            under.LockBits();
            over.LockBits();
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
            under.UnlockBits();
            over.UnlockBits();

            return under;
        }

        private static LockBitmap CutBitmap(LockBitmap oldBitmap, int xSize, int ySize)
        {
            var newBitmap = new LockBitmap(new Bitmap(xSize, ySize));

            newBitmap.LockBits();
            oldBitmap.LockBits();

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    var pixel = oldBitmap.GetPixel(x, y);
                    newBitmap.SetPixel(x, y, pixel);
                }
            }

            newBitmap.UnlockBits();
            oldBitmap.UnlockBits();

            return newBitmap;
        }

        private static LockBitmap DoubleSize(LockBitmap bitmap)
        {
            var newBitmap = new LockBitmap(new Bitmap(bitmap.Width * 2, bitmap.Height * 2));

            newBitmap.LockBits();
            bitmap.LockBits();

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
            newBitmap.UnlockBits();
            bitmap.UnlockBits();
            return newBitmap;
        }

        private static LockBitmap MakeIntransparent(LockBitmap bitmap)
        {
            bitmap.LockBits();
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
            bitmap.UnlockBits();
            return bitmap;
        }
        private static bool IsTransparent(Color c)
        {
            return c.A == 0;
        }



    }
}
