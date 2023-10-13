using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Windows.Media.Effects;
using Color = System.Drawing.Color;
using System.Runtime.InteropServices;
using Image = System.Drawing.Image;
using Bitmap = System.Drawing.Bitmap;
using System.Diagnostics;

namespace Upscale_Pixels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DropBlockText = "You can Drop the File here!";
        private int DataShower = 0;
        List<DragEventArgs> SeveralEDataList = new List<DragEventArgs>();
        public int HistorySteps = 0;
        public string MainInputImagePath = "";
        public string MainOutputImagepath = "";

        public MainWindow()
        {
            InitializeComponent();
            DropTextBlock.Text = DropBlockText;
            DataContext = new MyViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateBitmapIstancesAndSimilar();

            UpdateImage();
        }

        private void UpdateImage()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(MainOutputImagepath));
            OutputImage.Source = bitmapImage;
        }

        private void CreateBitmapIstancesAndSimilar()
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
                int Width = 1000, Height = Width;



                
                Bitmap FileBitmap = new Bitmap(MainInputImagePath); //FileBitmap ist die Eingabe-datei

                Bitmap BitMap = new Bitmap(Width, Height);  // Mit dem Bitmap wird ge&be - arbeitet


                // Calculating && etc.
                Color[,] PixeXYInformations = CopyPixelInformations(FileBitmap);    StrechPixelArrayToCustomResolution(PixeXYInformations, Width, Height);


                AttemptPixelnformations(BitMap, Width, Height, PixeXYInformations);

                SaveBitmap(BitMap);

            }
        }

        private void StrechPixelArrayToCustomResolution(Color[,] pixeXYInformations, int width, int height)
        {
            int[] InputResolution = GetArraysCoordinateLength(pixeXYInformations);
            int InputXResolution = InputResolution[0];
            int InputYResolution = InputResolution[1];

            int DivideFactorX = width / InputXResolution  * 10;
            int DivideFactorY = height / InputYResolution * 10;

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

        private void SaveBitmap(Bitmap BitMap)
        {
            //var SavePath = $"{AppContext.BaseDirectory}Hallo.png";
            var SavePath = $@"C:\Users\Dromi\OneDrive\Desktop\TestUpscale\Hallo.png";
            BitMap.Save(SavePath);
            MainOutputImagepath = SavePath;

        }

        private void AttemptPixelnformations(Bitmap BitMap, int Width, int Height, Color[,] PixeXYInformations) //Die wenn man die auflösung ändern will
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
                    }
                        r = PixeXYInformations[x, y].R;
                        g = PixeXYInformations[x, y].G;
                        b = PixeXYInformations[x, y].B;



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
        private static void AttemptPixelnformations(Bitmap BitMap, Bitmap BitmapScaleInformations, Color[,] PixeXYInformations) // Die hier wenn man die Auflösung nicht ändern will
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
        private int[] GetArraysCoordinateLength(Color[,] Array)
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

        private static Color[,] CopyPixelInformations(Bitmap FileBitmap)
        {
            int OriginalWidth = FileBitmap.Width, OriginalHeight = FileBitmap.Height;
            Color[,] PixeXYInformations = new Color[OriginalWidth, OriginalHeight];

            int ScaleFactor = 5;
            for (int x = 0; x < OriginalWidth; x++)
            {
                for (int y = 0; y < OriginalWidth; y++)
                {
                     PixeXYInformations[x, y] = FileBitmap.GetPixel(x, y);
                }
            }

            return PixeXYInformations;
        }
        private static Color[,] CopyPixelInformations(int Width, int Height, Bitmap FileBitmap)
        {
            int OriginalWidth = FileBitmap.Width, OriginalHeight = FileBitmap.Height;

            Color[,] PixeXYInformations = new Color[Width, Height];

            int AverageOriginalBitmapPixelSize = (OriginalWidth + OriginalHeight) / 2; 
            int AverageWorkBitmapPixelSize = (Width + Height) / 2;
            int ScaleFactor = (AverageWorkBitmapPixelSize / AverageOriginalBitmapPixelSize) + 1;

            int CenterX = Width / 2;
            int CenterY = Height / 2;
            int Radius = ( ((Width + Height) / 2) / 8 );


            double CornerRadiusIndex = 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double Distacesquared1 = Math.Sqrt(Math.Pow(x - CenterX, 2) + Math.Pow(y - CenterY, 2));

                    if (x > 0 && y > 0)
                    {
                        if (Distacesquared1 <= 50 && Distacesquared1 >= 0)
                        {
                        }
                            if (Math.Pow(x - CenterX * Math.Sin(x*y * 10) / 1, 2) + 10 < Math.Pow(y - CenterY + Math.Cos(x*y * 10) / 1, 2))
                            {
                                    PixeXYInformations[x, y] = FileBitmap.GetPixel(x / ScaleFactor, y / (ScaleFactor));
                            }
                            
                        
                    }
                }
                CornerRadiusIndex += 1 * Math.Pow(1, -10);
                Debug.WriteLine($"CornerRadius : {CornerRadiusIndex}\tRadius - CornerRadiusIndex : {Radius - CornerRadiusIndex}");
            }

            return PixeXYInformations;
        }



        private long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length; // Die Dateigröße in Bytes
            }
            catch (Exception ex)
            {
                // Hier können Sie Fehlerbehandlung für den Fall hinzufügen, dass die Datei nicht gefunden wird oder andere Probleme auftreten
                return -1; // Geben Sie -1 zurück, um anzuzeigen, dass ein Fehler aufgetreten ist
            }
        }

        private void WriteDataText(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0]; // Wir nehmen an, dass es sich um eine einzelne Datei handelt
                    long ByteFileSize = GetFileSize(filePath);

                    long MegaByteFileSize = GetFileSize(filePath);
                    decimal MegaByteFileSize_Int = Convert.ToDecimal(MegaByteFileSize);
                    MegaByteFileSize_Int /= 1000000;

                    string InfoData = $"File Size:\n" +
                                      $"{ByteFileSize}Bytes\n" +
                                      $"{MegaByteFileSize_Int}Megabytes";

                    InfoBox.Text = $"{InfoBox.Text}\n{InfoData}";
                }
            }
        }

        private void LabelDrop_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ShowFileData(e);
                ShowFileImage(e);
            }

        }

        private void LabelDrop_PreviewDrop(object sender, DragEventArgs e)
        {

        }
        private void ShowFileImage(DragEventArgs e)
        {
            string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string Path = Files[0];

            BitmapImage bitmapImage = new BitmapImage(new Uri(Path));
            OriginalImage.Source = bitmapImage;

            MainInputImagePath = Path; 
        }

        private void ShowFileData(DragEventArgs e)
        {
            SeveralEDataList.Add(e);
            InfoBox.Text = $"";
            WriteDataText(SeveralEDataList.Last());
        }



    }
    public class MyViewModel : INotifyPropertyChanged
    {
        private string _MyProperty;
        public string MyProperty
        {
            get => _MyProperty;

            set
            {
                if (_MyProperty != value)
                {
                    _MyProperty = value;
                    OnPropertyChanged(nameof(MyProperty));
                }
            }


        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
