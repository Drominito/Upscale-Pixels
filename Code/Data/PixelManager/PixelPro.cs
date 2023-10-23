using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Upscale_Pixels.Code.Data.PixelManager
{
    public static class PixelPro
    {

        public static void CreateBitmapIstancesAndSimilar(string MainInputImagePath, string MainOutputImagePath, System.Windows.Controls.Image OutputImage)
        {
            // Wenn der Pfad kein Wert hab, oder die länge kürzer oder gleich 1 ist.
            if (MainInputImagePath == null || MainInputImagePath.Length <= 1)
            {
                string Text = "Error 404\n\n" +
                              "Option 1.\n" +
                              "You File isnt Valuable or an propable Image!" +
                              "\n\nOption 2.\n" +
                              "Your Image is Empty!\nPut Your Image in to the Middle on the Left side!";

                MessageBox.Show(Text);
            }
            else
            {
                int Width = 10, Height = Width;

                Bitmap FileBitmap = new Bitmap(MainInputImagePath); //FileBitmap ist die Eingabe-datei

                Bitmap BitMap = new Bitmap(Width, Height);  // Mit dem Bitmap wird ge&be - arbeitet


                // Calculating && etc.
                Color[,] PixeXYInformations = PixelEditing.CopyPixelInformations(Width, Height, FileBitmap); PixelEditing.StrechPixelArrayToCustomResolution(PixeXYInformations, Width, Height, 10);


                PixelEditing.AttemptPixelnformations(BitMap, Width, Height, PixeXYInformations);

                MainOutputImagePath = PixelEditing.SaveBitmap(BitMap);

                PixelEditing.UpdateImage(MainInputImagePath, OutputImage);


            }
        }

        public static void StrechPixelArrayToCustomResolution(Color[,] pixeXYInformations, int width, int height, int ScaleFactor)
        {
            int[] InputResolution = GetArraysCoordinateLength(pixeXYInformations);
            int InputXResolution = InputResolution[0];
            int InputYResolution = InputResolution[1];

            int DivideFactorX = width / InputXResolution * ScaleFactor;
            int DivideFactorY = height / InputYResolution * ScaleFactor;

            Color[,] StrechedPixeXYInformations = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    StrechedPixeXYInformations[x, y] = pixeXYInformations[x / DivideFactorX, y / DivideFactorY];
                }
            }

            pixeXYInformations = StrechedPixeXYInformations;

        }

        public static void AttemptPixelnformations(Bitmap BitMap, int Width, int Height, Color[,] PixeXYInformations) //Die wenn man die auflösung ändern will
        {
            int[] OriginalXYCoordinates = GetArraysCoordinateLength(PixeXYInformations);
            int OriginalWidth = OriginalXYCoordinates[0];
            int OriginalHeight = OriginalXYCoordinates[1];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (x < OriginalWidth && y < OriginalHeight)
                    {
                        r = PixeXYInformations[x, y].R;
                        g = PixeXYInformations[x, y].G;
                        b = PixeXYInformations[x, y].B;
                    }



                    //int r = PixeXYInformations[x / OriginalWidth, y / OriginalWidth].R;
                    //int g = PixeXYInformations[x / OriginalWidth, y / OriginalWidth].G;
                    //int b = PixeXYInformations[x / OriginalWidth, y / OriginalWidth].B;


                    BitMap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    //try
                    //{
                    //}
                    //catch
                    //{
                    //    BitMap.SetPixel(x, y, Color.FromArgb(255,255,255));

                    //}
                }
            }
        }

        public static void AttemptPixelnformations(Bitmap BitMap, Bitmap BitmapScaleInformations, Color[,] PixeXYInformations) // Die hier wenn man die Auflösung nicht ändern will
        {
            int Width = BitmapScaleInformations.Width, Height = BitmapScaleInformations.Height;

            int AcceptedStatement = 0;
            int NotAcceptedStatement = 0;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height * 10; y++)
                {
                    int rnd_r = new Random().Next(255);
                    int rnd_scale = new Random().Next(1, 5);


                    int r = PixeXYInformations[x, y].R;
                    int g = PixeXYInformations[x, y].G;
                    int b = PixeXYInformations[x, y].B;



                    //BitMap.SetPixel(x / rnd_scale, y / rnd_scale, Color.FromArgb(r, g, b));
                    BitMap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            Debug.WriteLine($"False-Times : {NotAcceptedStatement}\n" +
                            $"True-Times : {AcceptedStatement}");
        }

        public static int[] GetArraysCoordinateLength(Color[,] Array)
        {
            int[] OutputArray = new int[2];
            int FirstNumberIndex = 0;
            int SecondNumberIndex = 0;
            // Der ist für den ersten Array
            while (true)
            {
                try
                {
                    Array[FirstNumberIndex, 0].ToArgb();
                    FirstNumberIndex++;
                }
                catch
                {
                    break;
                }
            }

            // Der ist für den zweiten Array
            while (true)
            {
                try
                {
                    Array[0, SecondNumberIndex].ToArgb();
                    SecondNumberIndex++;
                }
                catch
                {
                    break;
                }
            }

            OutputArray[0] = FirstNumberIndex;
            OutputArray[1] = SecondNumberIndex;

            return OutputArray;
        }

        public static Color[,] CopyPixelInformations(Bitmap FileBitmap)
        {
            int OriginalWidth = FileBitmap.Width, OriginalHeight = FileBitmap.Height;
            Color[,] PixeXYInformations = new Color[OriginalWidth, OriginalHeight];

            int ScaleFactor = 1;
            for (int x = 0; x < OriginalWidth; x++)
            {
                for (int y = 0; y < OriginalWidth; y++)
                {
                    PixeXYInformations[x * ScaleFactor, y * ScaleFactor] = FileBitmap.GetPixel(x / ScaleFactor, y / ScaleFactor);
                }
            }

            return PixeXYInformations;
        }

        public static Color[,] CopyPixelInformations(int Width, int Height, Bitmap FileBitmap)
        {
            int OriginalWidth = FileBitmap.Width, OriginalHeight = FileBitmap.Height;

            Color[,] PixeXYInformations = new Color[Width, Height];

            int AverageOriginalBitmapPixelSize = (OriginalWidth + OriginalHeight) / 2;
            int AverageWorkBitmapPixelSize = (Width + Height) / 2;
            int ScaleFactor = AverageWorkBitmapPixelSize / AverageOriginalBitmapPixelSize + 1;

            int CenterX = Width / 2;
            int CenterY = Height / 2;
            int Radius = (Width + Height) / 2 / 8;


            double CornerRadiusIndex = 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x <= OriginalWidth && y <= OriginalHeight)
                    {
                        double Distacesquared1 = Math.Sqrt(Math.Pow(x - CenterX, 2) + Math.Pow(y - CenterY, 2));

                        if (x > 0 && y > 0)
                        {
                            if (Distacesquared1 <= 50 && Distacesquared1 >= 0)
                            {
                            }
                            if (Math.Pow(x - CenterX * Math.Sin(x * y * 10) / 1, 2) + 10 < Math.Pow(y - CenterY + Math.Cos(x * y * 10) / 1, 2))
                            {
                                PixeXYInformations[x, y] = FileBitmap.GetPixel(x / ScaleFactor, y / ScaleFactor);
                            }


                        }
                    }
                }
                CornerRadiusIndex += 1 * Math.Pow(1, -10);
                Debug.WriteLine($"CornerRadius : {CornerRadiusIndex}\tRadius - CornerRadiusIndex : {Radius - CornerRadiusIndex}");
            }

            return PixeXYInformations;
        }

        public static void UpdateImage(string MainOutputImagePath, System.Windows.Controls.Image OutputImage)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(MainOutputImagePath));
            OutputImage.Source = bitmapImage;
        }

        public static string SaveBitmap(Bitmap BitMap)
        {
            //var SavePath = $"{AppContext.BaseDirectory}Hallo.png";
            string FileName = "TestBild";
            string ImageFormat = ".png";

            string ImagesDir = $@"{AppContext.BaseDirectory}\Images\";
            Directory.CreateDirectory(ImagesDir);
            var SavePath = $@"{ImagesDir}{FileName}{ImageFormat}";
            BitMap.Save(SavePath);

            return SavePath;

        }
    }
}
