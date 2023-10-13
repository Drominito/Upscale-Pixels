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


using Upscale_Pixels.Code;
using Upscale_Pixels.Code.DataManagment;

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

            HistoryData.CreateImagesHistoryPath();

            ComboBoxItem comboboxitem = new ComboBoxItem();
            //comboboxitem.Content = 
            //ImagesHistory.Items = Com;
            DataContext = new MyViewModel();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PixelEditing.CreateBitmapIstancesAndSimilar(MainInputImagePath, MainOutputImagepath);

            UpdateImage();
        }



        private void UpdateImage()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(MainOutputImagepath));
            OutputImage.Source = bitmapImage;
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


        public void ShowFileImage(DragEventArgs e)
        {
            string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string Path = Files[0];

            BitmapImage bitmapImage = new BitmapImage(new Uri(Path));
            OriginalImage.Source = bitmapImage;

            //GetHistoryFilePath(Path);
            HistoryData.AddInformationToHistoryFile(true, Path);

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
