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
        
        List<DragEventArgs> SeveralEDataList = new List<DragEventArgs>();
        
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PixelEditing.CreateBitmapIstancesAndSimilar(MainInputImagePath, MainOutputImagepath, OutputImage);

        }

        private void LabelDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                HistoryData.ShowFileData(e, InfoBox, SeveralEDataList);
                MainInputImagePath = HistoryData.ShowFileImage(e, OriginalImage);
            }
        }
        //private void Window_KeyDown(object sender, KeyEventArgs e) { }
        private void LabelDrop_PreviewDrop(object sender, DragEventArgs e) { }

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
